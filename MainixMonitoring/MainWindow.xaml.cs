using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Akavache;
using MahApps.Metro;
using MahApps.Metro.Controls;

using Renci.SshNet;

namespace MainixMonitoring
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainViewModel ViewModel_ { get; set; }
        
        //public Database1Entities1 Database1Entities_ { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            this.initialize();
        }

        private void initialize()
        {
            BlobCache.ApplicationName = "MainixMonitoring";

            this.ViewModel_ = new MainViewModel();
            this.mainGrid_.DataContext = this.ViewModel_;
            this.minerDataGrid_.ItemsSource = this.ViewModel_.MinerRigList_;
            this.ViewModel_.Timer_.Start();

            //this.Database1Entities_ = new Database1Entities1();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //this.ViewModel_.MinerRig2_.LocalEndPoint_ = new System.Net.IPEndPoint(IPAddress.Parse("192.168.0.134"), 3333);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //this.ViewModel_.MinerRig2_.LocalEndPoint_ = new System.Net.IPEndPoint(IPAddress.Parse("192.168.0.199"), 3333);
        }

        private void Tile_Click(object sender, RoutedEventArgs e)
        {
            Tile tile = sender as Tile;
            tile_border_clear();
            tile.BorderBrush = Brushes.White;
            tile.BorderThickness = new Thickness(2.5);
            string tile_name = tile.Title;

            if (tile_name == "Total Miner")
            {
                this.minerDataGrid_.ItemsSource = this.ViewModel_.MinerRigList_;
                //using (var transaction = new TransactionScope())
                //{
                //    MINERSTATUS m = new MINERSTATUS();
                //    m.NAME = "TEST1";

                //    this.Database1Entities_.MINERSTATUS.Add(m);
                //    this.Database1Entities_.SaveChanges();

                    
                //    transaction.Complete();
                //}

            }
            else if (tile_name == "Online Miner")
            {
                var list = from m in this.ViewModel_.MinerRigList_
                           where m.Status_ == StatusEnum.Online ||
                                 m.Status_ == StatusEnum.Warning
                           select m;
                this.minerDataGrid_.ItemsSource = list;
            }
            else if (tile_name == "Not Working")
            {
                var list = from m in this.ViewModel_.MinerRigList_
                           where m.Status_ == StatusEnum.NotWorking
                           select m;
                this.minerDataGrid_.ItemsSource = list;
            }
            else if (tile_name == "Warning")
            {
                var list = from m in this.ViewModel_.MinerRigList_
                           where m.Status_ == StatusEnum.Warning
                           select m;
                this.minerDataGrid_.ItemsSource = list;
            }
            else
            {

            }
        }

        private void tile_border_clear()
        {
            //Color currentblack = (Color)ThemeManager.GetAccent("BaseDark").Resources["BlackBrush"];
            //Color currentblack = (Color)ThemeManager.DetectAppStyle(Application.Current).Item2.Resources["BlackBrush"];

            //this.totalMinerTile_.BorderBrush = new SolidColorBrush(currentblack);
            //this.onlineMinerTile_.BorderBrush = new SolidColorBrush(currentblack);
            //this.notWorkingMinerTile_.BorderBrush = new SolidColorBrush(currentblack);
            //this.warningMinerTile_.BorderBrush = new SolidColorBrush(currentblack);

            Brush black_brush = (Brush)Application.Current.TryFindResource("WhiteBrush");
            this.totalMinerTile_.BorderBrush = black_brush;
            this.onlineMinerTile_.BorderBrush = black_brush;
            this.notWorkingMinerTile_.BorderBrush = black_brush;
            this.warningMinerTile_.BorderBrush = black_brush;

        }

        private void RemoteDesktopMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MinerRig minerRig = this.minerDataGrid_.SelectedItem as MinerRig;

            if (minerRig != null)
            {
                Process rdcProcess = new Process();

                string executable = Environment.ExpandEnvironmentVariables(@"%SystemRoot%\system32\mstsc.exe");

                if (executable != null)
                {
                    rdcProcess.StartInfo.FileName = executable;
                    rdcProcess.StartInfo.Arguments = "/v " + minerRig.IP_; // ip or name of computer to connect
                    rdcProcess.Start();
                }
            }
        }

        private void SSHMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MinerRig minerRig = this.minerDataGrid_.SelectedItem as MinerRig;

            if (minerRig != null)
            {
                Process rdcProcess = new Process();

                string executable = Environment.ExpandEnvironmentVariables(@"C:\Program Files\PuTTY\putty.exe");

                if (executable != null)
                {
                    rdcProcess.StartInfo.FileName = executable;
                    rdcProcess.StartInfo.Arguments = minerRig.IP_ + " -P"; // ip or name of computer to connect
                    rdcProcess.Start();
                }

                //using (var client = new SshClient(minerRig.IP_, minerRig.UserID_, "mainix" + minerRig.UserID_.Replace("miner", "")))
                //{
                //    client.Connect();
                //}
                //client.HostKeyReceived += (sender, e) =>
                //{
                //    if (expectedFingerPrint.Length == e.FingerPrint.Length)
                //    {
                //        for (var i = 0; i < expectedFingerPrint.Length; i++)
                //        {
                //            if (expectedFingerPrint[i] != e.FingerPrint[i])
                //            {
                //                e.CanTrust = false;
                //                break;
                //            }
                //        }
                //    }
                //    else
                //    {
                //        e.CanTrust = false;
                //    }
                //};

            }
        }

        private void minerDataGrid__PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MinerRig selected_minerRig = e.Source as MinerRig;

            if (selected_minerRig != null)
            {
                MinerRigInfoView v = new MinerRigInfoView();

                v.setViewModel(selected_minerRig.MinerRigInfo_);

                Window w = new Window();
                w.Content = v;
                if (w.ShowDialog() == true)
                {
                    selected_minerRig.reset_minerRigInfo(selected_minerRig.MinerRigInfo_);

                    this.ViewModel_.MinerRig_save();
                }

            }


        }

        private void newMinerInfoBtn__Click(object sender, RoutedEventArgs e)
        {
            Window w = new Window();
            MinerRigInfoView v = new MinerRigInfoView();
            MinerRigInfo newMinerRigInfo = new MinerRigInfo();
            v.setViewModel(newMinerRigInfo);

            w.Content = v;

            if (w.ShowDialog() == true)
            {
                this.ViewModel_.MinerRigList_.Add(newMinerRigInfo.GetMinerRig());
                this.ViewModel_.MinerRig_save();
            }
        }

        private void loadMinerInfoBtn__Click(object sender, RoutedEventArgs e)
        {
            this.ViewModel_.MinerRig_load();
        }

        private void saveMinerInfoBtn__Click(object sender, RoutedEventArgs e)
        {
            this.ViewModel_.MinerRig_save();
        }

        private void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MinerRig minerRig = this.minerDataGrid_.SelectedItem as MinerRig;

            if (minerRig != null)
            {
                this.ViewModel_.MinerRigList_.Remove(minerRig);
                this.ViewModel_.MinerRig_save();

            }
        }
        
    }
}
