using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Navigation;
using EdmLib;

namespace FansAndMotors
{
    public class DataLoad
    {
        public string ConString = @"Data Source=192.168.14.11;Initial Catalog=SWPlusDB;Persist Security Info=True;User ID=sa;Password=PDMadmin; Pooling=True";

        //public string ConString { get; set; }

        private string _q;
        private const string VaultName = "Vents-PDM";

        public DataTable DtStatus(int id)
        {
            var dt = new DataTable();

            var con = new SqlConnection(ConString);

            _q = @"SELECT P.Path AS 'Project Path', Replace (D.Filename, '.sldasm', '') AS 'File Name', S.Name AS 'Status Name'
                FROM [Vents-PDM].dbo.Projects AS P INNER JOIN
                     [Vents-PDM].dbo.DocumentsInProjects AS DP ON P.ProjectID = DP.ProjectID INNER JOIN
                     [Vents-PDM].dbo.Documents AS D ON DP.DocumentID = D.DocumentID INNER JOIN
                     [Vents-PDM].dbo.Status AS S ON D.CurrentStatusID = S.StatusID
                     WHERE (S.Name LIKE '%') AND (D.ObjectTypeID <> 0)
                     AND d.DocumentID = " + id;
            var sqlCmd = new SqlCommand(_q, con);
            var da = new SqlDataAdapter(sqlCmd);
            da.Fill(dt);
            return dt;
        }

        #region " MOTOR "
        private DataTable MotorDataTable()
        {
            var dt = new DataTable();

                var con = new SqlConnection(ConString);

                _q = @"SELECT * FROM dbo.GetMotors
                ORDER BY Nomenclature ";

                var sqlCmd = new SqlCommand(_q, con);
                var da = new SqlDataAdapter(sqlCmd);
                da.Fill(dt);
            return dt;
        }
        public class DtStringsMotor
        {
            public string IdNomenclature { get; set; }
            public string IdMotorTechData { get; set; }
            public string Nomenclature { get; set; }
            public string FilePathName { get; set; }
            public string Fuse { get; set; }
            public string FileNameFromPdm { get; set; }
            public string Specimen { get; set; }
            public string Phase { get; set; }
            public string Volt { get; set; }
            public string Frequency { get; set; }
            public string StartingTorgue { get; set; } // Пусковой момент
            public string MaxTorque { get; set; } // Номинальный момент
            public string Rpm { get; set; }
            public string Power { get; set; }
            public string PeakCurrent { get; set; }
            public string Temperature { get; set; }
            public string RotationDirection { get; set; }
            public string Capacitor { get; set; }
            public string ElecConn { get; set; }
            public string RaterTorque { get; set; }
            public string IdPdm { get; set; }
            public string NumberPole { get; set; }
            public string DiamRotor { get; set; }
            public string Stack { get; set; }
            public string NumberOfSlot { get; set; }
            public string InsulationClass { get; set; }
            public string Status { get; set; }
            public string ImageFilePath { get; set; }
            public string WindingCoilA { get; set; }
            public string WindingCoilB { get; set; }
            public string WeightA { get; set; }
            public string WeightB { get; set; }
            public string ResistanceA { get; set; }
            public string ResistanceB { get; set; }
            public string WindingDimA { get; set; }
            public string WindingDimB { get; set; }

    
        }
        public List<DtStringsMotor> MotorDataGridList()
        {
            return (from DataRow r in MotorDataTable().Rows select new DtStringsMotor
            {
                IdNomenclature = r["IDNomenclature"].ToString(),
                IdMotorTechData = r["IDMotorTechData"].ToString(),
                Fuse = r["Fuse"].ToString(),
                Phase = r["Phase"].ToString(),
                ImageFilePath = string.IsNullOrEmpty(r["IDPDM"].ToString()) ? "" : new Uri(@"Images\sldres1u_26.png", UriKind.RelativeOrAbsolute).ToString(),
                Nomenclature = r["Nomenclature"].ToString(),
                // Specimen = r["Specimen"].ToString(),
                FilePathName = string.IsNullOrEmpty(r["IDPDM"].ToString()) ? "" : DtStatus(Convert.ToInt32(r["IDPDM"].ToString())).Rows[0]["Project Path"].ToString(),
                FileNameFromPdm = string.IsNullOrEmpty(r["IDPDM"].ToString()) ? "" : DtStatus(Convert.ToInt32(r["IDPDM"].ToString())).Rows[0]["File Name"].ToString(),
                Power = r["Power"].ToString(),
                Volt = r["Volt"].ToString(),
                StartingTorgue = r["StartingTorgue"].ToString(),
                RaterTorque = r["RaterTorque"].ToString(),
                MaxTorque = r["MaxTorque"].ToString(),
                Temperature = "-" + r["PermArmTemperatureMin"] + "... +" + r["PermArmTemperatureMax"],
                Capacitor = r["Capacitor"].ToString(),
                RotationDirection = r["RotationDirection"].ToString(),
                ElecConn = r["ElecConn"].ToString(),
                Frequency = r["Frequency"].ToString(),
                PeakCurrent = r["PeakCurrent"].ToString(),
                Rpm = r["RPM"].ToString(),
                IdPdm = r["IDPDM"].ToString(),
                NumberPole = r["NumberPole"].ToString(),
                DiamRotor = r["DimRotor"].ToString(),
                Stack = r["Stack"].ToString(),
                NumberOfSlot = r["NumberOfSlot"].ToString(),
                InsulationClass = r["InsulationClass"].ToString(),
                Status = string.IsNullOrEmpty(r["IDPDM"].ToString()) ? "" : DtStatus(Convert.ToInt32(r["IDPDM"].ToString())).Rows[0]["Status Name"].ToString(),

                WindingCoilA = "ø" + r["WindingDimA"] + "x" + r["WindingCoilA"],
                WindingCoilB = "ø" + r["WindingDimB"] + "x" + r["WindingCoilB"],

                WeightA = r["WeightA"].ToString(),
                WeightB = r["WeightB"].ToString(),
                ResistanceA = r["ResistanceA"].ToString(),
                ResistanceB = r["ResistanceB"].ToString()

            }).ToList();
        }

