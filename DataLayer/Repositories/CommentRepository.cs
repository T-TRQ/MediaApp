using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using DataLayer.IRepositories;

namespace DataLayer.Repositories
{
   public class CommentRepository : Repository<Comment>, ICommentRepository
    {
		public CommentRepository(AppDbContext context) : base(context)
		{

		}
		public AppDbContext AppDbContext
		{
			get { return Context as AppDbContext; }
		}
	} 
}
