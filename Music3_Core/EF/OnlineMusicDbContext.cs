using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music3_Core.EF
{
    public class OnlineMusicDbContext : DbContext
    {
        private readonly IMediator _mediator;
        private IDbContextTransaction _currentTransaction;

        public OnlineMusicDbContext(DbContextOptions<OnlineMusicDbContext> options)
            : base(options)
        {

        }

        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

        public bool HasActiveTransaction => _currentTransaction != null;

        public OnlineMusicDbContext(DbContextOptions<OnlineMusicDbContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            System.Diagnostics.Debug.WriteLine("WarehouseManagementContext::ctor ->" + this.GetHashCode());
        }
    }
}
