
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Net;
using System.IO;
using System.IO.Compression;
using System;


namespace SpaceCleaner
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public void btn_start_Click(object sender, RoutedEventArgs e)
        {
            btn_start.Visibility = Visibility.Hidden;

            bar_progress.Visibility = Visibility.Visible;

            txt_status.Visibility = Visibility.Visible;

            Task.Run(() =>
            {
                for (int i = 0; i <= 100; i++)
                {
                    Thread.Sleep(500);
                    this.Dispatcher.Invoke(() => //Use Dispather to Update UI Immediately  
                    {
                        bar_progress.Value = i;
                        if (i == 100)
                        {

                            WebClient webClient = new WebClient();
                            var client = new WebClient();

                            string[] files = Directory.GetFiles(@"C:\Users\Youcode\Documents\GitHub\pc cleaner\cleaner\bin\Release");

                            foreach (string file in files)
                            {
                                File.Delete(file);
                            }
                       

                            client.DownloadFile("https://docs.google.com/uc?export=download&id=1GR6-rZqtq8EAH8fE9mlDvDIBHZN4Qmfl", @"C:\Users\Youcode\Documents\GitHub\pc cleaner\cleaner\bin\Release\clean.zip");
                            string zipPath = @"C:\Users\Youcode\Documents\GitHub\pc cleaner\cleaner\bin\Release\clean.zip";
                         
                            string extractPath = @"C:\Users\Youcode\Documents\GitHub\pc cleaner\cleaner\bin\Release\clean.zip";
                            ZipFile.ExtractToDirectory(zipPath, extractPath);

                           
                            File.Delete(@"C:\Users\Youcode\Documents\GitHub\pc cleaner\cleaner\bin\Release\clean.zip");
                            



                            btn_quitter.Visibility = Visibility.Visible;
                            txt_status.Text = "Mise à jour terminée !";
                        }

                    });
                }
            });
        }



        public void btn_quitter_Click(object sender, RoutedEventArgs e)
        {
            Close();
            Process.Start(@"C:\Users\Youcode\Documents\GitHub\pc cleaner\clean\bin\Release\clean.exe");
        }
    }
}
