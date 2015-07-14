using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;

namespace AirVentsOrderManager.Model
{
    public partial class OrderData
    {

        public static DataTable OrdersConstructorTable()
        {
            var ordersList = new DataTable();
            using (var con = new SqlConnection(App.ConString))
            {
                try
                {
                    con.Open();
                    var sqlCommand = new SqlCommand(
   @"SELECT
   e.LastName + ' ' + LEFT(e.FirsrtName, 1) + '.' AS 'Manager'
  ,e.EmpID
  ,o.ProjectNumber
  ,o.OrderID
  ,o.[Date]
  ,o.SupplyTotalStaticPressure
  ,o.SupplyStaticPressure
  ,o.SupplyAirflow
  ,o.ExhaustTotalStaticPressure
  ,o.ExhaustStaticPressure
  ,o.ExhaustAirflow
  ,o.ServiceAccess
  ,s.[Type] + ' ' + CAST(od.InternalNumber AS NVARCHAR) AS 'InternalNumberFull'
,od.InternalNumber 
  ,s.SizeID
  ,p.[Description]
  ,p.ProfilID
  ,od.requireddate,
  od.shippeddate,
  od.CompletionDate,
  od.FinishCompletionDate,
  c.LastName + ' ' + LEFT(c.FirstName, 1) + '.' AS 'Constructor',
  c.ConstructorID
FROM AirVents.[Order] o
INNER JOIN AirVents.DimensionType d
  ON o.DimensionTypeID = d.DimensionTypeID
INNER JOIN HumanResources.Employee e
  ON o.empid = e.empid
LEFT JOIN AirVents.OrderDetails od
  ON o.OrderID = od.OrderID
LEFT JOIN HumanResources.Constructor c
  ON od.ConstructorID = c.ConstructorID
INNER JOIN AirVents.Profil p
  ON d.ProfilID = p.ProfilID
INNER JOIN AirVents.StandardSize s
  ON d.SizeID = s.SizeID
", con);
                    var sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    sqlDataAdapter.Fill(ordersList);
                    sqlDataAdapter.Dispose();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Ошибка выгрузки данных из базы OrdersConstructorTable");
                }
                finally
                {
                    con.Close();
                }
            }
            return ordersList;
        }

        public static DataTable ConstructorsList()
        {
            var constructorsList = new DataTable();
            using (var con = new SqlConnection(App.ConString))
            {
                try
                {
                    con.Open();
                    var sqlCommand = new SqlCommand("SELECT * FROM  HumanResources.Constructor ORDER BY LastName", con);
                    var sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    sqlDataAdapter.Fill(constructorsList);
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
            return constructorsList;
        }

        public class OrdersConstructorDataClass
        {

            public string Expr1 { get; set; }
            public string Expr2 { get; set; }
            public string ProjectNumber { get; set; }
            public DateTime Date { get; set; }
            
            public string Type { get; set; }
            public string Description { get; set; }
            public string InternalNumber { get; set; }
            public string Order { get; set; }

            public string RequiredDate { get; set; }
            public string ShippedDate { get; set; }
            public string CompletionDate { get; set; }
            public string FinishCompletionDate { get; set; }
            
            public int ManagerId { get; set; } // Мэнэджер
            public int ConstructorId { get; set; } // Конструктор
            public int SizeId { get; set; } // Типоразмер
            public int ProfilId { get; set; }  // Профиль

            public bool ServiceAccess { get; set; }

            public double? SupplyTotalStaticPressure { get; set; }
            public double? SupplyStaticPressure { get; set; }
            public double? SupplyAirflow { get; set; }
            public double? ExhaustTotalStaticPressure { get; set; }
            public double? ExhaustStaticPressure { get; set; }
            public double? ExhaustAirflow { get; set; }
            
            public int OrderId { get; set; }

            public string InternalNumberFull { get; set; }

            public int IdNomenclature { get; set; }
            public string Nomenclature { get; set; }
            public int Quantity { get; set; }

        }

        public static IEnumerable<OrdersConstructorDataClass> OrdersConstructorDataList()
        {
            var list = (from DataRow row in OrdersConstructorTable().Rows
                        select new OrdersConstructorDataClass
                        {
                            Expr1 = row["Manager"].ToString(),
                            Expr2 = row["Constructor"].ToString(),
                            Order = row["InternalNumber"].ToString(),
                            InternalNumberFull = row["InternalNumberFull"].ToString(),
                            OrderId = row["OrderID"] != DBNull.Value ? Convert.ToInt32(row["OrderID"]) : 0,
                            Date = Convert.ToDateTime(row["Date"] != DBNull.Value ? Convert.ToDateTime(row["Date"]).ToShortDateString() : DateTime.Now.ToShortDateString()),

                            ManagerId = row["EmpID"] != DBNull.Value ? Convert.ToInt32(row["EmpID"]) : 0,
                            ConstructorId = row["ConstructorID"] != DBNull.Value ? Convert.ToInt32(row["ConstructorID"]) : 0,
                            SizeId = row["SizeID"] != DBNull.Value ? Convert.ToInt32(row["SizeID"]) : 0,
                            ProfilId = row["ProfilID"] != DBNull.Value ? Convert.ToInt32(row["ProfilID"]) : 0,

                            SupplyTotalStaticPressure = row["SupplyTotalStaticPressure"] != DBNull.Value ? Convert.ToDouble(row["SupplyTotalStaticPressure"]) : 0,
                            SupplyStaticPressure = row["SupplyStaticPressure"] != DBNull.Value ? Convert.ToDouble(row["SupplyStaticPressure"]) : 0,
                            SupplyAirflow = row["SupplyAirflow"] != DBNull.Value ? Convert.ToDouble(row["SupplyAirflow"]) : 0,
                            ExhaustTotalStaticPressure = row["ExhaustTotalStaticPressure"] != DBNull.Value ? Convert.ToDouble(row["ExhaustTotalStaticPressure"]) : 0,
                            ExhaustStaticPressure = row["ExhaustStaticPressure"] != DBNull.Value ? Convert.ToDouble(row["ExhaustStaticPressure"]) : 0,
                            ExhaustAirflow = row["ExhaustAirflow"] != DBNull.Value ? Convert.ToDouble(row["ExhaustAirflow"]) : 0,

                            ServiceAccess = row["ServiceAccess"] != DBNull.Value && Convert.ToBoolean(row["ServiceAccess"]),

                            ProjectNumber = row["ProjectNumber"].ToString(),
                            Description = row["Description"].ToString(),

                            RequiredDate = row["RequiredDate"] != DBNull.Value ? Convert.ToDateTime(row["RequiredDate"]).ToShortDateString() : null,
                            ShippedDate = row["ShippedDate"] != DBNull.Value ? Convert.ToDateTime(row["ShippedDate"]).ToShortDateString() : null,
                            CompletionDate = row["CompletionDate"] != DBNull.Value ? Convert.ToDateTime(row["CompletionDate"]).ToShortDateString() : null,
                            FinishCompletionDate = row["FinishCompletionDate"] != DBNull.Value ? Convert.ToDateTime(row["FinishCompletionDate"]).ToShortDateString() : null

                        }).ToList();
            return list.OrderByDescending(x => x.Date);
        }
        
        #region Редактирование Подбора Заказа

        public static int CreateOrderAirVents(int managerId, string projectNumber, DateTime? date, int sizeId, string profile,
            double supplyTotalStaticPressure, double supplyStaticPressure, double supplyAirflow, double exhaustTotalStaticPressure, double exhaustStaticPressure, double exhaustAirflow,
            bool? serviceAccess, string internalNumber,
            DateTime? requiredDate, DateTime? shippedDate, DateTime? completionDate, DateTime? finishCompletionDate,
            int constructorId, int iDnomenclature, int quantity, int idBom)
        {
            return
            CudOrderAirVents(null, 1, managerId, projectNumber, date, sizeId, profile,
            supplyTotalStaticPressure, supplyStaticPressure, supplyAirflow, exhaustTotalStaticPressure, exhaustStaticPressure, exhaustAirflow,
            serviceAccess, internalNumber, requiredDate, shippedDate, completionDate, finishCompletionDate,
            constructorId, iDnomenclature, quantity, idBom);
        }

        public void EditOrderAirVents2(int orderId, int managerId, string projectNumber, DateTime date, int sizeId, string profile,
            double supplyTotalStaticPressure, double supplyStaticPressure, double supplyAirflow, double exhaustTotalStaticPressure, double exhaustStaticPressure, double exhaustAirflow,
            bool serviceAccess, string internalNumber,
            DateTime requiredDate, DateTime shippedDate, DateTime completionDate, DateTime finishCompletionDate,
            int constructorId, int iDnomenclature, int quantity, int idBom)
        {
            CudOrderAirVents(orderId, 2, managerId, projectNumber, date, sizeId, profile,
            supplyTotalStaticPressure, supplyStaticPressure, supplyAirflow, exhaustTotalStaticPressure, exhaustStaticPressure, exhaustAirflow,
            serviceAccess, internalNumber, requiredDate, shippedDate, completionDate, finishCompletionDate,
            constructorId, iDnomenclature, quantity, idBom);
        }

        public static void DeleteOrderAirVents(int orderId)
        {
            DelOrderAirVents(orderId);
        }

        public static int CudOrderAirVents(int? orderId, int param, int managerId, string projectNumber, DateTime? date, int sizeId, string profile,
            double supplyTotalStaticPressure, double supplyStaticPressure, double supplyAirflow, double exhaustTotalStaticPressure, double exhaustStaticPressure, double exhaustAirflow,
            bool? serviceAccess, string internalNumber,
            DateTime? requiredDate, DateTime? shippedDate, DateTime? completionDate, DateTime? finishCompletionDate,
            int constructorId, int iDnomenclature, int quantity, int idBom)
        {
            using (var con = new SqlConnection(App.ConString))
            {
                int newOrderId;
                try
                {
                    con.Open();
                    var sqlCommand = new SqlCommand("[AirVents].[CUDOrderAirVents]", con) { CommandType = CommandType.StoredProcedure };

                    var sqlParameter = sqlCommand.Parameters;

                    if (orderId == null)
                    {
                        sqlParameter.AddWithValue("@OrderID", DBNull.Value);
                    }
                    else
                    {
                        sqlParameter.AddWithValue("@OrderID", orderId);
                    }
                    
                        sqlParameter.AddWithValue("@PARAM", param);
                        sqlParameter.AddWithValue("@Manager", managerId);
                        sqlParameter.AddWithValue("@ProjectNumber", projectNumber);
                        sqlParameter.AddWithValue("@Date", date);
                        sqlParameter.AddWithValue("@Size", sizeId);
                        sqlParameter.AddWithValue("@Profile", profile);

                        sqlParameter.AddWithValue("@SupplyTotalStaticPressure", supplyTotalStaticPressure);
                        sqlParameter.AddWithValue("@SupplyStaticPressure", supplyStaticPressure);
                        sqlParameter.AddWithValue("@SupplyAirflow", supplyAirflow);
                        sqlParameter.AddWithValue("@ExhaustTotalStaticPressure", exhaustTotalStaticPressure);
                        sqlParameter.AddWithValue("@ExhaustStaticPressure", exhaustStaticPressure);
                        sqlParameter.AddWithValue("@ExhaustAirflow", exhaustAirflow);

                        sqlParameter.AddWithValue("@ServiceAccess", serviceAccess);
                        sqlParameter.AddWithValue("@InternalNumber", internalNumber);

                        sqlParameter.AddWithValue("@RequiredDate", requiredDate);
                        sqlParameter.AddWithValue("@ShippedDate", shippedDate);
                        sqlParameter.AddWithValue("@CompletionDate", completionDate);

                        if (finishCompletionDate == null)
                        {
                            sqlParameter.AddWithValue("@FinishCompletionDate", DBNull.Value);
                        }
                        else
                        {
                            sqlParameter.AddWithValue("@FinishCompletionDate", finishCompletionDate);
                        }

                        sqlParameter.AddWithValue("@ConstructorID", constructorId);
                        sqlParameter.AddWithValue("@IDnomenclature", iDnomenclature);
                        sqlParameter.AddWithValue("@Quantity", quantity);
                        sqlParameter.AddWithValue("@IDBOM", idBom);

                        var returnParameter = sqlCommand.Parameters.Add("@OrderID", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;
                    
                    sqlCommand.ExecuteNonQuery();

                    Int32.TryParse(returnParameter.Value.ToString(), out newOrderId);

                }
                catch (Exception exception)
                {
                    MessageBox.Show("Введите корректные данные!\n" + exception.Message);
                    return 0;
                }
                finally
                {
                    con.Close();
                }
                return newOrderId;
            }
        }

        public static int CudOrderAirVents2(int? orderId, int param, int managerId, string projectNumber, DateTime? date, int sizeId, string profile,
            double supplyTotalStaticPressure, double supplyStaticPressure, double supplyAirflow, double exhaustTotalStaticPressure, double exhaustStaticPressure, double exhaustAirflow,
            bool? serviceAccess, string internalNumber,
            DateTime? requiredDate, DateTime? shippedDate, DateTime? completionDate, DateTime? finishCompletionDate,
            int constructorId, int iDnomenclature, int quantity, int idBom)
        {
            using (var con = new SqlConnection(App.ConString))
            {
                int newOrderId;
                try
                {
                    con.Open();
                    var sqlCommand = new SqlCommand("[AirVents].[CUDOrderAirVents]", con) { CommandType = CommandType.StoredProcedure };

                    var sqlParameter = sqlCommand.Parameters;

                    if (orderId == null)
                    {
                        sqlParameter.AddWithValue("@OrderID", DBNull.Value);
                    }
                    else
                    {
                        sqlParameter.AddWithValue("@OrderID", orderId);
                    }


                    sqlParameter.AddWithValue("@PARAM", param);
                    sqlParameter.AddWithValue("@Manager", managerId);
                    sqlParameter.AddWithValue("@ProjectNumber", projectNumber);
                    sqlParameter.AddWithValue("@Date", date);
                    sqlParameter.AddWithValue("@Size", sizeId);
                    sqlParameter.AddWithValue("@Profile", profile);

                    sqlParameter.AddWithValue("@SupplyTotalStaticPressure", supplyTotalStaticPressure);
                    sqlParameter.AddWithValue("@SupplyStaticPressure", supplyStaticPressure);
                    sqlParameter.AddWithValue("@SupplyAirflow", supplyAirflow);
                    sqlParameter.AddWithValue("@ExhaustTotalStaticPressure", exhaustTotalStaticPressure);
                    sqlParameter.AddWithValue("@ExhaustStaticPressure", exhaustStaticPressure);
                    sqlParameter.AddWithValue("@ExhaustAirflow", exhaustAirflow);

                    sqlParameter.AddWithValue("@ServiceAccess", serviceAccess);
                    sqlParameter.AddWithValue("@InternalNumber", internalNumber);

                    sqlParameter.AddWithValue("@RequiredDate", requiredDate);
                    sqlParameter.AddWithValue("@ShippedDate", shippedDate);
                    sqlParameter.AddWithValue("@CompletionDate", completionDate);

                    if (finishCompletionDate == null)
                    {
                        sqlParameter.AddWithValue("@FinishCompletionDate", DBNull.Value);
                    }
                    else
                    {
                        sqlParameter.AddWithValue("@FinishCompletionDate", finishCompletionDate);
                    }

                    sqlParameter.AddWithValue("@ConstructorID", constructorId);
                    sqlParameter.AddWithValue("@IDnomenclature", iDnomenclature);
                    sqlParameter.AddWithValue("@Quantity", quantity);
                    sqlParameter.AddWithValue("@IDBOM", idBom);

                    var returnParameter = sqlCommand.Parameters.Add("@OrderID", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;

                    sqlCommand.ExecuteNonQuery();

                    Int32.TryParse(returnParameter.Value.ToString(), out newOrderId);

                }
                catch (Exception exception)
                {
                    MessageBox.Show("Введите корректные данные!\n" + exception.Message);
                    return 0;
                }
                finally
                {
                    con.Close();
                }
                return newOrderId;
            }
        }

        public static void AddOrderAirVents(int managerId, string projectNumber, DateTime? date, int sizeId, int profileId,
            double supplyTotalStaticPressure, double supplyStaticPressure, double supplyAirflow, double exhaustTotalStaticPressure, double exhaustStaticPressure, double exhaustAirflow,
            bool? serviceAccess, string internalNumber,
            DateTime? requiredDate, DateTime? shippedDate, DateTime? completionDate, DateTime? finishCompletionDate,
            int constructorId, int? iDnomenclature, int? quantity)
        {
            using (var con = new SqlConnection(App.ConString))
            {
                try
                {
                    con.Open();
                    var sqlCommand = new SqlCommand("[AirVents].[CUDOrderAirVents]", con) { CommandType = CommandType.StoredProcedure };

                    var sqlParameter = sqlCommand.Parameters;
                                      
                    sqlParameter.AddWithValue("@OrderID", DBNull.Value);

                    sqlParameter.AddWithValue("@PARAM", 1);
                    sqlParameter.AddWithValue("@Manager", managerId);
                    sqlParameter.AddWithValue("@ProjectNumber", projectNumber);
                    sqlParameter.AddWithValue("@Date", date);
                    sqlParameter.AddWithValue("@Size", sizeId);
                    sqlParameter.AddWithValue("@Profile", profileId);
                    
                    #region SupplyTotalStaticPressure

                    if (Math.Abs(supplyTotalStaticPressure) < 1)
                    {
                        sqlParameter.AddWithValue("@SupplyTotalStaticPressure", DBNull.Value);
                    }
                    else
                    {
                        sqlParameter.AddWithValue("@SupplyTotalStaticPressure", supplyTotalStaticPressure);
                    }

                    #endregion

                    #region SupplyStaticPressure

                    if (Math.Abs(supplyStaticPressure) < 1)
                    {
                        sqlParameter.AddWithValue("@SupplyStaticPressure", DBNull.Value);
                    }
                    else
                    {
                        sqlParameter.AddWithValue("@SupplyStaticPressure", supplyStaticPressure);
                    }

                    #endregion

                    #region SupplyAirflow

                    if (Math.Abs(supplyStaticPressure) < 1)
                    {
                        sqlParameter.AddWithValue("@SupplyAirflow", DBNull.Value);
                    }
                    else
                    {
                        sqlParameter.AddWithValue("@SupplyAirflow", supplyAirflow);
                    }

                    #endregion

                    #region ExhaustTotalStaticPressure

                    if (Math.Abs(exhaustTotalStaticPressure) < 1)
                    {
                        sqlParameter.AddWithValue("@ExhaustTotalStaticPressure", DBNull.Value);
                    }
                    else
                    {
                        sqlParameter.AddWithValue("@ExhaustTotalStaticPressure", exhaustTotalStaticPressure);
                    }

                    #endregion

                    #region ExhaustStaticPressure

                    if (Math.Abs(exhaustStaticPressure) < 1)
                    {
                        sqlParameter.AddWithValue("@ExhaustStaticPressure", DBNull.Value);
                    }
                    else
                    {
                        sqlParameter.AddWithValue("@ExhaustStaticPressure", exhaustStaticPressure);
                    }

                    #endregion

                    #region ExhaustAirflow

                    if (Math.Abs(exhaustAirflow) < 1)
                    {
                        sqlParameter.AddWithValue("@ExhaustAirflow", DBNull.Value);
                    }
                    else
                    {
                        sqlParameter.AddWithValue("@ExhaustAirflow", exhaustAirflow);
                    }

                    #endregion
                    
                    sqlParameter.AddWithValue("@ServiceAccess", serviceAccess);
                    sqlParameter.AddWithValue("@InternalNumber", internalNumber);

                    sqlParameter.AddWithValue("@RequiredDate", requiredDate);
                    sqlParameter.AddWithValue("@ShippedDate", shippedDate);
                    sqlParameter.AddWithValue("@CompletionDate", completionDate);

                    #region FinishCompletionDate

                    if (finishCompletionDate == null)
                    {
                        sqlParameter.AddWithValue("@FinishCompletionDate", DBNull.Value);
                    }
                    else
                    {
                        sqlParameter.AddWithValue("@FinishCompletionDate", finishCompletionDate);
                    }

                    #endregion

                    sqlParameter.AddWithValue("@ConstructorID", constructorId);

                    #region IDnomenclature

                    if (iDnomenclature == null)
                    {
                        sqlParameter.AddWithValue("@IDnomenclature", DBNull.Value);
                    }
                    else
                    {
                        sqlParameter.AddWithValue("@IDnomenclature", iDnomenclature);
                    }

                    #endregion

                    #region Quantity

                    if (quantity == null)
                    {
                        sqlParameter.AddWithValue("@Quantity", DBNull.Value);
                    }
                    else
                    {
                        sqlParameter.AddWithValue("@Quantity", quantity);
                    }

                    #endregion

                    #region IDBOM
                    
                    sqlParameter.AddWithValue("@IDBOM", DBNull.Value);
                    

                    #endregion

                    //var returnParameter = sqlCommand.Parameters.Add("@OrderID", SqlDbType.Int);
                    //returnParameter.Direction = ParameterDirection.ReturnValue;

                    sqlCommand.ExecuteNonQuery();

                    //Int32.TryParse(returnParameter.Value.ToString(), out newOrderId);

                }
                catch (Exception exception)
                {
                    MessageBox.Show("Введите корректные данные!\n" + exception.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }


        /// <summary>
        /// Edits the order air vents.
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        /// <param name="managerId">The manager identifier.</param>
        /// <param name="projectNumber">The project number.</param>
        /// <param name="date">The date.</param>
        /// <param name="sizeId">The size identifier.</param>
        /// <param name="profileId">The profile identifier.</param>
        /// <param name="supplyTotalStaticPressure">The supply total static pressure.</param>
        /// <param name="supplyStaticPressure">The supply static pressure.</param>
        /// <param name="supplyAirflow">The supply airflow.</param>
        /// <param name="exhaustTotalStaticPressure">The exhaust total static pressure.</param>
        /// <param name="exhaustStaticPressure">The exhaust static pressure.</param>
        /// <param name="exhaustAirflow">The exhaust airflow.</param>
        /// <param name="serviceAccess">The service access.</param>
        /// <param name="internalNumber">The internal number.</param>
        /// <param name="requiredDate">The required date.</param>
        /// <param name="shippedDate">The shipped date.</param>
        /// <param name="completionDate">The completion date.</param>
        /// <param name="finishCompletionDate">The finish completion date.</param>
        /// <param name="constructorId">The constructor identifier.</param>
        /// <param name="iDnomenclature">The i dnomenclature.</param>
        /// <param name="quantity">The quantity.</param>
        /// <param name="idBom">The identifier bom.</param>
        public static void EditOrderAirVents(int orderId, int managerId, string projectNumber, DateTime? date, int sizeId, int profileId,
            double supplyTotalStaticPressure, double supplyStaticPressure, double supplyAirflow, double exhaustTotalStaticPressure, double exhaustStaticPressure, double exhaustAirflow,
            bool? serviceAccess, string internalNumber,
            DateTime? requiredDate, DateTime? shippedDate, DateTime? completionDate, DateTime? finishCompletionDate,
            int constructorId, int? iDnomenclature, int? quantity, int idBom)
        {
            using (var con = new SqlConnection(App.ConString))
            {
                try
                {
                    con.Open();
                    var sqlCommand = new SqlCommand("[AirVents].[CUDOrderAirVents]", con) { CommandType = CommandType.StoredProcedure };

                    var sqlParameter = sqlCommand.Parameters;
                    
                    sqlParameter.AddWithValue("@OrderID", orderId);

                    sqlParameter.AddWithValue("@PARAM", 2);
                    sqlParameter.AddWithValue("@Manager", managerId);
                    sqlParameter.AddWithValue("@ProjectNumber", projectNumber);
                    //sqlParameter.AddWithValue("@ProjectNumber", projectNumber);
                    sqlParameter.AddWithValue("@Date", date);
                    sqlParameter.AddWithValue("@Size", sizeId);
                    sqlParameter.AddWithValue("@Profile", profileId);

                    #region SupplyTotalStaticPressure

                    if (Math.Abs(supplyTotalStaticPressure) < 1)
                    {
                        sqlParameter.AddWithValue("@SupplyTotalStaticPressure", DBNull.Value);
                    }
                    else
                    {
                        sqlParameter.AddWithValue("@SupplyTotalStaticPressure", supplyTotalStaticPressure);
                    }

                    #endregion

                    #region SupplyStaticPressure

                    if (Math.Abs(supplyStaticPressure) < 1)
                    {
                        sqlParameter.AddWithValue("@SupplyStaticPressure", DBNull.Value);
                    }
                    else
                    {
                        sqlParameter.AddWithValue("@SupplyStaticPressure", supplyStaticPressure);
                    }

                    #endregion

                    #region SupplyAirflow

                    if (Math.Abs(supplyStaticPressure) < 1)
                    {
                        sqlParameter.AddWithValue("@SupplyAirflow", DBNull.Value);
                    }
                    else
                    {
                        sqlParameter.AddWithValue("@SupplyAirflow", supplyAirflow);
                    }

                    #endregion

                    #region ExhaustTotalStaticPressure

                    if (Math.Abs(exhaustTotalStaticPressure) < 1)
                    {
                        sqlParameter.AddWithValue("@ExhaustTotalStaticPressure", DBNull.Value);
                    }
                    else
                    {
                        sqlParameter.AddWithValue("@ExhaustTotalStaticPressure", exhaustTotalStaticPressure);
                    }

                    #endregion

                    #region ExhaustStaticPressure

                    if (Math.Abs(exhaustStaticPressure) < 1)
                    {
                        sqlParameter.AddWithValue("@ExhaustStaticPressure", DBNull.Value);
                    }
                    else
                    {
                        sqlParameter.AddWithValue("@ExhaustStaticPressure", exhaustStaticPressure);
                    }

                    #endregion

                    #region ExhaustAirflow

                    if (Math.Abs(exhaustAirflow) < 1)
                    {
                        sqlParameter.AddWithValue("@ExhaustAirflow", DBNull.Value);
                    }
                    else
                    {
                        sqlParameter.AddWithValue("@ExhaustAirflow", exhaustAirflow);
                    }

                    #endregion

                    sqlParameter.AddWithValue("@ServiceAccess", serviceAccess);
                    sqlParameter.AddWithValue("@InternalNumber", internalNumber);

                    sqlParameter.AddWithValue("@RequiredDate", requiredDate);
                    sqlParameter.AddWithValue("@ShippedDate", shippedDate);
                    sqlParameter.AddWithValue("@CompletionDate", completionDate);

                    #region FinishCompletionDate

                    if (finishCompletionDate == null)
                    {
                        sqlParameter.AddWithValue("@FinishCompletionDate", DBNull.Value);
                    }
                    else
                    {
                        sqlParameter.AddWithValue("@FinishCompletionDate", finishCompletionDate);
                    }

                    #endregion
                    
                    sqlParameter.AddWithValue("@ConstructorID", constructorId);

                    #region IDnomenclature

                    if (iDnomenclature == null)
                    {
                        sqlParameter.AddWithValue("@IDnomenclature", DBNull.Value);
                    }
                    else
                    {
                        sqlParameter.AddWithValue("@IDnomenclature", iDnomenclature);
                    }

                    #endregion

                    #region Quantity

                    if (quantity == null)
                    {
                        sqlParameter.AddWithValue("@Quantity", DBNull.Value);
                    }
                    else
                    {
                        sqlParameter.AddWithValue("@Quantity", quantity);
                    }

                    #endregion

                    #region IDBOM

                    if (idBom == 0)
                    {
                        sqlParameter.AddWithValue("@IDBOM", DBNull.Value);    
                    }
                    else
                    {
                        sqlParameter.AddWithValue("@IDBOM", idBom);
                    }
                    
                    
                    #endregion

                    sqlCommand.ExecuteNonQuery();

                }
                catch (Exception exception)
                {
                    MessageBox.Show("Введите корректные данные!\n" + exception.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }

        public static void DeleteBomItemAirVents(int idBom)
        {
            using (var con = new SqlConnection(App.ConString))
            {
                try
                {
                    con.Open();
                    var sqlCommand = new SqlCommand("[AirVents].[CUDOrderAirVents]", con) { CommandType = CommandType.StoredProcedure };

                    var sqlParameter = sqlCommand.Parameters;

                    sqlParameter.AddWithValue("@OrderID", DBNull.Value);

                    sqlParameter.AddWithValue("@PARAM", 4);
                    sqlParameter.AddWithValue("@Manager", DBNull.Value);
                    sqlParameter.AddWithValue("@ProjectNumber", DBNull.Value);
                    sqlParameter.AddWithValue("@Date", DBNull.Value);
                    sqlParameter.AddWithValue("@Size", DBNull.Value);
                    sqlParameter.AddWithValue("@Profile", DBNull.Value);

                    
                    sqlParameter.AddWithValue("@SupplyTotalStaticPressure", DBNull.Value);
                    sqlParameter.AddWithValue("@SupplyStaticPressure", DBNull.Value);
                    sqlParameter.AddWithValue("@SupplyAirflow", DBNull.Value);
                    sqlParameter.AddWithValue("@ExhaustTotalStaticPressure", DBNull.Value);
                    sqlParameter.AddWithValue("@ExhaustStaticPressure", DBNull.Value);
                    sqlParameter.AddWithValue("@ExhaustAirflow", DBNull.Value);


                    sqlParameter.AddWithValue("@ServiceAccess", DBNull.Value);
                    sqlParameter.AddWithValue("@InternalNumber", DBNull.Value);

                    sqlParameter.AddWithValue("@RequiredDate", DBNull.Value);
                    sqlParameter.AddWithValue("@ShippedDate", DBNull.Value);
                    sqlParameter.AddWithValue("@CompletionDate", DBNull.Value);
                    sqlParameter.AddWithValue("@FinishCompletionDate", DBNull.Value);
                    
                    sqlParameter.AddWithValue("@ConstructorID", DBNull.Value);
                    sqlParameter.AddWithValue("@IDnomenclature", DBNull.Value);
                    
                    sqlParameter.AddWithValue("@Quantity", DBNull.Value);
                    
                    sqlParameter.AddWithValue("@IDBOM", idBom);

                    sqlCommand.ExecuteNonQuery();

                }
                catch (Exception exception)
                {
                    MessageBox.Show("Введите корректные данные!\n" + exception.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }
        


        public static void DelOrderAirVents(int orderId)
        {
            using (var con = new SqlConnection(App.ConString))
            {                
                try
                {
                    con.Open();
                    var sqlCommand = new SqlCommand("[AirVents].[CUDOrderAirVents]", con) { CommandType = CommandType.StoredProcedure };

                    var sqlParameter = sqlCommand.Parameters;
                    
                    sqlParameter.AddWithValue("@OrderID", orderId);

                    sqlParameter.AddWithValue("@PARAM", 3);
                    sqlParameter.AddWithValue("@Manager", DBNull.Value);
                    sqlParameter.AddWithValue("@ProjectNumber", DBNull.Value);
                    sqlParameter.AddWithValue("@Date", DBNull.Value);
                    sqlParameter.AddWithValue("@Size", DBNull.Value);
                    sqlParameter.AddWithValue("@Profile", DBNull.Value);

                    sqlParameter.AddWithValue("@SupplyTotalStaticPressure", DBNull.Value);
                    sqlParameter.AddWithValue("@SupplyStaticPressure", DBNull.Value);
                    sqlParameter.AddWithValue("@SupplyAirflow", DBNull.Value);
                    sqlParameter.AddWithValue("@ExhaustTotalStaticPressure", DBNull.Value);
                    sqlParameter.AddWithValue("@ExhaustStaticPressure", DBNull.Value);
                    sqlParameter.AddWithValue("@ExhaustAirflow", DBNull.Value);

                    sqlParameter.AddWithValue("@ServiceAccess", DBNull.Value);
                    sqlParameter.AddWithValue("@InternalNumber", DBNull.Value);

                    sqlParameter.AddWithValue("@RequiredDate", DBNull.Value);
                    sqlParameter.AddWithValue("@ShippedDate", DBNull.Value);
                    sqlParameter.AddWithValue("@CompletionDate", DBNull.Value);
                    
                    sqlParameter.AddWithValue("@FinishCompletionDate", DBNull.Value);


                    sqlParameter.AddWithValue("@ConstructorID", DBNull.Value);
                    sqlParameter.AddWithValue("@IDnomenclature", DBNull.Value);
                    sqlParameter.AddWithValue("@Quantity", DBNull.Value);
                    sqlParameter.AddWithValue("@IDBOM", DBNull.Value);

                    //var returnParameter = sqlCommand.Parameters.Add("@OrderID", SqlDbType.Int);
                    //returnParameter.Direction = ParameterDirection.ReturnValue;

                    sqlCommand.ExecuteNonQuery();

                    //Int32.TryParse(returnParameter.Value.ToString(), out newOrderId);

                }
                catch (Exception exception)
                {
                    MessageBox.Show("Введите корректные данные!\n" + exception.Message);                    
                }
                finally
                {
                    con.Close();
                }                
            }
        }

        public static int AirVents_AddOrder(int managerId, int sizeId, string projectNumber, string profile, DateTime? dateOrder, int? supplyTotalStaticPressure,
            int? supplyStaticPressure, double? supplyAirflow, int? exhaustTotalStaticPressure, int? exhaustStaticPressure, double? exhaustAirflow, bool? isLeftSide)
        {
            int orderId;
            using (var con = new SqlConnection(App.ConString))
            {
                try
                {
                    con.Open();
                    var sqlCommand = new SqlCommand("AirVents_AddOrder", con) { CommandType = CommandType.StoredProcedure };

                    var sqlParameter = sqlCommand.Parameters;
                 
                    sqlParameter.AddWithValue("@Manager", managerId);
                    sqlParameter.AddWithValue("@Size", sizeId);
                    sqlParameter.AddWithValue("@ProjectNumber", projectNumber);
                    sqlParameter.AddWithValue("@Profile", profile);

                    sqlParameter.AddWithValue("@Date", dateOrder);

                    #region ПроизводительностьПриток

                    if (supplyTotalStaticPressure == null)
                    {
                        sqlParameter.AddWithValue("@SupplyTotalStaticPressure", DBNull.Value);
                    }
                    else
                    {
                        sqlParameter.AddWithValue("@SupplyTotalStaticPressure", supplyTotalStaticPressure);
                    }

                    #endregion

                    #region ПолноеСтатическоеДавлениеВентилятораПриток

                    if (supplyStaticPressure == null)
                    {
                        sqlParameter.AddWithValue("@SupplyStaticPressure", DBNull.Value);
                    }
                    else
                    {
                        sqlParameter.AddWithValue("@SupplyStaticPressure", supplyStaticPressure);
                    }

                    #endregion

                    #region СкоростьВСеченииПриток

                    if (supplyAirflow == null)
                    {
                        sqlParameter.AddWithValue("@SupplyAirflow", DBNull.Value);
                    }
                    else
                    {
                        sqlParameter.AddWithValue("@SupplyAirflow", supplyAirflow);
                    }

                    #endregion

                    #region ПроизводительностьВытяжка

                    if (exhaustTotalStaticPressure == null)
                    {
                        sqlParameter.AddWithValue("@ExhaustTotalStaticPressure", DBNull.Value);
                    }
                    else
                    {
                        sqlParameter.AddWithValue("@ExhaustTotalStaticPressure", exhaustTotalStaticPressure);
                    }

                    #endregion

                    #region ПолноеСтатическоеДавлениеВентилятораВитяжка

                    if (exhaustStaticPressure == null)
                    {
                        sqlParameter.AddWithValue("@ExhaustStaticPressure", DBNull.Value);
                    }
                    else
                    {
                        sqlParameter.AddWithValue("@ExhaustStaticPressure", exhaustStaticPressure);
                    }

                    #endregion

                    #region СкоростьВСеченииВитяжка

                    if (exhaustAirflow == null)
                    {
                        sqlParameter.AddWithValue("@ExhaustAirflow", DBNull.Value);
                    }
                    else
                    {
                        sqlParameter.AddWithValue("@ExhaustAirflow", exhaustAirflow);
                    }

                    #endregion

                    sqlParameter.AddWithValue("@ServiceAccess", isLeftSide);

                    var returnParameter = sqlCommand.Parameters.Add("@OrderID", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    sqlCommand.ExecuteNonQuery();
                    Int32.TryParse(returnParameter.Value.ToString(), out orderId);
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Введите корректные данные!\n" + exception.Message);
                    return 0;
                }
                finally
                {
                    con.Close();
                }
            }
            return orderId;
        }

        public static bool AirVent_Edit_Order(int orderId, int managerId, int sizeId, string projectNumber, string profile, DateTime dateOrder, int? supplyTotalStaticPressure,
            int? supplyStaticPressure, double? supplyAirflow, int? exhaustTotalStaticPressure, int? exhaustStaticPressure, double? exhaustAirflow, bool isLeftSide)
        {
            using (var con = new SqlConnection(App.ConString))
            {
                try
                {
                    con.Open();
                    var sqlCommand = new SqlCommand("AirVent_Edit_Order", con) { CommandType = CommandType.StoredProcedure };

                    var sqlParameter = sqlCommand.Parameters;

                    sqlParameter.AddWithValue("@OrderID", orderId);

                    sqlParameter.AddWithValue("@Manager", managerId);
                    sqlParameter.AddWithValue("@Size", sizeId);
                    sqlParameter.AddWithValue("@ProjectNumber", projectNumber);
                    sqlParameter.AddWithValue("@Profile", profile);

                    sqlParameter.AddWithValue("@Date", dateOrder);

                    #region ПроизводительностьПриток

                    if (supplyTotalStaticPressure == null)
                    {
                        sqlParameter.AddWithValue("@SupplyTotalStaticPressure", DBNull.Value);
                    }
                    else
                    {
                        sqlParameter.AddWithValue("@SupplyTotalStaticPressure", supplyTotalStaticPressure);
                    }

                    #endregion

                    #region ПолноеСтатическоеДавлениеВентилятораПриток

                    if (supplyStaticPressure == null)
                    {
                        sqlParameter.AddWithValue("@SupplyStaticPressure", DBNull.Value);
                    }
                    else
                    {
                        sqlParameter.AddWithValue("@SupplyStaticPressure", supplyStaticPressure);
                    }

                    #endregion

                    #region СкоростьВСеченииПриток

                    if (supplyAirflow == null)
                    {
                        sqlParameter.AddWithValue("@SupplyAirflow", DBNull.Value);
                    }
                    else
                    {
                        sqlParameter.AddWithValue("@SupplyAirflow", supplyAirflow);
                    }

                    #endregion

                    #region ПроизводительностьВытяжка

                    if (exhaustTotalStaticPressure == null)
                    {
                        sqlParameter.AddWithValue("@ExhaustTotalStaticPressure", DBNull.Value);
                    }
                    else
                    {
                        sqlParameter.AddWithValue("@ExhaustTotalStaticPressure", exhaustTotalStaticPressure);
                    }

                    #endregion

                    #region ПолноеСтатическоеДавлениеВентилятораВитяжка

                    if (exhaustStaticPressure == null)
                    {
                        sqlParameter.AddWithValue("@ExhaustStaticPressure", DBNull.Value);
                    }
                    else
                    {
                        sqlParameter.AddWithValue("@ExhaustStaticPressure", exhaustStaticPressure);
                    }

                    #endregion

                    #region СкоростьВСеченииВитяжка

                    if (exhaustAirflow == null)
                    {
                        sqlParameter.AddWithValue("@ExhaustAirflow", DBNull.Value);
                    }
                    else
                    {
                        sqlParameter.AddWithValue("@ExhaustAirflow", exhaustAirflow);
                    }

                    #endregion

                    sqlParameter.AddWithValue("@ServiceAccess", isLeftSide);

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
                }
            }
            return true;
        }

        public static bool AirVent_Delete_Order(int orderId)
        {
            using (var con = new SqlConnection(App.ConString))
            {
                try
                {
                    con.Open();
                    var sqlCommand = new SqlCommand("AirVent_Delete_Order", con) { CommandType = CommandType.StoredProcedure };
                    sqlCommand.Parameters.Add("@OrderID", SqlDbType.Int).Value = orderId;
                    sqlCommand.ExecuteNonQuery();
                }
                catch
                {
                    MessageBox.Show("Возможная причина: наличие заказа по данному подбору.", "Не удалось удалить подбор!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return false;
                }
                finally
                {
                    con.Close();
                }
            }
            return true;
        }
        
        public static bool AirVents_AddOrderDetail(string internalNumber, DateTime? requiredDate, DateTime? shippedDate, DateTime? completionDate,
            DateTime? finishCompletionDate, int orderId, int constructorId)
        {
            using (var con = new SqlConnection(App.ConString))
            {
                try
                {
                    con.Open();
                    var sqlCommand = new SqlCommand("AirVent_Add_OrderDetails", con) { CommandType = CommandType.StoredProcedure };

                    sqlCommand.Parameters.Add("@InternalNumber", SqlDbType.Char).Value = internalNumber;

                    sqlCommand.Parameters.Add("@RequiredDate", SqlDbType.Date).Value = requiredDate;
                    sqlCommand.Parameters.Add("@ShippedDate", SqlDbType.Date).Value = shippedDate;
                    sqlCommand.Parameters.Add("@CompletionDate", SqlDbType.Date).Value = completionDate;

                    #region ФактическаяСдачаКд

                    if (finishCompletionDate != null)
                    {
                        sqlCommand.Parameters.AddWithValue("@FinishCompletionDate", finishCompletionDate);
                    }
                    else
                    {
                        sqlCommand.Parameters.AddWithValue("@FinishCompletionDate", DBNull.Value);
                    }

                    #endregion

                    sqlCommand.Parameters.Add("@OrderID", SqlDbType.Int).Value = orderId;
                    sqlCommand.Parameters.Add("@ConstructorID", SqlDbType.Int).Value = constructorId;
                    sqlCommand.ExecuteNonQuery();
                }
                catch
                {
                    return false;
                }
                finally
                {
                    con.Close();
                }
            }
            return true;
        }

        public static bool AirVent_Edit_OrderDetails(int orderDetailId, DateTime? requiredDate, DateTime? shippedDate, DateTime? completionDate,
            DateTime? finishCompletionDate, int orderId, int constructorId)
        {
            using (var con = new SqlConnection(App.ConString))
            {
                try
                {
                    con.Open();
                    var sqlCommand = new SqlCommand("AirVent_Edit_OrderDetails", con) { CommandType = CommandType.StoredProcedure };

                    sqlCommand.Parameters.Add("@OrderDetailID", SqlDbType.Int).Value = orderDetailId;

                    sqlCommand.Parameters.Add("@RequiredDate", SqlDbType.Date).Value = requiredDate;
                    sqlCommand.Parameters.Add("@ShippedDate", SqlDbType.Date).Value = shippedDate;
                    sqlCommand.Parameters.Add("@CompletionDate", SqlDbType.Date).Value = completionDate;

                    #region ФактическаяСдачаКд

                    if (finishCompletionDate != null)
                    {
                        sqlCommand.Parameters.AddWithValue("@FinishCompletionDate", finishCompletionDate);
                    }
                    else
                    {
                        sqlCommand.Parameters.AddWithValue("@FinishCompletionDate", DBNull.Value);
                    }

                    #endregion

                    sqlCommand.Parameters.Add("@OrderID", SqlDbType.Int).Value = orderId;
                    sqlCommand.Parameters.Add("@ConstructorID", SqlDbType.Int).Value = constructorId;

                    sqlCommand.ExecuteNonQuery();
                }
                catch
                {
                    return false;
                }
                finally
                {
                    con.Close();
                }
            }
            return true;
        }

        public static bool AirVent_Delete_OrderDetails(int orderDetailId)
        {
            using (var con = new SqlConnection(App.ConString))
            {
                try
                {
                    con.Open();
                    var sqlCommand = new SqlCommand("AirVent_Delete_OrderDetails", con) { CommandType = CommandType.StoredProcedure };
                    sqlCommand.Parameters.Add("@OrderDeteilID", SqlDbType.Int).Value = orderDetailId;
                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                    MessageBox.Show("Не удалось удалить заказ!");
                    return false;
                }
                finally
                {
                    con.Close();
                }
            }
            return true;
        }

        #endregion

    }
}
