using Friss.Application.UnitOfWorks;
using Friss.Domain.Entities;
using Friss.Infrastructure.DAL;
using Friss.Infrastructure.DAL.Database;
using Friss.Infrastructure.DAL.FileSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friss.Application.Services
{
	public class DocumentService : IDocumentService
	{
		private IFrissFileSystemContext _frissFileSystemContext;
		private IFrissDbContext _frissDbContext;
		private IFileRepository _fileRepository;
		private IEntitiesRepository<Document> _documentRepository;

		public DocumentService(
			IFrissFileSystemContext frissFileSystemContext,
			IFrissDbContext frissDbContext,
			IFileRepository fileRepository,
			IEntitiesRepository<Document> documentRepository)
		{
			_frissFileSystemContext = frissFileSystemContext;
			_frissDbContext = frissDbContext;
			_fileRepository = fileRepository;
			_documentRepository = documentRepository;
		}

		public async Task<Stream> GetDocumentFile(Document document)
		{
			if (document == null)
			{
				throw new ArgumentNullException(nameof(document));
			}

			using (var unitOfWork = new DocumentUOW(_frissFileSystemContext, _frissDbContext))
			{
				var stream = _fileRepository.ReadFile(document.Id, Path.GetExtension(document.FileName));

				if (stream == null)
				{
					throw new FileNotFoundException($"{document.Id} file is not found");
				}

				document.LastAccessDate = DateTime.UtcNow;
				await unitOfWork.SaveChanges();

				return stream;
			}
		}

		public async Task<Guid> AddDocument(Stream fileStream, string fileName)
		{
			if (fileStream == null)
			{
				throw new ArgumentNullException(nameof(fileStream));
			}

			var extension = Path.GetExtension(fileName);

			if (string.IsNullOrEmpty(fileName))
			{
				throw new ArgumentException($"{fileName} cannot be empty");
			}

			if (string.IsNullOrEmpty(extension))
			{
				throw new ArgumentException($"{extension} cannot be empty");
			}

			using (var unitOfWork = new DocumentUOW(_frissFileSystemContext, _frissDbContext))
			{
				var documentId = Guid.NewGuid();

				_documentRepository.Add(new Document()
				{
					Id = documentId,
					CreatedDate = DateTime.UtcNow,
					LastAccessDate = null,
					FileName = fileName,
					OwnerName = "TODO",
					Size = fileStream.Length
				});

				_fileRepository.AddFile(documentId, extension, fileStream);

				await unitOfWork.SaveChanges();

				return documentId;
			}
		}
	}
}
