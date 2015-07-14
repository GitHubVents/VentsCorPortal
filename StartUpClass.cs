using System;
using Authorization;
using System.Windows;
using LaboratoryTest;
using FansAndMotors;

namespace VentsCorpPortal
{
    public class StartUpClass
    {

        //public const string ConnectionString = @"Data Source=192.168.14.11;Initial Catalog=SWPlusDB;Persist Security Info=True;User ID=sa;Password=PDMadmin; Pooling=True";

        [STAThread]

        private static void Main()
        {
            if (Properties.Settings.Default.RememberMe)
            {
                var userName = Properties.Settings.Default.UserName;
                var password = Properties.Settings.Default.Password;

                //MessageBox.Show(userName + " " + password);

                Admin.AuthenticateUser(userName, password);
                //Admin.AuthenticateUser("kb79", "1");

                if (Admin.AuthenticationOk)
                {
                    var app = new Application();
                    var main = new MainForm();
                    app.Run(main);
                }
            }
            else
            {
                var windowAuthorization = new AuthorizationForm();
                windowAuthorization.ShowDialog();

                if (AuthorizationForm.rememberMe)
                {
                    Properties.Settings.Default.UserName = AuthorizationForm.LoginUser;
                    Properties.Settings.Default.Password = AuthorizationForm.PasswordUser;
                    Properties.Settings.Default.RememberMe = true;
                    Properties.Settings.Default.Save();
                }
                else
                {
                    Properties.Settings.Default.RememberMe = false;
                }

                if (Admin.AuthenticationOk)
                {
                    var app = new Application();
                    var main = new MainForm();
                    app.Run(main);
                }

            }


          
        }
    }
}