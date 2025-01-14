namespace StockViewerApp_Project
{
    partial class Form_StockViewer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBox_Patterns = new System.Windows.Forms.ComboBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button_loader = new System.Windows.Forms.Button();
            this.button_Update = new System.Windows.Forms.Button();
            this.dateTimePicker_startDate = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker_endDate = new System.Windows.Forms.DateTimePicker();
            this.openFileDialog_stockPicker = new System.Windows.Forms.OpenFileDialog();
            this.candlestickBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.chart_OHLCV = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.candlestickBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_OHLCV)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.comboBox_Patterns);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.button_loader);
            this.panel1.Controls.Add(this.button_Update);
            this.panel1.Controls.Add(this.dateTimePicker_startDate);
            this.panel1.Controls.Add(this.dateTimePicker_endDate);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(776, 83);
            this.panel1.TabIndex = 8;
            // 
            // comboBox_Patterns
            // 
            this.comboBox_Patterns.FormattingEnabled = true;
            this.comboBox_Patterns.Location = new System.Drawing.Point(236, 59);
            this.comboBox_Patterns.Name = "comboBox_Patterns";
            this.comboBox_Patterns.Size = new System.Drawing.Size(527, 21);
            this.comboBox_Patterns.TabIndex = 10;
            this.comboBox_Patterns.SelectedIndexChanged += new System.EventHandler(this.comboBox_Patterns_SelectedIndexChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(512, 10);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(251, 20);
            this.textBox2.TabIndex = 5;
            this.textBox2.Text = "End Date";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(236, 11);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(251, 20);
            this.textBox1.TabIndex = 4;
            this.textBox1.Text = "Start Date";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button_loader
            // 
            this.button_loader.Location = new System.Drawing.Point(3, 3);
            this.button_loader.Name = "button_loader";
            this.button_loader.Size = new System.Drawing.Size(112, 77);
            this.button_loader.TabIndex = 0;
            this.button_loader.Text = "Pick a Stock";
            this.button_loader.UseVisualStyleBackColor = true;
            this.button_loader.Click += new System.EventHandler(this.button_loader_Click);
            // 
            // button_Update
            // 
            this.button_Update.Location = new System.Drawing.Point(121, 3);
            this.button_Update.Name = "button_Update";
            this.button_Update.Size = new System.Drawing.Size(108, 77);
            this.button_Update.TabIndex = 1;
            this.button_Update.Text = "Update";
            this.button_Update.UseVisualStyleBackColor = true;
            this.button_Update.Click += new System.EventHandler(this.button_Update_Click);
            // 
            // dateTimePicker_startDate
            // 
            this.dateTimePicker_startDate.Location = new System.Drawing.Point(235, 34);
            this.dateTimePicker_startDate.Name = "dateTimePicker_startDate";
            this.dateTimePicker_startDate.Size = new System.Drawing.Size(252, 20);
            this.dateTimePicker_startDate.TabIndex = 2;
            this.dateTimePicker_startDate.Value = new System.DateTime(2022, 1, 1, 0, 0, 0, 0);
            this.dateTimePicker_startDate.ValueChanged += new System.EventHandler(this.dateTimePicker_startDate_ValueChanged);
            // 
            // dateTimePicker_endDate
            // 
            this.dateTimePicker_endDate.Location = new System.Drawing.Point(511, 34);
            this.dateTimePicker_endDate.Name = "dateTimePicker_endDate";
            this.dateTimePicker_endDate.Size = new System.Drawing.Size(252, 20);
            this.dateTimePicker_endDate.TabIndex = 3;
            this.dateTimePicker_endDate.ValueChanged += new System.EventHandler(this.dateTimePicker_endDate_ValueChanged);
            // 
            // openFileDialog_stockPicker
            // 
            this.openFileDialog_stockPicker.Filter = "CSV files (*.csv)|*.csv|Monthly|*-Month.csv|Weekly|*-Week.csv|Daily|*-Day.csv";
            this.openFileDialog_stockPicker.FilterIndex = 2;
            this.openFileDialog_stockPicker.Multiselect = true;
            this.openFileDialog_stockPicker.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_stockPicker_FileOk);
            // 
            // candlestickBindingSource
            // 
            this.candlestickBindingSource.DataSource = typeof(StockViewerApp_Project.Candlestick);
            // 
            // chart_OHLCV
            // 
            chartArea1.Name = "ChartArea_OHLC";
            chartArea2.AlignWithChartArea = "ChartArea_OHLC";
            chartArea2.Name = "ChartArea_Volume";
            this.chart_OHLCV.ChartAreas.Add(chartArea1);
            this.chart_OHLCV.ChartAreas.Add(chartArea2);
            legend1.Alignment = System.Drawing.StringAlignment.Center;
            legend1.Enabled = false;
            legend1.LegendItemOrder = System.Windows.Forms.DataVisualization.Charting.LegendItemOrder.ReversedSeriesOrder;
            legend1.Name = "Legend_OHLCV";
            this.chart_OHLCV.Legends.Add(legend1);
            this.chart_OHLCV.Location = new System.Drawing.Point(15, 101);
            this.chart_OHLCV.Name = "chart_OHLCV";
            series1.ChartArea = "ChartArea_OHLC";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            series1.CustomProperties = "PriceDownColor=Red, PriceUpColor=Lime";
            series1.IsXValueIndexed = true;
            series1.Legend = "Legend_OHLCV";
            series1.Name = "Series_OHLC";
            series1.XValueMember = "Date";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series1.YValueMembers = "High,Low,Open,Close";
            series1.YValuesPerPoint = 4;
            series2.ChartArea = "ChartArea_Volume";
            series2.IsXValueIndexed = true;
            series2.Legend = "Legend_OHLCV";
            series2.Name = "Series_Volume";
            series2.XValueMember = "Date";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series2.YValueMembers = "Volume";
            this.chart_OHLCV.Series.Add(series1);
            this.chart_OHLCV.Series.Add(series2);
            this.chart_OHLCV.Size = new System.Drawing.Size(773, 337);
            this.chart_OHLCV.TabIndex = 9;
            this.chart_OHLCV.Text = "chart_OHLCV";
            // 
            // Form_StockViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.chart_OHLCV);
            this.Controls.Add(this.panel1);
            this.Name = "Form_StockViewer";
            this.Text = "Form_StockViewer";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.candlestickBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_OHLCV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button_loader;
        private System.Windows.Forms.Button button_Update;
        private System.Windows.Forms.DateTimePicker dateTimePicker_startDate;
        private System.Windows.Forms.DateTimePicker dateTimePicker_endDate;
        private System.Windows.Forms.OpenFileDialog openFileDialog_stockPicker;
        private System.Windows.Forms.BindingSource candlestickBindingSource;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_OHLCV;
        private System.Windows.Forms.ComboBox comboBox_Patterns;
    }
}

