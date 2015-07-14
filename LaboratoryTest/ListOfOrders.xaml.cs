namespace LaboratoryTest
{
    /// <summary>
    /// Interaction logic for ListOfOrders.xaml
    /// </summary>
    public partial class ListOfOrders
    {
        public ListOfOrders()
        {
            InitializeComponent();
        }

        readonly DataClass _dc = new DataClass();

        private void GridListOfOrders_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            DgListOfOrders.ItemsSource = _dc.ListOfOrders();
        }

        private void BtnNewProtocol_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var protControl = new prot();

            protControl.ShowDialog();
        }

    }
}
