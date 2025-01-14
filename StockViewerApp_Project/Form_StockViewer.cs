using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace StockViewerApp_Project
{
    public partial class Form_StockViewer : Form
    {
        // This is a list of all the candlesticks read from the file
        private List<SmartCandlestick> listOfCandlesticks = null;
        // This is a list of the candlesticks used as the Data Source for the DataGridView 
        private BindingList<SmartCandlestick> boundCandlesticks = null;
        // This is a list of the filtered candlesticks that are used in the update button
        private List<SmartCandlestick> filterCandlesticks = null;
        // This is a variable used to store the starting date
        private DateTime startDate = new DateTime(2022, 1, 1);
        // This is a variable used to store the ending date
        private DateTime endDate = DateTime.Now;
        //Dictionary to store all Recognizers
        private Dictionary<string, Recognizer> Dictionary_Recognizer;
        //Highest total chart value
        private double chartMax;
        //Lowest total chart value
        private double chartMin;

        /// <summary>
        ///  This is the Default/Parent Form_StockViewer constructor
        /// </summary>
        public Form_StockViewer()
        {
            InitializeComponent();
            InitializeRecognizer();
            // Creates a list of candlesticks with size 1024 and stores it in variable listOfCandlesticks
            listOfCandlesticks = new List<SmartCandlestick>(1024);
            //Sets Both date time picker values to the startDate and endDate variables
            dateTimePicker_startDate.Value = startDate;
            dateTimePicker_endDate.Value = endDate;
        }

        /// <summary>
        ///  This is the Child Form_StockViewer constructor
        /// </summary>
        /// <param name="stockPath">"File path of the child form"</param>
        /// <param name="start">"Inherited startDate from parent form"</param>
        /// <param name="end">"Inherited endDate from parent form"</param>
        public Form_StockViewer(string stockPath, DateTime start, DateTime end)
        {
            // Instantiate and then initialize each of the controls on the form
            InitializeComponent();
            InitializeRecognizer();

            // Set the starting and ending dates
            dateTimePicker_startDate.Value = start;
            dateTimePicker_endDate.Value = end;

            listOfCandlesticks = new List<SmartCandlestick>(1024);
            // Now Go read the file and place the returned list of candlesticks in listOfCandlesticks
            // Use the ReadCandlestickDataFromFile(string fname) that reads the specific file
            listOfCandlesticks = goReadFile(stockPath);

            // Filters and displays boundCandlesticks given the new listOfCandlesticks as well as the start and end Date.
            filter_and_display_boundCandlesticks(listOfCandlesticks, startDate, endDate);
        }

        /// <summary>
        /// button_loader event triggered on button click to open the file dialog and select a stock
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_loader_Click(object sender, EventArgs e)
        {
            this.Text = "Select a stock";
            // Displays the file dialog to the user and allows them to pick a stock
            openFileDialog_stockPicker.ShowDialog();
        }

        /// <summary>
        /// button_Update event triggered on button click to filter listOfCandlesticks within specified date range,
        /// Binds filtterCandlesticks to boundCandlesticks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Update_Click(object sender, EventArgs e)
        {
            // Verifies if listOfCandlesticks is empty and makes sure the endDate is after the startDate
            if ((listOfCandlesticks.Count != 0) & (startDate <= endDate))
            {
                // Filters and displays boundCandlesticks given the new listOfCandlesticks as well as the start and end Date.
                filter_and_display_boundCandlesticks(listOfCandlesticks, startDate, endDate);
            }
        }

        /// <summary>
        /// Open file dialog event trigered after selecting a file, 
        /// Reads the data from said file and stores it into listOfCandlesticks,
        /// Which is then filtered in filterCandlesticks and binded into boundCandlesticks,
        /// Finally, it displays the data to the chart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openFileDialog_stockPicker_FileOk(object sender, CancelEventArgs e)
        {
            // Get the starting and ending dates
            DateTime startDate = dateTimePicker_startDate.Value;
            DateTime endDate = dateTimePicker_endDate.Value;
            // Go through each
            int numberOfFiles = openFileDialog_stockPicker.FileNames.Count();
            for (int i = 0; i < numberOfFiles; i++)
            {
                // Get the ith path name
                string pathName = openFileDialog_stockPicker.FileNames[i];
                string ticker = Path.GetFileNameWithoutExtension(pathName);

                // This will be the form to view
                Form_StockViewer form_StockViewer;
                if (i == 0)
                {
                    // Go read the file and display the stock
                    form_StockViewer = this;
                    listOfCandlesticks = goReadFile(pathName);
                    // Filters and displays boundCandlesticks given the new listOfCandlesticks as well as the start and end Date.
                    filter_and_display_boundCandlesticks(listOfCandlesticks, startDate, endDate);
                    form_StockViewer.Text = "Parent: " + ticker;
                }
                else
                {
                    // Simply instantiate a new form by providing the path of the stock, the starting and ending dates
                    form_StockViewer = new Form_StockViewer(pathName, startDate, endDate);
                    form_StockViewer.Text = "Child: " + ticker;
                }

                // Go display the new form
                form_StockViewer.Show();
                form_StockViewer.BringToFront();
            }
        }

        /// <summary>
        /// Method that reads csv data from passed file, store in candlestick list, then return list
        /// </summary>
        /// <param name="filename">"Name of the file"</param>
        /// <returns></returns>
        private List<SmartCandlestick> goReadFile(string filename)
        {
            string referenceString = "Date,Open,High,Low,Close,Adj Close,Volume";

            //Construct list
            List<SmartCandlestick> listOfSmartCandlesticks = new List<SmartCandlestick>();

            //Pass the file path and file name to the StreamReader constructor
            using (StreamReader sr = new StreamReader(filename))
            {
                // Reads the first line of the file
                string line = sr.ReadLine();
                // Continues to read as long as the line follows the format of the referenceString
                if (line == referenceString)
                {
                    //Continue to read until you reach the end of file
                    while ((line = sr.ReadLine()) != null)
                    {
                        //This is where we need to instantiate the candlestick represented by the string
                        SmartCandlestick cs = new SmartCandlestick(line);
                        listOfSmartCandlesticks.Add(cs);
                    }
                    //listOfCandlesticks.Reverse();
                }
                else
                {
                    // Outputs error in case of a bad file tryed to open
                    Text = "Bad File: " + filename;
                }
            }
            //Run all Recognizers on list
            foreach (Recognizer r in Dictionary_Recognizer.Values)
            {
                //Adds dictionary entries for every pattern on every candlestick
                r.Recognize_All(listOfSmartCandlesticks);
            }
            //dataGridView_candlesticks.DataSource = listOfCandlesticks;
            return listOfSmartCandlesticks;
        }

        /// <summary>
        /// This function parses through candlesticks,
        /// Adds current candlestick to filter list if date is within specified date range
        /// Returns filtered list
        /// </summary>
        /// <param name="list">"listOfCandlesticks containing all data from the file"</param>
        /// <param name="start">"date time picker value set to the startDate variable"</param>
        /// <param name="end">"date time picker value set to the endDate variable"</param>
        /// <returns></returns>
        private List<SmartCandlestick> filterList(List<SmartCandlestick> list, DateTime start, DateTime end)
        {
            List<SmartCandlestick> filter = new List<SmartCandlestick>();
            //Iterate through each candlestick in listOfCandlesticks
            foreach (SmartCandlestick cs in list)
            {
                // Adds current candlestick to filter list if date is within specified date range
                if ((cs.date >= start) & (cs.date <= end))
                {
                    filter.Add(cs);
                }
            }
            return filter;
        }

        /// <summary>
        ///  This is function normalizes the chart
        /// </summary>
        private void normalizeChart(BindingList<SmartCandlestick> bindList)
        {
            //Set starting conditions for min and max variables
            decimal min = bindList.First().low, max = 0;
            //Iterate through each candle stick in list
            foreach (SmartCandlestick c in bindList)
            {
                //Check for greatest value (Ymax) and lowest value (Ymin)
                if (c.low < min) { min = c.low; }
                if (c.high > max) { max = c.high; }
            }
            //Set the Y axis of the chart area to (+-)2% of the ranges rounded to 2 decimal places
            chartMin = chart_OHLCV.ChartAreas["ChartArea_OHLC"].AxisY.Minimum = Math.Floor(Decimal.ToDouble(min) * 0.98);
            chartMax = chart_OHLCV.ChartAreas["ChartArea_OHLC"].AxisY.Maximum = Math.Ceiling(Decimal.ToDouble(max) * 1.02);
        }

        private void displayCandlesticks(BindingList<SmartCandlestick> bindList)
        {
            // Make sure we use the dynamic range of the chart
            normalizeChart(boundCandlesticks);

            // Sets boundCandlestciks as the DataSource for the dataGridView
            //dataGridView_candlesticks.DataSource = bindList;
            
            //Clear annotations for new chart (new form or new file)
            chart_OHLCV.Annotations.Clear();
            //Display data by binding list of data to chart
            chart_OHLCV.DataSource = bindList;
            chart_OHLCV.DataBind();
        }

        private void filter_and_display_boundCandlesticks(List<SmartCandlestick> listOfCandlesticks, DateTime startDate, DateTime endDate)
        {
            // Set filterCandlesticks to filtered listOfCandlesticks within specified date range
            filterCandlesticks = filterList(listOfCandlesticks, startDate, endDate);
            // Bind filtterCandlesticks to boundCandlesticks
            boundCandlesticks = new BindingList<SmartCandlestick>(filterCandlesticks);
            //Display boundCandlesticks data
            displayCandlesticks(boundCandlesticks);
        }

        private void dateTimePicker_startDate_ValueChanged(object sender, EventArgs e)
        {
            //Store starting date
            startDate = dateTimePicker_startDate.Value;
        }

        private void dateTimePicker_endDate_ValueChanged(object sender, EventArgs e)
        {
            //Store ending date
            endDate = dateTimePicker_endDate.Value;
        }

        private void InitializeRecognizer()
        {
            Dictionary_Recognizer = new Dictionary<string, Recognizer>();

            //Bullish Recognizer
            Recognizer r = new Recognizer_Bullish();
            Dictionary_Recognizer.Add(r.Pattern_Name, r);
            //Bearish Recognizer
            r = new Recognizer_Bearish();
            Dictionary_Recognizer.Add(r.Pattern_Name, r);
            //Neutral Recognizer
            r = new Recognizer_Neutral();
            Dictionary_Recognizer.Add(r.Pattern_Name, r);
            //Marubozu Recognizer
            r = new Recognizer_Marubozu();
            Dictionary_Recognizer.Add(r.Pattern_Name, r);
            //Hammer Recognizer
            r = new Recognizer_Hammer();
            Dictionary_Recognizer.Add(r.Pattern_Name, r);
            //Doji Recognizer
            r = new Recognizer_Doji();
            Dictionary_Recognizer.Add(r.Pattern_Name, r);
            //Dragonfly Doji Recognizer
            r = new Recognizer_Dragonfly_Doji();
            Dictionary_Recognizer.Add(r.Pattern_Name, r);
            //Gravestone Doji Recognizer
            r = new Recognizer_Gravestone_Doji();
            Dictionary_Recognizer.Add(r.Pattern_Name, r);
            //Bullish Engulfing Recognizer
            r = new Recognizer_Bullish_Engulfing();
            Dictionary_Recognizer.Add(r.Pattern_Name, r);
            //Bearish Engulfing Recognizer
            r = new Recognizer_Bearish_Engulfing();
            Dictionary_Recognizer.Add(r.Pattern_Name, r);
            //Bullish Harami Recognizer
            r = new Recognizer_Bullish_Harami();
            Dictionary_Recognizer.Add(r.Pattern_Name, r);
            //Bearish Harami Recognizer
            r = new Recognizer_Bearish_Harami();
            Dictionary_Recognizer.Add(r.Pattern_Name, r);
            //Peak Recognizer
            r = new Recognizer_Peak();
            Dictionary_Recognizer.Add(r.Pattern_Name, r);
            //Valley Recognizer
            r = new Recognizer_Valley();
            Dictionary_Recognizer.Add(r.Pattern_Name, r);

            //Initialize Combo Box
            comboBox_Patterns.Items.AddRange(Dictionary_Recognizer.Keys.ToArray());
        }

        /// <summary>
        /// comboBox_Patterns_SelectedIndexChanged event triggered when a pattern is selected on the comboBox
        /// This is function creates an arrow notation based on which pattern property is selected.
        /// </summary>
        private void comboBox_Patterns_SelectedIndexChanged(object sender, EventArgs e)
        {
            chart_OHLCV.Annotations.Clear();
            if (boundCandlesticks != null)
            {
                for (int i = 0; i < boundCandlesticks.Count; i++)
                {
                    //Convert current candlestick to smart candlestick and set its data point from the chart
                    SmartCandlestick scs = boundCandlesticks[i];
                    DataPoint dataPoint = chart_OHLCV.Series[0].Points[i];

                    //Displays annotation for current candlestick if selected pattern from dictionary is true
                    if (scs.Dictionary_Pattern[comboBox_Patterns.SelectedItem.ToString()])
                    {
                        int length = Dictionary_Recognizer[comboBox_Patterns.SelectedItem.ToString()].Pattern_Length;
                        //Annotate candlesticks for multi-candlestick patterns
                        if (length > 1)
                        {
                            //Skip indexes that cause out of bounds error
                            if (i == 0 | ((i == boundCandlesticks.Count() - 1) & length == 3))
                            {
                                continue;
                            }
                            //Initialize rectangle annotation
                            RectangleAnnotation rectangle = new RectangleAnnotation();
                            rectangle.SetAnchor(dataPoint);

                            double Ymax, Ymin;
                            //Scale width to number of candlesticks
                            double width = (90.0 / boundCandlesticks.Count()) * length;
                            //Find the min and max between every candlestick in pattern
                            //Even number pattern
                            if (length == 2)    
                            {
                                Ymax = (int)(Math.Max(scs.high, boundCandlesticks[i - 1].high));
                                Ymin = (int)(Math.Min(scs.low, boundCandlesticks[i - 1].low));
                                rectangle.AnchorOffsetX = ((width / length) / 2 - 0.25) * (-1); 
                            }
                            //Odd number pattern
                            else
                            {
                                Ymax = (int)(Math.Max(scs.high, Math.Max(boundCandlesticks[i + 1].high, boundCandlesticks[i - 1].high)));
                                Ymin = (int)(Math.Min(scs.low, Math.Min(boundCandlesticks[i + 1].low, boundCandlesticks[i - 1].low)));
                            }
                            //Scale height to chart bounds
                            double height = 40.0 * (Ymax - Ymin) / (chartMax - chartMin);
                            rectangle.Height = height; 
                            rectangle.Width = width;             
                            //Set Y to highest Y value for candlesticks
                            rectangle.Y = Ymax;
                            //Set area to transparent to see chart
                            rectangle.BackColor = Color.Transparent;
                            //Set perimeter width
                            rectangle.LineWidth = 2;
                            //Set perimeter style to dashed
                            rectangle.LineDashStyle = ChartDashStyle.Dash;                 
                            chart_OHLCV.Annotations.Add(rectangle);
                        }

                        //Initilialize arrow annotation
                        ArrowAnnotation arrow = new ArrowAnnotation();
                        arrow.AxisX = chart_OHLCV.ChartAreas[0].AxisX;
                        arrow.AxisY = chart_OHLCV.ChartAreas[0].AxisY;
                        arrow.Width = 0.5;
                        arrow.Height = 0.5;
                        //Annotate single pattern and main candlestick for multi-candlesticks
                        arrow.SetAnchor(dataPoint);
                        chart_OHLCV.Annotations.Add(arrow);
                    }
                }
            }
        }
    }
}
