using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using Newtonsoft;
using Newtonsoft.Json.Linq;

namespace MainixMonitoring
{
    public class ClaymoreMinerRig : MinerRig
    {
        public ClaymoreMinerRig(MinerRigInfo info)
        : base(info)
        {
            this.SendData_ = Encoding.UTF8.GetBytes(info.RPCParameterText_);
            this.MinerType_ = MinerTypeEnum.Claymore;
            this.HashUnit_ = "MH/s";
            //this.SocketConnectEventArgs_ = new SocketAsyncEventArgs();
            //this.SocketConnectEventArgs_.RemoteEndPoint = LocalEndPoint_;

            //this.SocketConnectEventArgs_.Completed += SocketConnectEventArgs__Completed;

            //this.SocketReceiveEventArgs_ = new SocketAsyncEventArgs();
            //this.SocketReceiveEventArgs_.SetBuffer(this.RecByte_, 0, 2048);
            //this.SocketReceiveEventArgs_.Completed += SocketEventArgs__Completed;
        }

        public byte[] RecByte_ = new byte[2048];
        public byte[] SendData_ { get; set; }

        public SocketAsyncEventArgs SocketConnectEventArgs_ { get; set; }
        public SocketAsyncEventArgs SocketReceiveEventArgs_ { get; set; }

        //private void SocketConnectEventArgs__Completed(object sender, SocketAsyncEventArgs e)
        //{
        //    this.TestData_ = Rand_.NextDouble() + 200;
        //}

        //private void SocketEventArgs__Completed(object sender, SocketAsyncEventArgs e)
        //{
        //    TestStrData_ = Encoding.UTF8.GetString(e.Buffer);
        //    this.TestData_ = Rand_.NextDouble();

        //    //this.Client_.Disconnect(true);
        //    //Console.WriteLine("메세지 받음: {0}", Encoding.Unicode.GetString(recByte));
        //}

        public override void GetMinerInfo()
        {
            using (Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                IAsyncResult result = s.BeginConnect(this.LocalEndPoint_, null, null);

                bool success = result.AsyncWaitHandle.WaitOne(1000, true);

                this.field_initialize();

                if (s.Connected)
                {
                    string rpcresult = "";

                    try
                    {
                        Array.Clear(this.RecByte_, 0, this.RecByte_.Length);

                        s.EndConnect(result);

                        s.Send(this.SendData_);

                        //SocketAsyncEventArgs arg = new SocketAsyncEventArgs();
                        //arg.SetBuffer(this.RecByte_, 0, 2048);
                        //arg.Completed += SocketEventArgs__Completed;

                        //s.ReceiveAsync(this.SocketReceiveEventArgs_);
                        s.Receive(this.RecByte_);
                        s.Shutdown(SocketShutdown.Both);
                        //this.TestStrData_ = Encoding.UTF8.GetString(this.RecByte_);
                        rpcresult = Encoding.UTF8.GetString(this.RecByte_);
                        JObject json = JObject.Parse(rpcresult);
                        parseJson(json);

                        
                    }
                    catch (Exception e)
                    {
                        this.Status_ = StatusEnum.NotWorking;

                    }

                }
                else
                {
                    this.Status_ = StatusEnum.NotWorking;
                }

                //s.Connect(this.LocalEndPoint_);
                s.Close();
            }
        }

        

        public void parseJson(JObject json)
        {
            
            JArray result = (JArray)json["result"];
            TimeSpan span = new TimeSpan(0, (int)result[1],0);
            this.RunningTime_ = (span.Days * 24 + span.Hours).ToString() + ":" + span.Minutes.ToString();

            //this.RunningTime_ = (string)result[1];

            string totalhash = ((string)result[2]).Split(';')[0];

            this.TotalHash_ = Convert.ToDouble(totalhash) /1000;

            var hashList_str = ((string)result[3]).Split(';');

            List<string> hashList_str2 = new List<string>();

            foreach (var item in hashList_str)
            {
                hashList_str2.Add((Convert.ToDouble(item) / 1000).ToString());
            }

            //this.Hash_ = (string)result[3];
            this.Hash_ = String.Join(";", hashList_str2);

            this.Status_ = StatusEnum.Online;
            this.GpuNum_ = hashList_str2.Count;

            if (this.GpuNum_ < 6)
            {
                this.Status_ = StatusEnum.Warning;
            }

            var temperatures = ((string)result[6]).Split(';').ToList<string>();

            //this.Temperature_ = String.Join(" ", temperatures.Take(this.GpuNum_));
            this.Temperature_ = "";
            this.MaxTemperature_ = 0.0;
            for (int i = 0; i < this.GpuNum_; i++)
            {
                double temp = Convert.ToDouble(temperatures[i * 2]);
                this.MaxTemperature_ = Math.Max(this.MaxTemperature_, temp);
                this.Temperature_ += temperatures[i*2] + " ";

            }

        }   

    }
}
