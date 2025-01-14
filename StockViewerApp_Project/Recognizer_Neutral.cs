using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockViewerApp_Project
{
    internal class Recognizer_Neutral : Recognizer
    {
        //Inherit Constructor
        public Recognizer_Neutral() : base("Neutral", 1)
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
                bool neutral = scs.bodyRange < (scs.range * 0.03m);
                scs.Dictionary_Pattern.Add(Pattern_Name, neutral);
                return neutral;
            }
        }
    }
}
