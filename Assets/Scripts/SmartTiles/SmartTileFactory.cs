using Assets.Scripts.Farmer.Backpack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.SmartTiles
{
    public class SmartTileFactory
    {
        private static Type _outputType = typeof(SmartTileBase);

        public static T Create<T>() where T : SmartTileBase, new()
        {
            _outputType = typeof(T);
            return (T)Activator.CreateInstance(_outputType) as T;
        }

        //main creation factory constructor
        public static T Create<T>(Vector3Int currentPostion, Tile tile) where T: SmartTileBase, new()
        {
            _outputType = typeof(T);
            return (T)Activator.CreateInstance(typeof(T), currentPostion, tile);
        }

        public static T Convert<T>(object baseObj) where T : new()
        {
            var derivedObj = new T();
            var props = baseObj.GetType().GetProperties();
            foreach(var prop in props)
            {
                if(prop.CanWrite)
                {
                    var val = prop.GetValue(baseObj);
                    if(val != null)
                        prop.SetValue(derivedObj, val);
                }
            }
            return derivedObj;
        }
    }
}
