using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockViewerApp_Project
{
    internal class Recognizer_Bullish_Harami : Recognizer
    {
        //Inherit Constructor
        public Recognizer_Bullish_Harami() : base("Bullish Harami", 2)
        {
        }

        //Abstract Method
        public override bool Recognize(List<SmartCandlestick> scsList, int index)
        {
            //Return existing value or calculate
            SmartCandlestick scs = scsList[index];
            if (scs.Dictionary_Pattern.TryGetValue(Pattern_Name, out bool value))
            {
                return value;
            }
            else
            {
                //Return false if out of bounds or continue to calculation
                int offset = Pattern_Length / 2;
                if (index < offset)
                {
                    scs.Dictionary_Pattern.Add(Pattern_Name, false);
                    return false;
                }
                else
                {
                    SmartCandlestick prev = scsList[index - offset];
                    bool bullsih = (prev.open > prev.close) & (scs.close > scs.open);
                    bool harami = (scs.topPrice < prev.topPrice) & (scs.bottomPrice > prev.bottomPrice);
                    bool bullish_harami = bullsih & harami;
                    scs.Dictionary_Pattern.Add(Pattern_Name, bullish_harami);
                    return bullish_harami;
                }
            }
        }
    }
}
