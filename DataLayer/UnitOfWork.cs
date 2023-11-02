using DataLayer.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly AppDbContext _context;

		public UnitOfWork(AppDbContext context)
		{
			_context = context;
			Users = new Repositories.UserRepository(_context);
			Posts = new Repositories.PostRepository(_context);
			FriendRequests = new Repositories.FriendRequestRepository(_context);
			Friends = new Repositories.FriendRepository(_context);
			Comments = new Repositories.CommentRepository(_context);
		}

		public IUserRepository Users { get; private set; }
		public IPostRepository Posts { get; private set; }
		public IFriendRequestRepository FriendRequests { get; private set; }
		public IFriendRepository Friends { get; private set; }
		public ICommentRepository Comments { get; set; }

		public int complete()
		{
			return _context.SaveChanges(); 
		}

		public void Dispose()
		{
			_context.Dispose();
		}
	}
}
