using Friss.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friss.Infrastructure.DAL.Database
{
	public class DocumentRepository: FrissDbRepository<Document>
	{
		IEntitiesRepository<Document> _entitiesRepository = null;

		public DocumentRepository(IFrissDbContext context)
			: base(context)
		{
		}
	}
}
