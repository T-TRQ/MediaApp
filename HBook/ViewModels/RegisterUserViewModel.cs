using HBook.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BusinessLayer;
using HBook.Views;
using System.Windows;

namespace HBook.ViewModels
{
	public class RegisterUserViewModel
	{
		private string _password;
		private string _repeatpassword;
		private string _email;
		private string _name;
		public string Password { private get { return _password; } set { _password = value; } }
		public string RepeatPassword { private get { return _repeatpassword; } set { _repeatpassword = value; } }
		public string Name { private get { return _name; } set { _name = value; } }
		public string Email { private get { return _email; } set { _email = value; } }

		public RegisterUserViewModel()
		{
			RegisterUserCommand = new RelayCommand(CreateUser, null);
		}

		public RelayCommand RegisterUserCommand { get; set; }

		public void CreateUser(object parameter)
		{
			if (Password == RepeatPassword && Password != "")
			{
				UserController uc = new UserController();
				if(uc.CreateUser(Name, Email, Password))
				{
					Console.WriteLine("User Created.");
				}
				else
					Console.WriteLine("User or password already exists!");
					
			}
			else
				Console.WriteLine("Passwords doesn't match!");
		}

		
	}
}
