using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bot
{
    /// <summary>
    /// model class to manage de SCV file data
    /// </summary>
    public class StockData
    {
        public string symbol = string.Empty;
        public DateTime date = DateTime.Now;
        public DateTime time = DateTime.Now;
        public double open = 0;
        public double high = 0;
        public double low = 0;
        public double close = 0;
        public double volume = 0;
    }
}
