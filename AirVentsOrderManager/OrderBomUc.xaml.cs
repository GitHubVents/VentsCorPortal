﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;

namespace AirVentsOrderManager
{
    /// <summary>
    /// Interaction logic for OrderBomInners.xaml
    /// </summary>
    public partial class OrderBomInners
    {
        public OrderBomInners()
        {
            InitializeComponent();
            DataTableToSee.Visibility = Visibility.Collapsed;
            
            //6 Нагреватель водяной
            //7 Охладитель водяной
            //8 Охладитель фреоновый
            //9 Рекуператор пластинчатый
            //11 Камера увлажнения
        }

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

           // OrderIdLbl.Content = OrderWindow.OrderId;
         //   SizeId.Content = OrderWindow.SizeAv;

            MotorFanBox.ItemsSource = ((IListSource)Motors()).GetList();
            MotorFanBox.DisplayMemberPath = "Nomenclature";
            MotorFanBox.SelectedValuePath = "IdNomenclature";
            MotorFanBox.SelectedIndex = 0;

            HeatExchangers.ItemsSource = ((IListSource)InnerItem(6)).GetList();
            HeatExchangers.DisplayMemberPath = "Column1";
            HeatExchangers.SelectedValuePath = "IdNomenclature";
            HeatExchangers.SelectedIndex = 0;

            ColdExchangers.ItemsSource = ((IListSource)InnerItem(7)).GetList();
            ColdExchangers.DisplayMemberPath = "Column1";
            ColdExchangers.SelectedValuePath = "IdNomenclature";
            ColdExchangers.SelectedIndex = 0;

            ColdExchangers2.ItemsSource = ((IListSource)InnerItem(8)).GetList();
            ColdExchangers2.DisplayMemberPath = "Column1";
            ColdExchangers2.SelectedValuePath = "IdNomenclature";
            ColdExchangers2.SelectedIndex = 0;

            RecuperatorBox.ItemsSource = ((IListSource)InnerItem(9)).GetList();
            RecuperatorBox.DisplayMemberPath = "Column1";
            RecuperatorBox.SelectedValuePath = "IdNomenclature";
            RecuperatorBox.SelectedIndex = 0;

            MoistureBox1.ItemsSource = ((IListSource)InnerItem(11)).GetList();
            MoistureBox1.DisplayMemberPath = "Column1";
            MoistureBox1.SelectedValuePath = "IdNomenclature";
            MoistureBox1.SelectedIndex = 0;

            DataTableToSee.ItemsSource =  InnerPartsOfOrder().AsDataView();//InnerItem().AsDataView();Motors().AsDataView();
            UpdateList();
        }
        
        void DeleteItem(object sender, RoutedEventArgs e)
        {
            var item = InnersList.SelectedItem as InnersListData;
            if (item != null) DelOrderBomItem(item.IdNomenclature, item.IdBom);
        }

        void UpdateList()
        {
            InnersList.ItemsSource = InnerPartsList();
        }

        #region Working with SQL

