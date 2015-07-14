using System;
using System.Windows;
using AirVentsOrderManager.Model;

namespace AirVentsOrderManager
{
    /// <summary>
    /// Interaction logic for MainUc.xaml
    /// </summary>
    public partial class MainUc
    {
        public MainUc()
        {
            InitializeComponent();
            ForDataTable.Visibility = Visibility.Collapsed;
        }

        void OrderGrid_Loaded(object sender, RoutedEventArgs e)
        {
            Update();
        }

        void Update()
        {
            try
            {
                //ForDataTable.ItemsSource = OrderData.OrdersConstructorTable().AsDataView();
                OrderGrid.ItemsSource = OrderData.OrdersConstructorDataList();
                CounLabel.Content = "Всього замовлень: " + OrderGrid.Items.Count;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), exception.Message);
            }
            
        }

        void DeleteItem(object sender, RoutedEventArgs e)
        {
            DeleteOrderDetails();
        }

        void DeleteOrderDetails()
        {
            var order = (OrderData.OrdersConstructorDataClass)OrderGrid.SelectedItem;

            if (MessageBox.Show("Удалить " + order.Order + "?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
            {
                Update();
                return;
            }

            if (order == null)
            {
                MessageBox.Show("Выберете строку в таблице для изменения.");
                return;
            }

            OrderData.DeleteOrderAirVents(order.OrderId);
            Update();
        }
        
        private void EditItem(object sender, RoutedEventArgs e)
        {
            var order = (OrderData.OrdersConstructorDataClass)OrderGrid.SelectedItem;

            var orderWindow = new OrderWindow
            {
                OrdersConstructorDataItem = order
            };
            orderWindow.ShowDialog();
            Update();
        }
    }
}
