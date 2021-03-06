﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friss.Domain.Entities
{
	public class Document : EntityBase
	{
		public string OwnerName { get; set; }
		public string FileName { get; set; }
		public long? Size { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime? LastAccessDate { get; set; }
	}
}
