using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MainixMonitoring
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public static KeyManager KeyManager_ = new KeyManager();

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainViewModel()
        {
            this.Timer_ = new DispatcherTimer();
            this.Timer_.Interval = new TimeSpan(0, 0, 0, 3);
            this.MinerRigList_ = new ObservableCollection<MinerRig>();
            //this.MinerRig_load();
        }

        //public void MinerRig_load()
        //{
        //    var minerRigInfoList = MinerRigInfo.BuiltInMinerList();

        //    foreach (var info in minerRigInfoList)
        //    {
        //        MinerRig rig = info.GetMinerRig();
        //        this.MinerRigList_.Add(rig);
        //        this.Timer_.Tick += new EventHandler(rig.Update);
        //        this.Timer_.Tick += new EventHandler(this.SummaryUpdate);
        //    }

        //}

        public async void MinerRig_load()
        {
            foreach (var rig in this.MinerRigList_)
            {
                this.Timer_.Tick -= new EventHandler(rig.Update);
            }

            this.Timer_.Tick -= new EventHandler(this.SummaryUpdate);

            this.MinerRigList_.Clear();

            string json = await MainViewModel.KeyManager_.get_key<string>("miners");
            
            var rig_infoList = JsonConvert.DeserializeObject<List<MinerRigInfo>>(json);

            foreach (var info in rig_infoList)
            {
                MinerRig rig = info.GetMinerRig();
                this.MinerRigList_.Add(rig);
                this.Timer_.Tick += new EventHandler(rig.Update);
            }

            this.Timer_.Tick += new EventHandler(this.SummaryUpdate);

        }
        public void MinerRig_save()
        {
            var list = from fig in this.MinerRigList_ select fig.MinerRigInfo_;

            string json = JsonConvert.SerializeObject(list);

            MainViewModel.KeyManager_.set_key("miners", json);
        }

        private void SummaryUpdate(object sender, EventArgs e)
        {
            this.TotalMinerNum_ = MinerRigList_.Count.ToString();

            this.OnlineMinerNum_ = (from k in MinerRigList_
                                    where k.Status_ == StatusEnum.Online ||
                                          k.Status_ == StatusEnum.Warning
                                    select k).ToList().Count.ToString();

            this.NotWorkingNum_ = (from k in MinerRigList_
                                    where k.Status_ == StatusEnum.NotWorking
                                    select k).ToList().Count.ToString();

            this.WarningNum_ = (from k in MinerRigList_
                                    where k.Status_ == StatusEnum.Warning
                                    select k).ToList().Count.ToString();

            var hashrates = from k in MinerRigList_ where k.TotalHash_ > 0.0 select k.TotalHash_;
            if (hashrates.Count() != 0)
                this.MinHashrate_ = hashrates.Min();

            this.MaxTemperature_ = (from k in MinerRigList_ select Convert.ToDouble(k.MaxTemperature_)).Max();

        }

        public DispatcherTimer Timer_ { get; set; }
        public ObservableCollection<MinerRig> MinerRigList_ { get; set; }

        #region TotalMinerNum_
        protected string totalMinerNum_;
        public string TotalMinerNum_
        {
            get { return this.totalMinerNum_; }
            set
            {
                if (this.totalMinerNum_ != value)
                {

                    this.totalMinerNum_ = value;
                    this.RaisePropertyChanged("TotalMinerNum_");
                }
            }
        }

        #endregion

        #region OnlineMinerNum_
        protected string onlineMinerNum_;
        public string OnlineMinerNum_
        {
            get { return this.onlineMinerNum_; }
            set
            {
                if (this.onlineMinerNum_ != value)
                {

                    this.onlineMinerNum_ = value;
                    this.RaisePropertyChanged("OnlineMinerNum_");
                }
            }
        }

        #endregion

        #region NotWorkingNum_
        protected string notWorkingNum_;
        public string NotWorkingNum_
        {
            get { return this.notWorkingNum_; }
            set
            {
                if (this.notWorkingNum_ != value)
                {

                    this.notWorkingNum_ = value;
                    this.RaisePropertyChanged("NotWorkingNum_");
                }
            }
        }

        #endregion

        #region WarningNum_
        protected string warningNum_;
        public string WarningNum_
        {
            get { return this.warningNum_; }
            set
            {
                if (this.warningNum_ != value)
                {

                    this.warningNum_ = value;
                    this.RaisePropertyChanged("WarningNum_");
                }
            }
        }

        #endregion

        #region MinHashrate_
        protected double minHashrate_;
        public double MinHashrate_
        {
            get { return this.minHashrate_; }
            set
            {
                if (this.minHashrate_ != value)
                {

                    this.minHashrate_ = value;
                    this.RaisePropertyChanged("MinHashrate_");
                }
            }
        }

        #endregion

        #region MaxTemperature_
        protected double maxTemperature_;
        public double MaxTemperature_
        {
            get { return this.maxTemperature_; }
            set
            {
                if (this.maxTemperature_ != value)
                {

                    this.maxTemperature_ = value;
                    this.RaisePropertyChanged("MaxTemperature_");
                }
            }
        }

        #endregion


    }
}
