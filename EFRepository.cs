using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Ventas
{
    public class EFRepository : iRepository
    {
        DbContext dbContext;

        public EFRepository(DbContext context)
        {
            this.dbContext = context;
        }
        public TEntity Create<TEntity>(TEntity toCreate) where TEntity : class
        {
            TEntity Result = default(TEntity);
            try
            {
                dbContext.Set<TEntity>().Add(toCreate);
                dbContext.SaveChanges();
                Result = toCreate;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el objeto", ex);
            }
            return Result;
        }

        public bool Delete<TEntity>(TEntity toDelete) where TEntity : class
        {
            bool Result = false;
            try
            {
                dbContext.Entry<TEntity>(toDelete).State = System.Data.Entity.EntityState.Deleted;
                Result = dbContext.SaveChanges() > 0;

            }
            catch (Exception)
            {
                throw new Exception("Error al eliminar el objeto");
            }
            return Result;
        }

        public void Dispose()
        {
            if (dbContext != null)
            {
                dbContext.Dispose();
            }
        }

        public List<TEntity> Filter<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
        {
            List<TEntity> Result = null;
            try
            {
                Result = dbContext.Set<TEntity>().Where(criteria).ToList();
            }
            catch (Exception)
            {
                throw new Exception("Error al filtrar los objetos");
            }
            return Result;
        }

        public TEntity Retrieve<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
        {
            TEntity Result = null;
            try
            {
                Result = dbContext.Set<TEntity>().FirstOrDefault(criteria);
            }
            catch (Exception)
            {
                throw new Exception("Error al obtener el objeto");
            }
            return Result;
        }

        public bool Update<TEntity>(TEntity toUpdate) where TEntity : class
        {
            bool Result = false;
            try
            {
                dbContext.Entry<TEntity>(toUpdate).State = System.Data.Entity.EntityState.Modified;
                Result = dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw new Exception("Error al actualizar el objeto");
            }
            return Result;
        }
    }
}
