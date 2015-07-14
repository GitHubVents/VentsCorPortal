using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Authorization
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class  Administation 
    {
        public Administation()
        {
            InitializeComponent();
        }

        readonly DataUser _du = new DataUser();

        public class CheckBoxListItem
        {
            public bool Sign { get; set; }
            public string ActionName { get; set; }
            public int GroupId { get; set; }

            public CheckBoxListItem(bool sign, string actionName, int groupId)
            {
                Sign = sign;
                ActionName = actionName;
                GroupId = groupId;
            }
        }

        //public class CheckBoxListItem
        //{
        //    public string ActionName { get; set; }
      
        //    public CheckBoxListItem(string actionName)
        //    {
        //        ActionName = actionName;
        //    }
        //}

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {

                ListViewGroup.ItemsSource = _du.ListGroup();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Add new group
        private void BtnAddGroup_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            var con = new SqlConnection(DataUser.ConString);

            var q = @"insert into ACL.Groups (Groupname) Values ( '" + TxtGroupName.Text + @"' )";

            var cmd = new SqlCommand(q, con);

            cmd.Connection.Open();

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();

            ListViewGroup.ItemsSource = _du.ListGroup();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        // Delete group
        private void BtnDeleteGroup_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var item = (DataUser.GroupClass)
                ListViewGroup.SelectedItem;

                var idGroup = item.GroupId;

                var con = new SqlConnection(DataUser.ConString);

                var q = @"DELETE ACL.Groups WHERE GroupID = " + idGroup;

                var cmd = new SqlCommand(q, con);

                cmd.Connection.Open();

                cmd.ExecuteNonQuery();

                cmd.Connection.Close();

                ListViewGroup.ItemsSource = _du.ListGroup();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        // Add user to group
        private void BtnAddUserToGroup_Click(object sender, RoutedEventArgs e)
        {
            var item = (DataUser.GroupClass)ListViewGroup.SelectedItem;

            if (item != null)
            {
                var idGroup = item.GroupId;
                var userNameListWin = new UserNameList();

                userNameListWin.GroupId = Convert.ToInt32(idGroup);

                userNameListWin.Show();
            }
            else
            {
                MessageBox.Show("Выберите группу!");
            }

        }

        // Delete user from group
        private void BtnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            var useritem = (DataUser.UserClass)ListViewUsers.SelectedItem;
            var groupitem = (DataUser.GroupClass)ListViewGroup.SelectedItem;
           
                var idUser = useritem.UserId;
                var idGroup = groupitem.GroupId;

                var con = new SqlConnection(DataUser.ConString);

                var q = @"DELETE ACL.GroupMembers WHERE IDGroup  = " + idGroup + @"AND IDUser =" + idUser;

                var cmd = new SqlCommand(q, con);

                cmd.Connection.Open();

                cmd.ExecuteNonQuery();


                cmd.Connection.Close();

                ListViewUsers.ItemsSource = _du.ListUserGroups(idGroup);

     
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }
        
        private void ListViewGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var itemGroupe = (DataUser.GroupClass)ListViewGroup.SelectedItem;

                var idGroup = itemGroupe.GroupId;

                ListViewUsers.ItemsSource = _du.ListUserGroups(idGroup);

                //var selected = _du.ListAction().Where(x => x.UserName == "Kb33").Count() ; //Where(x => x.UserName == "kb33");
                //var selected = _du.ListAction().OrderBy(x => x.GroupID == idGroup).ThenBy(x => x.UserName == "Kb33").ToList();

                var itemsListViewAction = _du.ListAction().Select(items =>
                    new CheckBoxListItem(Convert.ToBoolean(items.Sign), items.ActionName, items.GroupId)).Where(x =>
                        x.GroupId == idGroup).GroupBy(items =>
                            items.ActionName);
                ListViewAction.ItemsSource = itemsListViewAction;


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }


        public List<CheckBoxListItem> ReturnCheckedItems()
        {
            return ListViewAction.SelectedItems.Cast<CheckBoxListItem>().ToList();
        }

        private void BtnSaveEditAction_Click(object sender, RoutedEventArgs e)
        {

            if (ReturnCheckedItems() == null) return;

            foreach (var action in ReturnCheckedItems())
            {
                MessageBox.Show(action.ActionName);



            }
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            //Properties.Settings.Default.RememberMe = false;
           // Properties.Settings.Default.Password = "";
            Properties.Settings.Default.Save();

            var authentForm = new Administation();
            authentForm.Show();

            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //AuthForm = new Authenticated();
            //var LoginName = AuthForm.LoginUser;
            //MessageBox.Show(LoginName);
            //var selected = _du.ListAction().Count(x => x.UserName == LoginName);


            MessageBox.Show(Admin.CheckUsers(10).ToString());


        }

      
    }
}
