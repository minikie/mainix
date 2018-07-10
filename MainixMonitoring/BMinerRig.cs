using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MainixMonitoring
{
    public class BMinerRig : MinerRig
    {
        public BMinerRig(MinerRigInfo info)
        : base(info)
        {
            this.MinerType_ = MinerTypeEnum.BMiner;
            this.HashUnit_ = "Sols/s";
        }

        public override void GetMinerInfo()
        {
            try
            {
                string url = "http://" + this.LocalEndPoint_.ToString() + "/api/status";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "Get";
                request.KeepAlive = true;
                request.ContentType = "appication/json";
                //request.Headers.Add("Content-Type", "appication/json");
                //request.ContentType = "application/x-www-form-urlencoded";

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string myResponse = "";
                using (System.IO.StreamReader sr = new System.IO.StreamReader(response.GetResponseStream()))
                {
                    myResponse = sr.ReadToEnd();
                }

                this.TotalHash_ = 0;
                parseJson(JObject.Parse(this.TestStrData_ = myResponse));

            }
            catch (Exception)
            {
                this.Status_ = StatusEnum.NotWorking;
            }

            
        }

        public void parseJson(JObject json)
        {
            DateTime start_time = ConvertFromUnixTimestamp(Convert.ToDouble((string)json["start_time"]));
            TimeSpan span = DateTime.Now - start_time;

            //this.RunningTime_ = ConvertFromUnixTimestamp(Convert.ToDouble((string)json["start_time"])).ToString("yyyyMMddhhmmss");
            //this.RunningTime_ = (string)json["start_time"];
            this.RunningTime_ = (span.Days * 24 + span.Hours).ToString() + ":"+ span.Minutes.ToString();

            this.Version_ = (string)json["version"];

            JObject miners = (JObject)json["miners"];

            List<double> sols = new List<double>();

            foreach (var item in miners)
            {
                JObject jObject = (JObject)item.Value;

                sols.Add((double)jObject["solver"]["solution_rate"]);

            }

            this.TotalHash_ = sols.Sum();
            this.Hash_ = String.Join(";", sols);

            this.Status_ = StatusEnum.Online;
            this.GpuNum_ = miners.Count;
            if (miners.Count < 6)
            {
                this.Status_ = StatusEnum.Warning;
            }


        }

        DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }

    }
}
