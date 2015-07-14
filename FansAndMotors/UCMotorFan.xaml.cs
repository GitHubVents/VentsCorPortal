using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using EdmLib;
using LaboratoryTest;

namespace FansAndMotors
{
    /// <summary>
    /// Interaction logic for UCMotorFan.xaml
    /// </summary>
    public partial class UcMotorFan
    {
        public UcMotorFan()
        {
            InitializeComponent();
        }

        readonly DataLoad _dl = new DataLoad();
        public Order FrmOrder;
        
        public bool TypeRbMotor { get; set; }
        public bool TypeRbFan { get; set; }
        public string IdPdm { get; set; }
        public string FileNameFromPdm { get; set; }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //TypeRbFan = true;
                TypeRbMotor = true;

                RbMotor.IsChecked = TypeRbMotor;
                //RbFan.IsChecked = TypeRbFan;

                GenerateMotorFanDataGrid();
                GenerateContextMenu();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void GenerateMotorFanDataGrid()
        {
            DgCatalog.ItemsSource = null;
            DgCatalog.Columns.Clear();

            var col1 = new DataGridTemplateColumn();
            var factory1 = new FrameworkElementFactory(typeof(Image));
            var b1 = new Binding("ImageFilePath") { Mode = BindingMode.TwoWay };
            factory1.SetValue(Image.SourceProperty, b1);
            var cellTemplate1 = new DataTemplate { VisualTree = factory1 };
            col1.Width = 20;
            col1.CellTemplate = cellTemplate1;

            DgCatalog.Columns.Add(col1);

            if (TypeRbMotor) // type  MotorDataGridList
            {
                var motorList = _dl.ListColumn().Where(x => x.ColSetId == 1).ToList();

                foreach (var columnName in motorList)
                {

                    var column = new DataGridTextColumn
                    {
                        Header = columnName.ColDescription,
                        Binding = new Binding(columnName.Binding),
                        Width = columnName.DefaultWidth
                    };

                    DgCatalog.Columns.Add(column);
                }

                DgCatalog.ItemsSource = _dl.MotorDataGridList();
            }


            if (TypeRbFan) // type  FanDataGridList
            {
                var motorList = _dl.ListColumn().Where(x => x.ColSetId == 2).ToList();

                foreach (var columnName in motorList)
                {

                    var column = new DataGridTextColumn
                    {
                        Header = columnName.ColDescription,
                        Binding = new Binding(columnName.Binding),
                        Width = columnName.DefaultWidth

                    };

                    DgCatalog.Columns.Add(column);
                }

                DgCatalog.ItemsSource = _dl.FanDataGridList();
            }
        }

