using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friss.Infrastructure.DAL.FileSystem
{
	public class FrissFileRepository : IFileRepository
	{
		private IFrissFileSystemContext _frissFileSystemContext;

		public FrissFileRepository(IFrissFileSystemContext frissFileSystemContext)
		{
			_frissFileSystemContext = frissFileSystemContext;
		}

		public void AddFile(Guid fileId, string extension, Stream file)
		{
			_frissFileSystemContext.AddFile(fileId, extension, file);
		}

		public Stream ReadFile(Guid fileId, string extension)
		{
			return _frissFileSystemContext.ReadFile(fileId, extension);
		}
	}
}
