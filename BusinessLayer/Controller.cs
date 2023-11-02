using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
	public class Controller
	{
		//Främst här för reset som inte passar i de andra controllers.
		private AppDbContext AppDbContext { get; set; }
		private UnitOfWork UnitOfWork { get; set; }

		public Controller()
		{
			AppDbContext = new AppDbContext();
			UnitOfWork = new UnitOfWork(AppDbContext);
		}
		public void Reset()
		{
			AppDbContext.Reset();
		}
	}
}
