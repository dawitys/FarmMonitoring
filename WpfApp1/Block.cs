using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class Block
    {
        public Block()
        {

        }
        public Block(string name,string foundOnFarm,float length,float width)
        {
            Name = name;
            FoundOnFarm = foundOnFarm;
            Length = length;
            Width = Width;
        }
        public Block(string name, string foundOnFarm, float length, float width,string soilType)
        {
            Name = name;
            FoundOnFarm = foundOnFarm;
            Length = length;
            Width = Width;
            SoilType = soilType;
        }
        public Block(int id,string name, float width, float length, string soilType, float sm, float sp, float sn, float spt, float ss, float sc, float smg, string foundOnFarm)
        {
            Id = id;
            Name = name;
            FoundOnFarm = foundOnFarm;
            Length = length;
            Width = Width;
            SoilType = soilType;
            Soil_moisture=sm;
            Phosphorus=sp;
            Nitrogen=sn;
            Potassium=spt;
            Sulphur=ss;
            Calcium=sc;
            Magnesium=smg;

        }
        public int Id;
        public float Soil_moisture { get; set; }
        public float Phosphorus { get; set; }
        public float Nitrogen { get; set; }
        public float Potassium { get; set; }
        public float Sulphur { get; set; }
        public float Calcium { get; set; }
        public float Magnesium { get; set; }
        public String Name
        {
            get; set;
        }
        public float Length
        {
            get; set;
        }
        public float Width
        {
            get; set;
        }
        public string SoilType
        {
            get; set;
        }
        public String FoundOnFarm
        {
            get; set;
        }
    }
}
