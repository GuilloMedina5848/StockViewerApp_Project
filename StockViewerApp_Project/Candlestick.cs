using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockViewerApp_Project
{
    //Initialize candlestick data variables
    internal class Candlestick
    {
        public decimal open { get; set; }
        public decimal high { get; set; }
        public decimal low { get; set; }
        public decimal close { get; set; }
        public ulong volume { get; set; }
        public DateTime date { get; set; }

        /// <summary>
        ///  This is the Default Candlestick Constructor
        /// </summary>
        public Candlestick()
        {
        }

        /// <summary>
        ///  This is the String Candlestick Constructor
        ///  This constructor converts the data input from a csv file into a candlestick data and parses them
        /// </summary>
        public Candlestick(string csvLine)
        {
            //Reads csv as string and splits the data based on the following separators
            char[] seperators = new char[] { ',', ' ', '"' };
            string[] subs = csvLine.Split(seperators, StringSplitOptions.RemoveEmptyEntries);

            // Get the date string so that we can send it to DateTime.Parse
            string dateString = subs[0];
            // Parse the date
            date = DateTime.Parse(dateString);

            // Parse data of the candlestick
            decimal temp;
            bool success = decimal.TryParse(subs[1], out temp);
            if (success) open = temp;

            success = decimal.TryParse(subs[2], out temp);
            if (success) high = temp;

            success = decimal.TryParse(subs[3], out temp);
            if (success) low = temp;

            success = decimal.TryParse(subs[4], out temp);
            if (success) close = temp;

            ulong tempVolume;
            success = ulong.TryParse(subs[6], out tempVolume);
            if (success) volume = tempVolume;
        }
    }
}
