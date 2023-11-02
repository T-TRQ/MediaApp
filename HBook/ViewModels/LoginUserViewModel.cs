using HBook.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HBook.Views;
using BusinessLayer;
using System.Windows.Controls;
using System.Windows;

namespace HBook.ViewModels
{
	public class LoginUserViewModel
	{
		private string _email;
		public string Email { get { return _email; } set { _email = value; } }

		public RelayCommand RegisterCommand { get; set; }
		public RelayCommand LoginCommand { get; set; }
		public RelayCommand ResetCommand { get; set; }

		public LoginUserViewModel()
		{
			RegisterCommand = new RelayCommand(OpenRegisterWindow, null);
			LoginCommand = new RelayCommand(OpenMainWindow, null);
			ResetCommand = new RelayCommand(Reset, null);
		}

		private void OpenRegisterWindow(object parameter)
		{
			RegisterUser window = new RegisterUser();
			window.Show();
		}

		private void OpenMainWindow(object parameter)
		{
			string myPassword = ((PasswordBox)parameter).Password;
			UserController uc = new UserController();
			if(uc.Login(Email, myPassword))
			{
				MainWindow window = new MainWindow();
				window.Show();
			}
		}

		private void Reset(object parameter)
		{
			Controller c = new Controller();
			c.Reset();
		}
	}
}
