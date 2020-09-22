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

    public struct Filters
    {
        public bool Size;
        public bool FileName;
        public bool DirectoryName;

        public string Filter;
    }

    /// <summary>
    /// FilterWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FilterWindow : Window
    {

        public new Filters? DialogResult = null;

        public FilterWindow(Filters f)
        {
            InitializeComponent();

            CheckBox_Size.IsChecked = f.Size;
            CheckBox_FileName.IsChecked = f.FileName;
            CheckBox_DirectoryName.IsChecked = f.DirectoryName;

            TextBox_Filter.Text = f.Filter;
        }

        public new Filters? ShowDialog()
        {
            base.ShowDialog();
            return DialogResult;
        }

        private void Button_OK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = new Filters()
            {
                Size = (bool)CheckBox_Size.IsChecked,
                FileName = (bool)CheckBox_FileName.IsChecked,
                DirectoryName = (bool)CheckBox_DirectoryName.IsChecked,

                Filter = TextBox_Filter.Text,
            };

            Close();
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
