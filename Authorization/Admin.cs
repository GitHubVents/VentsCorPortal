using System;
using System.DirectoryServices;
using System.Linq;
using System.Windows;

namespace Authorization
{
    public static class Admin
    {
        static public string UserName { get; set; }
        static private string Password { get; set; }

        static public bool AuthenticationOk { get; set; }

        static public bool AuthenticateUser(string userName, string password)
        {
            bool ret;
            try
            {
                UserName = userName;
                Password = password;

                var directoryEntry = new DirectoryEntry("LDAP://" + "vents.local", UserName, Password);
                var directorySearcher = new DirectorySearcher(directoryEntry);
                directorySearcher.FindOne();
                ret = true;
            }
            catch
            {
                ret = false;
                
            }
            AuthenticationOk = ret;
            return ret;
        }

        static readonly DataUser Du = new DataUser();
        static public bool CheckUsers(int actionCode)
        {
            var q = Du.ListAction().Where(x => x.UserName.ToLower() == UserName.ToLower() & x.ActionCode == actionCode).Select(x => x.Sign);

            return Convert.ToBoolean(q.Single());
        }
    }
}