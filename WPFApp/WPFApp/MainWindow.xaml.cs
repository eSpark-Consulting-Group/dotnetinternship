using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {




        private ObservableCollection<UserInfo> users = new ObservableCollection<UserInfo>();

        
        public void UpdateRowCount()
        {
            rowCountLabel.Content = $"Row Count: {users.Count}";

            if (users.Count == 0)
            {
                main_info_text.Opacity = 100;
               
            }

            else if (users.Count != 0)
            {
                main_info_text.Opacity = 0;
               
            }
        }


        public MainWindow()
        {
            InitializeComponent();

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;


            myDG.ItemsSource = users;

            UpdateRowCount();


            


        }


        public void AddUserToDataGrid(UserInfo user)
        {
            users.Add(user);

            UpdateRowCount();

        }





        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();


        }

        private void btn_uc_Click(object sender, RoutedEventArgs e)
        {

            SignUp sup = new SignUp();
            sup.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            sup.ShowDialog();



        }









        UserInfo selectedUser;



        private void btn_del_data_Click(object sender, RoutedEventArgs e)
        {
            if (myDG.SelectedItem != null)
            {
                try
                {
                    selectedUser = (UserInfo)myDG.SelectedItem;
                    users.Remove(selectedUser);
                }
                catch (Exception)
                {

                    // throw;
                }

                UpdateRowCount();
            }
        }

        private void btn_undo_data_Click(object sender, RoutedEventArgs e)
        {
            if (selectedUser != null)
            {
                users.Add(selectedUser);
                selectedUser = null;

                UpdateRowCount();
            }
        }



        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            string searchQuery = search_box.Text.ToLower();

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                List<UserInfo> filteredUsers = users.Where(user =>
                    user.UserName.ToLower().Contains(searchQuery) ||
                    user.UserEmail.ToLower().Contains(searchQuery))
                    .ToList();

                myDG.ItemsSource = filteredUsers;
                rowCountLabel.Content = $"Row Count: {filteredUsers.Count}";
            }
            else
            {
                myDG.ItemsSource = users;

                UpdateRowCount();
            }
        }

      








        /*  
         internal void UpdateDataGrid(Dictionary<string, string> userData)
         {
             // Add the new user data to the DataGrid's items source
             myDG.Items.Add(new { Name = userData["Name"], Email = userData["Email"] });
         }
  */












    }
}
