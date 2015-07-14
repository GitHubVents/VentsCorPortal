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

        public class Manager
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int IdManager { get; set; }
        }

        public static List<Manager> ManagersList()
        {
            var table = ManagersTable();
            var list = (from DataRow row in table.Rows
                        select new Manager
                        {
                            FirstName = row["FirsrtName"].ToString(),
                            LastName = row["LastName"].ToString(),
                            IdManager = Convert.ToInt32(row["EmpID"])
                        }).ToList();
            return list;
        }

        public static DataTable ManagersTable()
        {
            var managersList = new DataTable();
            using (var con = new SqlConnection(App.ConString))
            {
                try
                {
                    con.Open();
                    var sqlCommand = new SqlCommand("SELECT * FROM  HumanResources.Employee ORDER BY LastName", con);
                    var sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    sqlDataAdapter.Fill(managersList);
                    sqlDataAdapter.Dispose();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Ошибка выгрузки данных из базы ManagersTable");
                }
                finally
                {
                    con.Close();
                }
            }
            return managersList;
        }

        public static bool AddManager(string userName, string lastName)
        {
            using (var con = new SqlConnection(App.ConString))
            {
                try
                {
                    con.Open();
                    var sqlCommand =
                        new SqlCommand("INSERT into HumanResources.Employee (LastName, FirsrtName) VALUES " + "('" + lastName + "','" + userName + "')", con);
                    var sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    var dataTable = new DataTable("Username");
                    sqlDataAdapter.Fill(dataTable);
                }
                catch (Exception)
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

        public static void UpdateManager(string userName, string lastName, int empId)
        {
            using (var con = new SqlConnection(App.ConString))
            {
                try
                {
                    con.Open();
                    var sqlCommand =
                       new SqlCommand(@"UPDATE HumanResources.Employee 
SET
LastName = '" + lastName + "'," +
"FirsrtName = '" + userName + "'" +
"WHERE EmpID = " + empId, con);
                    var sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    var dataTable = new DataTable("Username");
                    sqlDataAdapter.Fill(dataTable);
                    sqlDataAdapter.Dispose();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }

        public static void DeleteManager(int empId)
        {
            using (var con = new SqlConnection(App.ConString))
            {
                try
                {
                    con.Open();
                    var sqlCommand =
                       new SqlCommand(@"DELETE FROM  HumanResources.Employee " +
"WHERE EmpID = " + empId, con);
                    var sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    var dataTable = new DataTable("Username");
                    sqlDataAdapter.Fill(dataTable);
                    sqlDataAdapter.Dispose();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }

    }
}
