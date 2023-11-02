using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HBook.ViewModels;

namespace HBook.Views
{
    /// <summary>
    /// Interaction logic for SearchUser.xaml
    /// </summary>
    public partial class SearchUser : Window
    {
        SearchUserViewModel suv = new SearchUserViewModel();
        public SearchUser()
        {
            InitializeComponent();
            DataContext = suv;
        }
    }
}
