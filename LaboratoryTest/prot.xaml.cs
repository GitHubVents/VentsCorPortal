using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Office.Interop.Excel;
using MenuItem = System.Windows.Controls.MenuItem;
using Window = System.Windows.Window;

namespace LaboratoryTest
{
    /// <summary>
    /// Interaction logic for prot.xaml
    /// </summary>
    public partial class prot : Window
    {
        public prot()
        {
            InitializeComponent();
        }

        readonly DataClass _dc = new DataClass();

        public List<KeyValuePair<double, int>> XYvalue()
        {
            var XYvalueSti = new List<KeyValuePair<double, int>>();

            var items = DgParam.ItemsSource as List<DataClass.ClassListParam>;

            foreach (DataClass.ClassListParam t in items)
            {
                var ps = t.Ps;
                var qM3 = t.Q;

                XYvalueSti.Add(new KeyValuePair<double, int>(Convert.ToDouble(qM3), Convert.ToInt32(ps)));

            //    var valueList = new List<KeyValuePair<double, double>>()
            //    {
            //        new KeyValuePair<double, double>(Convert.ToDouble(ps), Convert.ToDouble(qM3))
            //    };
            }

            return XYvalueSti;
        }

        private void ShowColumnChart()
        {
            try
            {
         
                //var valueList = new List<KeyValuePair<double, double>>()
                //{
                //    new KeyValuePair<double, double>(1, 0),
                //    new KeyValuePair<double, double>(2, 1),
                //    new KeyValuePair<double, double>(10, 5),
                //    new KeyValuePair<double, double>(27, 20),
                //    new KeyValuePair<double, double>(40, 29),
                //};

                LineChart.Series.Clear();
                LineChart.Axes.Clear();

                //LineChart.Title = "test1";

                var ca = new LinearAxis
                {
                    Orientation = AxisOrientation.Y,
                    Minimum = 0,
                    Maximum = 160
                };

                LineChart.Axes.Add(ca);

                var cser = new LineSeries()
                {
                    DependentValueBinding = new Binding("Value"),
                    IndependentValueBinding = new Binding("Key"),
                    //ItemsSource = valueList,
                    ItemsSource = XYvalue(),
                    AnimationSequence = AnimationSequence.FirstToLast,
                    //Title = "das"
                };

                LineChart.Series.Add(cser);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void DgParam_Loaded(object sender, RoutedEventArgs e)
        {
            //LoadData();
            GenerateContextMenu();
        }

        #region " Context menu "
        public void GenerateContextMenu()
        {
            try
            {
                DgParam.ContextMenu = BuildContextMenu();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private MenuItem _miaDeleteRow;

        private ContextMenu BuildContextMenu()
        {
            var theMenu = new ContextMenu();

            _miaDeleteRow = new MenuItem { Header = "Удалить строку" };
            _miaDeleteRow.Click += DeleteRow_Click;

            theMenu.Items.Add(_miaDeleteRow);

            return theMenu;
        }

        private void DeleteRow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var con = new SqlConnection(_dc.ConString);
                con.Open();

                var item = (DataClass.ClassListParam)DgParam.SelectedItem;
                var deleteIdParam = item.IdParam;

                var q = @"DELETE [Lab].[Param] WHERE IdParam = " + deleteIdParam;

                var cmd = new SqlCommand(q, con);
                cmd.ExecuteNonQuery();

                con.Close();

                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        #endregion

        public void LoadData()
        {
            try
            {
                var number = NewProtocolForm.NumberNewProt;
                foreach (DataRow r in _dc.DtListParamTable(number).Rows)
                {
                    var date = (DateTime)r["LabOrderDate"];
                    var datestring = Convert.ToString(date.ToString("dd.MM.yy"));
                    LblNewProtocolNo.Content = r["LabOrderNumber"] + " от " + datestring;
                }


                DgParam.ItemsSource = _dc.ListParam(number);
                //DgMotorParam
                DgMotorParam.ItemsSource = _dc.ListMotorParam(number);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void AddProtToSql()
        {
            try
            {
                var number = NewProtocolForm.NumberNewProt;
                var con = new SqlConnection(_dc.ConString);
                con.Open();

                var spcmd = new SqlCommand("[Lab].[AddEditParam]", con) { CommandType = CommandType.StoredProcedure };

                var items = (DataClass.ClassListParam)DgParam.SelectedItem;

                var idParamStr = items.IdParam;

                var p = items.P;
                var i = items.I;
                var cos = items.Cos;
                var n = items.N;
                var ps = items.Ps;
                var q = items.Q;

                if (!string.IsNullOrEmpty(idParamStr))
                {spcmd.Parameters.AddWithValue("@IdParam", Convert.ToInt32(idParamStr));
                }
                else
                {spcmd.Parameters.AddWithValue("@IdParam", DBNull.Value);
                }

                spcmd.Parameters.AddWithValue("@LabOrderNumber", Convert.ToInt32(number));
                spcmd.Parameters.AddWithValue("@P", p == 0 ? 0 : Convert.ToDecimal(p));
                spcmd.Parameters.AddWithValue("@i", i == 0 ? 0 : Convert.ToDecimal(i));
                spcmd.Parameters.AddWithValue("@cos", cos == 0 ? 0 : Convert.ToDecimal(cos));
                spcmd.Parameters.AddWithValue("@n", n == 0 ? 0 : Convert.ToInt32(n));
                spcmd.Parameters.AddWithValue("@ps", ps == 0 ? 0 : Convert.ToInt32(ps));
                spcmd.Parameters.AddWithValue("@q", q == 0 ? 0 : Convert.ToDecimal(q));

                spcmd.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0}", ex.Message));
            }
        }

        private void DgParam_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex(@"[^0-9\.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void DgParam_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            AddProtToSql();

            DgParam.ItemsSource = null;

            var number = NewProtocolForm.NumberNewProt;
            DgParam.ItemsSource = _dc.ListParam(number);
            ShowColumnChart();
        }

        public void ExportToExcel()
        {
            var xlApp = new Microsoft.Office.Interop.Excel.Application();
            var objWorkBook = xlApp.Workbooks.Add();
            var objWorkSheet = (Worksheet)objWorkBook.Worksheets.Item[1];

            var items = DgParam.ItemsSource as List<DataClass.ClassListParam>;

            string cwd = System.IO.Directory.GetCurrentDirectory();
            //Console.WriteLine("cwd is '{0}'", cwd);

            objWorkBook = xlApp.Workbooks.Open(@"\\srvkb\SolidWorks Admin\Bat\TemplateNewOrder.xlsx");
            objWorkSheet = objWorkBook.Worksheets["Лист1"];

            //for (var j = 0; j <= DgParam.Items.Count - 1; j++)
            for (var j = 0; j < items.Count; j++)
            {
                var p = items[j].P;
                var i = items[j].I;
                var cos = items[j].Cos;
                var n = items[j].N;
                var ps = items[j].Ps;
                //var psinWg = items[j].PsinWg;

                var qLs = items[j].QLs;

                var qM3 = items[j].Q;

                var wis = items[j].Wls;

                objWorkSheet.Cells[j + 9, 1] = p;
                objWorkSheet.Cells[j + 9, 2] = i;
                objWorkSheet.Cells[j + 9, 3] = cos;
                objWorkSheet.Cells[j + 9, 4] = n;
                objWorkSheet.Cells[j + 9, 5] = ps;
                objWorkSheet.Cells[j + 9, 6] = qLs;
                objWorkSheet.Cells[j + 9, 7] = qM3;
                objWorkSheet.Cells[j + 9, 8] = wis;

            }
            var number = NewProtocolForm.NumberNewProt;
            objWorkBook.SaveAs(@"C:\Протокол № " + number + ".xlsx");
            objWorkBook.Close();

            xlApp = null;

        }

        #region " DataGrid Motor Param "
     
        private void DgMotorParam_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            try
            {
                AddMotorParamToSql();
                //DgMotorParam.ItemsSource = null;
                //var number = NewProtocolForm.NumberNewProt;
                //DgMotorParam.ItemsSource = _dc.ListMotorParam(number);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void AddMotorParamToSql()
        {
            //var con = new SqlConnection(_dc.ConString);
            //con.Open();

            //var spcmd = new SqlCommand("[Lab].[AddEditMotorParam]", con) { CommandType = CommandType.StoredProcedure };

            var items = (DataClass.ClassListMotorParam)DgMotorParam.SelectedItem;

            //var idMotorParam = items.IdMotorParam;
            var r0 = items.R0;
            //var rt = items.Rt;
            //var temp = items.Temp;

            MessageBox.Show(r0.ToString());

            //spcmd.Parameters.AddWithValue("@IdMotorParam", Convert.ToInt32(idMotorParam));

            //spcmd.Parameters.AddWithValue("@R0", r0 == 0 ? 0 : Convert.ToDouble(r0));
            //spcmd.Parameters.AddWithValue("@Rt", rt == 0 ? 0 : Convert.ToInt32(rt));
            //spcmd.Parameters.AddWithValue("@Temp", temp == 0 ? 0 : Convert.ToDouble(temp));

            //spcmd.ExecuteNonQuery();

            //con.Close();
        }

        private void DgMotorParam_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex(@"[^0-9\.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        #endregion

        #region " Menu "
        private void MenuSaveToExcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ExportToExcel();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private NewProtocolForm _newProForm;
        private void NewProtocol_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _newProForm = new NewProtocolForm() { ProtForm = this };
                _newProForm.ShowDialog();
                ShowColumnChart();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

    }
}