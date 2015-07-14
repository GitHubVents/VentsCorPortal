using System;
using System.Windows;
using System.Windows.Input;

namespace Authorization
{
    /// <summary>
    /// Interaction logic for Authenticated.xaml
    /// </summary>
    public partial class AuthorizationForm
    {
       

        public AuthorizationForm()
        {
            InitializeComponent();

            //UserName.Text = Properties.Settings.Default.UserName;
            //Password.Password = Properties.Settings.Default.Password;
            //RememberMe.IsChecked = Properties.Settings.Default.RememberMe;
        }

        static public string LoginUser { get; set; }
        static public string PasswordUser { get; set; }
        static public bool rememberMe { get; set; }

        void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoginUser = UserName.Text;
                PasswordUser = Password.Password;

                //MessageBox.Show(LoginUser + " - " + PasswordUser);

                if (Admin.AuthenticateUser(LoginUser, PasswordUser))
                {
                    if (RememberMe.IsChecked == true)
                    {

                        rememberMe = true;
                    }
                    else
                    {
                        rememberMe = false;
                    }

                    //var aaa = new MotorFan.MotorFanMain("Менеджер");
                    //this.Hide();
                    //aaa.Show();

                    //var homeForm = new MainWindow();

                    //homeForm.Show();

                    Close();

                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void Grid_Loaded_1(object sender, RoutedEventArgs e)
        {
            try
            {

                UserName.Text = LoginUser;

                if (!Admin.AuthenticateUser(LoginUser, PasswordUser))
                {
                   
                }

                //if (rememberMe)
                //{
                //    MessageBox.Show("show =)");

                //}
                //else
                //{
                //    MessageBox.Show("show =(");
                //}

            }
            catch (Exception exception)
            {
                MessageBox.Show("Во время запуска программы возникла ошибка" + exception);
            }
        }

        private void LabelAdmin_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //if (Admin.AuthenticateUser(UserName.Text, Password.Password))
            //{
                var homeForm = new Administation();

                homeForm.Show();

                //Close();
            //}
            //else
            //{
            //    MessageBox.Show("Неверный логин или пароль!");
            //}
        }

        private void UserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Button_Click(this, new RoutedEventArgs());
            }
        }

        private void Password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Button_Click(this, new RoutedEventArgs());
            }
        }


    }
}
