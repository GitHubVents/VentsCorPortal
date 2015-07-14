using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SmokeExtraction
{
    class DataLoad
    {
        public string ConString = @"Data Source=192.168.14.11;Initial Catalog=SWPlusDB;Persist Security Info=True;User ID=sa;Password=PDMadmin; Pooling=True";
        public string q;
        private static int ColSetId;

        public DataTable FireResistantDampersDataTable()
        {
            var dt = new DataTable();

            var con = new SqlConnection(ConString);

            q = @"SELECT IDNomenclature, Nomenclature, IDDamper, FireResistance, PurposeTypeCode,
                        DesignVariant, DamperWidth, DamperHeight, NumberOfFlanges, ProtectiveGrille, 
                        ActuatorLocation, ActuatorType
                        FROM SmokeRemoval.GetFireResistantDampers";
            var sqlCmd = new SqlCommand(q, con);
            var da = new SqlDataAdapter(sqlCmd);
            da.Fill(dt);
            return dt;
        }
        public class DtStringsFireResistantDampers
        {
            public string IdNomenclature { get; set; }
            public string Nomenclature { get; set; }
            public string IdDamper { get; set; }
            public string FireResistance { get; set; }
            public string PurposeTypeCode { get; set; }
            public string DesignVariant { get; set; }
            public string DamperWidth { get; set; }
            public string DamperHeight { get; set; }
            public string NumberOfFlanges { get; set; }
            public string ProtectiveGrille { get; set; }
            public string ActuatorLocation { get; set; }
            public string ActuatorType { get; set; }
        }
        public List<DtStringsFireResistantDampers> FireResistantDampersGridList()
        {
            return (from DataRow r in FireResistantDampersDataTable().Rows select new DtStringsFireResistantDampers
            {
                IdNomenclature = r["IdNomenclature"].ToString(),
                Nomenclature = r["Nomenclature"].ToString(),
                IdDamper = r["IdDamper"].ToString(),
                FireResistance = r["FireResistance"].ToString(),
                PurposeTypeCode = r["PurposeTypeCode"].ToString(),
                DesignVariant = r["DesignVariant"].ToString(),
                DamperWidth = r["DamperWidth"].ToString(),
                DamperHeight = r["DamperHeight"].ToString(),
                NumberOfFlanges = r["NumberOfFlanges"].ToString(),
                ProtectiveGrille = r["ProtectiveGrille"].ToString(),
                ActuatorLocation = r["ActuatorLocation"].ToString(),
                ActuatorType = r["ActuatorType"].ToString()
            }).ToList();
        }

        public class ColumnClass
        {
            public string ColDescription { get; set; }
            public string Binding { get; set; }
            public int DefaultWidth { get; set; }
            public int IdNomenclatureGroup { get; set; }
            public string Number { get; set; }
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

            cmd.CommandText = @"SELECT cc.ColumnName, cc.DefaultWidth, cc.[Binding], ccc.Number, ccc.IDNomenclatureGroup  FROM MotorFan.CustomColumn cc
                                    INNER JOIN MotorFan.CustomColumnConfig ccc ON cc.ColumnID = ccc.ColumnID";

            cmd.Connection = con;
            da.SelectCommand = cmd;
            da.Fill(dt);

            var listString = (from DataRow datarow in dt.Rows select new ColumnClass
            {
            ColDescription = datarow["ColumnName"].ToString(),
            Binding = datarow["Binding"].ToString(),
            DefaultWidth = Convert.ToInt32(datarow["DefaultWidth"]),
            IdNomenclatureGroup = Convert.ToInt32(datarow["IDNomenclatureGroup"]),
            Number = datarow["Number"] == DBNull.Value ? "" : datarow["Number"].ToString(),
            }).ToList();

            con.Close();

            return listString;

        }

        // Крышный центробежный вентилятор дымоудаления
        public class DtStringsSmokeRemoval
        {
            public string Nomenclature { get; set; }
            public string IdFan { get; set; }
            public string AirDischarge { get; set; }
            public string IdImpeller { get; set; }
            public string MaximumAirTemperature { get; set; }
            public string InstalledPowerCapacity { get; set; }
            public string RotationSpeed { get; set; }
        }
        public List<DtStringsSmokeRemoval> SmokeRemovalGridList()
        {
            var dt = new DataTable();

            var con = new SqlConnection(ConString);

            q = @"SELECT n.Nomenclature, f.IDFan, f.AirDischargeВirection, f.IDImpeller, f.MaximumAirTemperature, f.InstalledPowerCapacity, f.RotationSpeed
                        FROM SmokeRemoval.Fans AS f INNER JOIN
                        dbo.Nomenclature AS n ON f.IDnomenclature = n.IDNomenclature";

            var sqlCmd = new SqlCommand(q, con);
            var da = new SqlDataAdapter(sqlCmd);
            da.Fill(dt);

            var listString = (from DataRow datarow in dt.Rows select new DtStringsSmokeRemoval
            {
                Nomenclature = datarow["Nomenclature"].ToString(),
                IdFan = datarow["IdFan"].ToString(),
                AirDischarge = datarow["AirDischargeВirection"].ToString(),
                IdImpeller = datarow["IDImpeller"].ToString(),
                MaximumAirTemperature = datarow["MaximumAirTemperature"].ToString(),
                InstalledPowerCapacity = datarow["InstalledPowerCapacity"].ToString(),
                RotationSpeed = datarow["RotationSpeed"].ToString()
            }).ToList();

            con.Close();

            return listString;
        }

        // Крышный вытяжной каминный вентилятор для усиления тяги вытяжки дымовых газов
        // Клапан противопожарный дымовой универсальный
        // Клапан протипожарный огнезадерживающий

    }
}