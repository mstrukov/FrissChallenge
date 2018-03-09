using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friss.Infrastructure.DAL.FileSystem
{
	class AddFileInfo
	{
		public string Extension { get; set; }
		public Stream FileStream { get; set; }
	}
}
