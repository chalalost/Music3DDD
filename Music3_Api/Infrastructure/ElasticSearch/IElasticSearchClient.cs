using Music3_Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Music3_Api.Infrastructure.ElasticSearch
{
    public interface IElasticSearchClient<T> where T : BaseModel
    {
        public Task<bool> InsertOrUpdateAsync(T entity);
        public bool InsertOrUpdate(T entity);
        public Task<bool> InsertOrUpdateManyAsync(IEnumerable<T> lists);
        public Task<bool> DeleteManyAsync(IEnumerable<string> ids);
        public Task<long> CountAllAsync();
    }
}
