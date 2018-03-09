using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friss.Infrastructure.DAL
{
	public interface IFileRepository
	{
		void AddFile(Guid fileId, string extension, Stream file);

		Stream ReadFile(Guid fileId);
	}
}
