using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using AirVentsOrderManager.Model;

namespace AirVentsOrderManager
{
    /// <summary>
    /// Interaction logic for AllOrder.xaml
    /// </summary>
    public partial class AllOrder
    {
        public OrderData.OrdersConstructorDataClass OrdersConstructorDataItem { get; set; }

        public AllOrder()
        {
            InitializeComponent();

            DataGridOrderInfo.Visibility = Visibility.Collapsed;
            DataGridOrderInfoList.Visibility = Visibility.Collapsed;

            InnersList.Visibility = Visibility.Collapsed;
            DataTableToSee.Visibility = Visibility.Collapsed;
            IdLabelsGrid.Visibility = Visibility.Collapsed;

            _newList = new List<InnersListData>();

            #region КТБ

            Конструктор.ItemsSource = ((IListSource)OrderData.ConstructorsList()).GetList();
            Конструктор.DisplayMemberPath = "LastName";
            Конструктор.SelectedValuePath = "ConstructorID";
            Конструктор.SelectedIndex = 0;

            #endregion

            ДатаПоступленияЗаказа.SelectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            ДатаОтгрузкиЗаказа.SelectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            ПланируемаяСдачаКд.SelectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            ФактическаяСдачаКд.SelectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            #region Подбор

            МенеджерыBox.ItemsSource = ((IListSource)OrderData.ManagersTable()).GetList();
            МенеджерыBox.DisplayMemberPath = "LastName";
            МенеджерыBox.SelectedValuePath = "EmpID";
            МенеджерыBox.SelectedIndex = 0;

            Типоразмер.ItemsSource = ((IListSource)OrderData.AirVentsStandardSize()).GetList();
            Типоразмер.DisplayMemberPath = "Type";
            Типоразмер.SelectedValuePath = "SizeID";
            Типоразмер.SelectedIndex = 0;

            RecuperatorBoxSizeAv.ItemsSource = Типоразмер.ItemsSource;
            RecuperatorBoxSizeAv.DisplayMemberPath = "Type";
            RecuperatorBoxSizeAv.SelectedValuePath = "SizeID";
            RecuperatorBoxSizeAv.SelectedIndex = 0;

            MoistureBoxSizeAv.ItemsSource = Типоразмер.ItemsSource;
            MoistureBoxSizeAv.DisplayMemberPath = "Type";
            MoistureBoxSizeAv.SelectedValuePath = "SizeID";
            MoistureBoxSizeAv.SelectedIndex = 0;

            ColdExchangersSizeAv.ItemsSource = Типоразмер.ItemsSource;
            ColdExchangersSizeAv.DisplayMemberPath = "Type";
            ColdExchangersSizeAv.SelectedValuePath = "SizeID";
            ColdExchangersSizeAv.SelectedIndex = 0;

            ColdExchangers1SizeAv.ItemsSource = Типоразмер.ItemsSource;
            ColdExchangers1SizeAv.DisplayMemberPath = "Type";
            ColdExchangers1SizeAv.SelectedValuePath = "SizeID";
            ColdExchangers1SizeAv.SelectedIndex = 0;

            HeatExchangersSizeAv.ItemsSource = Типоразмер.ItemsSource;
            HeatExchangersSizeAv.DisplayMemberPath = "Type";
            HeatExchangersSizeAv.SelectedValuePath = "SizeID";
            HeatExchangersSizeAv.SelectedIndex = 0;

            MotorFanBoxSizeAv.ItemsSource = Типоразмер.ItemsSource;
            MotorFanBoxSizeAv.DisplayMemberPath = "Type";
            MotorFanBoxSizeAv.SelectedValuePath = "SizeID";
            MotorFanBoxSizeAv.SelectedIndex = 0;

            ДатаЗаказаDate.SelectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            #endregion
        }

        void Типоразмер_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Типоразмер.SelectedValue == null) return;

