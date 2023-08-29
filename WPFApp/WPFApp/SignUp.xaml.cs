using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.IO;
using WPFApp;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {

        static byte[] Encrypt(string simpletext, byte[] key, byte[] iv)
        {
            byte[] cipheredtext;
            using (Aes aes = Aes.Create())
            {
                ICryptoTransform encryptor = aes.CreateEncryptor(key, iv);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(simpletext);
                        }

                        cipheredtext = memoryStream.ToArray();
                    }
                }
            }
            return cipheredtext;
        }

        public SignUp()
        {
            InitializeComponent();
            this.WindowStyle = WindowStyle.None;

            this.MaxHeight = this.MaxWidth = this.MinHeight = this.MinWidth = 450;

            signup_fields_info_1.Visibility = Visibility.Collapsed;
            signup_fields_info_2.Visibility = Visibility.Collapsed;
            signup_fields_info_3.Visibility = Visibility.Collapsed;
            signup_fields_info_4.Visibility = Visibility.Collapsed;

        }





        private void Button_Click(object sender, RoutedEventArgs e)
        {



            this.Close();
        }

        private void pass_KeyDown(object sender, KeyEventArgs e)
        {

        }






        private void btn_go_Click(object sender, RoutedEventArgs e)
        {



            Regex regex;
            Match match;

            {

                if (EmailBox.Text == "" || NameBox.Text == "" || passbox.Password == "" || confirm_passbox.Password == "")
                {
                    signup_fields_info_1.Visibility = Visibility.Collapsed;
                    signup_fields_info_2.Visibility = Visibility.Collapsed;
                    signup_fields_info_3.Visibility = Visibility.Collapsed;

                    signup_fields_info_4.Visibility = Visibility.Visible;

                    signup_fields_info_4.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(194, 12, 12));

                    signup_fields_info_4.Content = "Field(s) cannot be empty";

                }



                if (EmailBox.Text != "")
                {

                    regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,4})+)$");
                    match = regex.Match(EmailBox.Text);

                    if (match.Success == false)
                    {
                        signup_fields_info_1.Visibility = Visibility.Visible;

                        signup_fields_info_1.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(194, 12, 12));

                        signup_fields_info_1.Content = "Please type a valid email address!";
                    }


                    else
                    {
                        signup_fields_info_1.Visibility = Visibility.Collapsed;
                    }

                }



                if (NameBox.Text != "")
                {
                    regex = new Regex(@"^[a-zA-Z][\w]*$");
                    match = regex.Match(NameBox.Text);

                    if (match.Success == false)
                    {
                        signup_fields_info_2.Visibility = Visibility.Visible;

                        signup_fields_info_2.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(194, 12, 12));

                        signup_fields_info_2.Content = "Please type a valid Username!";
                    }



                    else
                    {
                        signup_fields_info_2.Visibility = Visibility.Collapsed;

                    }


                }



                if (passbox.Password != confirm_passbox.Password)
                {
                    signup_fields_info_3.Visibility = Visibility.Visible;

                    signup_fields_info_3.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(194, 12, 12));

                    signup_fields_info_3.Content = "password not matched";

                }

                if (passbox.Password == confirm_passbox.Password)
                {
                    signup_fields_info_3.Visibility = Visibility.Collapsed;
                }

                if (EmailBox.Text != "" && NameBox.Text != "" && passbox.Password != "" && confirm_passbox.Password != "")
                {
                    signup_fields_info_4.Visibility = Visibility.Collapsed;
                }



                if (signup_fields_info_1.IsVisible.Equals(false) && signup_fields_info_2.IsVisible.Equals(false) && signup_fields_info_3.IsVisible.Equals(false) && signup_fields_info_4.IsVisible.Equals(false))
                {
                    MessageBox.Show("Welcome " + NameBox.Text, "Greetings", button: MessageBoxButton.OK, icon: MessageBoxImage.Information);
                    this.Close();




                    //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

                    // code for password encryption

                    byte[] key = new byte[16];

                    byte[] iv = new byte[16];

                    using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
                    {
                        rng.GetBytes(key);
                        rng.GetBytes(iv);
                    }

                    //Encrypt the password
                    byte[] encryptedPassword = Encrypt(passbox.Password, key, iv);
                    string encryptedPasswordString = Convert.ToBase64String(encryptedPassword);




                    //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx





                    // Create a User object and add it to the data grid
                    UserInfo newUser = new UserInfo { UserName = NameBox.Text, UserEmail = EmailBox.Text, UserPassword = encryptedPasswordString };


                    if (Application.Current.MainWindow is MainWindow mainWindow)
                    {
                        mainWindow.AddUserToDataGrid(newUser);

                        MainWindow mw = new MainWindow();
                        //mw.UpdateRowCount();

                    }

                    

                    

                }

            }


        }


    }
}