        #endregion

        #region " FAN "

        public  DataTable FanDataTable()
        {
            var dt = new DataTable();

            var con = new SqlConnection(ConString);

            _q = @"SELECT * FROM GetFans";
          
            var sqlCmd = new SqlCommand(_q, con);

            var da = new SqlDataAdapter(sqlCmd);

            da.Fill(dt);

            return dt;
        }

        public class DtStringsFan
        {
            public string ImageFilePath { get; set; }
            //public string IDMotorTechData { get; set; }
            public string IdNomenclature { get; set; }
            public string Nomenclature { get; set; }
            public string MotorName { get; set; }
            public string Volt { get; set; }
            public string Frequency { get; set; }
            public string Phase { get; set; }
            public string FilePathName { get; set; }
            public string FileNameFromPdm { get; set; }
            //public string Specimen { get; set; }
            public string Rpm { get; set; }
            public string PowerInput { get; set; }
            //public string PeakCurrent { get; set; }
            public string Temperature { get; set; }

            public string TypeName { get; set; }
            public string DescriptionImpeller { get; set; }
            public string FanSize { get; set; }

            //public string ElecConn { get; set; }
            //public string RaterTorque { get; set; }
            public string IdPdm { get; set; }
            //public string NumberPole { get; set; }
            //public string DiamRotor { get; set; }
            //public string Stack { get; set; }
            //public string NumberOfSlot { get; set; }
            //public string InsulationClass { get; set; }
            //public string Status { get; set; }
            public string RotationDirection { get; set; }
            public string IdTData { get; set; }
            public string Status { get; set; }
            
        }
        public List<DtStringsFan> FanDataGridList()
        {
            return (from DataRow r in FanDataTable().Rows select new DtStringsFan
                        {
                            IdNomenclature = r["IdNomenclature"].ToString(),
                            Nomenclature = r["Fan"].ToString(),
                            Phase = r["Phase"].ToString(),
                            IdPdm = r["IdPdm"].ToString(),
                            ImageFilePath = string.IsNullOrEmpty(r["IDPDM"].ToString()) ? "" : new Uri(@"Images\sldres1u_26.png", UriKind.RelativeOrAbsolute).ToString(),
                            MotorName = r["Motor"].ToString(),
                            FilePathName = string.IsNullOrEmpty(r["IDPDM"].ToString()) ? "" : DtStatus(Convert.ToInt32(r["IDPDM"].ToString())).Rows[0]["Project Path"].ToString(),
                            FileNameFromPdm = string.IsNullOrEmpty(r["IDPDM"].ToString()) ? "" : DtStatus(Convert.ToInt32(r["IDPDM"].ToString())).Rows[0]["File Name"].ToString(),
                            PowerInput = r["PowerInput"].ToString(),
                            Volt = r["Volt"].ToString(),
                            Temperature = "-" + r["PermArmTemperatureMin"] + "... +" + r["PermArmTemperatureMax"],
                            Frequency = r["Frequency"].ToString(),
                            Rpm = r["RPM"].ToString(),
                            TypeName = r["TypeName"].ToString(),
                            DescriptionImpeller = r["Description"].ToString(),
                            FanSize = r["FanSize"].ToString(),
                            RotationDirection = r["RotationDirection"].ToString(),
                            Status = string.IsNullOrEmpty(r["IDPDM"].ToString()) ? "" : DtStatus(Convert.ToInt32(r["IDPDM"].ToString())).Rows[0]["Status Name"].ToString()
                            //IdTData = r["IDTData"].ToString()

                        }).ToList();
        }

