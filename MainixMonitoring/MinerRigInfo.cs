using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;

namespace MainixMonitoring
{
    public class MinerRigInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public MinerRigInfo()
        {
        }

        public MinerRig GetMinerRig()
        {
            if (MinerType_ == MinerTypeEnum.BMiner)
            {
                return new BMinerRig(this);
            }
            else if (MinerType_ == MinerTypeEnum.Claymore)
            {
                return new ClaymoreMinerRig(this);
            }
            else
            {
                return new ClaymoreMinerRig(this);
            }
        }

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

        public string validation()
        {
            try
            {
                IPEndPoint iPEnd = new IPEndPoint(IPAddress.Parse(this.IP_), this.Port_);

                return "success";
            }
            catch (Exception e)
            {
                return e.Message;
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

        public string IP_ { get; set; }
        public int Port_ { get; set; }

        [JsonIgnore]
        public string RPCParameterText_
        {
            get
            {
                if (this.MinerType_ == MinerTypeEnum.Claymore)
                    return ProgramVariables.ClaymoreRpcParameter;
                else if (this.MinerType_ == MinerTypeEnum.BMiner)
                    return ProgramVariables.BMinerRpcParameter;
                else
                    return "unknown miner type";
            }
        }

        public MinerTypeEnum MinerType_ { get; set; }
        public OSTypeEnum OSType_ { get; set; }

        //public static List<MinerRigInfo> BuiltInMinerList()
        //{
        //    List<MinerRigInfo> list = new List<MinerRigInfo>();

        //    list.Add(new MinerRigInfo() { Name_ = "1", UserID_ = "miner2-4" ,IP_ = "192.168.0.109", OSType_ = OSTypeEnum.Ubuntu, Port_ = 3333, MinerType_ = MinerTypeEnum.Claymore });
        //    list.Add(new MinerRigInfo() { Name_ = "2", UserID_ = "miner2-8",IP_ = "192.168.0.120", OSType_ = OSTypeEnum. Ubuntu, Port_ = 3333, MinerType_ = MinerTypeEnum.Claymore });
        //    list.Add(new MinerRigInfo() { Name_ = "3", UserID_ = "miner3-4", IP_ = "192.168.0.122", OSType_ = OSTypeEnum.Ubuntu, Port_ = 3333, MinerType_ = MinerTypeEnum.Claymore });
        //    list.Add(new MinerRigInfo() { Name_ = "4", UserID_ = "miner2-3", IP_ = "192.168.0.110", OSType_ = OSTypeEnum.Ubuntu, Port_ = 3333, MinerType_ = MinerTypeEnum.Claymore });
        //    list.Add(new MinerRigInfo() { Name_ = "5", UserID_ = "miner3-9", IP_ = "192.168.0.121", OSType_ = OSTypeEnum.Ubuntu, Port_ = 3333, MinerType_ = MinerTypeEnum.Claymore });
        //    list.Add(new MinerRigInfo() { Name_ = "6", UserID_ = "miner2-2", IP_ = "192.168.0.111", OSType_ = OSTypeEnum.Ubuntu, Port_ = 3333, MinerType_ = MinerTypeEnum.Claymore });
        //    list.Add(new MinerRigInfo() { Name_ = "7", UserID_ = "miner4-1", IP_ = "192.168.0.124", OSType_ = OSTypeEnum.Ubuntu, Port_ = 3333, MinerType_ = MinerTypeEnum.Claymore });
        //    list.Add(new MinerRigInfo() { Name_ = "8", UserID_ = "miner3-3", IP_ = "192.168.0.127", OSType_ = OSTypeEnum.Ubuntu, Port_ = 3333, MinerType_ = MinerTypeEnum.Claymore });
        //    list.Add(new MinerRigInfo() { Name_ = "9", UserID_ = "miner1-5", IP_ = "192.168.0.105", OSType_ = OSTypeEnum.Ubuntu, Port_ = 3333, MinerType_ = MinerTypeEnum.Claymore });
        //    list.Add(new MinerRigInfo() { Name_ = "10",UserID_ = "miner1-7", IP_ = "192.168.0.106", OSType_ = OSTypeEnum.Ubuntu, Port_ = 3333, MinerType_ = MinerTypeEnum.Claymore });
        //    list.Add(new MinerRigInfo() { Name_ = "11", UserID_ = "miner1-2", IP_ = "192.168.0.104", OSType_ = OSTypeEnum.Ubuntu, Port_ = 3333, MinerType_ = MinerTypeEnum.Claymore });
        //    list.Add(new MinerRigInfo() { Name_ = "12", UserID_ = "miner2-7", IP_ = "192.168.0.118", OSType_ = OSTypeEnum.Ubuntu, Port_ = 3333, MinerType_ = MinerTypeEnum.Claymore });
        //    list.Add(new MinerRigInfo() { Name_ = "13", UserID_ = "miner2-10", IP_ = "192.168.0.116", OSType_ = OSTypeEnum.Ubuntu, Port_ = 3333, MinerType_ = MinerTypeEnum.Claymore });
        //    list.Add(new MinerRigInfo() { Name_ = "14", UserID_ = "miner2-9", IP_ = "192.168.0.115", OSType_ = OSTypeEnum.Ubuntu, Port_ = 3333, MinerType_ = MinerTypeEnum.Claymore });
        //    list.Add(new MinerRigInfo() { Name_ = "15", UserID_ = "miner2-11", IP_ = "192.168.0.117", OSType_ = OSTypeEnum.Ubuntu, Port_ = 3333, MinerType_ = MinerTypeEnum.Claymore });
        //    list.Add(new MinerRigInfo() { Name_ = "16", UserID_ = "miner2-6", IP_ = "192.168.0.108", OSType_ = OSTypeEnum.Ubuntu, Port_ = 3333, MinerType_ = MinerTypeEnum.Claymore });
        //    list.Add(new MinerRigInfo() { Name_ = "17", UserID_ = "miner1-8", IP_ = "192.168.0.107", OSType_ =OSTypeEnum.Ubuntu, Port_ = 3333, MinerType_ = MinerTypeEnum.Claymore });
        //    list.Add(new MinerRigInfo() { Name_ = "18", UserID_ = "miner1-6", IP_ = "192.168.0.138", OSType_ = OSTypeEnum.Ubuntu, Port_ = 3333, MinerType_ = MinerTypeEnum.Claymore });
        //    list.Add(new MinerRigInfo() { Name_ = "19", UserID_ = "miner1-3", IP_ = "192.168.0.101", OSType_ = OSTypeEnum.Ubuntu, Port_ = 3333, MinerType_ = MinerTypeEnum.Claymore });
        //    list.Add(new MinerRigInfo() { Name_ = "20", UserID_ = "miner1-1", IP_ = "192.168.0.103", OSType_ = OSTypeEnum.Ubuntu, Port_ = 3333, MinerType_ = MinerTypeEnum.Claymore });
        //    list.Add(new MinerRigInfo() { Name_ = "21", UserID_ = "miner1-4", IP_ = "192.168.0.102", OSType_ = OSTypeEnum.Ubuntu, Port_ = 3333, MinerType_ = MinerTypeEnum.Claymore });
        //    list.Add(new MinerRigInfo() { Name_ = "22", UserID_ = "miner2-12", IP_ = "192.168.0.119", OSType_ = OSTypeEnum.Ubuntu, Port_ = 3333, MinerType_ = MinerTypeEnum.Claymore });
        //    list.Add(new MinerRigInfo() { Name_ = "23", UserID_ = "miner2-5", IP_ = "192.168.0.112", OSType_ = OSTypeEnum.Ubuntu, Port_ = 3333, MinerType_ = MinerTypeEnum.Claymore });
        //    list.Add(new MinerRigInfo() { Name_ = "24", UserID_ = "miner3-7", IP_ = "192.168.0.125", OSType_ = OSTypeEnum.Ubuntu, Port_ = 3333, MinerType_ = MinerTypeEnum.Claymore });
        //    list.Add(new MinerRigInfo() { Name_ = "25", UserID_ = "miner3-12", IP_ = "192.168.0.123", OSType_ = OSTypeEnum.Ubuntu, Port_ = 3333, MinerType_ = MinerTypeEnum.Claymore });
        //    list.Add(new MinerRigInfo() { Name_ = "26", UserID_ = "miner2-1", IP_ = "192.168.0.114", OSType_ = OSTypeEnum.Ubuntu, Port_ = 3333, MinerType_ = MinerTypeEnum.Claymore });
        //    list.Add(new MinerRigInfo() { Name_ = "27", UserID_ = "miner3-2", IP_ = "192.168.0.126", OSType_ = OSTypeEnum.Ubuntu, Port_ = 3333, MinerType_ = MinerTypeEnum.Claymore });
        //    list.Add(new MinerRigInfo() { Name_ = "28", UserID_ = "miner3-6", IP_ = "192.168.0.128", OSType_ = OSTypeEnum.Ubuntu, Port_ = 3333, MinerType_ = MinerTypeEnum.Claymore });
        //    list.Add(new MinerRigInfo() { Name_ = "29", UserID_ = "miner4-2", IP_ = "192.168.0.129", OSType_ = OSTypeEnum.Ubuntu, Port_ = 3333, MinerType_ = MinerTypeEnum.Claymore });
        //    list.Add(new MinerRigInfo() { Name_ = "30", UserID_ = "miner3-1", IP_ = "192.168.0.132", OSType_ = OSTypeEnum.Ubuntu, Port_ = 3333, MinerType_ = MinerTypeEnum.Claymore });
        //    list.Add(new MinerRigInfo() { Name_ = "31", UserID_ = "miner3-8", IP_ = "192.168.0.131", OSType_ = OSTypeEnum.Ubuntu, Port_ = 3333, MinerType_ = MinerTypeEnum.Claymore });
        //    list.Add(new MinerRigInfo() { Name_ = "32", UserID_ = "miner3-11",IP_ = "192.168.0.130", OSType_ = OSTypeEnum.Ubuntu, Port_ = 3333, MinerType_ = MinerTypeEnum.Claymore });
        //    list.Add(new MinerRigInfo() { Name_ = "33", UserID_ = "miner3-5", IP_ = "192.168.0.134", OSType_ = OSTypeEnum.Ubuntu, Port_ = 3333, MinerType_ = MinerTypeEnum.Claymore });
        //    list.Add(new MinerRigInfo() { Name_ = "34", UserID_ = "miner3-10", IP_ = "192.168.0.133", OSType_ =OSTypeEnum.Ubuntu, Port_ = 3333, MinerType_ = MinerTypeEnum.Claymore });
        //    list.Add(new MinerRigInfo() { Name_ = "35", UserID_ = "miner4-5", IP_ = "192.168.0.136", OSType_ = OSTypeEnum.Ubuntu, Port_ = 3333, MinerType_ = MinerTypeEnum.Claymore });
        //    list.Add(new MinerRigInfo() { Name_ = "36", UserID_ = "miner4-6", IP_ = "192.168.0.137", OSType_ = OSTypeEnum.Ubuntu, Port_ = 3333, MinerType_ = MinerTypeEnum.Claymore });

        //    //list.Add(new MinerRigInfo() { Name_ = "test not working", IP_ = "192.168.0.200", Port_ = 3333, MinerType_ = MinerTypeEnum.Claymore });



        //    return list;
        //}
    }
}

