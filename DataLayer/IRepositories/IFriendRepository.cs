using Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.IRepositories
{
	public interface IFriendRepository : IRepository<Friend>
	{
		ObservableCollection<Friend> GetFriends(User user);
	}
}
