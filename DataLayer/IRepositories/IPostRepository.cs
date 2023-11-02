using Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.IRepositories
{
	public interface IPostRepository : IRepository<Post>
	{
		ObservableCollection<Post> GetWholePostsByUser(int id);

		ObservableCollection<Post> GetWholePostsBySearchTerm(int id, string searchTerm);  
	}
}
