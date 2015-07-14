using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using AirVentsOrderManager.Model;

namespace AirVentsOrderManager
{
    /// <summary>
    /// Interaction logic for ManagerEditorUc.xaml
    /// </summary>
    public partial class ManagerEditorUc
    {
        public ManagerEditorUc()
        {
            InitializeComponent();
            UpdateData();
        }

        void UpdateData()
        {
            ТаблицаМенеджеров.ItemsSource = OrderData.ManagersList();
            Managers = OrderData.ManagersList();
        }

        List<OrderData.Manager> Managers { get; set; }

        void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        void DeleteItem(object sender, RoutedEventArgs e)
        {
            DeleteManager();
        }

        void ТаблицаМенеджеров_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                DeleteManager();
            }
        }

        void DeleteManager()
        {
            var manager = (OrderData.Manager) ТаблицаМенеджеров.SelectedItem;

            if (MessageBox.Show("Удалить " + manager.LastName + " " + manager.FirstName + "?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
            {
                UpdateData();
                return;
            }
                
            if (manager == null)
            {
                MessageBox.Show("Выберете строку в таблице для изменения.");
                return;
            }
            OrderData.DeleteManager(manager.IdManager);
            UpdateData();
        }

        void ТаблицаМенеджеров_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                var manager = (OrderData.Manager) ТаблицаМенеджеров.SelectedItem;

                if (manager == null) return;

                if (manager.IdManager != 0)
                {
                    var oldManager = Managers.Single(x => x.IdManager == manager.IdManager);
                    if (oldManager.FirstName == manager.FirstName && oldManager.LastName == manager.LastName) return;
                    OrderData.UpdateManager(manager.FirstName, manager.LastName, manager.IdManager);
                    UpdateData();
                }
                else
                {
                    if ( string.IsNullOrEmpty(manager.FirstName) && string.IsNullOrEmpty(manager.LastName)) return;
                    OrderData.AddManager(manager.FirstName, manager.LastName);
                    UpdateData();
                }
            }
            catch
            {
                // MessageBox.Show(exception.ToString());
            }
        }

    }
}
