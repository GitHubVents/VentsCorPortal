using System;
using System.Drawing;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using AirVentsOrderManager;
using Authorization;
using FansAndMotors;
using System.Deployment.Application;
using LaboratoryTest;
using SmokeExtraction;
using Brushes = System.Windows.Media.Brushes;

namespace VentsCorpPortal
{
    /// <summary>
    /// Interaction logic for MainForm.xaml
    /// </summary>
    public partial class MainForm
    {
        public MainForm()
        {
            InitializeComponent();
            var dl = new DataLoad(); // LaboratoryTest
            var dc = new DataClass(); // FansAndMotors

        }

        public string PublishVersion
        {
            get
            {
                if (!ApplicationDeployment.IsNetworkDeployed) return "Not Published";
                var ver = ApplicationDeployment.CurrentDeployment.CurrentVersion;
                return string.Format("Каталог электромоторов и вентиляторов ver. {0}.{1}.{2}.{3}", ver.Major, ver.Minor, ver.Build, ver.Revision);
            }
        }

        private AuthorizationForm _windowAuthorization;
        private UcMotorFan MotorFanUserControl;

        //readonly DataUser _du = new DataUser();

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {

            if ((string) BtnExit.Content == "Назад")
            {
                BtnHome();
            }
            else
            {
                Properties.Settings.Default.UserName = AuthorizationForm.LoginUser;
                Properties.Settings.Default.Password = "";
                Properties.Settings.Default.RememberMe = false;
                Properties.Settings.Default.Save();

                AuthorizationForm.LoginUser = Properties.Settings.Default.UserName;
                AuthorizationForm.rememberMe = Properties.Settings.Default.RememberMe;

                _windowAuthorization = new AuthorizationForm();

                _windowAuthorization.Show();

                Close();
                
            }

        }

        private void LoadDg_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow.Title = PublishVersion;
                LoginedUser.Content = DataUser.GetLoginedUserName;

                //var motorFanForm = new UCMotorFan();
                ////var testUc = new UCTest();

                //MessageBox.Show(Admin.CheckUsers(1).ToString());

                //// Загрузка каталога Моторов Id - 10
                //if (Admin.CheckUsers(1))
                //{
                //    LoadDg.Children.Add(motorFanForm);
                //}
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        #region " Check Action Code "
        // CheckActionCode
        //1.StackFansAndMotors
        private void StackFansAndMotors_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (CheckActionCode.ViewCatalogueFanAndMotors())
            {
                UniformGrid.Visibility = Visibility.Hidden;
                var motorFanForm = new UcMotorFan();
                LoadDg.Children.Add(motorFanForm);

                BtnExit.Content = "Назад";


            }
            else
            {
                MessageBox.Show("Нарушение прав доступа!");
            }
      
        }
        private void StackFansAndMotors_MouseMove(object sender, MouseEventArgs e)
        {
            StackFansAndMotors.Background = Brushes.LightBlue;
            StackFansAndMotors.Opacity = 0.6;
            

        }
        private void StackFansAndMotors_MouseLeave(object sender, MouseEventArgs e)
        {
            StackFansAndMotors.Background = Brushes.AliceBlue;
            StackFansAndMotors.Opacity = 0.6;
        }

        //2.StackSmoke
        private void StackSmoke_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                UniformGrid.Visibility = Visibility.Hidden;
                var smokeExtractionMainUc = new SmokeExtractionMainUc();
                LoadDg.Children.Add(smokeExtractionMainUc);
                BtnExit.Content = "Назад";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void StackSmoke_MouseMove(object sender, MouseEventArgs e)
        {
            StackSmoke.Background = Brushes.LightBlue;
            StackSmoke.Opacity = 0.6;
        }
        private void StackSmoke_MouseLeave(object sender, MouseEventArgs e)
        {
            StackSmoke.Background = Brushes.AliceBlue;
        }

        //3.StackPanelLab
        private void StackPanelLab_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //if (CheckActionCode.)
                //  {
                
                UniformGrid.Visibility = Visibility.Hidden;
                var listOfOrders = new ListOfOrders();
                LoadDg.Children.Add(listOfOrders);
                BtnExit.Content = "Назад";
                //  }
                //else
                //{
                //    MessageBox.Show("Нарушение прав доступа!");
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void StackPanelLab_MouseMove(object sender, MouseEventArgs e)
        {
            StackPanelLab.Background = Brushes.LightBlue;
            StackPanelLab.Opacity = 0.6;
        }
        private void StackPanelLab_MouseLeave(object sender, MouseEventArgs e)
        {
            StackPanelLab.Background = Brushes.AliceBlue;
        }

        //3.AirVentsOrder
        private void StackAirVentsOrder_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                UniformGrid.Visibility = Visibility.Hidden;
                var listOfOrders = new MainWindowAirVentsOrder();
                LoadDg.Children.Add(listOfOrders);
                BtnExit.Content = "Назад";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void StackAirVentsOrder_MouseMove(object sender, MouseEventArgs e)
        {
            StackAirVentsOrder.Background = Brushes.LightBlue;
            StackAirVentsOrder.Opacity = 0.6;
        }
        private void StackAirVentsOrder_MouseLeave(object sender, MouseEventArgs e)
        {
            StackAirVentsOrder.Background = Brushes.AliceBlue;
        }

        public void BtnHome()
        {
            LoadDg.Children.Clear();
            UniformGrid.Visibility = Visibility.Visible;
            BtnExit.Content = "Выход";
        }

        #endregion

      
    }
}