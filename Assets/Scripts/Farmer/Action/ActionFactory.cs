using System;
using UnityEngine;

namespace Farmer.Action
{
    public class ActionFactory
    {
        private static Type _outputType = typeof(ActionBase);

        public static T Create<T>() where T : ActionBase, new()
        {
            _outputType = typeof(T);
            return (T) Activator.CreateInstance(_outputType) as T;
        }

        public static T Create<T>(Vector3Int gridPosition) where T : ActionBase
        {
            _outputType = typeof(T);
            return (T) Activator.CreateInstance(typeof(T), gridPosition);
        }
    }
}