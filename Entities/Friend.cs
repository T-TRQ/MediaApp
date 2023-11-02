using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
	public class Friend
	{
		public int id { get; set; }
		public User From { get; set; }
		public User To { get; set; }

		public Friend(User to, User from) 
		{
			To = to;
			From = from;
		}
		public Friend()
		{

		}
	}
}
