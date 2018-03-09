using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friss.Infrastructure.DAL.FileSystem
{
	public class FrissFileSystemContext : IFrissFileSystemContext
	{
		private string _storagePath = null;
		private ConcurrentDictionary<Guid, AddFileInfo> _addedFiles = new ConcurrentDictionary<Guid, AddFileInfo>();

		public FrissFileSystemContext(string storagePath)
		{
			_storagePath = storagePath;
		}

		public void AddFile(Guid fileId, string extension, Stream file)
		{
			_addedFiles.TryAdd(fileId, new AddFileInfo()
			{
				Extension = extension,
				FileStream = file
			});
		}

		public Stream ReadFile(Guid fileId, string extension)
		{
			return new FileStream(GetFilePath(fileId, extension), FileMode.Open, FileAccess.Read);
		}

		public async Task SaveChanges()
		{
			await Task.WhenAll(_addedFiles.Select(x => WriteFile(x)).ToArray());
		}

		private async Task WriteFile(KeyValuePair<Guid, AddFileInfo> addFileInfo)
		{
			string filePath = GetFilePath(addFileInfo.Key, addFileInfo.Value.Extension);
			string tempFilePath = GetFilePath(addFileInfo.Key, addFileInfo.Value.Extension, "temp_");

			//Looks like there's no real async delete function in .NET out of the box
			File.Delete(tempFilePath);

			using (var fileStream = File.Create(tempFilePath))
			{
				await addFileInfo.Value.FileStream.CopyToAsync(fileStream);
			}

			File.Delete(filePath);
			File.Move(tempFilePath, filePath);
		}

		private string GetFilePath(Guid fileId, string extension, string prefix = null)
		{
			return Path.Combine(_storagePath, $"{prefix ?? ""}{fileId.ToString()}{extension}");
		}
	}
}
