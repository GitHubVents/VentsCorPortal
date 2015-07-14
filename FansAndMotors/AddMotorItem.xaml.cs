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

namespace FansAndMotors
{
    /// <summary>
    /// Interaction logic for AddMotorItem.xaml
    /// </summary>
    public partial class AddMotorItem : Window
    {
        public AddMotorItem()
        {
            InitializeComponent();
        }

        readonly DataLoad _dl = new DataLoad();

        private void GridHome_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                BindComboBox();

                CboMotorType.LayoutUpdated += TxtBlockGenerateName_Loaded;
                CboPole.LayoutUpdated += TxtBlockGenerateName_Loaded;
                CboVolt.LayoutUpdated += TxtBlockGenerateName_Loaded;
                CboMotorParam.LayoutUpdated += TxtBlockGenerateName_Loaded;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region GENERATE NAME
        private void StringName(string typeName, string pole, string volt, string param)
        {
            LblGenerateName.Content = typeName + pole + volt + "-" + param;
        }
        #endregion

        public void BindComboBox()
        {
            CboMotorType.SelectedValuePath = "Code";
            CboMotorType.DisplayMemberPath = "TypeName";
            CboMotorType.ItemsSource = _dl.MotorType().DefaultView;

            CboPole.SelectedValuePath = "Code";
            CboPole.DisplayMemberPath = "Code";
            CboPole.ItemsSource = _dl.Pole().DefaultView;

            CboVolt.SelectedValuePath = "Code";
            CboVolt.DisplayMemberPath = "Volt";
            CboVolt.ItemsSource = _dl.Volt().DefaultView;


            CboMotorParam.SelectedValuePath = "Param";
            CboMotorParam.DisplayMemberPath = "Param";
            CboMotorParam.ItemsSource = _dl.MotorParams().DefaultView;

        }

