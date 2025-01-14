using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockViewerApp_Project
{
    //Initialize smart candlestick data variables 
    internal class SmartCandlestick : Candlestick
    {
        public decimal range { get; set; }
        public decimal topPrice { get; set; }
        public decimal bottomPrice { get; set; }
        public decimal bodyRange { get; set; }
        public decimal upperTail { get; set; }
        public decimal lowerTail { get; set; }

        //Dictionary storing boolean values defining which patterns the SmartCandlestick follows
        public Dictionary<string, bool> Dictionary_Pattern = new Dictionary<string, bool>();

        /// <summary>
        /// This is the Inherit Constructor
        /// This constructor gets extra properties of the SmartCandlestick and categorizes which pattern properties it follows
        /// </summary>
        public SmartCandlestick(string csvLine) : base(csvLine)
        {
            ComputeExtraProperties();
            ComputePatternProperties();
        }

        /// <summary>
        /// This is the Conversion Constructor
        /// This constructor converts the data from the candlestick to the SmartCandlestick
        /// </summary>
        public SmartCandlestick(Candlestick cs)
        {
            date = cs.date;
            open = cs.open;
            close = cs.close;
            high = cs.high;
            low = cs.low;
            volume = cs.volume;
            ComputeExtraProperties();
            ComputePatternProperties();
        }

        /// <summary>
        /// Computes the extra properties of a SmartCandlestick and stores in member variables
        /// </summary>
        private void ComputeExtraProperties()
        {
            range = high - low;
            topPrice = Math.Max(open, close);
            bottomPrice = Math.Min(open, close);
            bodyRange = topPrice - bottomPrice;
            upperTail = high - topPrice;
            lowerTail = bottomPrice - low;
        }

        /// <summary>
        /// Categorizes pattern properties of the SmartCandlestick and stores them as boolean values in a dictionary
        /// </summary>
        private void ComputePatternProperties()
        {
            //Bullish
            bool bullish = close > open;
            Dictionary_Pattern.Add("Bullish", bullish);

            //Bearish
            bool bearish = open > close;
            Dictionary_Pattern.Add("Bearish", bearish);

            //Neutral
            bool neutral = bodyRange < (range * 0.03m);
            Dictionary_Pattern.Add("Neutral", neutral);

            //Marubozu
            bool marubozu = bodyRange > (range * 0.96m);
            Dictionary_Pattern.Add("Marubozu", marubozu);

            //Hammer
            bool hammer = (bodyRange < range * 0.25m) & (lowerTail > range * 0.66m);
            Dictionary_Pattern.Add("Hammer", hammer);

            //Doji
            bool doji = bodyRange < (range * 0.03m);
            Dictionary_Pattern.Add("Doji", doji);

            //Dragonfly doji
            bool dragonfly_doji = doji & (lowerTail > range * 0.66m);
            Dictionary_Pattern.Add("Dragonfly Doji", dragonfly_doji);

            //Gravestone doji
            bool gravestone_doji = doji & (upperTail > range * 0.66m);
            Dictionary_Pattern.Add("Gravestone Doji", gravestone_doji);
        }
    }
}
