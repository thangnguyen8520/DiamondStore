﻿using DiamondStoreRepository.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DiamondStoreRepository.Interfaces
{
    public interface IGenericRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");
        Task<TEntity?> GetByIdAsync(object id, string includeProperties = "");
        Task<TEntity?> GetByIdNoIncludeAsync(object id);

        Task AddAsync(TEntity entity);
        Task AddRangeAsync(List<TEntity> entities);
        void Update(TEntity entity);
        void UpdateRange(List<TEntity> entities);
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null);
        Task<Pagination<TEntity>> ToPagination(int pageIndex = 0, int pageSize = 10);
        Task<Pagination<TEntity>> ToPaginationAsync(IQueryable<TEntity> query, int pageIndex = 0, int pageSize = 10);
        Task<int> SaveAsync(TEntity entity);
        void UpdateAsync(TEntity entity);
        Task UpdateTaskAsync(TEntity entity); // Change void to Task
        Task DeleteTaskAsync(object id);       // Change void to Task
        Task DeleteTaskAsync(TEntity entityToDelete); // Change void to Task
    }
}
