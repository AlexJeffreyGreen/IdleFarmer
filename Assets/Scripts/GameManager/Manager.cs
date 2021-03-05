using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.GameManager
{
    public sealed class Manager
    {
        private static readonly object _lock = new object();
        private static Manager _manager;
        public Manager() { }

        
        public static Manager GameManagement
        {
            get
            {
                lock(_lock)
                {
                    if(_manager == null)
                        _manager = new Manager();
                }
                return _manager;

            }
        }


        public void BuildOutTileGrid() { throw new NotImplementedException(); }
        public SmartTiles.SmartTileBase FindTileByPosition() { throw new NotImplementedException(); }
        public void DayManagement() { throw new NotImplementedException(); }

    }
}
