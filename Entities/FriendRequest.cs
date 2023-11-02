using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
	public class FriendRequest
	{
		public int id { get; set; }
		public User FromUser { get; set; }
		public User ToUser { get; set; }
		public bool IsApproved { get; set; } = false;

		public FriendRequest(User fromUser, User toUser)
		{
			FromUser = fromUser;
			ToUser = toUser;
		}
		public FriendRequest()
		{

		}
	}
}
