using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlClient;

namespace Authorization
{
    public class DataUser
    {
        public const string ConString = @"Data Source=192.168.14.11;Initial Catalog=SWPlusDB;Persist Security Info=True;User ID=sa;Password=PDMadmin; Pooling=True";

        public class UserClass
        {
            public string UserName { get; set; }
            public string FullName { get; set; }
            public string UserId { get; set; }
 
        }

        public List<UserClass> ListUser()
        {
            var listString = new List<UserClass>();

            var con = new SqlConnection(ConString);

            con.Open();

            var da = new SqlDataAdapter();
            var ds = new DataSet();
            var dt = new DataTable();
            var cmd = new SqlCommand();

            ds.Tables.Add(dt);
            cmd.CommandText = "SELECT UserID, Username, FullName FROM ACL.Users ORDER BY Username";

            cmd.Connection = con;
            da.SelectCommand = cmd;
            da.Fill(dt);

            foreach (DataRow datarow in dt.Rows)
            {
                var columnValue = new UserClass
                {
                    UserId = datarow["UserID"].ToString(),
                    UserName = datarow["Username"].ToString(),
                    FullName = datarow["FullName"].ToString()
                  
                };

                listString.Add(columnValue);
            }
            
            con.Close();

            return listString;

        }

        public class GroupClass
        {
            public string GroupName { get; set; }
            public int GroupId { get; set; }
        }
        public List<GroupClass> ListGroup()
        {

            var listString = new List<GroupClass>();

            var con = new SqlConnection(ConString);

            con.Open();

            var da = new SqlDataAdapter();
            var ds = new DataSet();
            var dt = new DataTable();
            var cmd = new SqlCommand();

            ds.Tables.Add(dt);
            cmd.CommandText = "SELECT GroupID, Groupname FROM ACL.Groups";

            cmd.Connection = con;
            da.SelectCommand = cmd;
            da.Fill(dt);

            foreach (DataRow datarow in dt.Rows)
            {
                var columnValue = new GroupClass
                {
                    GroupName = datarow["GroupName"].ToString(),
                    GroupId = Convert.ToInt32(datarow["GroupID"])
                };

                listString.Add(columnValue);
            }

            con.Close();

            return listString;

        }
        public List<UserClass> ListUserGroups(int idGroup)
        {
            
            var listString = new List<UserClass>();

            var con = new SqlConnection(ConString);
            var da = new SqlDataAdapter();
            var ds = new DataSet();
            var dt = new DataTable();
            var cmd = new SqlCommand();

            con.Open();

            ds.Tables.Add(dt);
            cmd.CommandText = @"SELECT U.Username, U.UserID
                                FROM ACL.GroupMembers INNER JOIN
                                ACL.Users AS U ON ACL.GroupMembers.IDUser = U.UserID INNER JOIN
                                ACL.Groups AS G ON ACL.GroupMembers.IDGroup = G.GroupID
                                WHERE G.GroupID =" + idGroup;

            cmd.Connection = con;
            da.SelectCommand = cmd;
            da.Fill(dt);

            foreach (DataRow datarow in dt.Rows)
            {
                var columnValue = new UserClass
                {
                    UserName = datarow["Username"].ToString(),
                    UserId = datarow["UserID"].ToString()

                };
                listString.Add(columnValue);
            }

            con.Close();

            return listString;
        }

        public class ActionClass 
        {
            public string Sign { get; set; }
            public string ActionName { get; set; }
            public string UserName { get; set; }
            public int GroupId { get; set; }
            public int ActionCode { get; set; }

        }

        public List<ActionClass> ListAction()
        {
            var con = new SqlConnection(ConString);

            con.Open();

            var da = new SqlDataAdapter();
            var ds = new DataSet();
            var dt = new DataTable();
            var cmd = new SqlCommand();

            ds.Tables.Add(dt);

//            cmd.CommandText = @"SELECT R.Sign, ACL.ActionName.ActionCode, ACL.ActionName.ActionName, U.Username, G.GroupID
//                                FROM ACL.ActionName LEFT OUTER JOIN
//                                ACL.ActionRights AS R ON ACL.ActionName.IDActionName = R.IDActionName LEFT OUTER JOIN
//                                ACL.Groups AS G ON R.GroupID = G.GroupID LEFT OUTER JOIN
//                                ACL.GroupMembers AS M ON G.GroupID = M.IDGroup LEFT OUTER JOIN
//                                ACL.Users AS U ON M.IDUser = U.UserID";
//                                //WHERE Groups.GroupID = " + groupId;


            cmd.CommandText = @"  SELECT        AN.ActionCode, R.Sign, U.Username, AN.ActionName
                        FROM            ACL.ActionName AS AN INNER JOIN
                         ACL.ActionRights AS R ON AN.IDActionName = R.IDActionName INNER JOIN
                         ACL.Users AS U ON R.UserID = U.UserID";

            cmd.Connection = con;
            da.SelectCommand = cmd;
            da.Fill(dt);

            var listString = (from DataRow datarow in dt.Rows select new ActionClass
            {
                //Sign = (datarow["Sign"] is DBNull) ? null : datarow["Sign"].ToString(),
                //ActionName = (datarow["ActionName"] is DBNull) ? null : datarow["ActionName"].ToString(),
                //GroupId = (datarow["GroupID"] is DBNull) ? 0 : Convert.ToInt32(datarow["GroupID"]),
                //UserName = (datarow["Username"] is DBNull) ? null : datarow["Username"].ToString(),
                //ActionCode = (datarow["ActionCode"] is DBNull) ? 0 : Convert.ToInt32(datarow["ActionCode"])


                Sign = datarow["Sign"].ToString(),

                ActionName = datarow["ActionName"].ToString(),

                //GroupId = Convert.ToInt32(datarow["GroupID"]),

                UserName = datarow["Username"].ToString(),

                ActionCode = Convert.ToInt32(datarow["ActionCode"])

            }).ToList();

            con.Close();

            return listString;
        }

        private static string GetLoginedUser()
        {
            var du = new DataUser();
            var q = from x in du.ListUser() where x.UserName.ToLower() == Admin.UserName.ToLower() select x.FullName;

            return q.Single();
        }

        public static string GetLoginedUserName
        {
            get { return GetLoginedUser(); }
        }
    }
}