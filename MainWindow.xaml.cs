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

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MyDataContext _one;
        private MyDataContext _two;

        public MainWindow()
        {
            InitializeComponent();
            _one = new MyDataContext(1);
            _two = new MyDataContext(101);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ((MyDataContext)this.LayoutRoot.DataContext).SelectedItems.Clear();
        }

        private void One_Button_Click(object sender, RoutedEventArgs e)
        {
            LayoutRoot.DataContext = _one;
        }

        private void Two_Button_Click(object sender, RoutedEventArgs e)
        {
            LayoutRoot.DataContext = _two;
        }
    }
}
