using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Tiles
{
    public abstract class FarmTile_Base : MonoBehaviour
    {
        public Vector3Int currentPosition { get; set; }
    }
}
