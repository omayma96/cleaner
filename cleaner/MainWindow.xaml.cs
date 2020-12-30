using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.IO.Compression;
using System.Net;



namespace cleaner
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
       

        public object MessageBoxButtons { get; private set; }
        public object MessageBoxIcon { get; private set; }

        public MainWindow()
        {
            InitializeComponent();

            date.Text = File.ReadAllText(@"..\history.txt");
            date2.Text = File.ReadAllText(@"..\delete.txt");
        }

        public  void btn_analyse_Click(object sender, RoutedEventArgs e)
        {
          

            label_analyse.Content = "En cours d'analyse...";
            btn_analyse.Visibility = Visibility.Hidden;

            info_nettoyer.Visibility = Visibility.Hidden;
            info_analyse.Visibility = Visibility.Hidden;
            info_maj.Visibility = Visibility.Hidden;
            date.Visibility = Visibility.Hidden;
            date2.Visibility = Visibility.Hidden;
            btn_nettoyer.Visibility = Visibility.Hidden;
            btn_historique.Visibility = Visibility.Hidden;
            btn_maj.Visibility = Visibility.Hidden;
            btnShowInfo.Visibility = Visibility.Visible;
            pbLoad.Visibility = Visibility.Visible;
            nbr.Visibility = Visibility.Visible;
           
            DirectoryInfo di = new DirectoryInfo(System.IO.Path.GetTempPath());
            var temp = System.IO.Path.GetTempPath();

            double fileCount = di.GetFiles().Count();
            pbLoad.Value = 0;
            pbLoad.Maximum = fileCount; var files = Directory.GetFiles(temp, "*.*", SearchOption.AllDirectories);


            Task.Run(() =>
            {

                for (int i = 0; i <= fileCount + 1; i++)
                {
                    Thread.Sleep(600);
                    this.Dispatcher.Invoke(() => //Use Dispather to Update UI Immediately  
                   
                    {
                        Dispatcher.Invoke(() =>
                        {
                            result.Text += $" {i}- {files[i]} {Environment.NewLine}";
                        });

                        
                        double percent = (i / pbLoad.Maximum) * 100;
                        nbr.Visibility = Visibility.Visible;
                        double percentRound = Math.Round(percent, 1);
                        nbr.Text = percentRound.ToString() + "%";
                        pbLoad.Value = i;

                        if (i == (fileCount + 1))
                        {


                            label_analyse.Content = "L'analyse est terminée";
                            date.Text = histo();
                            File.WriteAllText(@"..\history.txt", histo());





                            btn_nettoyer.Visibility = Visibility.Visible;
                            btn_historique.Visibility = Visibility.Visible;
                            btn_maj.Visibility = Visibility.Visible;


                            btn_analyse.Visibility = Visibility.Visible;
                      
                            nbr.Visibility = Visibility.Hidden;
                            pbLoad.Visibility = Visibility.Hidden;
                            btnShowInfo.Visibility = Visibility.Hidden;
                            result.Visibility = Visibility.Hidden;
                            info_analyse.Visibility = Visibility.Visible;
                            info_nettoyer.Visibility = Visibility.Visible;
                            date.Visibility = Visibility.Visible;
                            info_maj.Visibility = Visibility.Visible;

                        }


                    });
                }

            }

           );

}
   

        public void btn_vue_Click(object sender, RoutedEventArgs e)
        {

            label_analyse.Content = "Analyse de Pc nécessaire";
            btn_nettoyer.Visibility = Visibility.Visible;
            btn_historique.Visibility = Visibility.Visible;
            btn_maj.Visibility = Visibility.Visible;


            btn_analyse.Visibility = Visibility.Visible;

            nbr.Visibility = Visibility.Hidden;
            pbLoad.Visibility = Visibility.Hidden;
            btnShowInfo.Visibility = Visibility.Hidden;
            result.Visibility = Visibility.Hidden;
            info_analyse.Visibility = Visibility.Visible;
            info_nettoyer.Visibility = Visibility.Visible;
            date.Visibility = Visibility.Visible;
            date2.Visibility = Visibility.Visible;
            info_maj.Visibility = Visibility.Visible;
        }


        public void hideInfo(object sender, RoutedEventArgs e)
        {
            result.Visibility = Visibility.Hidden;
        }
        public void ShowInfo(object sender, RoutedEventArgs e)
        {
            result.Visibility = Visibility.Visible;

        }
        public void progress_ValueChanged_1(object sender, RoutedEventArgs e)
        {
            
        }
        public  void btn_nettoyer_Click(object sender, RoutedEventArgs e)
        {
         
            label_analyse.Content = "le nettoyage est en cours...";
            btn_analyse.Visibility = Visibility.Hidden;

            info_nettoyer.Visibility = Visibility.Hidden;
            info_analyse.Visibility = Visibility.Hidden;
            info_maj.Visibility = Visibility.Hidden;
            date.Visibility = Visibility.Hidden;
            date2.Visibility = Visibility.Hidden;
            btn_nettoyer.Visibility = Visibility.Hidden;
            btn_historique.Visibility = Visibility.Hidden;
            btn_maj.Visibility = Visibility.Hidden;
            btnShowInfo.Visibility = Visibility.Visible;
            pbLoad.Visibility = Visibility.Visible;
            nbr.Visibility = Visibility.Visible;

            DirectoryInfo di = new DirectoryInfo(System.IO.Path.GetTempPath());
            var temp = System.IO.Path.GetTempPath();

            double fileCount = di.GetFiles().Count();
            pbLoad.Value = 0;
            pbLoad.Maximum = fileCount; var files = Directory.GetFiles(temp, "*.*", SearchOption.AllDirectories);


            Task.Run(() =>
            {

                for (int i = 0; i <= fileCount + 1; i++)
                {
                    Thread.Sleep(600);
                    this.Dispatcher.Invoke(() => //Use Dispather to Update UI Immediately  

                    {
                        Dispatcher.Invoke(() =>
                        {
                            result.Text += $" {i}- {files[i]} {Environment.NewLine}";
                        });


                        double percent = (i / pbLoad.Maximum) * 100;
                        nbr.Visibility = Visibility.Visible;
                        double percentRound = Math.Round(percent, 1);
                        nbr.Text = percentRound.ToString() + "%";
                        pbLoad.Value = i;

                        if (i == (fileCount + 1))
                        {
                            string[] Temp = Directory.GetFiles(System.IO.Path.GetTempPath(), "*.*", SearchOption.AllDirectories);
                           

                            try
                            {
                                foreach (string file in Temp)
                                {
                                    try
                                    {
                                        File.Delete(file);

                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);

                                    }
                                }
                                foreach (DirectoryInfo dir in di.GetDirectories("*.*", SearchOption.AllDirectories))
                                {
                                    try
                                    {
                                        dir.Delete(true);
                                        Directory.Delete(dir.FullName);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);

                                    }

                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);

                            }


                            label_analyse.Content = "Le nettoyage est terminé";
                            date2.Text = histo();
                            File.WriteAllText(@"..\delete.txt", histo());





                            btn_nettoyer.Visibility = Visibility.Visible;
                            btn_historique.Visibility = Visibility.Visible;
                            btn_maj.Visibility = Visibility.Visible;


                            btn_analyse.Visibility = Visibility.Visible;

                            nbr.Visibility = Visibility.Hidden;
                            pbLoad.Visibility = Visibility.Hidden;
                            btnShowInfo.Visibility = Visibility.Hidden;
                            result.Visibility = Visibility.Hidden;
                            info_analyse.Visibility = Visibility.Visible;
                            info_nettoyer.Visibility = Visibility.Visible;
                            date.Visibility = Visibility.Visible;
                            date2.Visibility = Visibility.Visible;
                            info_maj.Visibility = Visibility.Visible;

                        }


                    });
                }

            }

           );
        }

        
     

        private string histo()
        {
            var date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            return date;
        }

        public void btn_historique_Click(object sender, RoutedEventArgs e)
        {
            label_analyse.Content = "L'historique";
            btn_analyse.Visibility = Visibility.Hidden;

                info_nettoyer.Visibility = Visibility.Hidden;
                info_analyse.Visibility = Visibility.Visible;
                info_maj.Visibility = Visibility.Visible;
                date.Visibility = Visibility.Visible;
                date2.Visibility = Visibility.Visible;
                btn_nettoyer.Visibility = Visibility.Hidden;
                btn_historique.Visibility = Visibility.Hidden;
                btn_maj.Visibility = Visibility.Hidden;
                btnShowInfo.Visibility = Visibility.Hidden;
                pbLoad.Visibility = Visibility.Hidden;
                nbr.Visibility = Visibility.Hidden;
           
         

        }

        private void btn_maj_Click(object sender, RoutedEventArgs e)
        {
            WebClient webClient = new WebClient();

            try
            {
                if (!webClient.DownloadString("https://pastebin.com/rQjhDTvc").Contains("1.0.0"))
                {
                    if (MessageBox.Show("Il y'a une nouvelle version de Space Pc cleaner, vous voullez l'installer ?", "Cleaner", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {

                        using (var client = new WebClient())
                        {

                            Process.Start(@"C:\Users\Youcode\Documents\GitHub\pc cleaner\SpaceCleaner\bin\Release\SpaceCleaner.exe");
                            this.Close();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }



        }

        private void btn_histo_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
