using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;

namespace Authorization
{
    /// <summary>
    /// Interaction logic for UserNameList.xaml
    /// </summary>
    public partial class UserNameList
    {
        public UserNameList()
        {
            InitializeComponent();
        }

        readonly DataUser _du = new DataUser();

        public class CheckBoxListItem
        {
            public bool Checked { get; set; }
            public string UserName { get; set; }
            public string UserId { get; set; }
            public string FullName { get; set; }

            public CheckBoxListItem(bool ch, string userName, string userId, string fullName)
            {
                Checked = ch;
                UserName = userName;
                UserId = userId;
                FullName = fullName;


            }
        }

        public int GroupId { get; set; }

        private void GridUsersNameList_Loaded(object sender, RoutedEventArgs e)
        {
            var itemsListViewUsers = _du.ListUser().Select(items =>
                new CheckBoxListItem(false, items.UserName, items.UserId, items.FullName)).ToList();
                ListViewAllUsers.ItemsSource = itemsListViewUsers;

        }

        public List<CheckBoxListItem> ReturnCheckedItems()
        {
            return ListViewAllUsers.SelectedItems.Cast<CheckBoxListItem>().ToList();
        }

        private Administation FormAdmin;

        // Add user to groupe
        private void BtnAddUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            var con = new SqlConnection(DataUser.ConString);

            con.Open();

            foreach (var userId in ReturnCheckedItems())
            {
                var q = @"INSERT INTO ACL.GroupMembers (IDGroup, IDUser ) VALUES ( " + GroupId + @", " + userId.UserId + @")";

                var cmd = new SqlCommand(q, con);
                cmd.ExecuteNonQuery();
            }

            Close();

            con.Close();

            FormAdmin = new Administation();
            FormAdmin.ListViewUsers.ItemsSource = _du.ListUserGroups(GroupId);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

          
        }

   
    }
}
