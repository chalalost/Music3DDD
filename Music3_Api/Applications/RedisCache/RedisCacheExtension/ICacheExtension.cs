using StackExchange.Redis;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Music3_Api.Applications.RedisCache.RedisCacheExtension
{
    /// <summary>
    /// Extension Cache Redis
    /// </summary>
    public interface ICacheExtension : IDisposable
    {
        /// <summary>
        /// Get All Name Keys
        /// </summary>
        /// <returns>List Name Keys</returns>
        IEnumerable<string> GetAllNameKey();

        /// <summary>
        /// Get All Name Keys By contains
        /// </summary>
        /// <param name="contains">Search by</param>
        /// <returns></returns>
        IEnumerable<string> GetAllNameKeyByContains(string contains);

        /// <summary>
        /// Remove All Keys
        /// </summary>
        /// <returns></returns>
        Task RemoveAllKeys();

        /// <summary>
        /// Remove All Keys By
        /// </summary>
        /// <param name="contains">Remove by</param>
        /// <returns></returns>
        Task RemoveAllKeysBy(string contains);


        Task<RedisResult> RemoveAll();


        public bool IsConnected { get; }
    }
}
