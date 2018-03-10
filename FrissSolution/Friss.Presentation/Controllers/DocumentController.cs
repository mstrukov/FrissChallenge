using Friss.Application.Services;
using Friss.Domain.Entities;
using Friss.Infrastructure.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace Friss.Presentation.Controllers
{
	public class DocumentController : ApiController
	{
		private IDocumentService _documentService;
		private IEntitiesRepository<Document> _documentRepository;

		public DocumentController(IDocumentService documentService, IEntitiesRepository<Document> documentRepository)
		{
			_documentService = documentService;
			_documentRepository = documentRepository;
		}

		// GET api/<controller>
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// GET api/<controller>/5
		public async Task<IHttpActionResult> Get(Guid id)
		{
			var document = _documentRepository.GetById(id);

			//TODO add check for user name
			if (document == null)
			{
				return NotFound();
			}

			var fileStream = await _documentService.GetDocumentFile(document);

			HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
			{
				Content = new StreamContent(fileStream)
			};

			httpResponseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
			{
				FileName = document.FileName,
				Size = document.Size
			};

			httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

			return ResponseMessage(httpResponseMessage);
		}

		// POST api/<controller>
		public async Task<IHttpActionResult> Post()
		{
			//TODO verify for admin role
			if (!Request.Content.IsMimeMultipartContent())
			{
				throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
			}

			var provider = new MultipartMemoryStreamProvider();
			await Request.Content.ReadAsMultipartAsync(provider);

			var file = provider.Contents.FirstOrDefault();

			if (file == null)
			{
				return BadRequest("File is missing");
			}

			var filename = file.Headers.ContentDisposition.FileName.Trim('\"');
			var fileExtension = Path.GetExtension(filename);
			var fileSize = file.Headers.ContentDisposition.Size;

			if (string.IsNullOrEmpty(filename) || string.IsNullOrEmpty(fileExtension))
			{
				return BadRequest("File name is invalid");
			}

			using (var stream = await file.ReadAsStreamAsync())
			{
				var documentId = await _documentService.AddDocument(stream, Path.GetExtension(filename), fileSize);
				return CreatedAtRoute("DefaultApi", new { id = documentId }, new { id = documentId });
			}
		}
	}
}