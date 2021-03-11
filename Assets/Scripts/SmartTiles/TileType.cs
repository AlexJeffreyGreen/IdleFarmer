using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public enum TileType
    {
        GRASS = 0,
        TILLED,
        SELECTOR
    }

    public enum TileMapType
    {
        GRASS = 0,
        TILLED,
        SELECTOR
    }

    public static class EnumExtensions
    {
        public static int ToInt(this Enum enumValue)
        {
            return Convert.ToInt32(enumValue);
        }
    }
}
