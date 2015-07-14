using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace LaboratoryTest
{
    /// <summary>
    /// Interaction logic for NewProtocolForm.xaml
    /// </summary>
    public partial class NewProtocolForm : Window
    {




        public NewProtocolForm()
        {
            InitializeComponent();
        }

        readonly DataClass _dc = new DataClass();
        public prot ProtForm { get; set; }
        static public int NumberNewProt { get; set; }

        private void GridNewProtocol_Loaded(object sender, RoutedEventArgs e)
        {
            DateP.SelectedDate = (DateTime.Today);

            LoadCboImpeller();
            LoadCboVolt();
        }

        private void BtnCreatNewProtocol_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                NewProtocolo();



                //ProtForm = new prot();

                ProtForm.LoadData();



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public void NewProtocolo()
        {

            NumberNewProt = Convert.ToInt32(TxtNewProtocolNo.Text);

            if (DateP.SelectedDate == null) return;

            var date = (DateTime)DateP.SelectedDate;
            var strTempData = Convert.ToString(date.ToString("dd.MM.yy"));

            var con = new SqlConnection(_dc.ConString);
            con.Open();

            var spcmd = new SqlCommand("[Lab].[AddLabOrder]", con) { CommandType = CommandType.StoredProcedure };

            spcmd.Parameters.AddWithValue("@Number", Convert.ToInt32(NumberNewProt));
            spcmd.Parameters.AddWithValue("@Date", Convert.ToDateTime(strTempData));

            spcmd.ExecuteNonQuery();

            con.Close();

            Close();
        }

        public void LoadCboImpeller()
        {
            try
            {

                var con = new SqlConnection(_dc.ConString);
                var cmd = new SqlCommand
                {
                    Connection = con,
                    CommandType = CommandType.Text,
                    CommandText = @"SELECT IdFanType, TypeName FROM [MotorFan].[FanType] Order By TypeName"
                };

                var dt = new DataTable();
                var da = new SqlDataAdapter { SelectCommand = cmd };

                con.Open();
                da.Fill(dt);

                CboNewImpeller.SelectedValuePath = "IdFanType";
                CboNewImpeller.DisplayMemberPath = "TypeName";
                CboNewImpeller.ItemsSource = dt.DefaultView;

                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void LoadCboVolt()
        {
            try
            {
                var dt = new DataTable();

                const string queryString = @"SELECT IDSupply, CAST(Volt AS Nvarchar(5)) + '/' + CAST(Frequency AS Nvarchar(5)) AS Volt FROM MotorFan.Supply"; ;

                var con = new SqlConnection(_dc.ConString);

                var da = new SqlDataAdapter(queryString, con);
                con.Open();

                da.Fill(dt);

                CboNewVolt.SelectedValuePath = "IdSupply";
                CboNewVolt.DisplayMemberPath = "Volt";
                CboNewVolt.ItemsSource = dt.DefaultView;

                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