        #endregion

        public class ColumnClass
        {
            public string ColDescription {get; set; }
            public string Binding { get; set; }
            public int DefaultWidth { get; set; }
            public int ColSetId { get; set; } 
        }

        public List<ColumnClass> ListColumn()
        {
            var con = new SqlConnection(ConString);

            con.Open();

            var da = new SqlDataAdapter();
            var ds = new DataSet();
            var dt = new DataTable();
            var cmd = new SqlCommand();

            ds.Tables.Add(dt);
            cmd.CommandText = @"SELECT cc.ColumnName, cc.DefaultWidth, cc.[Binding], ccc.IDNomenclatureGroup  FROM MotorFan.CustomColumn cc
                                INNER JOIN MotorFan.CustomColumnConfig ccc ON cc.ColumnID = ccc.ColumnID";

            cmd.Connection = con;
            da.SelectCommand = cmd;
            da.Fill(dt);

            var listString = (from DataRow datarow in dt.Rows select new ColumnClass
                {
                    ColDescription = datarow["ColumnName"].ToString(),
                    Binding = datarow["Binding"].ToString(),
                    DefaultWidth = Convert.ToInt32(datarow["DefaultWidth"]),
                    ColSetId = Convert.ToInt32(datarow["IDNomenclatureGroup"])
                }).ToList();

            con.Close();

            return listString;
        }

        public IEdmVault5 Vault1;
        public IEdmVault8 Vault2;
        public IEdmFolder5 Folder;

        public void LoadVault()
        {
                if (Vault1 == null)
                {
                    Vault1 = new EdmVault5();
                }

                Vault2 = (IEdmVault8)Vault1;

                var ok = Vault1.IsLoggedIn;

                if (!ok)
                {
                    Vault1.LoginAuto(VaultName, 0);
                }
        }

        public DataTable TradeMark()
        {
            var con = new SqlConnection(ConString);
            var cmd = new SqlCommand
            {
                Connection = con,
                CommandType = CommandType.Text,
                CommandText = @"SELECT IDTradeMark, Name, Code FROM MotorFan.TradeMark"
            };

            var dt = new DataTable();
            var da = new SqlDataAdapter { SelectCommand = cmd };

            con.Open();

            da.Fill(dt);

            con.Close();

            return dt;
        }

        public DataTable MotorType()
        {
            var con = new SqlConnection(ConString);
            var cmd = new SqlCommand
            {
                Connection = con,
                CommandType = CommandType.Text,
                CommandText = @"SELECT IDMotorType, TypeName, Code
                            FROM MotorFan.MotorType"
            };

            var dt = new DataTable();
            var da = new SqlDataAdapter { SelectCommand = cmd };

            con.Open();

            da.Fill(dt);

            con.Close();

            return dt;
        }

        public DataTable Pole()
        {
            var con = new SqlConnection(ConString);
            var cmd = new SqlCommand
            {
                Connection = con,
                CommandType = CommandType.Text,

                CommandText = @"SELECT IdPole,  Code
                            FROM Pole"
            };

            var dt = new DataTable();
            var da = new SqlDataAdapter { SelectCommand = cmd };

            con.Open();

            da.Fill(dt);

            con.Close();

            return dt;
        }

        public DataTable Volt()
        {
            var con = new SqlConnection(ConString);
            var cmd = new SqlCommand
            {
                Connection = con,
                CommandType = CommandType.Text,
                CommandText = @"SELECT IDSupply, CAST(Volt AS NVARCHAR(5)) + '/' + CAST(Frequency AS NVARCHAR(5)) AS Volt, Code
                FROM MotorFan.Supply Order By Volt"
            };

            var dt = new DataTable();
            var da = new SqlDataAdapter { SelectCommand = cmd };

            con.Open();

            da.Fill(dt);


            con.Close();

            return dt;
        }

        public DataTable MotorParams()
        {
            //NumberOfSlot
            var con = new SqlConnection(ConString);
            var cmd = new SqlCommand
            {
                Connection = con,
                CommandType = CommandType.Text,
                CommandText = @"SELECT IDParam, CAST(DimRotor AS nvarchar(5)) + CAST(Stack AS nvarchar(5)) + MotorLab AS Param, FanLab
                FROM MotorFan.MotorParams"
            };

            var dt = new DataTable();
            var da = new SqlDataAdapter { SelectCommand = cmd };

            con.Open();

            da.Fill(dt);

            con.Close();

            return dt;
        }
    }
}