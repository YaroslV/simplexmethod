using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using lab5.Code;
using System.Collections.ObjectModel;

namespace lab5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Element> elements = new ObservableCollection<Element>()
        {
            new Element(){RowName = "S1", P1 = 1.2, P2 = 1.4, P3 = 0.8, Norm = 1.6},
            new Element(){RowName = "S2", P1 = 80, P2 = 280, P3 = 240, Norm = 200},
            new Element(){RowName = "S3", P1 = 5, P2 = 5, P3 = 100, Norm = 10}
        };

        private ObservableCollection<Prices> prices = new ObservableCollection<Prices>()
        {
            new Prices(){CP1 = 3, CP2 = 4, CP3 = 5}
        };

        public MainWindow()
        {
            InitializeComponent();

            _grid.ItemsSource = elements;
            _priceGrid.ItemsSource = prices;

            _priceGrid.CanUserAddRows = false;
            _grid.CanUserSortColumns = false;
            _grid.CanUserAddRows = false;
            _grid.CanUserDeleteRows = false;
            _grid.CanUserResizeRows = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            const int rowN = 4;
            double[][] matrix = new double[rowN][];
            for(int i = 0; i < rowN - 1; i++)
            {
                matrix[i] = new double[rowN];
                matrix[i][0] = elements[i].Norm;
                matrix[i][1] = elements[i].P1;
                matrix[i][2] = elements[i].P2;
                matrix[i][3] = elements[i].P3;
            }
            matrix[rowN - 1] = new double[rowN];
            matrix[rowN - 1][0] = 0;
            matrix[rowN - 1][1] = prices[0].CP1;
            matrix[rowN - 1][2] = prices[0].CP2;
            matrix[rowN - 1][3] = prices[0].CP3;


            SimplexMethod method = new SimplexMethod();
            method.SetMatrix(matrix);
            var res = method.DoTheStuff();
            double min = prices[0].CP1 * res[0] + prices[0].CP2 * res[1] + prices[0].CP3 * res[2];
            _result.Text = string.Format("F min = {0} при x1 = {1} , x2 = {2}, x3 = {3}",min ,res[0],res[1],res[2]);
        }
    }
}
