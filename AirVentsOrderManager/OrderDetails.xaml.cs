using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using AirVentsOrderManager.Model;

namespace AirVentsOrderManager
{
    /// <summary>
    /// Interaction logic for OrderDetails.xaml
    /// </summary>
    public partial class OrderDetails
    {
        public OrderDetails()
        {
            InitializeComponent();

            #region Конструктора

            Конструктор.ItemsSource = ((IListSource)OrderData.ConstructorsList()).GetList();
            Конструктор.DisplayMemberPath = "LastName";
            Конструктор.SelectedValuePath = "ConstructorID";
            Конструктор.SelectedIndex = 0;

            #endregion

            ДатаПоступленияЗаказа.SelectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            ДатаОтгрузкиЗаказа.SelectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            ПланируемаяСдачаКд.SelectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        }

        void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(НомерЗаказа.Text))
            {
                MessageBox.Show("Введите номер заказа!");
            }

            //OrderData.AirVents_AddOrderDetail
            //    (НомерЗаказа.Text,
            //    ДатаПоступленияЗаказа.SelectedDate,
            //    ДатаОтгрузкиЗаказа.SelectedDate,
            //    ПланируемаяСдачаКд.SelectedDate,
            //    ФактическаяСдачаКд.SelectedDate,
            //    OrderWindow.OrderId,
            //    Convert.ToInt32(Конструктор.SelectedValue));
        }

        void НомерЗаказа_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
