using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Project1_Stocks
{
    public partial class Form_Stock_Display : Form
    {
        private string filePath;

        // Constructor that initializes the stock display form with the provided date range
        public Form_Stock_Display(DateTime startDate, DateTime endDate)
        {
            InitializeComponent();
            SetupChart();       // Sets up the chart areas and series
            CenterLabels();     // Centers labels within the form

            // Initialize date pickers with the provided date range
            dateTimePicker_startDateUpdate.Value = startDate;
            dateTimePicker_endDateUpdate.Value = endDate;

            // Disable the update button initially
            button_updateDates.Enabled = false;

            // Attach event handlers to enable the update button when dates change
            dateTimePicker_startDateUpdate.ValueChanged += DatePicker_ValueChanged;
            dateTimePicker_endDateUpdate.ValueChanged += DatePicker_ValueChanged;

            // Attach event handler for the update button click event
            button_updateDates.Click += Button_updateDates_Click;

            // Re-center labels when the form is resized
            this.Resize += (s, e) => CenterLabels();
        }

        // Method to set stock name and timeframe for display
        public void SetStockNameAndTimeFrame(string stockName, string timeFrame)
        {
            label_stockNameAndTimeFrame.Text = $"{stockName} - {timeFrame}";
        }

        // Centers labels in the middle of the form
        private void CenterLabels()
        {
            label_stockNameAndTimeFrame.Left = (this.ClientSize.Width - label_stockNameAndTimeFrame.Width) / 2;
            label_startAndEndDates.Left = (this.ClientSize.Width - label_startAndEndDates.Width) / 2;
        }

        // Enables the update button when the date pickers' values change
        private void DatePicker_ValueChanged(object sender, EventArgs e)
        {
            button_updateDates.Enabled = true;
        }

        // Event handler for updating the displayed data based on new date range
        private void Button_updateDates_Click(object sender, EventArgs e)
        {
            DateTime newStartDate = dateTimePicker_startDateUpdate.Value;
            DateTime newEndDate = dateTimePicker_endDateUpdate.Value;

            // Update date range label and reload stock data with the new dates
            SetStartAndEndDates(newStartDate, newEndDate);
            LoadStockData(filePath, newStartDate, newEndDate);
            NormalizeChart(chart_stocks.Series["Candlestick"]);
            AnnotatePeaksAndValleys(chart_stocks.Series["Candlestick"]);

            // Disable the update button after updating
            button_updateDates.Enabled = false;
        }

        // Updates the label displaying the start and end dates
        public void SetStartAndEndDates(DateTime startDate, DateTime endDate)
        {
            label_startAndEndDates.Text = $"{startDate:MMM dd, yyyy} - {endDate:MMM dd, yyyy}";
        }

        // Sets up the chart areas and styles for displaying candlestick and volume data
        private void SetupChart()
        {
            chart_stocks.Series.Clear();
            chart_stocks.ChartAreas.Clear();

            // Setup candlestick chart area with specific style and grid settings
            var chartAreaCandlestick = new ChartArea("ChartAreaCandlestick");
            chartAreaCandlestick.AxisX.MajorGrid.Enabled = false;
            chartAreaCandlestick.AxisY.MajorGrid.Enabled = true;
            chartAreaCandlestick.AxisX.LabelStyle.Format = "MMM dd";
            chartAreaCandlestick.AxisY.LabelStyle.Format = "F2";  // Round Y-axis to nearest penny
            chartAreaCandlestick.BackColor = Color.White;
            chartAreaCandlestick.BorderColor = Color.LightGray;
            chartAreaCandlestick.BorderWidth = 1;
            chart_stocks.ChartAreas.Add(chartAreaCandlestick);

            chartAreaCandlestick.Position = new ElementPosition(0, 0, 100, 80);

            // Setup volume chart area, aligned with the candlestick area
            var chartAreaVolume = new ChartArea("ChartAreaVolume");
            chartAreaVolume.AxisX.MajorGrid.Enabled = false;
            chartAreaVolume.AxisY.MajorGrid.Enabled = false;
            chartAreaVolume.BackColor = Color.WhiteSmoke;
            chartAreaVolume.AlignWithChartArea = "ChartAreaCandlestick";
            chart_stocks.ChartAreas.Add(chartAreaVolume);

            // Set the height of the volume chart area to be shorter
            chartAreaVolume.Position = new ElementPosition(0, 80, 100, 20);
            chartAreaVolume.AxisX.LabelStyle.Enabled = false;

            // Define candlestick series properties for the chart
            var candlestickSeries = new Series("Candlestick")
            {
                ChartType = SeriesChartType.Candlestick,
                XValueType = ChartValueType.DateTime,
                YValuesPerPoint = 4,
                ChartArea = "ChartAreaCandlestick"
            };
            candlestickSeries["PriceUpColor"] = "LimeGreen";
            candlestickSeries["PriceDownColor"] = "Red";
            chart_stocks.Series.Add(candlestickSeries);

            // Define volume series properties for the chart
            var volumeSeries = new Series("Volume")
            {
                ChartType = SeriesChartType.Column,
                XValueType = ChartValueType.DateTime,
                ChartArea = "ChartAreaVolume",
                Color = Color.LimeGreen
            };
            chart_stocks.Series.Add(volumeSeries);
        }

        // Normalizes the chart to fit within a certain Y-axis range
        private void NormalizeChart(Series candlestickSeries)
        {
            double maxPrice = double.MinValue;
            double minPrice = double.MaxValue;

            foreach (var point in candlestickSeries.Points)
            {
                double high = point.YValues[0];
                double low = point.YValues[1];
                if (high > maxPrice) maxPrice = high;
                if (low < minPrice) minPrice = low;
            }

            var yAxis = chart_stocks.ChartAreas["ChartAreaCandlestick"].AxisY;
            yAxis.Minimum = Math.Round(minPrice * 0.98, 2); // Round to nearest penny
            yAxis.Maximum = Math.Round(maxPrice * 1.02, 2);
        }

        // Annotates peaks and valleys on the chart
        private void AnnotatePeaksAndValleys(Series candlestickSeries)
        {
            for (int i = 1; i < candlestickSeries.Points.Count - 1; i++)
            {
                var prev = candlestickSeries.Points[i - 1].YValues[1];
                var curr = candlestickSeries.Points[i].YValues[1];
                var next = candlestickSeries.Points[i + 1].YValues[1];

                bool isPeak = curr > prev && curr > next;
                bool isValley = curr < prev && curr < next;

                if (isPeak)
                {
                    AddAnnotation(candlestickSeries.Points[i], Color.Green, "Peak");
                }
                else if (isValley)
                {
                    AddAnnotation(candlestickSeries.Points[i], Color.Red, "Valley");
                }
            }
        }

        // Adds an annotation to a point on the chart
        private void AddAnnotation(DataPoint point, Color color, string label)
        {
            var annotation = new TextAnnotation
            {
                Text = label,
                ForeColor = color,
                X = point.XValue,
                Y = point.YValues[0],
                AnchorX = point.XValue,
                AnchorY = point.YValues[0],
                AnchorAlignment = ContentAlignment.TopCenter
            };
            chart_stocks.Annotations.Add(annotation);

            var lineAnnotation = new HorizontalLineAnnotation
            {
                AxisX = chart_stocks.ChartAreas[0].AxisX,
                AxisY = chart_stocks.ChartAreas[0].AxisY,
                IsInfinitive = true,
                ClipToChartArea = chart_stocks.ChartAreas[0].Name,
                LineColor = color,
                AnchorY = point.YValues[0]
            };
            chart_stocks.Annotations.Add(lineAnnotation);
        }

        // Loads stock data from the selected file, filtering by the specified date range
        public void LoadStockData(string filePath, DateTime startDate, DateTime endDate)
        {
            this.filePath = filePath; // Store file path for reuse
            DataTable stockData = new DataTable();

            // Extract stock ticker and timeframe from the file name
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            string[] nameParts = fileName.Split('-');
            string ticker = nameParts[0];
            string timeFrame = nameParts.Length > 1 ? nameParts[1] : "Unknown";

            SetStockNameAndTimeFrame(ticker, timeFrame);

            // Read the CSV file and populate the DataTable, filtering by date
            using (var sr = new StreamReader(filePath))
            {
                string[] headers = sr.ReadLine().Split(',');
                foreach (string header in headers)
                {
                    stockData.Columns.Add(header);
                }

                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(',');
                    DataRow dr = stockData.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = rows[i];
                    }

                    DateTime rowDate = DateTime.Parse(dr["Date"].ToString());
                    if (rowDate >= startDate && rowDate <= endDate)
                    {
                        stockData.Rows.Add(dr);
                    }
                }
            }

            PopulateChart(stockData);            // Populate chart with filtered data
            NormalizeChart(chart_stocks.Series["Candlestick"]);
            AnnotatePeaksAndValleys(chart_stocks.Series["Candlestick"]);
        }

        // Populates the chart with stock data, including candlestick and volume series
        private void PopulateChart(DataTable stockData)
        {
            var candlestickSeries = chart_stocks.Series["Candlestick"];
            var volumeSeries = chart_stocks.Series["Volume"];

            candlestickSeries.Points.Clear();
            volumeSeries.Points.Clear();

            int index = 0;
            foreach (DataRow row in stockData.Rows)
            {
                DateTime date = DateTime.Parse(row["Date"].ToString());
                double volume = Convert.ToDouble(row["Volume"]);
                if (volume == 0) continue;

                double open = Convert.ToDouble(row["Open"]);
                double high = Convert.ToDouble(row["High"]);
                double low = Convert.ToDouble(row["Low"]);
                double close = Convert.ToDouble(row["Close"]);

                // Create a SmartCandlestick instance for this data point
                SmartCandlestick candlestick = new SmartCandlestick(open, high, low, close);

                // Identify the pattern and set it as the tooltip
                string pattern = IdentifyCandlestickPattern(candlestick);

                // Create data point for the candlestick
                DataPoint candlestickPoint = new DataPoint(index, new double[] { high, low, open, close });
                candlestickSeries.Points.Add(candlestickPoint);

                // Set the tooltip text for this specific candlestick
                toolTip_candlestick.SetToolTip(chart_stocks,
                    $"Date: {date:MMM dd}\nPattern: {pattern}\nOpen: {open}\nHigh: {high}\nLow: {low}\nClose: {close}");

                // Add volume data point
                DataPoint volumePoint = new DataPoint
                {
                    XValue = index,
                    YValues = new double[] { volume },
                    Color = close >= open ? Color.Green : Color.Red
                };
                volumeSeries.Points.Add(volumePoint);

                index++;
            }
        }


        // Helper method to identify candlestick pattern
        private string IdentifyCandlestickPattern(SmartCandlestick candlestick)
        {
            if (candlestick.IsMarubozu) return "Marubozu";
            if (candlestick.IsHammer) return "Hammer";
            if (candlestick.IsDragonflyDoji) return "Dragonfly Doji";
            if (candlestick.IsGravestoneDoji) return "Gravestone Doji";
            if (candlestick.IsDoji) return "Doji";
            if (candlestick.IsBullish) return "Bullish";
            if (candlestick.IsBearish) return "Bearish";
            return "Neutral";
        }

    }

    // Base Candlestick Class
    public class Candlestick
    {
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }

        // Constructor to initialize candlestick data
        public Candlestick(double open, double high, double low, double close)
        {
            Open = open;
            High = high;
            Low = low;
            Close = close;
        }
    }

    // SmartCandlestick Class with Pattern Detection
    public class SmartCandlestick : Candlestick
    {
        // Constructor calls base constructor to initialize properties
        public SmartCandlestick(double open, double high, double low, double close)
            : base(open, high, low, close)
        {
        }

        public double Range => High - Low;
        public double BodyRange => Math.Abs(Open - Close);
        public double TopPrice => Math.Max(Open, Close);
        public double BottomPrice => Math.Min(Open, Close);
        public double UpperTail => High - TopPrice;
        public double LowerTail => BottomPrice - Low;

        public bool IsBullish => Close > Open;
        public bool IsBearish => Close < Open;
        public bool IsNeutral => Close == Open;
        public bool IsMarubozu => Open == Low && Close == High;
        public bool IsHammer => LowerTail > BodyRange * 2 && UpperTail < BodyRange;
        public bool IsDoji => BodyRange < (Range * 0.1);
        public bool IsDragonflyDoji => IsDoji && Open == Low;
        public bool IsGravestoneDoji => IsDoji && Open == High;
    }
}