        public void GenerateContextMenu()
        {
            try
            {
                DgCatalog.ContextMenu = BuildContextMenu();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region " Context menu "

        private MenuItem _miaAddMotorRow;
        private MenuItem _miaDeleteMotorRow;
        private MenuItem _miaLinkToPdm;
        private MenuItem _miaDeleteWithPdm;
        private MenuItem _miaGoToFile;
        private MenuItem _miaNewRequest;

        private ContextMenu BuildContextMenu()
        {
            var theMenu = new ContextMenu();

            _miaAddMotorRow = new MenuItem { Header = "Добавить позицию" };
            _miaAddMotorRow.Click += AddMotorRowMenu_Click;

            _miaDeleteMotorRow = new MenuItem { Header = "Удалить позицию" };
            _miaDeleteMotorRow.Click += DeleteMotorOrFanRow_Click;

            _miaLinkToPdm = new MenuItem { Header = "Связать с моделью..." };
            _miaLinkToPdm.Click += LinkToPdm_Click;

            _miaDeleteWithPdm = new MenuItem { Header = "Удалить связь с PDM" };
            _miaDeleteWithPdm.Click += DeleteWithPdm_Click;

            _miaGoToFile = new MenuItem { Header = "Перейти к файлу..." };
            _miaGoToFile.Click += OpenPdmFolder_Click;

            _miaNewRequest = new MenuItem { Header = "Оформить заявку..." };
            _miaNewRequest.Click += NewRequest_Click;

            if (CheckActionCode.UserCanEditCatalogueFanAndMotors())
            {
                DgCatalog.IsReadOnly = false;

                theMenu.Items.Add(_miaAddMotorRow);
                theMenu.Items.Add(_miaDeleteMotorRow);
            }


            if (CheckActionCode.UserCanAddLinkToPdm())
            {

                theMenu.Items.Add(_miaLinkToPdm);
                theMenu.Items.Add(_miaDeleteWithPdm);
            }

            theMenu.Items.Add(_miaGoToFile);
            theMenu.Items.Add(_miaNewRequest);

            return theMenu;
        }

        // Context menu add new motor row
        private void AddMotorRowMenu_Click(object sender, RoutedEventArgs e)
        {
            if (TypeRbMotor)
            {
                var addMotorItem = new AddMotorItem();
                addMotorItem.ShowDialog();
                //DgCatalog.ItemsSource = new List<DataLoad.DtStringsMotor> { new DataLoad.DtStringsMotor { MotorName = "" } };
            }

            if (!TypeRbFan) return;
            var addFanItem = new AddFanItem();

            addFanItem.ShowDialog();
        }

        // Context menu delete Motor 
        private void DeleteMotorOrFanRow_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Будет удалена запись, продолжить?", "Удаление записи.", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {

            }
            else
            {

                var con = new SqlConnection(_dl.ConString);
                con.Open();


                var id = 0;

                if (TypeRbMotor)
                {
                    var idMotorOrFan = (DataLoad.DtStringsMotor)DgCatalog.SelectedItem;
                    id = Convert.ToInt32(idMotorOrFan.IdNomenclature);
                }

                if (TypeRbFan)
                {
                    var idMotorOrFan = (DataLoad.DtStringsFan)DgCatalog.SelectedItem;
                    id = Convert.ToInt32(idMotorOrFan.IdNomenclature);
                }

                var q = @"DELETE Nomenclature WHERE IdNomenclature = " + id;

                var cmd = new SqlCommand(q, con);
                cmd.ExecuteNonQuery();

                con.Close();

                DgCatalog.ItemsSource = _dl.MotorDataGridList();

            }
        }

        // Context menu open pdm folder
        private void OpenPdmFolder_Click(object sender, RoutedEventArgs e)
        {
           

            if (TypeRbMotor)
            {
                var selectCell = (DataLoad.DtStringsMotor)DgCatalog.SelectedItem;

                if (selectCell.IdPdm != "")
                {
                    _dl.LoadVault();

                    if (_dl.Vault2 != null)
                    {
                        _dl.Vault2.OpenContainingFolder(Convert.ToInt32(selectCell.IdPdm), 0);
                    }
                    else
                    {
                        MessageBox.Show("Локальный вид не найден!");
                    }
                }
            }

            if (TypeRbFan)
            {
                var selectCell = (DataLoad.DtStringsFan)DgCatalog.SelectedItem;

                if (selectCell.IdPdm != "")
                {
                    _dl.LoadVault();

                    if (_dl.Vault2 != null)
                    {
                        _dl.Vault2.OpenContainingFolder(Convert.ToInt32(selectCell.IdPdm), 0);
                    }
                    else
                    {
                        MessageBox.Show("Локальный вид не найден!");
                    }
                }
            }   
        }

        // Context menu GoToFilePdm
        private void LinkToPdm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //var item = DataGridSql.SelectedItem;
                //var textBlock = DataGridSql.SelectedCells[1].Column.GetCellContent(item) as TextBlock;

                var id = 0;

                if (TypeRbMotor)
                {
                    var idMotorOrFan = (DataLoad.DtStringsMotor)DgCatalog.SelectedItem;

                    id = Convert.ToInt32(idMotorOrFan.IdNomenclature);
                }

