using HBook.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;

namespace HBook.ViewModels
{
    public class PostViewModel : BaseViewModel
    {
        private string _text;
        private byte[] _data;


        public string Text { private get { return _text; } set { _text = value; } }
        public byte[] Data { private get { return _data; } set { _data = value; } }



        public RelayCommand AddPicture { get; set;  }
        public RelayCommand CreatePost { get; set; }


        public PostViewModel()
        {
            CreatePost = new RelayCommand(CreateNewPost, null);
            AddPicture = new RelayCommand(BrowsePicture, null);
        }

        public void CreateNewPost(object parameter)
        {
            PostController pu = new PostController();
            pu.CreateNewPost(Text, Data);
        }

        public void BrowsePicture(object parameter)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "JPG Files (.jpg)|.jpg|JPEG Files (.jpeg)|.jpeg|PNG Files (.png)|.png|GIF Files (.gif)|.gif";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                txtBrowse.Text = filename;
                FileName = filename;
            }
        }

    }
}
