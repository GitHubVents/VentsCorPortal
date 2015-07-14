using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmokeExtraction
{
    /// <summary>
    /// Interaction logic for SmokeExtractionMainUC.xaml
    /// </summary>
    public partial class SmokeExtractionMainUc : UserControl
    {
        public SmokeExtractionMainUc()
        {
            InitializeComponent();
        }

        #region " VARIABLES "
            private const int ColSetId1 = 16;   // Крышный центробежный вентилятор дымоудаления
            private const int ColSetId2 = 16;   // Крышный вытяжной каминный вентилятор для усиления тяги вытяжки дымовых газов
            private const int ColSetId3 = 5;    // Клапан противопожарный дымовой универсальный
            private const int ColSetId4 = 15;   // Клапан протипожарный огнезадерживающий
        #endregion

        #region " LOAD "

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            Rb1.IsChecked = true;
            PopulateDataGrid(ColSetId1);
        }

        public void PopulateDataGrid(int rbValue)
        {
            DataGridSmoke.ItemsSource = null;
            DataGridSmoke.Columns.Clear();

            //MessageBox.Show(rbValue.ToString());

            var dl = new DataLoad();

            var motorList = dl.ListColumn().Where(x => x.IdNomenclatureGroup == rbValue).ToList();

            var listOrderByNumber = motorList.OrderBy(x => x.Number).ToList();

            foreach (var columnName in listOrderByNumber)
            {
                var column = new DataGridTextColumn
                {
                    Header = columnName.ColDescription,
                    Binding = new Binding(columnName.Binding),
                    //DisplayIndex = Convert.ToInt32(columnName.Number),
                    Width = columnName.DefaultWidth
                };

                //MessageBox.Show(columnName.Number);

                DataGridSmoke.Columns.Add(column);
            }

            DataGridSmoke.ItemsSource = dl.FireResistantDampersGridList();
        }

        #endregion

        #region " RADIO BUTTONS "

        // RadioButton1 Крышный центробежный вентилятор дымоудаления
        private void Rb1_Click(object sender, RoutedEventArgs e)
        {
            PopulateDataGrid(ColSetId1);
        }

        // RadioButton2 Крышный вытяжной каминный вентилятор для усиления тяги вытяжки дымовых газов
        private void Rb2_Click(object sender, RoutedEventArgs e)
        {
            PopulateDataGrid(ColSetId2);
        }

        // RadioButton3 Клапан противопожарный дымовой универсальный
        private void Rb3_Click(object sender, RoutedEventArgs e)
        {
            PopulateDataGrid(ColSetId3);
        }

        // RadioButton4 Клапан протипожарный огнезадерживающий
        private void Rb4_Click(object sender, RoutedEventArgs e)
        {
            PopulateDataGrid(ColSetId4);
        }

        #endregion
    }
}