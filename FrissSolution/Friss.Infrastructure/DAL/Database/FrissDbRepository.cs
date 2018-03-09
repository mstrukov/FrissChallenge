using Friss.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friss.Infrastructure.DAL.Database
{
	public abstract class FrissDbRepository<TEntity> : IEntitiesRepository<TEntity>, IDisposable
		where TEntity : EntityBase
	{
		private bool _disposed = false;
		IDbSet<TEntity> _dbSet;
		IFrissDbContext _context;

		public FrissDbRepository(IFrissDbContext context)
		{
			_context = context;
			_dbSet = context.Set<TEntity>();
		}

		public virtual TEntity GetById(int id)
		{
			return _dbSet.Find(id);
		}

		public virtual void Add(TEntity item)
		{
			_dbSet.Add(item);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				if (disposing)
				{
					_context.Dispose();
				}
			}
			this._disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
