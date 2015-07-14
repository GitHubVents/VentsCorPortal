using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;

namespace FansAndMotors
{
    /// <summary>
    /// Interaction logic for AddFanItem.xaml
    /// </summary>
    public partial class AddFanItem : Window
    {
        public AddFanItem()
        {
            InitializeComponent(); 
        }

        private void GridHomeAddFanItem_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDgMotorAddFanItem();
            BindComboBoxes();
        }

        readonly DataLoad _dl = new DataLoad();

        public void LoadDgMotorAddFanItem()
        {
            try
            {
                DgMotor.ItemsSource = _dl.MotorDataGridList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void BindComboBoxes()
        {
            LoadCboType();
            LoadCboFansSize();
            ComboBoxTradeMark();
        }

        public void LoadCboType()
        {
            try
            {

            var con = new SqlConnection(_dl.ConString);
            var cmd = new SqlCommand
            {
                Connection = con,
                CommandType = CommandType.Text,
                CommandText = @"SELECT IdFanType, TypeName FROM [MotorFan].[FanType] Order By TypeName"
            };

            var dt = new DataTable();
            var da = new SqlDataAdapter {SelectCommand = cmd};

            con.Open();
            da.Fill(dt);

            CboType.SelectedValuePath = "IdFanType";
            CboType.DisplayMemberPath = "TypeName";
            CboType.ItemsSource = dt.DefaultView;

            con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void LoadCboFansSize()
        {
            try
            {
                var dt = new DataTable();

                const string queryString = @"SELECT IdFanSize, FanSize FROM [MotorFan].[FanSize] Order By FanSize"; ;

                var con = new SqlConnection(_dl.ConString);

                var da = new SqlDataAdapter(queryString, con);
                con.Open();

                da.Fill(dt);

                CboSize.SelectedValuePath = "IdFanSize";
                CboSize.DisplayMemberPath = "FanSize";
                CboSize.ItemsSource = dt.DefaultView;

                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ComboBoxTradeMark()
        {
            CboTradeMark.SelectedValuePath = "Code";
            CboTradeMark.DisplayMemberPath = "Name";
            CboTradeMark.ItemsSource = _dl.TradeMark().DefaultView;
        }

        private void CboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cboTypeIdFan = Convert.ToInt32(CboType.SelectedValue);
            var cboFanSize = Convert.ToInt32(CboSize.SelectedValue);

            //DgImpeller.ItemsSource = DtImpeller(cboTypeIdFan, cboFanSize).DefaultView;

            DgImpeller.ItemsSource = ImperllerList(cboTypeIdFan, cboFanSize);

        }

        private void CboSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cboTypeIdFan = Convert.ToInt32(CboType.SelectedValue);
            var cboFanSize = Convert.ToInt32(CboSize.SelectedValue);

            //DgImpeller.ItemsSource = DtImpeller(cboTypeIdFan, cboFanSize).DefaultView;
            DgImpeller.ItemsSource = ImperllerList(cboTypeIdFan, cboFanSize);

        }

        private void CboTradeMark_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        public DataTable DtImpeller(int typeFanId, int fanSizeId)
        {
            var dt = new DataTable();

            var con = new SqlConnection(_dl.ConString);

            var q = @"SELECT IDImpeller, IDFanType, IDFanSize, Wight, Hub, Description, RotationDirection
                    FROM MotorFan.Impeller WHERE IDFanType = " + typeFanId + @" AND IDFanSize = " + fanSizeId;
            var sqlCmd = new SqlCommand(q, con);
            var da = new SqlDataAdapter(sqlCmd);
            da.Fill(dt);

            return dt;
        }

        public class ImpellerClass
        {
            public int ImpellerId { get; set; }
            public int TypeFanId { get; set; }
            public int FanSizeId { get; set; }
            public string Description { get; set; }
            public string Width { get; set; }
            public string Hub { get; set; }
            public string RotationDirection { get; set; }
        }

        public List<ImpellerClass> ImperllerList(int fanType, int fanSize )
        {
            return (from DataRow datarow in DtImpeller(fanType, fanSize).Rows select new ImpellerClass
                    {
                        ImpellerId = Convert.ToInt32(datarow["IDImpeller"]),
                        TypeFanId = Convert.ToInt32(datarow["IdFanType"]),
                        Description = datarow["Description"].ToString(),
                        Width = datarow["Wight"].ToString(),
                        Hub = datarow["Hub"].ToString(),
                        RotationDirection = datarow["RotationDirection"].ToString() == "False" ? "Левое" : "Правое"
                    }).ToList();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            AddFanRow();
            Close();
            DgMotor.ItemsSource = _dl.MotorDataGridList();
        }

        //public void addFanRow(string idFan)
        public void AddFanRow()
        {
            try
            {

            var itemIdMoto = (DataLoad.DtStringsMotor)DgMotor.SelectedItem;
            var idMotor = itemIdMoto.IdMotorTechData;

            var itemIdImpeller = (ImpellerClass)DgImpeller.SelectedItem;
            var idImpeller = itemIdImpeller.ImpellerId;

            //var xxtAirFlow = TxtAirFlow.Text;
            //var xxtPrm = TxtPrm.Text;
            //var txtPowerInput = TxtPowerInput.Text;
            //var xxtCurrntMax = TxtCurrntMax.Text;
            //var txtSoundPressureLevel = TxtSoundPressureLevel.Text;

            var con = new SqlConnection(_dl.ConString);
            con.Open();

            var spcmd = new SqlCommand("[MotorFan].[AddOrEditFan]", con) { CommandType = CommandType.StoredProcedure };

  
            spcmd.Parameters.AddWithValue("@IDFan", DBNull.Value);
            spcmd.Parameters.AddWithValue("@IDMotorTechData", Convert.ToInt32(idMotor));
            spcmd.Parameters.AddWithValue("@IDImpeller", Convert.ToInt32(idImpeller));


            spcmd.ExecuteNonQuery();

            con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}