            ТипКаркаса.ItemsSource = ((IListSource)OrderData.ProfilSizeTable(Convert.ToInt32(Типоразмер.SelectedValue))).GetList();
            ТипКаркаса.DisplayMemberPath = "Description";
            ТипКаркаса.SelectedValuePath = "ProfilID";
            ТипКаркаса.SelectedIndex = 0;
            
            RecuperatorBoxSizeAv.SelectedValue = Типоразмер.SelectedValue;
            HeatExchangersSizeAv.SelectedValue = Типоразмер.SelectedValue;
            MoistureBoxSizeAv.SelectedValue = Типоразмер.SelectedValue;
            ColdExchangersSizeAv.SelectedValue = Типоразмер.SelectedValue;
            ColdExchangers1SizeAv.SelectedValue = Типоразмер.SelectedValue;
            MotorFanBoxSizeAv.SelectedValue = Типоразмер.SelectedValue;

            MotorFanBoxUpdate(Типоразмер);
            HeatExchangersSizeAvBoxUpdate(Типоразмер);
            ColdExchangers1SizeAvBoxUpdate(Типоразмер);
            ColdExchangersSizeAvBoxUpdate(Типоразмер);
            RecuperatorBoxSizeAvBoxUpdate(Типоразмер);
            MoistureBoxSizeAvBoxUpdate(Типоразмер);
        }

        static void InputDigits(TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9,.]");
            e.Handled = regex.IsMatch(e.Text);
        }

        void Производительность_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            InputDigits(e);
        }

        void СкоростьВСечении_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            InputDigits(e);
        }

        void Отмена(object sender, RoutedEventArgs e)
        {
            CloseParentWindow();
        }

        void СохранитьВсе(object sender, RoutedEventArgs e)
        {
            if (OrdersConstructorDataItem == null)
            {
                if (string.IsNullOrEmpty(Номерподбора.Text))
                {
                    MessageBox.Show(" Введите номер подбора!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                if (NewInnersList.Items.Count == 0)
                {
                    MessageBox.Show(" Добавте комплектующие (минимум одну позицию)!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var partsBom = (List<InnersListData>)NewInnersList.ItemsSource;
                
                if (partsBom != null && partsBom.Count > 0)
                {
                    foreach (var part in partsBom)
                    {
                        AddOrderAirVents(Convert.ToInt32(part.IdNomenclature) , Convert.ToInt32(part.Count));
                    }
                }

                try
                {
                    new PdmFilesFoldersOrder
                    {
                        OrderName = Типоразмер.Text + " " + НомерЗаказа.Text,
                        UnitTypeFrameless = Convert.ToInt32(ТипКаркаса.SelectedValue) == 6,
                        VaultName = App.PdmVaultName// "Tets_debag", //"Vents-PDM",
                    }.CreateOrder();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.ToString());
                }
            }
            else
            {
                var partsBom = (List<InnersListData>)NewInnersList.ItemsSource;

                foreach (var part in partsBom)
                {
                    //MessageBox.Show(" IdNomenclature - " + part.IdNomenclature + " Count - " + part.Count + " IdBom - " + part.IdBom, НомерЗаказа.Text);
                    EditOrderAirVents(Convert.ToInt32(part.IdNomenclature), Convert.ToInt32(part.Count), Convert.ToInt32(part.IdBom));

                }
                    if (partsBom.Count == 0)
                    {
                        EditOrderAirVents(0, 0, 0);
                    }
                
            }
            
            CloseParentWindow();
        }

        void CloseParentWindow()
        {
            var parentWindow = Window.GetWindow(this);
            if (parentWindow != null) parentWindow.Close();
        }
        
        #region  Комплектующие

        public class InnersListData
        {
            public string Name { get; set; }
            public int IdNomenclature { get; set; }
            public int IdBom { get; set; }

            public string Model { get; set; }
            public int Count { get; set; }
            public string Manufactoring { get; set; }
            public string Notes { get; set; }
        }

        void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            if (OrdersConstructorDataItem != null)
                NewInnersList.ItemsSource = InnerPartsList(OrdersConstructorDataItem.OrderId);
            UpdateList();
        }

        #region BOM Working with SQL

        static DataTable Motors(int sizeId)
        {
            var standartSizeTable = new DataTable();

            using (var con = new SqlConnection(App.ConString))
            {
                try
                {
                    con.Open();
                    var sqlCommand = new SqlCommand(@"SELECT n.IdNomenclature, n.Nomenclature FROM Nomenclature n
  INNER JOIN AirVents.Fans f ON n.IDNomenclature = f.IDNomenclature
  INNER JOIN AirVents.StandardSize ss ON f.SizeID = ss.SizeID
  WHERE n.IDNomenclatureGroup = 14
  AND ss.SizeID = " + sizeId, con);
                    var sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    sqlDataAdapter.Fill(standartSizeTable);
                    sqlDataAdapter.Dispose();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Ошибка выгрузки данных из базы");
                }
                finally
                {
                    con.Close();
                }
            }
            return standartSizeTable;
        }

        static DataTable InnerItem(int id, int sizeId)
        {
            var standartSizeTable = new DataTable();
            using (var con = new SqlConnection(App.ConString))
            {
                try
                {
                    con.Open();
                    var sqlCommand = new SqlCommand(@"SELECT n.IdNomenclature, n.Nomenclature + ' - ' + he.Model FROM Nomenclature n
  INNER JOIN AirVents.HeaterExchander he ON n.IdNomenclature = he.IdNomenclature
  WHERE n.IDNomenclatureGroup = " + id +
  " AND he.SizeID = " + sizeId, con);
                    var sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    sqlDataAdapter.Fill(standartSizeTable);
                    sqlDataAdapter.Dispose();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Ошибка выгрузки данных из базы");
                }
                finally
                {
                    con.Close();
                }
            }
            return standartSizeTable;
        }

        private List<InnersListData> _newList;

        static IEnumerable<InnersListData> InnerPartsList(int orderId)
        {
            var table = InnerPartsOfOrder(orderId);
            var list = (from DataRow row in table.Rows
                        select new InnersListData
                        {
                            Name = row["Nomenclature"].ToString(),
                            IdNomenclature = Convert.ToInt32(row["IDNomenclature"]),
                            Model = row["Model"].ToString(),
                            Count = Convert.ToInt32(row["quantity"]),
                            IdBom = Convert.ToInt32(row["IDBOM"]),
                            Notes = row["Notes"].ToString(),
                        }).ToList();
            return list;
        }

        static DataTable InnerPartsOfOrder(int orderId)
        {
            var standartSizeTable = new DataTable();
            using (var con = new SqlConnection(App.ConString))
            {
                try
                {
                    con.Open();

                    var sqlCommand = new SqlCommand(@"SELECT
ob.IDBOM,
o.ProjectNumber[Order],
od.InternalNumber,
ob.Quantity,
n.Nomenclature,
n.IDNomenclature,
he.Notes,
IsNull(he.Model, '') +  IsNull(f.Model, '') AS 'Model'
FROM Nomenclature n
  LEFT JOIN AirVents.HeaterExchander he ON n.IDNomenclature = he.IDNomenclature
  LEFT JOIN AirVents.Fans f ON n.IDNomenclature = f.IDNomenclature
  INNER JOIN AirVents.OrderBOM ob ON n.IDNomenclature = ob.IDnomenclature
  INNER JOIN AirVents.[Order] o ON ob.IDOrder = o.OrderID
  INNER JOIN AirVents.OrderDetails od ON o.OrderID = od.OrderID
  WHERE o.OrderID = " + orderId, con);
                    var sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    sqlDataAdapter.Fill(standartSizeTable);
                    sqlDataAdapter.Dispose();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Ошибка выгрузки данных из базы");
                }
                finally
                {
                    con.Close();
                }
            }
            return standartSizeTable;
        }
        
        void EditOrderBomItem(int quantity, int idNomenclature, int idBom)
        {
            WorkWithParam(2, quantity, idNomenclature, idBom);
        }

        void DelOrderBomItem(int idNomenclature, int idBom)
        {
            WorkWithParam(3, 0, idNomenclature, idBom);
        }

        void WorkWithParam(int param, int quantity, int idNomenclature, int idBom)
        {
            using (var con = new SqlConnection(App.ConString))
            {
                try
                {
                    con.Open();
                    var sqlCommand = new SqlCommand("[AirVents].[OrderBOMAddEditDel]", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    var sqlParameter = sqlCommand.Parameters;

                    sqlParameter.AddWithValue("@IDnomenclature", idNomenclature);
                    sqlParameter.AddWithValue("@quantity", quantity);
                    sqlParameter.AddWithValue("@IDBOM", idBom);
                    sqlParameter.AddWithValue("@PARAM", param);

                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Введите корректные данные!\n" + exception.Message);
                }
                finally
                {
                    con.Close();
                    UpdateList();
                }
            }
        }

        void UpdateList()
        {
            if (OrdersConstructorDataItem != null)
                NewInnersList.ItemsSource = InnerPartsList(OrdersConstructorDataItem.OrderId);
        }

        #endregion
        
        void AddToNewList(InnersListData newInnerData)
        {
            var list = (List<InnersListData>)NewInnersList.ItemsSource;
            if (list == null)
            {
                list = _newList;
                list.Add(newInnerData);
                NewInnersList.ItemsSource = null;
                NewInnersList.ItemsSource = list;
            }
            else
            {
                _newList = list;
                _newList.Add(newInnerData);
                NewInnersList.ItemsSource = null;
                NewInnersList.ItemsSource = _newList;
            }
            NewInnersList.BringIntoView();
        }

        void AddMotorFan_Click(object sender, RoutedEventArgs e)
        {
            if (MotorFanBox.SelectedValue != null)

                AddToNewList(new InnersListData
                {
                    IdNomenclature = Convert.ToInt32(MotorFanBox.SelectedValue),
                    Count = 1,
                    Name = MotorFanBox.Text
                });
        }

        void AddHeatExchangers_Click(object sender, RoutedEventArgs e)
        {
            if (HeatExchangers.SelectedValue != null)
                AddToNewList(new InnersListData
                {
                    IdNomenclature = Convert.ToInt32(HeatExchangers.SelectedValue),
                    Count = 1,
                    Name = HeatExchangers.Text
                });
        }

        void AddColdExchangers_Click(object sender, RoutedEventArgs e)
        {
            if (ColdExchangers.SelectedValue != null)
                AddToNewList(new InnersListData
                {
                    IdNomenclature = Convert.ToInt32(ColdExchangers.SelectedValue),
                    Count = 1,
                    Name = ColdExchangers.Text
                });
        }

        void AddColdExchangers2_Click(object sender, RoutedEventArgs e)
        {
            if (ColdExchangers2.SelectedValue != null)
                AddToNewList(new InnersListData
                {
                    IdNomenclature = Convert.ToInt32(ColdExchangers2.SelectedValue),
                    Count = 1,
                    Name = ColdExchangers2.Text
                });
        }

        void Recuperator_Click(object sender, RoutedEventArgs e)
        {
            if (RecuperatorBox.SelectedValue != null)
                AddToNewList(new InnersListData
                {
                    IdNomenclature = Convert.ToInt32(RecuperatorBox.SelectedValue),
                    Count = 1,
                    Name = RecuperatorBox.Text
                });
        }

        void Moisture1_Click(object sender, RoutedEventArgs e)
        {
            if (MoistureBox1.SelectedValue != null)
                AddToNewList(new InnersListData
                {
                    IdNomenclature = Convert.ToInt32(MoistureBox1.SelectedValue),
                    Count = 1,
                    Name = MoistureBox1.Text
                });
        }

        void InnersList_CurrentCellChanged(object sender, EventArgs e)
        {
            var item = InnersList.SelectedItem as InnersListData;
            if (item != null) EditOrderBomItem(item.Count, item.IdNomenclature, item.IdBom);
        }

        void DeleteItem(object sender, RoutedEventArgs e)
        {
            var item = InnersList.SelectedItem as InnersListData;
            if (item != null) DelOrderBomItem(item.IdNomenclature, item.IdBom);
        }

        #endregion

        #region Inners

        void MotorFanBoxSizeAv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MotorFanBoxUpdate(MotorFanBoxSizeAv);
        }

        void MotorFanBoxUpdate(Selector comboBox)
        {
            try
            {
                var sizeId = Convert.ToInt32(comboBox.SelectedValue);
                MotorFanBox.ItemsSource = ((IListSource)Motors(sizeId)).GetList();
                MotorFanBox.DisplayMemberPath = "Nomenclature";
                MotorFanBox.SelectedValuePath = "IdNomenclature";
                MotorFanBox.SelectedIndex = 0;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        void HeatExchangersSizeAvBoxUpdate(Selector comboBox)
        {
            try
            {
                var sizeId = Convert.ToInt32(comboBox.SelectedValue);
                HeatExchangers.ItemsSource = ((IListSource)InnerItem(6, sizeId)).GetList();
                HeatExchangers.DisplayMemberPath = "Column1";
                HeatExchangers.SelectedValuePath = "IdNomenclature";
                HeatExchangers.SelectedIndex = 0;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        void HeatExchangersSizeAv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            HeatExchangersSizeAvBoxUpdate(HeatExchangersSizeAv);
        }

        void ColdExchangers1SizeAvBoxUpdate(Selector comboBox)
        {
            try
            {
                var sizeId = Convert.ToInt32(comboBox.SelectedValue);
                ColdExchangers.ItemsSource = ((IListSource)InnerItem(7, sizeId)).GetList();
                ColdExchangers.DisplayMemberPath = "Column1";
                ColdExchangers.SelectedValuePath = "IdNomenclature";
                ColdExchangers.SelectedIndex = 0;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        void ColdExchangers1SizeAv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ColdExchangers1SizeAvBoxUpdate(ColdExchangers1SizeAv);
        }

        void ColdExchangersSizeAvBoxUpdate(Selector comboBox)
        {
            try
            {
                var sizeId = Convert.ToInt32(comboBox.SelectedValue);
                ColdExchangers2.ItemsSource = ((IListSource)InnerItem(8, sizeId)).GetList();
                ColdExchangers2.DisplayMemberPath = "Column1";
                ColdExchangers2.SelectedValuePath = "IdNomenclature";
                ColdExchangers2.SelectedIndex = 0;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        void ColdExchangersSizeAv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ColdExchangersSizeAvBoxUpdate(ColdExchangersSizeAv);
        }

        void RecuperatorBoxSizeAv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RecuperatorBoxSizeAvBoxUpdate(RecuperatorBoxSizeAv);
        }
        
        void RecuperatorBoxSizeAvBoxUpdate(Selector comboBox)
        {
            try
            {
                var sizeId = Convert.ToInt32(comboBox.SelectedValue);
                RecuperatorBox.ItemsSource = ((IListSource)InnerItem(9, sizeId)).GetList();
                RecuperatorBox.DisplayMemberPath = "Column1";
                RecuperatorBox.SelectedValuePath = "IdNomenclature";
                RecuperatorBox.SelectedIndex = 0;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        void MoistureBoxSizeAvBoxUpdate(Selector comboBox)
        {
            try
            {
                var sizeId = Convert.ToInt32(comboBox.SelectedValue);
                MoistureBox1.ItemsSource = ((IListSource)InnerItem(11, sizeId)).GetList();
                MoistureBox1.DisplayMemberPath = "Column1";
                MoistureBox1.SelectedValuePath = "IdNomenclature";
                MoistureBox1.SelectedIndex = 0;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        void MoistureBoxSizeAv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MoistureBoxSizeAvBoxUpdate(MoistureBoxSizeAv);
        }

        #endregion

        void НомерЗаказа_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        void DeleteNewItem(object sender, RoutedEventArgs e)
        {
            var part = NewInnersList.SelectedItem as InnersListData;
            if (part == null) return;
            DeleteBomItemAirVents(part.IdBom);
            UpdateList();
        }

        #region Add Edit Delete Order

        void AddOrderAirVents(int? iDnomenclature, int? quantity)
        {
            try
            {
                    OrderData.AddOrderAirVents
                    (
                    Convert.ToInt32(МенеджерыBox.SelectedValue),
                    Номерподбора.Text,
                    ДатаЗаказаDate.SelectedDate,
                    Convert.ToInt32(Типоразмер.SelectedValue),
                    Convert.ToInt32(ТипКаркаса.SelectedValue),
                    
                    string.IsNullOrEmpty(ПроизводительностьПриток.Text) ? 0 : Convert.ToInt32(ПроизводительностьПриток.Text),
                    string.IsNullOrEmpty(ПолноеСтатическоеДавлениеВентилятораПриток.Text) ? 0 : Convert.ToInt32(ПолноеСтатическоеДавлениеВентилятораПриток.Text),
                    string.IsNullOrEmpty(СкоростьВСеченииПриток.Text) ? 0 : Convert.ToDouble(СкоростьВСеченииПриток.Text),

                    string.IsNullOrEmpty(ПроизводительностьВытяжка.Text) ? 0 : Convert.ToInt32(ПроизводительностьВытяжка.Text),
                    string.IsNullOrEmpty(ПолноеСтатическоеДавлениеВентилятораВитяжка.Text) ? 0 : Convert.ToInt32(ПолноеСтатическоеДавлениеВентилятораВитяжка.Text),
                    string.IsNullOrEmpty(СкоростьВСеченииВитяжка.Text) ? 0 : Convert.ToDouble(СкоростьВСеченииВитяжка.Text),

                    Left.IsChecked,
                    НомерЗаказа.Text,

                    ДатаПоступленияЗаказа.SelectedDate,
                    ДатаОтгрузкиЗаказа.SelectedDate,
                    ПланируемаяСдачаКд.SelectedDate,
                    ФактическаяСдачаКд.SelectedDate,

                    Convert.ToInt32(Конструктор.SelectedValue),
                    iDnomenclature,
                    quantity
                    );
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        void EditOrderAirVents(int iDnomenclature, int quantity, int idBom)
        {
            try
            {
                OrderData.EditOrderAirVents
                (
                OrdersConstructorDataItem.OrderId,
                Convert.ToInt32(МенеджерыBox.SelectedValue),
                Номерподбора.Text,
                ДатаЗаказаDate.SelectedDate,
                Convert.ToInt32(Типоразмер.SelectedValue),
                Convert.ToInt32(ТипКаркаса.SelectedValue),

                string.IsNullOrEmpty(ПроизводительностьПриток.Text) ? 0 : Convert.ToInt32(ПроизводительностьПриток.Text),
                string.IsNullOrEmpty(ПолноеСтатическоеДавлениеВентилятораПриток.Text) ? 0 : Convert.ToInt32(ПолноеСтатическоеДавлениеВентилятораПриток.Text),
                string.IsNullOrEmpty(СкоростьВСеченииПриток.Text) ? 0 : Convert.ToDouble(СкоростьВСеченииПриток.Text),

                string.IsNullOrEmpty(ПроизводительностьВытяжка.Text) ? 0 : Convert.ToInt32(ПроизводительностьВытяжка.Text),
                string.IsNullOrEmpty(ПолноеСтатическоеДавлениеВентилятораВитяжка.Text) ? 0 : Convert.ToInt32(ПолноеСтатическоеДавлениеВентилятораВитяжка.Text),
                string.IsNullOrEmpty(СкоростьВСеченииВитяжка.Text) ? 0 : Convert.ToDouble(СкоростьВСеченииВитяжка.Text),

                Left.IsChecked,
                НомерЗаказа.Text,

                ДатаПоступленияЗаказа.SelectedDate,
                ДатаОтгрузкиЗаказа.SelectedDate,
                ПланируемаяСдачаКд.SelectedDate,
                ФактическаяСдачаКд.SelectedDate,

                Convert.ToInt32(Конструктор.SelectedValue),
                iDnomenclature,
                quantity,
                idBom
                );
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        static void DeleteBomItemAirVents(int idBom)
        {
            try
            {
                OrderData.DeleteBomItemAirVents(idBom);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        #endregion
        
        void Grid_Loaded_1(object sender, RoutedEventArgs e)
        {
            
            #region OrdersInfo

            //OrderName.Header = Convert.ToString(OrdersConstructorDataItem.OrderId != 0 ? "Заказ " + OrdersConstructorDataItem.OrderId : "Заказ");

            var item = OrdersConstructorDataItem;

            if (item == null)
            {
                return;
            }

            #endregion

            #region Data and Comboxes

            try
            {
                ДатаЗаказаDate.SelectedDate = item.Date;
            }
            catch (Exception ex){MessageBox.Show(ex.Message);}

            try
            {
                МенеджерыBox.SelectedValue = item.ManagerId;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            try
            {
                Конструктор.SelectedValue = item.ConstructorId;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            // ToDo 
            try
            {
                Типоразмер.SelectedValue = item.SizeId;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            try
            {
                ТипКаркаса.SelectedValue = item.ProfilId;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            
            #endregion

            #region Order and Project Numbers

            try
            {
                Номерподбора.Text = item.ProjectNumber;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            try
            {
                НомерЗаказа.Text = item.Order;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            
            #endregion
            
            #region Dates for Order

            try
            {
                ДатаПоступленияЗаказа.SelectedDate = Convert.ToDateTime(item.RequiredDate);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }


            try
            {
                ДатаОтгрузкиЗаказа.SelectedDate = Convert.ToDateTime(item.ShippedDate);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            try
            {
                ПланируемаяСдачаКд.SelectedDate = Convert.ToDateTime(item.CompletionDate);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            try
            {
                #region
                //var existDate = Convert.ToDateTime(item.FinishCompletionDate);
                //var today = DateTime.Now;
                //var comparison = (DateComparisonResult)today.CompareTo(existDate);
                //MessageBox.Show("CompareTo method returns {0}: {1:d} is {2} than {3:d}",
                //                  (int)comparison, today, comparison.ToString().ToLower(),
                //                  existDate);
                #endregion

                ФактическаяСдачаКд.SelectedDate = Convert.ToDateTime(item.FinishCompletionDate);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            #endregion

            #region Side

            try
            {
                var isLeft = item.ServiceAccess;
                Left.IsChecked = isLeft;
                Right.IsChecked = !isLeft;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            
            #endregion

            #region TTX

            try
            {
                ПроизводительностьПриток.Text = Convert.ToString(item.SupplyTotalStaticPressure);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            
            try
            {
                ПолноеСтатическоеДавлениеВентилятораПриток.Text = Convert.ToString(item.SupplyStaticPressure);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            try
            {
                СкоростьВСеченииПриток.Text = Convert.ToString(item.SupplyAirflow);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            try
            {
                ПроизводительностьВытяжка.Text = Convert.ToString(item.ExhaustTotalStaticPressure);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            try
            {
                ПолноеСтатическоеДавлениеВентилятораВитяжка.Text = Convert.ToString(item.ExhaustStaticPressure);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            try
            {
                СкоростьВСеченииВитяжка.Text = Convert.ToString(item.ExhaustAirflow);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }


            #endregion

        }

    }
}