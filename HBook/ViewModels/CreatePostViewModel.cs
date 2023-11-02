using BusinessLayer;
using Entities;
using HBook.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBook.ViewModels
{
	class CreatePostViewModel : BaseViewModel
	{
        private string _text ="";
        public string Text { private get { return _text; } set { _text = value; OnPropertyChanged();} }
        public string FileName { get; set; }


        public RelayCommand BrowseCommand { get; set; }
        public RelayCommand CreatePostCommand { get; set; }


        public CreatePostViewModel()
        {
            CreatePostCommand = new RelayCommand(CreateNewPost, CanCreatePost);
            BrowseCommand = new RelayCommand(BrowseImage, null);
        }

        private bool CanCreatePost(object parameter)
        {
            if(Text!=null && Text!="")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void CreateNewPost(object parameter)
        {
            UserController uc = new UserController();
            PostController pc = new PostController();
            User user = uc.GetUser(Singleton.GetLoggedIn()._loggedInUser.UserId);
            byte[] image = null;
            if (FileName!=null)
                image = ImageToByteArray();
            uc.CreateNewPost(Text, image, user);
            
        }

        public void BrowseImage(object parameter)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|GIF Files (*.gif)|*.gif";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                FileName = filename;
            }
        }
        private byte[] ImageToByteArray()
        {
            FileStream fs = new FileStream(FileName, FileMode.Open, FileAccess.Read);
            byte[] data = new byte[fs.Length];
            fs.Read(data, 0, Convert.ToInt32(fs.Length));

            fs.Close();
            return data;
        }
    }
}
