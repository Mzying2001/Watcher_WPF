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

    /// <summary>
    /// FilterWindow dialog result
    /// </summary>
    public struct Fdr
    {
        public string Filter { get; set; }
        public NFilters Nfilters { get; set; }
    }

    /// <summary>
    /// FilterWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FilterWindow : Window
    {

        public new Fdr? DialogResult { get; set; }

        public FilterWindow(NFilters nfilters, string filter)
        {
            InitializeComponent();

            CheckBox_Size.IsChecked = nfilters.Size;
            CheckBox_FileName.IsChecked = nfilters.FileName;
            CheckBox_DirectoryName.IsChecked = nfilters.DirectoryName;

            TextBox_Filter.Text = filter;
        }

        public new Fdr? ShowDialog()
        {
            base.ShowDialog();
            return DialogResult;
        }

        private void Button_OK_Click(object sender, RoutedEventArgs e)
        {
            NFilters nf = new NFilters
            {
                Size = (bool)CheckBox_Size.IsChecked,
                FileName = (bool)CheckBox_FileName.IsChecked,
                DirectoryName = (bool)CheckBox_DirectoryName.IsChecked,
            };

            DialogResult = new Fdr()
            {
                Filter = TextBox_Filter.Text,
                Nfilters = nf,
            };

            Close();
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