                if (TypeRbFan)
                {
                    var idMotorOrFan = (DataLoad.DtStringsFan)DgCatalog.SelectedItem;

                    id = Convert.ToInt32(idMotorOrFan.IdNomenclature);
                }

                _dl.LoadVault();

                var dlg = new Microsoft.Win32.OpenFileDialog();

                var rootFolder = _dl.Vault1.RootFolderPath;
                var folderPath = _dl.Vault1.GetFolderFromPath(rootFolder + @"\Проекты\Blauberg Motors\");

                dlg.Filter = "SolidWorks Assembly |*.SLDASM";

                dlg.InitialDirectory = folderPath.LocalPath;

                var result = dlg.ShowDialog();

                if (result == true)
                {
                    var filename = dlg.FileName;

                    ShowReferences(filename);

                    var con = new SqlConnection(_dl.ConString);

                    con.Open();

                    var q = @"UPDATE Nomenclature
                                    SET IDPDM = ' " + IdPdm + @" '
                                    WHERE IDNomenclature = '" + id + @"'";

                    var sqlCmd = new SqlCommand(q, con);

                    sqlCmd.ExecuteNonQuery();

                    con.Close();

                    DgCatalog.ItemsSource = _dl.MotorDataGridList();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // Context menu Delete from Pdm
        private void DeleteWithPdm_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Будет удалена связь с моделью из SW-EPDM, продолжить?", "Удалить связь с моделью.", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
            }
            else
            {
                var con = new SqlConnection(_dl.ConString);
                con.Open();


                var id = 0;

                if (TypeRbMotor)
                {
                    var idMotorOrFan = (DataLoad.DtStringsMotor)DgCatalog.SelectedItem;
                    id = Convert.ToInt32(idMotorOrFan.IdNomenclature);
                }

                if (TypeRbFan)
                {
                    var idMotorOrFan = (DataLoad.DtStringsFan)DgCatalog.SelectedItem;
                    id = Convert.ToInt32(idMotorOrFan.IdNomenclature);
                }

                
                var q = @" UPDATE Nomenclature SET IDPdm = NULL WHERE IdNomenclature = " + id;

                var cmd = new SqlCommand(q, con);
                cmd.ExecuteNonQuery();

                con.Close();

                DgCatalog.ItemsSource = _dl.MotorDataGridList();

            }
        }

