using Friss.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friss.Infrastructure.DAL.Database
{
	public class FrissDbContext : DbContext, IFrissDbContext
	{
		public DbSet<Document> Documents { get; set; }

		public new IDbSet<T> Set<T>() where T : class
		{
			return base.Set<T>();
		}

		public new async Task SaveChanges()
		{
			await base.SaveChangesAsync();
		}
	}
}
