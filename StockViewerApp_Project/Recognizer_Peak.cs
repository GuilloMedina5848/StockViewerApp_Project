using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockViewerApp_Project
{
    internal class Recognizer_Peak : Recognizer
    {
        //Inherit Constructor
        public Recognizer_Peak() : base("Peak", 3)
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
                if ((index < offset) | (index == scsList.Count() - offset))
                {
                    scs.Dictionary_Pattern.Add(Pattern_Name, false);
                    return false;
                }
                else
                {
                    SmartCandlestick prev = scsList[index - offset];
                    SmartCandlestick next = scsList[index + offset];
                    bool peak = (scs.high > prev.high) & (scs.high > next.high);
                    scs.Dictionary_Pattern.Add(Pattern_Name, peak);
                    return peak;
                }
            }
        }
    }
}
