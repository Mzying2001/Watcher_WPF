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

namespace Watcher_WPF
{

    public struct Filter
    {
        public bool Size;
        public bool FileName;
        public bool DirectoryName;

        public List<string> WhiteList;
        public List<string> BlackList;
    }

    /// <summary>
    /// FilterWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FilterWindow : Window
    {

        public new Filter DialogResult;

        public FilterWindow()
        {
            InitializeComponent();
        }

        public new Filter ShowDialog()
        {
            base.ShowDialog();
            return DialogResult;
        }

        private void Button_OK_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
