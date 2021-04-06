using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Farmer.Backpack
{
    public class BackpackFactory
    {
        private static Type _outputType = typeof(BackpackBase);

        public static T Create<T>() where T : BackpackBase, new()
        {
            _outputType = typeof(T);
            return (T)Activator.CreateInstance(_outputType) as T;
        }

    }
}
