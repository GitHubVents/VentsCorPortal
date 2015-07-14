using System;
using System.Windows;
using AirVentsOrderManager.Model;

namespace AirVentsOrderManager
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow
    {
        public OrderData.OrdersConstructorDataClass OrdersConstructorDataItem { get; set; }
        
        public OrderWindow()
        {
            InitializeComponent();
        }
       
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                UcGrid.Children.Clear();
                var allOrder = new AllOrder
                {
                    OrdersConstructorDataItem = OrdersConstructorDataItem
                };
                UcGrid.Children.Add(allOrder);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.StackTrace);
            }
            
        }
    }
}
