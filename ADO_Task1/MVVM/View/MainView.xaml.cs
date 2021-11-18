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
using System.Windows.Shapes;

namespace ADO_Task1.MVVM.View
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void InsertT_MouseDown(object sender, MouseButtonEventArgs e)
        {
            InsertTxt1.Visibility = Visibility.Visible;
            InsertTxt12.Visibility = Visibility.Visible;
            InsertTxt13.Visibility = Visibility.Visible;
            InsertTxt14.Visibility = Visibility.Visible;
            InsertTxt15.Visibility = Visibility.Visible;
            InsertTxt16.Visibility = Visibility.Visible;
            InsertTxt17.Visibility = Visibility.Visible;
            InsertTxt18.Visibility = Visibility.Visible;
            InsertTxt19.Visibility = Visibility.Visible;
            txtB1.Visibility = Visibility.Visible;
            txtB2.Visibility = Visibility.Visible;
            txtB3.Visibility = Visibility.Visible;
            txtB4.Visibility = Visibility.Visible;
            txtB5.Visibility = Visibility.Visible;
            txtB6.Visibility = Visibility.Visible;
            txtB7.Visibility = Visibility.Visible;
            txtB8.Visibility = Visibility.Visible;
            txtB9.Visibility = Visibility.Visible;
            btnins2.Visibility = Visibility.Visible;

        }
    }
}
