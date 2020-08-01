using System;
using System.Collections.Generic;
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

        readonly WWatcher watcher;
        readonly SolidColorBrush BRUSH_DEFAULT = Brushes.LawnGreen;
        readonly SolidColorBrush BRUSH_MESSAGE = Brushes.Yellow;
        readonly SolidColorBrush BRUSH_ERROR = Brushes.Red;

        public MainWindow()
        {
            InitializeComponent();

            watcher = new WWatcher();
            watcher.AddCreatedEvent(Watcher_Changes);
            watcher.AddChangedEvent(Watcher_Changes);
            watcher.AddDeletedEvent(Watcher_Changes);
            watcher.AddRenamedEvent(Watcher_Renamed);

            ShowMessage(Application.ResourceAssembly.ToString());
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

                    ShowMessage("watcher stopped...");
                }
                catch (Exception ex)
                {
                    ShowError(ex);
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

                    ShowMessage("watcher started...");
                }
                catch (Exception ex)
                {
                    ShowError(ex);
                }
            }
        }

        private void Button_clear_Click(object sender, RoutedEventArgs e)
        {
            richTextBox_main.Document.Blocks.Clear();
        }

        private void Button_save_Click(object sender, RoutedEventArgs e)
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
                    ShowMessage($"saved log to \"{path}\"");
                }
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
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
            ShowMessage($"Set path: \"{watcher.Path}\"");
        }

        protected void Watcher_Changes(object sender, FileSystemEventArgs e)
        {
            WriteLine($"[*] {e.ChangeType}: \"{e.FullPath}\"");
        }

        protected void Watcher_Renamed(object sender, RenamedEventArgs e)
        {
            WriteLine($"[*] {e.ChangeType}: \"{e.OldFullPath}\" -> \"{e.FullPath}\"");
        }

        private void WriteLine(string text)
        {
            WriteLine(text, BRUSH_DEFAULT);
        }

        private void WriteLine(string text,SolidColorBrush brush)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                richTextBox_main.Document.Blocks.Add(new Paragraph(new Run(text) { Foreground = brush }));
            }));
        }

        private void ShowMessage(string message)
        {
            WriteLine($"[+] {message}", BRUSH_MESSAGE);
        }

        private void ShowError(Exception e)
        {
            WriteLine($"[-] {e.Message}", BRUSH_ERROR);
        }
    }
}
