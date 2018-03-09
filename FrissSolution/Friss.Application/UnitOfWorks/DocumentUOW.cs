using Friss.Domain.Entities;
using Friss.Infrastructure.DAL;
using Friss.Infrastructure.DAL.Database;
using Friss.Infrastructure.DAL.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friss.Application.UnitOfWorks
{
	public class DocumentUOW : FrissUOWBase
	{
		private bool _disposed = false;
		private IFrissFileSystemContext _frissFileSystemContext;
		private IFrissDbContext _frissDbContext;
		public IFileRepository FileRepository { get; }
		public IEntitiesRepository<Document> DocumentRepository { get; }

		public DocumentUOW(
			IFrissFileSystemContext frissFileSystemContext,
			IFrissDbContext frissDbContext,
			IFileRepository fileRepository,
			IEntitiesRepository<Document> documentRepository)
		{
			_frissFileSystemContext = frissFileSystemContext;
			_frissDbContext = frissDbContext;
			FileRepository = fileRepository;
			DocumentRepository = documentRepository;
		}

		protected override void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				if (disposing)
				{
					_frissDbContext.Dispose();
				}
			}
			this._disposed = true;
		}

		protected override async Task Commit()
		{
			await Task.WhenAll(new List<Task>()
			{
				_frissFileSystemContext.SaveChanges(),
				_frissDbContext.SaveChanges()
			});
		}
	}
}
