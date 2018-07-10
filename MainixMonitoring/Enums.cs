using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainixMonitoring
{
    public enum OSTypeEnum
    {
        Windows10,
        Ubuntu,
        Unknown
    }

    public enum StatusEnum
    {
        Online,
        NotWorking,
        //NotConnected,
        Warning
    }

    public enum MinerTypeEnum
    {
        Claymore,
        BMiner
    }
}
