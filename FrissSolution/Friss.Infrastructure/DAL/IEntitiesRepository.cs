using Friss.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friss.Infrastructure.DAL
{
	public interface IEntitiesRepository<TEntity>
		where TEntity : EntityBase
	{
		void Add(TEntity item);

		TEntity GetById(int id);
	}
}