        // Context menu new order to lab
        //TODO: DataTable
        private class DgCatalogRow
        {
            public string FanName { get; set; }
            public int Volt { get; set; }
            public int Frequency { get; set; }
            public string MotorName { get; set; }
            public string Impeller { get; set; }
            public string IdTData { get; set; }
        }
        public DataTable DtMotorFanLab()
        {
            var dt = new DataTable();

            try
            { 
                dt.Columns.Add("FanName");
                dt.Columns.Add("Volt");
                dt.Columns.Add("Frequency");
                dt.Columns.Add("MotorName");
                dt.Columns.Add("Impeller");
                dt.Columns.Add("IdTData");

                var dtSelectItems = DgCatalog.SelectedItems.Cast<DataLoad.DtStringsFan>().Select(fanName => new DgCatalogRow
                {
                    FanName = fanName.Nomenclature,
                    Volt = Convert.ToInt32(fanName.Volt),
                    Frequency = Convert.ToInt32(fanName.Frequency),
                    MotorName = fanName.MotorName,
                    Impeller = fanName.DescriptionImpeller,
                    IdTData = fanName.IdTData
                });

                foreach (var r in dtSelectItems)
                {
                    dt.Rows.Add(r.FanName, r.Volt, r.Frequency, r.MotorName, r.Impeller, r.IdTData);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            return dt;

        }

        private void NewRequest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FrmOrder = new Order { DtMotorFan = DtMotorFanLab() };
                FrmOrder.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        #endregion

        // Get var from PDM file and add to SQL
        public void GetVariables(string fileName, int idMotor)
        {
            try
            {
                _dl.Folder = default(IEdmFolder5);

                IEdmFile5 edmFile = _dl.Vault1.GetFileFromPath(fileName, out _dl.Folder);
                var pEnumVar = (IEdmEnumeratorVariable8)edmFile.GetEnumeratorVariable();

                object oVarSlot; // "Number of slot"
                object oVarDiameter; // Diameter of the rotor
                object oVarStackSize; // "Stack size"
                object oVarInsulationClass; // "InsulationClass"
                object oVarFuse; // "Fuse"
                object oVarCapacitor; // "Capacitor"

                var ok1 = pEnumVar.GetVar("Number of slot", "00", out oVarSlot);
                var ok2 = pEnumVar.GetVar("Diameter of the rotor", "00", out oVarDiameter);
                var ok3 = pEnumVar.GetVar("Stack size", "00", out oVarStackSize);
                var ok4 = pEnumVar.GetVar("Capacitor", "00", out oVarCapacitor);
                var ok5 = pEnumVar.GetVar("InsulationClass", "00", out oVarInsulationClass);
                var ok6 = pEnumVar.GetVar("Fuse", "00", out oVarFuse);

                var con = new SqlConnection(_dl.ConString);
                con.Open();

                var spcmd = new SqlCommand("[MotorFan].[UpdateDataFromPdm]", con) { CommandType = CommandType.StoredProcedure };

                spcmd.Parameters.AddWithValue("@IDMotor", idMotor);

                if (!ok1) { spcmd.Parameters.AddWithValue("@NumberOfSlot", DBNull.Value); }
                else { spcmd.Parameters.AddWithValue("@NumberOfSlot", Convert.ToInt32(oVarSlot)); }

                if (!ok2) { spcmd.Parameters.AddWithValue("@DiamRotor", DBNull.Value); }
                else { spcmd.Parameters.AddWithValue("@DiamRotor", Convert.ToInt32(oVarDiameter)); }

                if (!ok3) { spcmd.Parameters.AddWithValue("@Stack", DBNull.Value); }
                else { spcmd.Parameters.AddWithValue("@Stack", Convert.ToInt32(oVarStackSize)); }

                if (!ok4) { spcmd.Parameters.AddWithValue("@Capacitor", DBNull.Value); }
                else { spcmd.Parameters.AddWithValue("@Capacitor", Convert.ToInt32(oVarCapacitor)); }

                spcmd.Parameters.AddWithValue("@InsulationClass", !ok5 ? DBNull.Value : oVarInsulationClass);

                spcmd.Parameters.AddWithValue("@Fuse", !ok6 ? DBNull.Value : oVarFuse);

                spcmd.ExecuteNonQuery();

                con.Close();

                DgCatalog.ItemsSource = _dl.MotorDataGridList();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Add new row to sql
        public void AddNewMotorRow(string idMotor)
        {
            try
            {
                var con = new SqlConnection(_dl.ConString);
                con.Open();

                var spcmd = new SqlCommand("[MotorFan].[AddOrEditMotor]", con) { CommandType = CommandType.StoredProcedure };
                var motorNameItem = (DataLoad.DtStringsMotor)DgCatalog.SelectedItem;

                // Edit row motor
                if (!string.IsNullOrEmpty(idMotor))
                {

                    spcmd.Parameters.AddWithValue("@IDMotor", Convert.ToInt32(idMotor));
                }
                else
                {
                    spcmd.Parameters.AddWithValue("@IDMotor", DBNull.Value);
                }

                // Edit row motor
                if (string.IsNullOrEmpty(motorNameItem.IdMotorTechData))
                {
                    spcmd.Parameters.AddWithValue("@IDMotorTechData", DBNull.Value);
                }
                else
                {
                    spcmd.Parameters.AddWithValue("@IDMotorTechData", Convert.ToInt32(motorNameItem.IdMotorTechData));
                }

                spcmd.Parameters.AddWithValue("@MotorName", motorNameItem.Nomenclature);
                spcmd.Parameters.AddWithValue("@Volt", Convert.ToInt32(motorNameItem.Volt));
                spcmd.Parameters.AddWithValue("@Frequency", Convert.ToInt32(motorNameItem.Frequency));

                if (string.IsNullOrEmpty(motorNameItem.Rpm)) { spcmd.Parameters.AddWithValue("@RPM", DBNull.Value); }
                else { spcmd.Parameters.AddWithValue("@RPM", Convert.ToInt32(motorNameItem.Rpm)); }

                if (string.IsNullOrEmpty(motorNameItem.InsulationClass)) { spcmd.Parameters.AddWithValue("@InsulationClass", DBNull.Value); }
                else { spcmd.Parameters.AddWithValue("@InsulationClass", motorNameItem.InsulationClass); }

                if (string.IsNullOrEmpty(motorNameItem.Fuse)) { spcmd.Parameters.AddWithValue("@Fuse", DBNull.Value); }
                else { spcmd.Parameters.AddWithValue("@Fuse", motorNameItem.Fuse); }

                //ColRaterTorque
                if (string.IsNullOrEmpty(motorNameItem.RaterTorque)) { spcmd.Parameters.AddWithValue("@RaterTorque", DBNull.Value); }
                else { spcmd.Parameters.AddWithValue("@RaterTorque", motorNameItem.RaterTorque); }

                if (string.IsNullOrEmpty(motorNameItem.Power)) { spcmd.Parameters.AddWithValue("@Power", DBNull.Value); }
                else { spcmd.Parameters.AddWithValue("@Power", Convert.ToDecimal(motorNameItem.Power.Replace(".", ","))); }

                if (string.IsNullOrEmpty(motorNameItem.PeakCurrent)) { spcmd.Parameters.AddWithValue("@PeakCurrent", DBNull.Value); }
                else { spcmd.Parameters.AddWithValue("@PeakCurrent", Convert.ToDecimal(motorNameItem.PeakCurrent.Replace(".", ","))); }

                if (string.IsNullOrEmpty(motorNameItem.ElecConn)) { spcmd.Parameters.AddWithValue("@ElecConn", DBNull.Value); }
                else { spcmd.Parameters.AddWithValue("@ElecConn", motorNameItem.ElecConn); }

                if (string.IsNullOrEmpty(motorNameItem.DiamRotor)) { spcmd.Parameters.AddWithValue("@DiamRotor", DBNull.Value); }
                else { spcmd.Parameters.AddWithValue("@DiamRotor", Convert.ToInt32(motorNameItem.DiamRotor)); }

                if (string.IsNullOrEmpty(motorNameItem.Capacitor)) { spcmd.Parameters.AddWithValue("@Capacitor", DBNull.Value); }
                else { spcmd.Parameters.AddWithValue("@Capacitor",Convert.ToDecimal( motorNameItem.Capacitor)); }

                if (string.IsNullOrEmpty(motorNameItem.Stack)) { spcmd.Parameters.AddWithValue("@Stack", DBNull.Value); }
                else { spcmd.Parameters.AddWithValue("@Stack", Convert.ToInt32(motorNameItem.Stack)); }

                if (string.IsNullOrEmpty(motorNameItem.NumberOfSlot)) { spcmd.Parameters.AddWithValue("@NumberOfSlot", DBNull.Value); }
                else { spcmd.Parameters.AddWithValue("@NumberOfSlot", Convert.ToInt32(motorNameItem.NumberOfSlot)); }

                if (string.IsNullOrEmpty(motorNameItem.NumberPole)) { spcmd.Parameters.AddWithValue("@NumberPole", DBNull.Value); }
                else { spcmd.Parameters.AddWithValue("@NumberPole", Convert.ToInt32(motorNameItem.NumberPole)); }

                // DimA
                var dimA = motorNameItem.WindingCoilA;
                var windingDimA = dimA.Substring(1, dimA.LastIndexOf('x') - 1);
                var i = dimA.IndexOf('x');
                var windingCoilA = dimA.Substring(i).Replace("x", "");

                // DimB
                var dimB = motorNameItem.WindingCoilB;
                var windingDimB = dimB.Substring(1, dimB.LastIndexOf('x') - 1);
                var j = dimB.IndexOf('x');
                var windingCoilB = dimB.Substring(j).Replace("x", "");

                if (string.IsNullOrEmpty(windingDimA)) { spcmd.Parameters.AddWithValue("@WindingDimA", DBNull.Value); }
                else { spcmd.Parameters.AddWithValue("@WindingDimA", Convert.ToDecimal(windingDimA.Replace(".", ","))); }

                if (string.IsNullOrEmpty(windingDimB)) { spcmd.Parameters.AddWithValue("@WindingDimB", DBNull.Value); }
                else { spcmd.Parameters.AddWithValue("@WindingDimB", Convert.ToDecimal(windingDimB.Replace(".", ","))); }

                if (string.IsNullOrEmpty(windingCoilA)) { spcmd.Parameters.AddWithValue("@WindingCoilA", DBNull.Value); }
                else { spcmd.Parameters.AddWithValue("@WindingCoilA", Convert.ToInt32(windingCoilA)); }

                if (string.IsNullOrEmpty(windingCoilB)) { spcmd.Parameters.AddWithValue("@WindingCoilB", DBNull.Value); }
                else { spcmd.Parameters.AddWithValue("@WindingCoilB", Convert.ToInt32(windingCoilB)); }

                if (string.IsNullOrEmpty(motorNameItem.WeightA)) { spcmd.Parameters.AddWithValue("@WeightA", DBNull.Value); }
                else { spcmd.Parameters.AddWithValue("@WeightA", Convert.ToInt32(motorNameItem.WeightA)); }
                if (string.IsNullOrEmpty(motorNameItem.WeightB)) { spcmd.Parameters.AddWithValue("@WeightB", DBNull.Value); }
                else { spcmd.Parameters.AddWithValue("@WeightB", Convert.ToInt32(motorNameItem.WeightB)); }

                if (string.IsNullOrEmpty(motorNameItem.ResistanceA)) { spcmd.Parameters.AddWithValue("@ResistanceA", DBNull.Value); }
                else { spcmd.Parameters.AddWithValue("@ResistanceA", Convert.ToDecimal(motorNameItem.ResistanceA.Replace(".", ","))); }

                if (string.IsNullOrEmpty(motorNameItem.ResistanceB)) { spcmd.Parameters.AddWithValue("@ResistanceB", DBNull.Value); }
                else { spcmd.Parameters.AddWithValue("@ResistanceB", Convert.ToDecimal(motorNameItem.ResistanceB.Replace(".", ","))); }

                spcmd.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Get references from PDM
        private void ShowReferences(string filePath)
        {
            _dl.Folder = default(IEdmFolder5);
            var file = _dl.Vault1.GetFileFromPath(filePath, out _dl.Folder);

            if (file == null)
            {
                MessageBox.Show("Файл не найден!");
            }
            else
            {

                var @ref = file.GetReferenceTree(_dl.Folder.ID);

                IdPdm = Convert.ToString(@ref.FileID);
                FileNameFromPdm = @ref.Name;

            }
        }

        private void RbMotor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TypeRbMotor =  true;
                TypeRbFan = false;
                GenerateMotorFanDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        
        }

        private void RbFan_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TypeRbFan = true;
                TypeRbMotor = false;
                GenerateMotorFanDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnSaveEdit_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Button save
            if (TypeRbMotor)
            {
                var item = (DataLoad.DtStringsMotor)DgCatalog.SelectedItem;
                var idMotor = item.IdNomenclature;

                AddNewMotorRow(idMotor);

                DgCatalog.ItemsSource = _dl.MotorDataGridList();
            }

            if (TypeRbFan)
            {
                //var item = (DataLoad.DtStringsFan)DgCatalog.SelectedItem;

                //var idFan = item.IdFan;

                //AddNewMotorRow(idMotor);

                DgCatalog.ItemsSource = _dl.FanDataGridList();
            }


            
        }
    }
}