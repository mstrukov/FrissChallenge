using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Friss.IntegrationTests.WebAPI
{
	[TestClass]
	public class DocumentControllerTests
	{
		private readonly string _webAPIUrl;

		public DocumentControllerTests()
		{
			_webAPIUrl = ConfigurationManager.AppSettings["WebAPIUrl"];
		}

		[TestMethod]
		public void DocumentController_UploadDocument_ShouldReturnDocumentId()
		{
			string fileContent = "file content";
			string fileName = "testfile.txt";
			var fileContentBytes = Encoding.UTF8.GetBytes(fileContent);

			using (var client = new HttpClient())
			{
				using (var content = new MultipartFormDataContent())
				{
					content.Add(new StreamContent(
						new MemoryStream(fileContentBytes)),
						"testfile",
						fileName);

					string location = null;
					using (var message = client.PostAsync(_webAPIUrl, content).Result)
					{
						Assert.IsTrue(message.IsSuccessStatusCode);
						Assert.AreEqual(message.StatusCode, System.Net.HttpStatusCode.Created);
						Assert.IsNotNull(message.Headers.Location);

						location = message.Headers.Location.AbsoluteUri;
					}

					using (var fileMessage = client.GetAsync(location).Result)
					{
						string fileMessageContent = fileMessage.Content.ReadAsStringAsync().Result;

						Assert.IsTrue(fileMessage.IsSuccessStatusCode);
						Assert.AreEqual(fileMessage.StatusCode, System.Net.HttpStatusCode.OK);
						Assert.AreEqual(fileName, fileMessage.Content.Headers.ContentDisposition.FileName);
						Assert.AreEqual(fileContent, fileMessageContent);
					}
				}
			}
		}
	}
}
