using Music3_Core.Entities.BaseEntity;
using Music3_Core.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Music3_Core.Extension.IRepositories
{
    public partial interface IRepositoryEF<T> where T : BaseEntity
    {
        public IUnitOfWork UnitOfWork { get; }

        public IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int records = 0,
            string includeProperties = "");
        public Task<IEnumerable<T>> GetAsync(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int records = 0,
        string includeProperties = "");
        IEnumerable<T> GetList(Func<T, bool> filter = null);
        Task<T> GetFirstAsync(string id);
        Task<T> GetFirstAsyncAsNoTracking(string id);
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> ListAllAsync();
        Task<IEnumerable<T>> ListByListId(IEnumerable<string> ids);
        void Update(T entity);
        void Delete(T entity);
        /*void BulkInsert(IList<T> listEntity);
        Task BulkInsertAsync(IList<T> listEntity);
        void BulkUpdate(IList<T> listEntity);
        Task BulkUpdateAsync(IList<T> listEntity);
        void BulkDelete(IList<T> listEntity);
        Task<int> BulkDeleteEditOnDeleteAsync(IEnumerable<string> listEntity);

        Task BulkInsertOrUpdateAsync(IList<T> listEntity);*/
    }
}
