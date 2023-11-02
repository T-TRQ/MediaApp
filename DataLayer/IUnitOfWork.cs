using DataLayer.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
	public interface IUnitOfWork : IDisposable
	{
		IUserRepository Users { get; }
		IFriendRequestRepository FriendRequests { get; }
		IFriendRepository Friends { get; }
		IPostRepository Posts { get; }
		ICommentRepository Comments { get; }

		int complete();
	}
}
