using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Handbook
{
    public class DayLogItem
    {
        private List<string> _lines = new List<string>();
        private int _dayCount = 0;
        
        public List<string> Lines
        {
            get { return this._lines; }
        }

        public int DayCount
        {
            get { return this._dayCount; }
        }

        public DayLogItem(List<string> lines, int dayCount)
        {
            this._lines = lines;
            this._dayCount = dayCount;
        }
    }
}
