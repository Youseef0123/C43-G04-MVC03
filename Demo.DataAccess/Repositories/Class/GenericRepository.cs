using Demo.DataAccess.Data.Context;
using Demo.DataAccess.model.shared;
using Demo.DataAccess.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Demo.DataAccess.Repositories.Class
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ApplicationDbContext _dbContext;

        // Constructor
        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<TEntity> GetAll(bool withTracking = false)
        {
            var query = _dbContext.Set<TEntity>().Where(e => e.IsDeleted != true);
            return withTracking ? query.ToList() : query.AsNoTracking().ToList();
        }

        public TEntity? GetById(int id) => _dbContext.Set<TEntity>().Find(id);

        public void Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            // SaveChanges is handled by UnitOfWork
        }

        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            // SaveChanges is handled by UnitOfWork
        }

        public void Remove(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            // SaveChanges is handled by UnitOfWork
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Where(predicate).ToList();
        }
    }
}
