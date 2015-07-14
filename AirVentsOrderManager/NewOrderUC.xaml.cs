using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using AirVentsOrderManager.Model;

namespace AirVentsOrderManager
{
    
    public delegate void MyEventHandler(object sender, MyEventArgs e);

    public class MyEventArgs : EventArgs
    {
        
    }

    /// <summary>
    /// Interaction logic for NewOrderUc.xaml
    /// </summary>
    public partial class NewOrderUc
    {

        public delegate void ButtonClickedEventHandler(object sender, EventArgs e);
        public event ButtonClickedEventHandler OnUserControlButtonClicked;

        public NewOrderUc()
        {

           // Ok.Click += OnButtonClicked;

            InitializeComponent();

            МенеджерыBox.ItemsSource = ((IListSource)OrderData.ManagersTable()).GetList();
            МенеджерыBox.DisplayMemberPath = "LastName";
            МенеджерыBox.SelectedValuePath = "EmpID";
            МенеджерыBox.SelectedIndex = 0;

            Типоразмер.ItemsSource = ((IListSource)OrderData.AirVentsStandardSize()).GetList();
            Типоразмер.DisplayMemberPath = "IdBom";
            Типоразмер.SelectedValuePath = "SizeID";
            Типоразмер.SelectedIndex = 0;

            ДатаЗаказаDate.SelectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

        }

        void OnButtonClicked(object sender, EventArgs e)
        {
            if (OnUserControlButtonClicked != null)
                OnUserControlButtonClicked(this, e);
        }

        public event UserControlClickHandler InnerButtonClick;
        public delegate void UserControlClickHandler(object sender, EventArgs e);
        protected void YourButton_Click(object sender, EventArgs e)
        {
            if (InnerButtonClick != null)
            {
                Ok_Click( sender, null);

                InnerButtonClick(sender, e);
            }
        }

        public event RoutedEventHandler ClickNext
        {
            add
            {
                if (Ok == null) return;
                if (value != null) Ok.Click += value;
            }
            remove
            {
                if (Ok == null) return;
                if (value != null) Ok.Click -= value;
            }
        }
        
        public event MyEventHandler MyEvent;
        protected virtual void OnMyEvent(MyEventArgs e)
        {
            MyEvent(this, e);
        }
       
        void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Номерподбора.Text)) 
            {
                MessageBox.Show(" Введите номер подбора!", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            if (string.IsNullOrEmpty(Номерподбора.Text))
            {
                MessageBox.Show(" Введите номер подбора!", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            //OrderWindow.OrderId =
            //    OrderData.AirVents_AddOrder
            //    (
            //    Convert.ToInt32(МенеджерыBox.SelectedValue),
            //    Convert.ToInt32(Типоразмер.SelectedValue),
            //    Номерподбора.Text,
            //    ТипКаркаса.Text,
            //    ДатаЗаказаDate.SelectedDate,

            //    string.IsNullOrEmpty(ПроизводительностьПриток.Text) ? 0 : Convert.ToInt32(ПроизводительностьПриток.Text),
            //    string.IsNullOrEmpty(ПолноеСтатическоеДавлениеВентилятораПриток.Text) ? 0 : Convert.ToInt32(ПолноеСтатическоеДавлениеВентилятораПриток.Text),
            //    string.IsNullOrEmpty(СкоростьВСеченииПриток.Text) ? 0 : Convert.ToDouble(СкоростьВСеченииПриток.Text),

            //    string.IsNullOrEmpty(ПроизводительностьВытяжка.Text) ? 0 : Convert.ToInt32(ПроизводительностьВытяжка.Text),
            //    string.IsNullOrEmpty(ПолноеСтатическоеДавлениеВентилятораВитяжка.Text) ? 0 : Convert.ToInt32(ПолноеСтатическоеДавлениеВентилятораВитяжка.Text),
            //    string.IsNullOrEmpty(СкоростьВСеченииВитяжка.Text) ? 0 : Convert.ToDouble(СкоростьВСеченииВитяжка.Text),
                
            //    Left.IsChecked
            //    );

            //MessageBox.Show(Convert.ToString(OrderWindow.OrderId), Convert.ToString(OrderWindow.SizeAv));

            OrderWindow.SizeAv = Convert.ToInt32(Типоразмер.SelectedValue);
        }
        
        static void InputDigits(TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9,.]");
            e.Handled = regex.IsMatch(e.Text);
        }

        void СкоростьВСечении_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            InputDigits(e);
        }

        void Производительность_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            InputDigits(e);
        }

        void Типоразмер_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (Типоразмер.SelectedValue == null) return;

            ТипКаркаса.ItemsSource = ((IListSource)OrderData.ProfilSizeTable(Convert.ToInt32(Типоразмер.SelectedValue))).GetList();
            ТипКаркаса.DisplayMemberPath = "Description";
            ТипКаркаса.SelectedValue = "Description";
            ТипКаркаса.SelectedIndex = 0;
        }

    }
}
