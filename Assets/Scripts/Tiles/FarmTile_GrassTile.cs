using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Tiles
{
    public class FarmTile_GrassTile : FarmTile_Base
    {
        public Guid id;

        private void Awake()
        {
            if(id == null || id == Guid.Empty)
                id = Guid.NewGuid();

           // Debug.Log(id);
            //Debug.Log(this.gameObject);
        }
        private void Start()
        {

        }

        private void Update()
        {
            //Debug.Log(id);
        }

        public void OnClickEventTriggered()
        {
            Debug.Log($"Hello from {this.transform.position}");
        }
    }
}
