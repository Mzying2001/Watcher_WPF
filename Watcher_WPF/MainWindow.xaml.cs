using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using Microsoft.Win32;

namespace Watcher_WPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        readonly Watcher watcher;

        readonly SolidColorBrush BRUSH_DEFAULT = Brushes.LawnGreen;
        readonly SolidColorBrush BRUSH_MESSAGE = Brushes.Yellow;
        readonly SolidColorBrush BRUSH_ERROR = Brushes.Red;

        public MainWindow()
        {
            InitializeComponent();

            watcher = new Watcher(true, true, true);
            watcher.Created += Watcher_Changes;
            watcher.Changed += Watcher_Changes;
            watcher.Deleted += Watcher_Changes;
            watcher.Renamed += Watcher_Renamed;

            PrintMsg(Application.ResourceAssembly.ToString());
        }

        private void Button_doit_Click(object sender, RoutedEventArgs e)
        {
            if (watcher.IsStarted)
            {//stop
                try
                {
                    watcher.IsStarted = false;

                    button_doit.Content = "start";
                    button_doit.Foreground = Brushes.Black;
                    textBox_path.IsEnabled = true;

                    PrintMsg("watcher stopped...");
                }
                catch (Exception ex)
                {
                    PrintErr(ex);
                }
            }
            else
            {//start
                try
                {
                    SetPath(textBox_path.Text.Trim());
                    watcher.IsStarted = true;

                    button_doit.Content = "stop";
                    button_doit.Foreground = Brushes.Red;
                    textBox_path.IsEnabled = false;

                    PrintMsg("watcher started...");
                }
                catch (Exception ex)
                {
                    PrintErr(ex);
                }
            }
        }

        private void Button_options_Click(object sender, RoutedEventArgs e)
        {
            options.IsOpen = true;
        }

        private void OptionsMenu_Opened(object sender, RoutedEventArgs e)
        {
            allow_edit.IsChecked = !richTextBox_main.IsReadOnly;
            topmost_switcher.IsChecked = Topmost;
        }

        private void SaveLogs(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog()
                {
                    Filter = "TXT|*.txt",
                    FileName = $"watcher_{DateTime.Now:yyyyMMddHHmmss}"
                };
                if (sfd.ShowDialog() == true)
                {
                    string path = sfd.FileName;
                    string content = new TextRange(
                    richTextBox_main.Document.ContentStart,
                    richTextBox_main.Document.ContentEnd).Text;

                    File.WriteAllText(path, content);
                    PrintMsg($"saved log to \"{path}\"");
                }
            }
            catch (Exception ex)
            {
                PrintErr(ex);
            }
        }

        private void ClearLogs(object sender, RoutedEventArgs e)
        {
            richTextBox_main.Document.Blocks.Clear();
        }

        private void Switch_Window_Topmost(object sender, RoutedEventArgs e)
        {
            Topmost = !Topmost;
        }

        private void Allow_edit_Click(object sender, RoutedEventArgs e)
        {
            richTextBox_main.IsReadOnly = !richTextBox_main.IsReadOnly;
        }

        private void View_source_code_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/Mzying2001/Watcher_WPF");
        }

        private void TextBox_path_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Button_doit_Click(null, null);
        }

        private void RichTextBox_main_TextChanged(object sender, TextChangedEventArgs e)
        {
            richTextBox_main.ScrollToEnd();
        }

        private void SetPath(string path)
        {
            watcher.Path = path;
            PrintMsg($"set path: \"{watcher.Path}\"");
        }

        string last = string.Empty;
        private void Watcher_Changes(object sender, FileSystemEventArgs e)
        {
            string output = $"[*] {e.ChangeType}: \"{e.FullPath}\"";

            if (last.Equals(output))
                return;

            Println(output);
            last = output;
        }

        private void Watcher_Renamed(object sender, RenamedEventArgs e)
        {
            Println($"[*] {e.ChangeType}: \"{e.OldFullPath}\" -> \"{e.FullPath}\"");
        }

        private void Println(string text,SolidColorBrush brush)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                richTextBox_main.Document.Blocks.Add(new Paragraph(new Run(text) { Foreground = brush }));
            }));
        }

        private void Println(string text)
        {
            Println(text, BRUSH_DEFAULT);
        }

        private void PrintMsg(string message)
        {
            Println($"[+] {message}", BRUSH_MESSAGE);
        }

        private void PrintErr(Exception e)
        {
            Println($"[-] {e}", BRUSH_ERROR);
        }
    }
}
