using DataLayer.IRepositories;
using Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DataLayer.Repositories
{
	public class PostRepository : Repository<Post>, IPostRepository
	{
		public PostRepository(AppDbContext context) : base(context)
		{

		}
		
		public AppDbContext AppDbContext
		{
			get { return Context as AppDbContext; }
		}

		public ObservableCollection<Post> GetWholePostsBySearchTerm(int id, string searchTerm)
		{
			return new ObservableCollection<Post>(AppDbContext.Post.Include("User").Where(x => x.User.UserId == id && x.Text.Contains(searchTerm)));
		}

		public ObservableCollection<Post> GetWholePostsByUser(int id) 
		{
			return new ObservableCollection<Post>(AppDbContext.Post.Include("User").Where(x=>x.User.UserId==id));
		}

		public override Post Get(int id)
		{
			return AppDbContext.Set<Post>()
				.Include(p => p.User)
				.Include(p => p.Comments)
				.Include(p => p.Comments.Select(c => c.User))
				.Where(p => p.id == id)
				.FirstOrDefault();
		}
	}
}
