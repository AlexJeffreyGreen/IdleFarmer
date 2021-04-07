using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Plants
{
    public class Seed
    {
        public SpriteCollection SpriteCollection { get; set; }
        public int Gestation_Period { get; set; }
        public float Price_Per_Seed { get; set; }
        public float Price_At_Harvest { get; set; }
        public float Percentage_Of_Lucky { get; set; }
        public int Tile_Size { get; set; }
        public SeedType SeedType { get; set; }
        public string Name { get; set; }
    }


}
