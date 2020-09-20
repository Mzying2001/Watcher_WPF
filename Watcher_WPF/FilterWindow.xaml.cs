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

        public new Filter? DialogResult = null;

        public FilterWindow(Filter f)
        {
            InitializeComponent();

            CheckBox_Size.IsChecked = f.Size;
            CheckBox_FileName.IsChecked = f.FileName;
            CheckBox_DirectoryName.IsChecked = f.DirectoryName;

            foreach (string item in f.WhiteList)
                TextBox_WhiteList.Text += item + " ";

            foreach (string item in f.BlackList)
                TextBox_BlackBox.Text += item + " ";
        }

        public new Filter? ShowDialog()
        {
            base.ShowDialog();
            return DialogResult;
        }

        private List<string> GetList(string list)
        {
            List<string> ret = new List<string>();

            foreach(string item in list.Split(' '))
            {
                if (string.IsNullOrEmpty(item) || ret.Contains(item))
                    continue;

                ret.Add(item);
            }

            return ret;
        }

        private void Button_OK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = new Filter()
            {
                Size = (bool)CheckBox_Size.IsChecked,
                FileName = (bool)CheckBox_FileName.IsChecked,
                DirectoryName = (bool)CheckBox_DirectoryName.IsChecked,

                WhiteList = GetList(TextBox_WhiteList.Text),
                BlackList = GetList(TextBox_BlackBox.Text),
            };

            Close();
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
