using System;
using System.Windows;

namespace AirVentsOrderManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowAirVentsOrder
    {
        public MainWindowAirVentsOrder()
        {
            try
            {
                InitializeComponent();
                UpdateMainUc();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        void UpdateMainUc()
        {
            try
            {
                MainGrid.Children.Clear();
                MainGrid.Children.Add(new MainUc());
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
            
        }

        private OrderWindow editManagerWindow;
        void NewOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                editManagerWindow = new OrderWindow();
                editManagerWindow.ShowDialog();
                UpdateMainUc();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        void ExportToExel_Click(object sender, RoutedEventArgs e)
        {

        }

        void EditManager_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var editManagerWindow = new ManagerEditorUc();
                editManagerWindow.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
            
        }

        void EditorСomponentry_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var editorСomponentryWindow = new СomponentryEditor();
                editorСomponentryWindow.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
            
        }
    }
}