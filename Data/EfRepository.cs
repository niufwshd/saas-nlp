using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GovTown.Core;
using GovTown.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebApplication5.Data;

namespace GovTown.Data
{
    /// <summary>
    /// Entity Framework repository
    /// </summary>
    public partial class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly SchoolContext _context;

        

        private DbSet<T> _entities;

        public EfRepository(SchoolContext context)
        {
            this._context = context;
        }

        #region interface members

        public virtual IQueryable<T> Table
        {
            get
            {
				if (_context.ChangeTracker.QueryTrackingBehavior == QueryTrackingBehavior.NoTracking)
				{
					return this.Entities.AsNoTracking();
				}
				return this.Entities;
            }
        }

        public virtual IQueryable<T> TableUntracked
        {
            get
            {
                return this.Entities.AsNoTracking();
            }
        }

		public virtual ICollection<T> Local
		{
			get
			{
				return this.Entities.Local;
			}
		}


        public T GetById(object id)
        {
            return this.Entities.Find(id);
        }

        public void Insert(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            this.Entities.Add(entity);
            _context.SaveChanges();
        }

        public void InsertRange(IEnumerable<T> entities, int batchSize = 100)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");

            Entities.AddRange(entities);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            var avoidingAttachedEntity =  GetById(entity.Id);
            _context.Entry(avoidingAttachedEntity).State = EntityState.Detached;

            var entry = _context.Entry(entity);
            if (entry.State == EntityState.Detached) _context.Attach(entity);

            _context.Entry(entity).State = EntityState.Modified;

            _context.SaveChanges();
        }

		public void UpdateRange(IEnumerable<T> entities)
		{
            if (entities == null)
                throw new ArgumentNullException("entities");

            Entities.UpdateRange(entities);
            _context.SaveChanges();
        }

            public void Delete(T entity)
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

            
            Entities.Remove(entity);
            _context.SaveChanges();
        }

		public void DeleteRange(IEnumerable<T> entities)
		{
			if (entities == null)
				throw new ArgumentNullException("entities");

            Entities.RemoveRange(entities);
            _context.SaveChanges();
        }

		[Obsolete("Use the extension method from 'GovTown.Core, GovTown.Core.Data' instead")]
        public IQueryable<T> Expand(IQueryable<T> query, string path)
        {
            Guard.ArgumentNotNull(query, "query");
            Guard.ArgumentNotEmpty(path, "path");

            return query.Include(path);
        }

		[Obsolete("Use the extension method from 'GovTown.Core, GovTown.Core.Data' instead")]
        public IQueryable<T> Expand<TProperty>(IQueryable<T> query, Expression<Func<T, TProperty>> path)
        {
            Guard.ArgumentNotNull(query, "query");
            Guard.ArgumentNotNull(path, "path");

            return query.Include(path);
        }

		public bool IsModified(T entity)
		{
			Guard.ArgumentNotNull(() => entity);
			var entry = _context.Entry(entity);

			if (entry != null)
			{
				var modified = entry.State == EntityState.Modified;
				return modified;
			}

			return false;
		}


        public DbContext Context
        {
            get { return _context; }
        }


        #endregion

        #region Helpers

        //protected internal ObjectContextBase InternalContext
        //{
        //    get { return _context as ObjectContextBase; }
        //}

        private DbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                {
                    _entities = _context.Set<T>();
                }
                return _entities as DbSet<T>;
            }
        }

        #endregion
    }
}