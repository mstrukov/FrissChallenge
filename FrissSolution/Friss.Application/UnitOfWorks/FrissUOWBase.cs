using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Friss.Application.UnitOfWorks
{
	public abstract class FrissUOWBase : IDisposable
	{
		/// <summary>
		/// Ensuares all changes are committed at the same time thus reducing possible locks time
		/// </summary>
		public async Task SaveChanges()
		{
			using (TransactionScope tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
			{
				//TODO: Implement a transaction manager for file repository
				//to make sure files are written to a store in a transactional way
				await Commit();

				tran.Complete();
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected abstract Task Commit();

		protected abstract void Dispose(bool disposing);
	}
}
