using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace AirVentsOrderManager.Model
{
    public partial class OrderData
    {
        public static DataTable AirVentsStandardSize()
        {
            var standartSizeTable = new DataTable();
            using (var con = new SqlConnection(App.ConString))
            {
                try
                {
                    con.Open();
                    var sqlCommand = new SqlCommand("SELECT * FROM  AirVents.StandardSize ORDER BY Type", con); // ORDER BY SizeID
                    var sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    sqlDataAdapter.Fill(standartSizeTable);
                    sqlDataAdapter.Dispose();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Ошибка выгрузки данных из базы AirVentsStandardSize");
                }
                finally
                {
                    con.Close();
                }
            }
            return standartSizeTable;
        }

        public static DataTable ProfilSizeTable(int idSize)
        {
            var profilSizeTable = new DataTable();
            try
            {
                using (var con = new SqlConnection(App.ConString))
                {
                    try
                    {
                        con.Open();
                        var sqlCommand = new SqlCommand(@"SELECT AirVents.Profil.Description, AirVents.Profil.ProfilID FROM AirVents.DimensionType INNER JOIN
                      AirVents.Profil ON AirVents.DimensionType.ProfilID = AirVents.Profil.ProfilID INNER JOIN
                      AirVents.StandardSize ON AirVents.DimensionType.SizeID = AirVents.StandardSize.SizeID
                      WHERE AirVents.DimensionType.SizeID = " + idSize, con);
                        var sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                        sqlDataAdapter.Fill(profilSizeTable);
                        sqlDataAdapter.Dispose();
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
            return profilSizeTable;
        }
    }
}
