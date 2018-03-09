using System;
using System.IO;
using System.Threading.Tasks;

namespace Friss.Infrastructure.DAL.FileSystem
{
	public interface IFrissFileSystemContext
	{
		void AddFile(Guid fileId, string extension, Stream file);
		Task SaveChanges();
	}
}