using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Threading;
using System.Threading.Tasks;
namespace Ka2Jax
{
    public partial class MainWindow : Window
    {
        private string lastClipboardText = string.Empty;
        private CancellationTokenSource? cts;
        public MainWindow()
        {
            InitializeComponent();
            ClearButton.Click += ClearButton_Click;
            ConvertButton.Click += ConvertButton_Click;
            CopyButton.Click += CopyButton_Click;
            ClipboardToggle.Checked += ClipboardToggle_Checked;
            ClipboardToggle.Unchecked += ClipboardToggle_Unchecked;
            StartClipboardMonitoring();
            // 添加快捷键支持
            this.InputBindings.Add(new KeyBinding(
                new RoutedUICommand("Copy", "Copy", typeof(MainWindow)),
                new KeyGesture(Key.C, ModifierKeys.Control)));
            CommandBindings.Add(new CommandBinding(
                ApplicationCommands.Copy,
                (s, e) => CopyOutputToClipboard()));
        }
        
        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            CopyOutputToClipboard();
        }
        
        private void CopyOutputToClipboard()
        {
            try
            {
                if (!string.IsNullOrEmpty(OutputTextBox.Text))
                {
                    System.Windows.Clipboard.SetText(OutputTextBox.Text);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Failed to copy: {ex.Message}", "Error",
                    System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        private void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string input = InputTextBox.Text;
                string output = ConvertKaTeXToMathJax(input);
                OutputTextBox.Text = output;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"转换出错: {ex.Message}", "错误",
                    System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        private string ConvertKaTeXToMathJax(string input)
        {
            // 转换 \[ 为 $$
            string result = input.Replace(@"\[", "$$");
            // 转换 \] 为 $$
            result = result.Replace(@"\]", "$$");
            
            // 转换 \( 为 $，并去掉后面的空格
            result = Regex.Replace(result, @"\\\(\s*", "$");
            // 转换 \) 为 $，并去掉前面的空格
            result = Regex.Replace(result, @"\s*\\\)", "$");
            
            // 调试输出
            Console.WriteLine("转换结果：");
            Console.WriteLine(result);
            
            return result;
        }

        private async void StartClipboardMonitoring()
        {
            cts = new CancellationTokenSource();
            try
            {
                while (!cts.Token.IsCancellationRequested)
                {
                    await Task.Delay(500, cts.Token);
                    
                    if (ClipboardToggle.IsChecked == true && System.Windows.Clipboard.ContainsText())
                    {
                        string clipboardText = System.Windows.Clipboard.GetText();
                        if (clipboardText != lastClipboardText)
                        {
                            lastClipboardText = clipboardText;
                            string convertedText = ConvertKaTeXToMathJax(clipboardText);
                            System.Windows.Clipboard.SetText(convertedText);
                        }
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // 正常退出
            }
        }

        private void ClipboardToggle_Checked(object sender, RoutedEventArgs e)
        {
            ClipboardToggle.Content = "剪贴板监听：开";
            if (cts == null || cts.IsCancellationRequested)
            {
                StartClipboardMonitoring();
            }
        }

        private void ClipboardToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            ClipboardToggle.Content = "剪贴板监听：关";
            cts?.Cancel();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            InputTextBox.Clear();
            OutputTextBox.Clear();
        }

        protected override void OnClosed(EventArgs e)
        {
            if (cts != null)
            {
                cts.Cancel();
                cts.Dispose();
                cts = null;
            }
            base.OnClosed(e);
        }
    }
}