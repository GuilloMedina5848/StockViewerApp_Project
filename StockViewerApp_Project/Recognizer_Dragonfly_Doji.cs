using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockViewerApp_Project
{
    internal class Recognizer_Dragonfly_Doji : Recognizer
    {
        //Inherit Constructor
        public Recognizer_Dragonfly_Doji() : base("Dragonfly Doji", 1)
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
                bool dragonfly = scs.lowerTail > (scs.range * 0.66m);
                bool doji = scs.bodyRange < (scs.range * 0.03m);
                bool dragonfly_doji = dragonfly & doji;
                scs.Dictionary_Pattern.Add(Pattern_Name, dragonfly_doji);
                return dragonfly_doji;
            }
        }
    }
}
