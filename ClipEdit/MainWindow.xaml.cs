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

namespace ClipEdit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // TODO: make this configurable in a settings screen
        // TODO: use application temporary folder as default
        static string TMP_FILE_PATH = @"C:\tmp\textfilter";

        private void btnConvert_Click(object sender, RoutedEventArgs e)
        {
            // TODO: save text to file, with filename having date and time, filtername and "_input"
            using (StreamWriter outfile = new StreamWriter(TMP_FILE_PATH + @"\input.txt"))
            {
                outfile.Write(myTextbox.Text);
            }

            // invoke external process, and save STDOUIT to same filename with "_output"
            Process p = new Process();

            // TODO: make this external application selectable and configurable
            p.StartInfo.FileName = "perl";
            p.StartInfo.Arguments = @"c:\tmp\filters\inyou.pl";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.Start();

            using (StreamWriter sw = p.StandardInput)
            {
                sw.Write(myTextbox.Text);
            }

            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();

            myTextbox.Text = output;

        }
    }
}
