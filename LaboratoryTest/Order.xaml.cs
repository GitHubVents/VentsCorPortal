using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Authorization;
using System.Net;
using System.Net.Mail;
using Outlook = Microsoft.Office.Interop.Outlook;


namespace LaboratoryTest
{
    /// <summary>
    /// Interaction logic for Order.xaml
    /// </summary>
    public partial class Order
    {
        public Order()
        {
            InitializeComponent();
            OrderCreateLabel.Content = DataUser.GetLoginedUserName;
        }

        readonly DataClass _dc = new DataClass();
 
        private void OrderForm_Loaded(object sender, RoutedEventArgs e)
        {
            var date = DateTime.Now;

            OrderLabel.Content = "Заявка на проведение испытаний №" + "  от " + date.ToShortDateString();

            DgLab.ItemsSource = ListMotorFanLab();

        }

        private void DgLab_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = Convert.ToString((e.Row.GetIndex() + 1));
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        public void SendMessage()
        {
            try
            {
                //var oApp = new Outlook.Application();

                //var oMsg = (Outlook.MailItem)oApp.CreateItem(Outlook.OlItemType.olMailItem);

                //var oRecip = (Outlook.Recipient)oMsg.Recipients.Add("n.chaikin@vents.kiev.ua");
                //oRecip.Resolve();

                //oMsg.Subject = "Тема";
                //oMsg.Body = "Ку!";

                ////var source = "C:\\setupxlg.txt"; // Вложенный файл
                ////var displayName = "MyFirstAttachment";
                ////var position = (int)oMsg.Body.Length + 1;
                ////var attachType = (int)Outlook.OlAttachmentType.olByValue;  

                //////var oAttach = oMsg.Attachments.Add(source, displayName, position, attachType);
                //oMsg.Save();
                //oMsg.Send();

  
                var mail = new MailMessage("d.orel@vents.kiev.ua", "n.chaikin@vents.kiev.ua");
                var client = new SmtpClient();
                client.Port = 26;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Host = "192.168.12.2";

                mail.Subject = "this is a test email.";
                mail.Body = "this is my test email body";
                client.Send(mail);
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("{0}", ex));
            }
        }

        public void AddOrder()
        {
            var itemIdTData = (DgCatalogRow)DgLab.SelectedItem;
            var itemCapacitor = (DgCatalogRow)DgLab.SelectedItem;

            var idTData = itemIdTData.IdTData;
            var capacitor = itemCapacitor.Mkf;
            var typeItem = "";

            if (RbSerialSample.IsChecked == true)
            {
                typeItem = "0";
            }
            if (RbExperiencedSample.IsChecked == true)
            {
                typeItem = "1";
            }
            if (RbPosledorabotka.IsChecked == true)
            {
                typeItem = "2";
            }

            //RbSerialSample.Content 
            //RbExperiencedSample
            //RbPosledorabotka
            //RbEstablished
            //Rb5NoEstablished

            var con = new SqlConnection(_dc.ConString);
            con.Open();

            var spcmd = new SqlCommand("[Lab].[AddOrder]", con) { CommandType = CommandType.StoredProcedure };

            spcmd.Parameters.AddWithValue("@IDTData", Convert.ToInt32(idTData));
            spcmd.Parameters.AddWithValue("@TypeItem", typeItem);
            spcmd.Parameters.AddWithValue("@InfoItem", TxtInfoItem.Text);
            spcmd.Parameters.AddWithValue("@TestProcedure", DescMethodOfTest.Text);
            spcmd.Parameters.AddWithValue("@Note", TxtNote.Text);
            spcmd.Parameters.AddWithValue("@Capacitor", capacitor);
            spcmd.Parameters.AddWithValue("@UserName", OrderCreateLabel.Content);

            spcmd.ExecuteNonQuery();

            con.Close();
        }

        public class DgCatalogRow
        {
            public string FanName { get; set; }
            public string Volt { get; set; }
            public string Frequency { get; set; }
            public string MotorName { get; set; }
            public string Impeller { get; set; }
            public string Mkf { get; set; }
            public string IdTData { get; set; }
        }
        public DataTable DtMotorFan { get; set; }
        private IEnumerable<DgCatalogRow> ListMotorFanLab()
        {
            var listMotorStr = new List<DgCatalogRow>();

            foreach (DataRow r in DtMotorFan.Rows)
            {
                var dgCatalogRow = new DgCatalogRow
                {
                    FanName = r["FanName"].ToString(),
                    Volt = r["Volt"].ToString(),
                    Frequency = r["Frequency"].ToString(),
                    MotorName = r["MotorName"].ToString(),
                    Impeller = r["Impeller"].ToString(),
                    IdTData = r["IdTData"].ToString()
                };

                listMotorStr.Add(dgCatalogRow);
            }

            return listMotorStr;
        }

    }
}