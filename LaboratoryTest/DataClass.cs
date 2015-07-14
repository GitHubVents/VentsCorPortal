using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;

namespace LaboratoryTest
{
    public class DataClass
    {
        public string ConString = @"Data Source=192.168.14.11;Initial Catalog=SWPlusDB;Persist Security Info=True;User ID=sa;Password=PDMadmin; Pooling=True";

        //public string ConString { get; set; }

        private string _q;
  
        private DataTable DtListOfOrders()
        {
            using (var con = new SqlConnection(ConString))
            {
                var dt = new DataTable();

                _q = @"SELECT TypeItem, InfoItem, TestProcedure, Note, Capacitor, U.FullName
                    FROM Lab.LabItems AS L
                    INNER JOIN ACL.Users AS U ON L.IDUser = U.UserID";

                var sqlCmd = new SqlCommand(_q, con);

                var da = new SqlDataAdapter(sqlCmd);

                da.Fill(dt);

                return dt;
            }
        }
        public class ClassListOfOrders
        {
            public string TypeItem { get; set; }
            public string InfoItem { get; set; }
            public string TestProcedure { get; set; }
            public string Note { get; set; }
            public string Capacitor { get; set; }
            public string FullName { get; set; }
            //public string FanName { get; set; }
        }
        public List<ClassListOfOrders> ListOfOrders()
        {
            return (from DataRow r in DtListOfOrders().Rows
                    select new ClassListOfOrders
                        {
                            TypeItem = r["TypeItem"].ToString(),
                            InfoItem = r["InfoItem"].ToString(),
                            TestProcedure = r["TestProcedure"].ToString(),
                            Note = r["Note"].ToString(),
                            Capacitor = r["Capacitor"].ToString(),
                            FullName = r["FullName"].ToString(),
                            //FanName = r["FanName"].ToString()

                        }).ToList();
        }

        public DataTable DtListParamTable(int numberOrder)
        {
            using (var con = new SqlConnection(ConString))
            {
                var dt = new DataTable();

                _q = @"SELECT P.IdParam, LabOr.IDLabOrder, LabOr.LabOrderNumber, LabOr.LabOrderDate, P.P, P.I, P.[Cos], P.n, P.Ps, P.Q  FROM [Lab].[LabOrders] AS LabOr
                    INNER JOIN Lab.Param AS P ON LabOr.IDLabOrder = P.IDLabOrder WHERE LabOr.LabOrderNumber = " + numberOrder;

                var sqlCmd = new SqlCommand(_q, con);

                var da = new SqlDataAdapter(sqlCmd);

                da.Fill(dt);

                return dt;
            }
        }
        public class ClassListParam
        {
            public string IdParam { get; set; }
            public double? P { get; set; }
            public double? I { get; set; }
            public double? Cos { get; set; }
            public int? N { get; set; }
            public int? Ps { get; set; }
            public double? PsinWg { get; set; }
            public double? Q { get; set; }
            public double? QCfm { get; set; }
            public double? QLs { get; set; }
            public double? Wls { get; set; }
        }

