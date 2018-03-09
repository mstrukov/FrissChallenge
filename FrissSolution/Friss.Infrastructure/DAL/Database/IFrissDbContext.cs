using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friss.Infrastructure.DAL.Database
{
	public interface IFrissDbContext
	{
		IDbSet<T> Set<T>() where T : class;
		Task SaveChanges();
		void Dispose();
	}
}