        DataTable Motors()
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
  AND ss.SizeID = " + OrderWindow.SizeAv, con);
                    var sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    sqlDataAdapter.Fill(standartSizeTable);
                    sqlDataAdapter.Dispose();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Ошибка выгрузки данных из базы Motors");
                }
                finally
                {
                    con.Close();
                }
            }
            return standartSizeTable;
        }

        static DataTable InnerItem(int id)
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
  " AND he.SizeID = " + OrderWindow.SizeAv, con);
                    var sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    sqlDataAdapter.Fill(standartSizeTable);
                    sqlDataAdapter.Dispose();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Ошибка выгрузки данных из базы InnerItem");
                }
                finally
                {
                    con.Close();
                }
            }
            return standartSizeTable;
        }

        IEnumerable<InnersListData> InnerPartsList()
        {
            var table = InnerPartsOfOrder();
            var list = (from DataRow row in table.Rows
                        select new InnersListData
                        {
                            Name = row["Nomenclature"].ToString(),
                            IdNomenclature = Convert.ToInt32(row["IDNomenclature"]),
                            Model  = row["Model"].ToString(),
                            Count = Convert.ToInt32(row["quantity"]),
                            IdBom = Convert.ToInt32(row["IDBOM"]),
                            //Manufactoring  = row["FirsrtName"].ToString(),
                            Notes = row["Notes"].ToString(),
                        }).ToList();
            return list;
        }
        
        DataTable InnerPartsOfOrder()
        {
            var standartSizeTable = new DataTable();
            using (var con = new SqlConnection(App.ConString))
            {
                try
                {
                    con.Open();

                    var sqlCommand = new SqlCommand(@"SELECT ob.IDBOM, o.ProjectNumber[Order],  od.InternalNumber, ob.Quantity, n.Nomenclature, n.IDNomenclature,  he.Notes, IsNull(he.Model, '') +  IsNull(f.Model, '') AS 'Model' FROM Nomenclature n
  LEFT JOIN AirVents.HeaterExchander he ON n.IDNomenclature = he.IDNomenclature
  LEFT JOIN AirVents.Fans f ON n.IDNomenclature = f.IDNomenclature
  INNER JOIN AirVents.OrderBOM ob ON n.IDNomenclature = ob.IDnomenclature
  INNER JOIN AirVents.[Order] o ON ob.IDOrder = o.OrderID
  INNER JOIN AirVents.OrderDetails od ON o.OrderID = od.OrderID
  WHERE o.OrderID = " + 1,
                      //OrderWindow.OrderId,
                      con);
                    var sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    sqlDataAdapter.Fill(standartSizeTable);
                    sqlDataAdapter.Dispose();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Ошибка выгрузки данных из базы InnerPartsOfOrder");
                }
                finally
                {
                    con.Close();
                }
            }
            return standartSizeTable;
        }

        void AddOrderBomItem(int quantity, int idNomenclature)
        {
            if (!WorkWithParam(1, quantity, idNomenclature, 0))
            {
              //  return EditOrderBomItem(int quantity, int idNomenclature)
            }
            //return true;
        }

        void EditOrderBomItem(int quantity, int idNomenclature, int idBom)
        {
            WorkWithParam(2, quantity, idNomenclature, idBom);
        }

        void DelOrderBomItem(int idNomenclature, int idBom)
        {
            WorkWithParam(3, 0, idNomenclature, idBom);
        }

        bool WorkWithParam(int param, int quantity, int idNomenclature, int idBom)
        {
            using (var con = new SqlConnection(App.ConString))
            {
                try
                {
                    con.Open();
                    var sqlCommand = new SqlCommand("[AirVents].[OrderBOMAddEditDel]", con) { CommandType = CommandType.StoredProcedure };

                    var sqlParameter = sqlCommand.Parameters;

           //         sqlParameter.AddWithValue("@IDOrder", OrderWindow.OrderId);
                    sqlParameter.AddWithValue("@IDnomenclature", idNomenclature);
                    sqlParameter.AddWithValue("@quantity", quantity);

                    sqlParameter.AddWithValue("@IDBOM", idBom);
                    
                    sqlParameter.AddWithValue("@PARAM", param);

                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Введите корректные данные!\n" + exception.Message);
                    return false;
                }
                finally
                {
                    con.Close();
                    UpdateList();
                }
            }
            return true;
        }

        #endregion

        void InnersList_CurrentCellChanged(object sender, EventArgs e)
        {
            var item = InnersList.SelectedItem as InnersListData;

            if (item != null)

                EditOrderBomItem(item.Count, item.IdNomenclature, item.IdBom);
        }

        void AddMotorFan_Click(object sender, RoutedEventArgs e)
        {
            if (MotorFanBox.SelectedValue != null)
                AddOrderBomItem(1, Convert.ToInt32(MotorFanBox.SelectedValue.ToString()));
        }

        void AddHeatExchangers_Click(object sender, RoutedEventArgs e)
        {
            if (HeatExchangers.SelectedValue != null)
                AddOrderBomItem(1, Convert.ToInt32(HeatExchangers.SelectedValue.ToString()));
        }

        void AddColdExchangers_Click(object sender, RoutedEventArgs e)
        {
            if (ColdExchangers.SelectedValue != null)
                AddOrderBomItem(1, Convert.ToInt32(ColdExchangers.SelectedValue.ToString()));
        }

        void AddColdExchangers2_Click(object sender, RoutedEventArgs e)
        {
            if (ColdExchangers2.SelectedValue != null)
                AddOrderBomItem(1, Convert.ToInt32(ColdExchangers2.SelectedValue.ToString()));
        }

        void Recuperator_Click(object sender, RoutedEventArgs e)
        {
            if (RecuperatorBox.SelectedValue != null)
                AddOrderBomItem(1, Convert.ToInt32(RecuperatorBox.SelectedValue.ToString()));
        }

        void Moisture1_Click(object sender, RoutedEventArgs e)
        {
            if (MoistureBox1.SelectedValue != null)
                AddOrderBomItem(1, Convert.ToInt32(MoistureBox1.SelectedValue.ToString()));
        }
    }
}
