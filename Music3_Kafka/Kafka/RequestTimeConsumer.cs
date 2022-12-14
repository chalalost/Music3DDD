using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Music3_Kafka.EventBus.Abstractions;
using Music3_Kafka.EventBus;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Polly.Retry;
using Autofac;
using Polly;
using Microsoft.Extensions.Hosting;

namespace Music3_Kafka.Kafka
{
    /// <summary>
    ///     A simple example demonstrating how to set up a Kafka consumer as an
    ///     IHostedService.
    /// </summary>
    public class RequestTimeConsumer : BackgroundService
    {
        private readonly IKafKaConnection _kafKaConnection;
        private readonly string topic;
        private readonly IConsumer<string, byte[]> kafkaConsumer;
        private string Topic = "OnlineMusic-KafKa";
        private readonly ILogger<EventKafKa> _logger;
        private readonly IEventBusSubscriptionsManager _subsManager;
        private readonly ILifetimeScope _autofac;
        const string BROKER_NAME = "event_bus";
        const string AUTOFAC_SCOPE_NAME = "event_bus";
        public RequestTimeConsumer(IKafKaConnection kafKaConnection, ILogger<EventKafKa> logger,
            ILifetimeScope autofac, IEventBusSubscriptionsManager subsManager)
        {
            this.topic = Topic;
            _kafKaConnection = kafKaConnection;
            this.kafkaConsumer = this._kafKaConnection.ConsumerConfig;
            _autofac = autofac;
            _subsManager = subsManager;
            _logger = logger;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var policy = RetryPolicy.Handle<SocketException>()
                   .Or<KafkaRetriableException>()
                   .WaitAndRetry(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                   {
                       Log.Warning(ex, "Kafka Client could not connect after {TimeOut}s ({ExceptionMessage})", $"{time.TotalSeconds:n1}", ex.Message);
                   }
               );

            policy.Execute(() =>
            {
                new Thread(() => StartConsumerLoop(stoppingToken)).Start();

            });
            return Task.CompletedTask;
        }

        private async Task StartConsumerLoop(CancellationToken cancellationToken)
        {
            Log.Information("Listen to Kafka");
            if (!_kafKaConnection.IsConnectedConsumer)
            {
                Log.Error("Kafka Client is not connected");
                _kafKaConnection.TryConnectConsumer();
            }
            else
            {
                kafkaConsumer.Subscribe(this.topic);
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        var cr = this.kafkaConsumer.Consume(cancellationToken);
                        if (cr.Message != null)
                        {
                            var message = Encoding.UTF8.GetString(cr.Value);
                            await ProcessEvent(cr.Key, message);

                        }
                        kafkaConsumer.StoreOffset(cr);
                    }
                    catch (ConsumeException e)
                    {
                        Log.Error($"Consume error: {e.Error.Reason}");
                        this.kafkaConsumer.Close();
                        if (e.Error.IsFatal)
                        {
                            break;
                        }
                    }
                }
                this.kafkaConsumer.Close();
            }

        }
        private async Task ProcessEvent(string eventName, string message)
        {
            _logger.LogTrace("Processing KafKa event: {EventName}", eventName);

            if (_subsManager.HasSubscriptionsForEvent(eventName))
            {
                using (var scope = _autofac.BeginLifetimeScope(AUTOFAC_SCOPE_NAME))
                {
                    var subscriptions = _subsManager.GetHandlersForEvent(eventName);
                    foreach (var subscription in subscriptions)
                    {
                        if (subscription.IsDynamic)
                        {
                            var handler = scope.ResolveOptional(subscription.HandlerType) as IDynamicIntegrationEventHandler;
                            if (handler == null) continue;
                            using dynamic eventData = JsonDocument.Parse(message);
                            await Task.Yield();
                            await handler.Handle(eventData);
                        }
                        else
                        {
                            var handler = scope.ResolveOptional(subscription.HandlerType);
                            if (handler == null) continue;
                            var eventType = _subsManager.GetEventTypeByName(eventName);
                            var integrationEvent = JsonSerializer.Deserialize(message, eventType, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                            var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);

                            await Task.Yield();
                            await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { integrationEvent });
                        }
                    }
                }
            }
            else
            {
                Log.Warning("No subscription for KafKa event: {EventName}", eventName);
            }
        }

        public override void Dispose()
        {
            this.kafkaConsumer.Close(); // Commit offsets and leave the group cleanly.
            this.kafkaConsumer.Dispose();
            base.Dispose();
        }
    }
}
