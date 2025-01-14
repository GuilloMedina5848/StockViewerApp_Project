using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockViewerApp_Project
{
    internal abstract class Recognizer
    {
        //Abstract Properties
        public string Pattern_Name;
        public int Pattern_Length;

        //Constructor
        protected Recognizer(string pN, int pL)
        {
            Pattern_Name = pN;
            Pattern_Length = pL;
        }

        //Abstract Method
        public abstract bool Recognize(List<SmartCandlestick> scsList, int index);

        //Concrete Method
        public void Recognize_All(List<SmartCandlestick> scsList)
        {
            for (int i = 0; i < scsList.Count; i++)
            {
                Recognize(scsList, i);
            }
        }
    }
}
