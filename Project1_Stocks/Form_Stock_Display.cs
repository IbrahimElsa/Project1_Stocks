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
        public Form_Stock_Display()
        {
            InitializeComponent();
            SetupChart();
        }

        /// <summary>
        /// Sets up chart properties for candlestick and volume display.
        /// </summary>
        private void SetupChart()
        {
            chart_stocks.Series.Clear();
            chart_stocks.ChartAreas.Clear();

            // Configure chart area
            var chartArea = new ChartArea("ChartArea1");
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = true;
            chartArea.AxisX.LabelStyle.Format = "MMM dd";

            // Set background colors for better visibility
            chartArea.BackColor = Color.White;
            chartArea.BorderColor = Color.LightGray;
            chartArea.BorderWidth = 1;

            // Configure grid lines for better readability
            chartArea.AxisX.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisY.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);

            // Adjust Y-axis range for candlesticks
            chartArea.AxisY.Minimum = 180;  // Set minimum value based on data
            chartArea.AxisY.Maximum = 250;  // Set maximum value based on data

            chart_stocks.ChartAreas.Add(chartArea);

            // Configure candlestick series
            var candlestickSeries = new Series("Candlestick");
            candlestickSeries.ChartType = SeriesChartType.Candlestick;
            candlestickSeries.XValueType = ChartValueType.DateTime;
            candlestickSeries.YValuesPerPoint = 4;  // High, Low, Open, Close

            // Enhanced candlestick appearance
            candlestickSeries["PriceUpColor"] = "LimeGreen";   // Body color when price is up
            candlestickSeries["PriceDownColor"] = "Red";       // Body color when price is down
            candlestickSeries["PointWidth"] = "1.0";           // Increased width to remove gaps
            candlestickSeries["OpenCloseStyle"] = "Triangle";  // Candlestick style
            candlestickSeries["ShowOpenClose"] = "Both";       // Show both open and close

            // Disable any automatic spacing between points
            candlestickSeries["EmptyPointValue"] = "Zero";
            candlestickSeries["MaxPixelPointWidth"] = "15";    // Limit maximum width of candlesticks

            chart_stocks.Series.Add(candlestickSeries);

            // Configure volume series
            var volumeSeries = new Series("Volume");
            volumeSeries.ChartType = SeriesChartType.Column;
            volumeSeries.XValueType = ChartValueType.DateTime;
            volumeSeries.YAxisType = AxisType.Secondary;

            // Set volume color to blue and adjust width to match candlesticks
            volumeSeries.Color = Color.FromArgb(128, 0, 0, 255);  // Semi-transparent blue
            volumeSeries["PointWidth"] = "0.8";                   // Match candlestick spacing
            volumeSeries["EmptyPointValue"] = "Zero";             // Handle empty points consistently

            chart_stocks.Series.Add(volumeSeries);
        }

        /// <summary>
        /// Loads stock data from a CSV file and displays it in the DataGridView and Chart.
        /// </summary>
        /// <param name="filePath">The path to the CSV file.</param>
        /// <param name="startDate">The start date for filtering data.</param>
        /// <param name="endDate">The end date for filtering data.</param>
        public void LoadStockData(string filePath, DateTime startDate, DateTime endDate)
        {
            DataTable stockData = new DataTable();

            // Extract ticker and time frame from file name
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            string[] nameParts = fileName.Split('-');
            if (nameParts.Length == 2)
            {
                string ticker = nameParts[0];
                string timeFrame = nameParts[1];
                label_stockName.Text = $"{ticker} - {timeFrame}";
            }
            else
            {
                label_stockName.Text = "Invalid file name format";
            }

            // Read CSV into DataTable
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
                    // Filter data by date range
                    DateTime rowDate = DateTime.Parse(dr["Date"].ToString());
                    if (rowDate >= startDate && rowDate <= endDate)
                    {
                        stockData.Rows.Add(dr);
                    }
                }
            }

            // Bind DataTable to DataGridView
            dataGridView1.DataSource = stockData;

            // Load data into Chart
            PopulateChart(stockData);
        }

        /// <summary>
        /// Populates the chart with stock data, filtering out non-trading days (zero volume),
        /// and removing gaps by using an index-based X-axis.
        /// </summary>
        /// <param name="stockData">The stock data to be displayed.</param>
        private void PopulateChart(DataTable stockData)
        {
            var candlestickSeries = chart_stocks.Series["Candlestick"];
            var volumeSeries = chart_stocks.Series["Volume"];

            candlestickSeries.Points.Clear();
            volumeSeries.Points.Clear();

            int index = 0;  // Use an index to plot points consecutively

            foreach (DataRow row in stockData.Rows)
            {
                DateTime date = DateTime.Parse(row["Date"].ToString());
                double volume = Convert.ToDouble(row["Volume"]);

                // Skip non-trading days with zero volume
                if (volume == 0) continue;

                double open = Convert.ToDouble(row["Open"]);
                double high = Convert.ToDouble(row["High"]);
                double low = Convert.ToDouble(row["Low"]);
                double close = Convert.ToDouble(row["Close"]);

                // Add candlestick data point using index for X-axis
                candlestickSeries.Points.AddXY(index, high, low, open, close);
                candlestickSeries.Points[index].AxisLabel = date.ToString("MMM dd"); // Set date as custom label

                // Add volume data point
                DataPoint volumePoint = new DataPoint();
                volumePoint.XValue = index;
                volumePoint.YValues = new double[] { volume };
                volumePoint.Color = Color.FromArgb(128, 0, 0, 255);  // Semi-transparent blue
                volumeSeries.Points.Add(volumePoint);

                index++;  // Increment index for consecutive plotting
            }

            // Adjust axis labels and scaling
            var chartArea = chart_stocks.ChartAreas[0];

            // Format Y-axis for price (left side)
            chartArea.AxisY.LabelStyle.Format = "C2";  // Currency format with 2 decimal places

            // Format Y-axis for volume (right side)
            chartArea.AxisY2.LabelStyle.Format = "N0";  // Number format with no decimal places
            chartArea.AxisY2.Title = "Volume";
            chartArea.AxisY2.TitleFont = new Font("Arial", 8, FontStyle.Regular);

            // Disable automatic date-based intervals on X-axis
            chartArea.AxisX.Interval = 1;
            chartArea.AxisX.LabelStyle.IsEndLabelVisible = true;
            chartArea.AxisX.ScaleView.Zoomable = false;
            chartArea.AxisX.LabelStyle.Angle = -45; // Rotate labels if needed for better visibility

            // Make sure all data points are visible
            chartArea.RecalculateAxesScale();
        }

    }
}