        public List<ClassListParam> ListParam(int numberOrder)
        {
            var listParamStr = new List<ClassListParam>();

            foreach (DataRow r in DtListParamTable(numberOrder).Rows)
            {

                var classListParam = new ClassListParam();

                classListParam.IdParam = r["IdParam"].ToString();
                classListParam.P = r["P"] == DBNull.Value ? 0 : Convert.ToDouble(r["P"]);
                classListParam.I = r["I"] == DBNull.Value ? 0 : Convert.ToDouble(r["I"]);
                classListParam.Cos = r["Cos"] == DBNull.Value ? 0 : Convert.ToDouble(r["Cos"]);
                classListParam.N = r["n"] == DBNull.Value ? 0 : Convert.ToInt32(r["n"]);

                classListParam.Ps = r["Ps"] == DBNull.Value ? 0 : Convert.ToInt32(r["Ps"]);

                var psinWgSumm = Math.Round(Convert.ToInt32(classListParam.Ps) * 0.001 * 4.01463, 2);
                classListParam.PsinWg = string.IsNullOrEmpty(psinWgSumm.ToString()) ? 0 : psinWgSumm;

                classListParam.Q = r["Q"] == DBNull.Value ? 0 : Convert.ToDouble(r["Q"]);

                var qCfmSumm = Math.Round((Convert.ToDouble(classListParam.Q) * 0.588), 1);
                classListParam.QCfm = string.IsNullOrEmpty(qCfmSumm.ToString()) ? 0 : qCfmSumm;

                var qLsSumm = Math.Round(Math.Abs((Convert.ToDouble(classListParam.Q) * 1000) / 3600), 1);
                classListParam.QLs = string.IsNullOrEmpty(qLsSumm.ToString()) ? 0 : qLsSumm;

                var wlsSumm = Math.Round(Math.Abs(Convert.ToDouble(classListParam.P) / (Convert.ToDouble(classListParam.QLs))), 3);
                classListParam.Wls = string.IsNullOrEmpty(wlsSumm.ToString()) ? 0 : wlsSumm;

                listParamStr.Add(classListParam);

            }

            return listParamStr;

            //return (from DataRow r in DtListParamTable(numberOrder).Rows select new ClassListParam
            //{
            //    IdParam = r["IdParam"].ToString(),
            //    P = r["P"] == DBNull.Value ? 0 : Convert.ToDouble(Math.Round((Convert.ToDouble(r["Q"]) * 0.588), 1)]),
            //    I = r["I"] == DBNull.Value ? 0 : Convert.ToDouble(r["I"]),
            //    Cos = r["Cos"] == DBNull.Value ? 0 : Convert.ToDouble(r["Cos"]),
            //    N = r["n"] == DBNull.Value ? 0 : Convert.ToInt32(r["n"]),
            //    Ps = r["Ps"] == DBNull.Value ? 0 : Convert.ToInt32(r["Ps"]),

            //    PsinWg = Math.Round(Convert.ToDouble(r["Ps"]) * 0.001 * 4.01463, 2),

            //    Q = r["Q"] == DBNull.Value ? 0 : Convert.ToDouble(r["Q"]),

            //    QCfm = Math.Round((Convert.ToDouble(r["Q"]) * 0.588), 1), 
            //    QLs = Math.Round(Math.Abs((Convert.ToDouble(r["Q"]) * 1000) / 3600), 1),
            //    Wls = Math.Round(Math.Abs(Math.Abs(Convert.ToDouble(r["P"]) / (Convert.ToDouble(r["Q"]) * 1000) / 3600)), 1)

            //}).ToList();
        }

        private DataTable DtListMotorParamTable(int numberOrder)
        {
            using (var con = new SqlConnection(ConString))
            {
                var dt = new DataTable();

                _q = @"SELECT O.LabOrderNumber, P.Temp, P.R0, P.Rt, P.IDMotorParam
                       FROM Lab.MotorParam AS P INNER JOIN
                       Lab.LabOrders AS O ON P.IDLabOrder = O.IDLabOrder
                       WHERE O.LabOrderNumber =" + numberOrder;

                var sqlCmd = new SqlCommand(_q, con);

                var da = new SqlDataAdapter(sqlCmd);

                da.Fill(dt);

                return dt;
            }
        }
        public class ClassListMotorParam
        {
            public string IdMotorParam { get; set; }
            public double? R0 { get; set; }
            public int? Rt { get; set; }
            public int? Temp { get; set; }
            public double? Tdelta { get; set; }
            public double? Tsdvig { get; set; }
        }
        public List<ClassListMotorParam> ListMotorParam(int numberOrder)
        {

            var listParamStr = new List<ClassListMotorParam>();


            foreach (DataRow r in DtListMotorParamTable(numberOrder).Rows)
            {
                var classListMotorParam = new ClassListMotorParam();

                classListMotorParam.IdMotorParam = r["IdMotorParam"].ToString();
                classListMotorParam.R0 = r["R0"] == DBNull.Value ? 0 : Convert.ToDouble(r["R0"]);
                classListMotorParam.Rt = r["Rt"] == DBNull.Value ? 0 : Convert.ToInt32(r["Rt"]);
                classListMotorParam.Temp = r["Temp"] == DBNull.Value ? 0 : Convert.ToInt32(r["Temp"]);


                var tdeltaSumm =
                    Math.Round(
                        Math.Abs((Convert.ToInt32(classListMotorParam.Rt) - Convert.ToDouble(classListMotorParam.R0)) / Convert.ToDouble(classListMotorParam.R0) *
                                 (234.5 + Convert.ToInt32(classListMotorParam.Temp))), 1);

                classListMotorParam.Tdelta = string.IsNullOrEmpty(tdeltaSumm.ToString()) ? 0 : tdeltaSumm;


                var tsdvigSumm = Math.Round((Convert.ToDouble(classListMotorParam.Tdelta) + Convert.ToInt32(classListMotorParam.Temp)), 1);
                classListMotorParam.Tsdvig = string.IsNullOrEmpty(tsdvigSumm.ToString()) ? 0 : tsdvigSumm;

        
                listParamStr.Add(classListMotorParam);
            }

            return listParamStr;   
        }

    }
}
