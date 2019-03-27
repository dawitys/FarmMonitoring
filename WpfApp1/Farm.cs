using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class Farm
    {
        public Farm()
        {
        
        }
        public Farm(int id,string name,string identity,string region,string woreda)
        {
            Id = id;
            Name = name;
            Identity = identity;
            Region = region;
            Woreda = woreda;
        }
        public Farm(string name, string identity,float area,float distane,string region, string woreda,string kebele,float humid, float seasonalRainfall,string climateType, float annualRainfall, float minTemp, float maxTemp,float alt)
        {
            Name = name;
            Identity = identity;            
            Area = area;
            Distance = distane;
            Region = region;
            Woreda = woreda;
            Kebele = kebele;
            AirHumidity = humid;
            SeasonalRainFall = seasonalRainfall;
            ClimateType = climateType;
            AnnualRainFall = annualRainfall;
            MinTemp = minTemp;
            MaxTemp = maxTemp;
            Altitude = alt;

        }
        public Farm(int id,string name, string identity, float area, float distane, string region, string woreda, string kebele, float humid, float seasonalRainfall, string climateType, float annualRainfall, float minTemp, float maxTemp, float alt)
        {
            Id = id;
            Name = name;
            Identity = identity;
            Area = area;
            Distance = distane;
            Region = region;
            Woreda = woreda;
            Kebele = kebele;
            AirHumidity = humid;
            SeasonalRainFall = seasonalRainfall;
            ClimateType = climateType;
            AnnualRainFall = annualRainfall;
            MinTemp = minTemp;
            MaxTemp = maxTemp;
            Altitude = alt;

        }
        public int Id { get; set; }
        public string Identity
        {
            get; set;
        }
        public string Name
        {
            get; set;
        }
        public float Area
        {
            get; set;
        }
        public float Distance
        {
            get; set;
        }
        public string Region
        {
            get; set;
        }
        public string Woreda
        {
            get; set;
        }
        public string Kebele
        {
            get; set;
        }
        public float AirHumidity
        {
            get;  set;
        }
        public float SeasonalRainFall
        {
            get; set;
        }
        public string ClimateType
        {
            get; set;
        }
        public float AnnualRainFall
        {
            get; set;
        }
        public float MinTemp
        {
            get; set;
        }
        public float MaxTemp
        {
            get; set;
        }
        public float Altitude
        {
            get; set;
        }
    }
}