        private void TxtBlockGenerateName_Loaded(object sender, EventArgs e)
        {
            var typeName = Convert.ToString(CboMotorType.SelectedValue);
            var pole = Convert.ToString(CboPole.SelectedValue);
            var volt = Convert.ToString(CboVolt.SelectedValue);
            var param = Convert.ToString(CboMotorParam.SelectedValue);

            StringName(typeName, pole, volt, param);
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddMotor();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void AddMotor()
        {
            var con = new SqlConnection(_dl.ConString);
            con.Open();

            var spcmd = new SqlCommand("[MotorFan].[AddOrEditMotor]", con) { CommandType = CommandType.StoredProcedure };

            spcmd.Parameters.AddWithValue("@IDMotor", DBNull.Value);
         
            spcmd.Parameters.AddWithValue("@IDMotorTechData", DBNull.Value);
       
            //spcmd.Parameters.AddWithValue("@MotorName", motorNameItem.MotorName);

            //spcmd.Parameters.AddWithValue("@Volt", Convert.ToInt32(motorNameItem.Volt));

            //spcmd.Parameters.AddWithValue("@Frequency", Convert.ToInt32(motorNameItem.Frequency));

            //if (string.IsNullOrEmpty(motorNameItem.Rpm)) { spcmd.Parameters.AddWithValue("@RPM", DBNull.Value); }
            //else { spcmd.Parameters.AddWithValue("@RPM", Convert.ToInt32(motorNameItem.Rpm)); }

            //if (string.IsNullOrEmpty(motorNameItem.InsulationClass)) { spcmd.Parameters.AddWithValue("@InsulationClass", DBNull.Value); }
            //else { spcmd.Parameters.AddWithValue("@InsulationClass", motorNameItem.InsulationClass); }

            //if (string.IsNullOrEmpty(motorNameItem.Fuse)) { spcmd.Parameters.AddWithValue("@Fuse", DBNull.Value); }
            //else { spcmd.Parameters.AddWithValue("@Fuse", motorNameItem.Fuse); }

            ////ColRaterTorque
            //if (string.IsNullOrEmpty(motorNameItem.RaterTorque)) { spcmd.Parameters.AddWithValue("@RaterTorque", DBNull.Value); }
            //else { spcmd.Parameters.AddWithValue("@RaterTorque", motorNameItem.RaterTorque); }

            //if (string.IsNullOrEmpty(motorNameItem.Power)) { spcmd.Parameters.AddWithValue("@Power", DBNull.Value); }
            //else { spcmd.Parameters.AddWithValue("@Power", Convert.ToDecimal(motorNameItem.Power.Replace(".", ","))); }

            //if (string.IsNullOrEmpty(motorNameItem.PeakCurrent)) { spcmd.Parameters.AddWithValue("@PeakCurrent", DBNull.Value); }
            //else { spcmd.Parameters.AddWithValue("@PeakCurrent", Convert.ToDecimal(motorNameItem.PeakCurrent.Replace(".", ","))); }

            //if (string.IsNullOrEmpty(motorNameItem.ElecConn)) { spcmd.Parameters.AddWithValue("@ElecConn", DBNull.Value); }
            //else { spcmd.Parameters.AddWithValue("@ElecConn", motorNameItem.ElecConn); }

            //if (string.IsNullOrEmpty(motorNameItem.DiamRotor)) { spcmd.Parameters.AddWithValue("@DiamRotor", DBNull.Value); }
            //else { spcmd.Parameters.AddWithValue("@DiamRotor", Convert.ToInt32(motorNameItem.DiamRotor)); }

            //if (string.IsNullOrEmpty(motorNameItem.Capacitor)) { spcmd.Parameters.AddWithValue("@Capacitor", DBNull.Value); }
            //else { spcmd.Parameters.AddWithValue("@Capacitor", Convert.ToDecimal(motorNameItem.Capacitor)); }

            //if (string.IsNullOrEmpty(motorNameItem.Stack)) { spcmd.Parameters.AddWithValue("@Stack", DBNull.Value); }
            //else { spcmd.Parameters.AddWithValue("@Stack", Convert.ToInt32(motorNameItem.Stack)); }

            //if (string.IsNullOrEmpty(motorNameItem.NumberOfSlot)) { spcmd.Parameters.AddWithValue("@NumberOfSlot", DBNull.Value); }
            //else { spcmd.Parameters.AddWithValue("@NumberOfSlot", Convert.ToInt32(motorNameItem.NumberOfSlot)); }

            //if (string.IsNullOrEmpty(motorNameItem.NumberPole)) { spcmd.Parameters.AddWithValue("@NumberPole", DBNull.Value); }
            //else { spcmd.Parameters.AddWithValue("@NumberPole", Convert.ToInt32(motorNameItem.NumberPole)); }

            //// DimA
            //var dimA = motorNameItem.WindingCoilA;
            //var windingDimA = dimA.Substring(1, dimA.LastIndexOf('x') - 1);
            //var i = dimA.IndexOf('x');
            //var windingCoilA = dimA.Substring(i).Replace("x", "");

            //// DimB
            //var dimB = motorNameItem.WindingCoilB;
            //var windingDimB = dimB.Substring(1, dimB.LastIndexOf('x') - 1);
            //var j = dimB.IndexOf('x');
            //var windingCoilB = dimB.Substring(j).Replace("x", "");

            //if (string.IsNullOrEmpty(windingDimA)) { spcmd.Parameters.AddWithValue("@WindingDimA", DBNull.Value); }
            //else { spcmd.Parameters.AddWithValue("@WindingDimA", Convert.ToDecimal(windingDimA.Replace(".", ","))); }

            //if (string.IsNullOrEmpty(windingDimB)) { spcmd.Parameters.AddWithValue("@WindingDimB", DBNull.Value); }
            //else { spcmd.Parameters.AddWithValue("@WindingDimB", Convert.ToDecimal(windingDimB.Replace(".", ","))); }

            //if (string.IsNullOrEmpty(windingCoilA)) { spcmd.Parameters.AddWithValue("@WindingCoilA", DBNull.Value); }
            //else { spcmd.Parameters.AddWithValue("@WindingCoilA", Convert.ToInt32(windingCoilA)); }

            //if (string.IsNullOrEmpty(windingCoilB)) { spcmd.Parameters.AddWithValue("@WindingCoilB", DBNull.Value); }
            //else { spcmd.Parameters.AddWithValue("@WindingCoilB", Convert.ToInt32(windingCoilB)); }

            //if (string.IsNullOrEmpty(motorNameItem.WeightA)) { spcmd.Parameters.AddWithValue("@WeightA", DBNull.Value); }
            //else { spcmd.Parameters.AddWithValue("@WeightA", Convert.ToInt32(motorNameItem.WeightA)); }
            //if (string.IsNullOrEmpty(motorNameItem.WeightB)) { spcmd.Parameters.AddWithValue("@WeightB", DBNull.Value); }
            //else { spcmd.Parameters.AddWithValue("@WeightB", Convert.ToInt32(motorNameItem.WeightB)); }

            //if (string.IsNullOrEmpty(motorNameItem.ResistanceA)) { spcmd.Parameters.AddWithValue("@ResistanceA", DBNull.Value); }
            //else { spcmd.Parameters.AddWithValue("@ResistanceA", Convert.ToDecimal(motorNameItem.ResistanceA.Replace(".", ","))); }

            //if (string.IsNullOrEmpty(motorNameItem.ResistanceB)) { spcmd.Parameters.AddWithValue("@ResistanceB", DBNull.Value); }
            //else { spcmd.Parameters.AddWithValue("@ResistanceB", Convert.ToDecimal(motorNameItem.ResistanceB.Replace(".", ","))); }

            spcmd.ExecuteNonQuery();

            con.Close();
        }

    }
}
