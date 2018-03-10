using Friss.Domain.Entities;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Friss.Application.Services
{
	public interface IDocumentService
	{
		Task<Guid> AddDocument(Stream fileStream, string fileName, long? size);
		Task<Stream> GetDocumentFile(Document document);
	}
}