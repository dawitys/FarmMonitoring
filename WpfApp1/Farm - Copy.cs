using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class WeatherReport
    {
        public WeatherReport()
        {
        
        }
        public WeatherReport(string type,int minT,int maxT,DateTime rt)
        {
            Type = type;
            MinTemp = minT;
            MaxTemp = maxT;
            RecordedAt = rt;
        }
        public string Type{ get; set; }
        public int MaxTemp { get; set; }
        public int MinTemp { get; set; }
        public DateTime RecordedAt { get; set; }
    }
}
