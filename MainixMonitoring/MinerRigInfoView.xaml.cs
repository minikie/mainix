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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MainixMonitoring
{
    /// <summary>
    /// MinerRigInfoView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MinerRigInfoView : UserControl
    {
        public MinerRigInfo ViewModel_ { get; private set; }
        public MinerRigInfoView()
        {
            InitializeComponent();
        }

        public void setViewModel(MinerRigInfo minerRigInfo)
        {
            this.ViewModel_ = minerRigInfo;
            this.mainGrid_.DataContext = this.ViewModel_;

        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            string status = this.ViewModel_.validation();

            if (status == "success")
            {
                ((Window)this.Parent).DialogResult = true;
                ((Window)this.Parent).Close();
            }
            else
            {
                MessageBox.Show(status);
            }
        }

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            ((Window)this.Parent).DialogResult = false;
            ((Window)this.Parent).Close();
        }


    }
}
