using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace MainixMonitoring
{
    public abstract class MinerRig : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        //public MinerRig(MinerRigInfo minerRigInfo,
        //    MinerEngine minerEngine)
        //{
        //    this.MinerEngine_ = minerEngine;

        //}

        public MinerRig(MinerRigInfo info)
        {
            this.MinerRigInfo_ = info;

            reset_minerRigInfo(info);

            this.Status_ = StatusEnum.NotWorking;
        }


        internal void reset_minerRigInfo(MinerRigInfo info)
        {
            this.LocalEndPoint_ = new IPEndPoint(IPAddress.Parse(info.IP_), info.Port_);
            this.Name_ = info.Name_;
            this.UserID_ = info.UserID_;
            this.IP_ = info.IP_;
            this.Port_ = info.Port_;
            this.OSType_ = info.OSType_;
        }

        protected void field_initialize()
        {
            this.RunningTime_ = "-";
            this.TotalHash_ = 0;
            this.GpuNum_ = 0;
        }

        //public Random Rand_ = new Random();
        //public Socket Client_ { get; set; }

        public IPEndPoint LocalEndPoint_ { get; set; }

        public MinerRigInfo MinerRigInfo_ { get; set; }


        #region TestData_
        protected double testData_;
        public double TestData_
        {
            get { return this.testData_; }
            set
            {
                if (this.testData_ != value)
                {

                    this.testData_ = value;
                    this.RaisePropertyChanged("TestData_");
                }
            }
        }

        #endregion

        #region TestStrData_
        protected string testStrData_;
        public string TestStrData_
        {
            get { return this.testStrData_; }
            set
            {
                if (this.testStrData_ != value)
                {

                    this.testStrData_ = value;
                    this.RaisePropertyChanged("TestStrData_");
                }
            }
        }

        #endregion

        #region Name_
        protected string name_;
        public string Name_
        {
            get { return this.name_; }
            set
            {
                if (this.name_ != value)
                {

                    this.name_ = value;
                    this.RaisePropertyChanged("Name_");
                }
            }
        }

        #endregion

        #region UserID_
        protected string userID_;
        public string UserID_
        {
            get { return this.userID_; }
            set
            {
                if (this.userID_ != value)
                {

                    this.userID_ = value;
                    this.RaisePropertyChanged("UserID_");
                }
            }
        }

        #endregion

        #region IP_
        protected string ip_;
        public string IP_
        {
            get { return this.ip_; }
            set
            {
                if (this.ip_ != value)
                {

                    this.ip_ = value;
                    this.RaisePropertyChanged("IP_");
                }
            }
        }

        #endregion

        #region Port_
        protected int port_;
        public int Port_
        {
            get { return this.port_; }
            set
            {
                if (this.port_ != value)
                {

                    this.port_ = value;
                    this.RaisePropertyChanged("Port_");
                }
            }
        }

        #endregion

        #region RunningTime_
        protected string runningTime_;
        public string RunningTime_
        {
            get { return this.runningTime_; }
            set
            {
                if (this.runningTime_ != value)
                {

                    this.runningTime_ = value;
                    this.RaisePropertyChanged("RunningTime_");
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

        #region Temperature_
        protected string temperature_;
        public string Temperature_
        {
            get { return this.temperature_; }
            set
            {
                if (this.temperature_ != value)
                {

                    this.temperature_ = value;
                    this.RaisePropertyChanged("Temperature_");
                }
            }
        }

        #endregion

        #region TotalHash_
        protected double totalHash_;
        public double TotalHash_
        {
            get { return this.totalHash_; }
            set
            {
                if (this.totalHash_ != value)
                {

                    this.totalHash_ = value;
                    this.RaisePropertyChanged("TotalHash_");
                }
            }
        }

        #endregion

        #region Hash_
        protected string hash_;
        public string Hash_
        {
            get { return this.hash_; }
            set
            {
                if (this.hash_ != value)
                {

                    this.hash_ = value;
                    this.RaisePropertyChanged("Hash_");
                }
            }
        }

        #endregion

        #region HashUnit_
        protected string hashUnit_;
        public string HashUnit_
        {
            get { return this.hashUnit_; }
            set
            {
                if (this.hashUnit_ != value)
                {

                    this.hashUnit_ = value;
                    this.RaisePropertyChanged("HashUnit_");
                }
            }
        }

        #endregion

        #region GpuNum_
        protected int gpuNum_;
        public int GpuNum_
        {
            get { return this.gpuNum_; }
            set
            {
                if (this.gpuNum_ != value)
                {

                    this.gpuNum_ = value;
                    this.RaisePropertyChanged("GpuNum_");
                }
            }
        }

        #endregion

        #region MinerType_
        protected MinerTypeEnum minerType_;
        public MinerTypeEnum MinerType_
        {
            get { return this.minerType_; }
            set
            {
                if (this.minerType_ != value)
                {

                    this.minerType_ = value;
                    this.RaisePropertyChanged("MinerType_");
                }
            }
        }

        #endregion

        #region OSType_
        protected OSTypeEnum osType_;
        public OSTypeEnum OSType_
        {
            get { return this.osType_; }
            set
            {
                if (this.osType_ != value)
                {

                    this.osType_ = value;
                    this.RaisePropertyChanged("OSType_");
                }
            }
        }


        #endregion

        #region Version_
        protected string version_;
        public string Version_
        {
            get { return this.version_; }
            set
            {
                if (this.version_ != value)
                {

                    this.version_ = value;
                    this.RaisePropertyChanged("Version_");
                }
            }
        }

        #endregion

        #region Status_
        protected StatusEnum status_;
        public StatusEnum Status_
        {
            get { return this.status_; }
            set
            {
                if (this.status_ != value)
                {

                    this.status_ = value;
                    this.RaisePropertyChanged("Status_");
                }
            }
        }

        #endregion


        //public bool ConnectToMiner()
        //{
        //    try
        //    {
        //        if (!this.Client_.Connected)
        //        {
        //            this.Client_.Connect(LocalEndPoint_);
        //            //this.Client_.Close();
        //            //this.Client_.ConnectAsync(SocketConnectEventArgs_);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        this.TestData_ = Rand_.NextDouble() + 100;
        //        Console.Write("Unable to connect to remote end point!\r\n");
        //    }

        //    return this.Client_.Connected;
        //}

        //public void CloseMiner()
        //{
        //    this.Client_.Close();
        //}

        public async void Update(object sender, EventArgs e)
        {
            //var result = Task<double>.Run(() => Rand_.NextDouble());

            //TestData_ = await result;// Rand_.NextDouble();
            //this.Client_.Connect();
            //this.Client_.Send(this.SendData_);
            //Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //s.Connect(this.LocalEndPoint_);
            //s.Send(this.SendData_);

            //SocketAsyncEventArgs arg = new SocketAsyncEventArgs();
            //arg.SetBuffer(this.RecByte_, 0, 2048);
            //arg.Completed += SocketEventArgs__Completed;

            //s.ReceiveAsync(this.SocketReceiveEventArgs_);
            //s.Receive(this.RecByte_);
            //s.Close();

            await Task.Run(() => GetMinerInfo());

            //this.Client_.Receive(this.RecByte_);

            //string test= Encoding.UTF8.GetString(this.RecByte_);

            //this.Client_.Close();
            // 받은 메세지를 출력
            //TestData_ = Rand_.NextDouble();
            try
            {
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        public abstract void GetMinerInfo();

    }
}
