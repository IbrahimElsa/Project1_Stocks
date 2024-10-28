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

            // Configure main chart area for candlesticks
            var chartAreaCandlestick = new ChartArea("ChartAreaCandlestick");
            chartAreaCandlestick.AxisX.MajorGrid.Enabled = false;
            chartAreaCandlestick.AxisY.MajorGrid.Enabled = true;
            chartAreaCandlestick.AxisX.LabelStyle.Format = "MMM dd";

            chartAreaCandlestick.BackColor = Color.White;
            chartAreaCandlestick.BorderColor = Color.LightGray;
            chartAreaCandlestick.BorderWidth = 1;

            chartAreaCandlestick.AxisX.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartAreaCandlestick.AxisY.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);

            chartAreaCandlestick.AxisY.Minimum = 180;  // Adjust based on data
            chartAreaCandlestick.AxisY.Maximum = 250;  // Adjust based on data

            chartAreaCandlestick.Position = new ElementPosition(0, 0, 100, 70); // 70% height for candlestick chart
            chart_stocks.ChartAreas.Add(chartAreaCandlestick);

            // Configure second chart area for volume
            var chartAreaVolume = new ChartArea("ChartAreaVolume");
            chartAreaVolume.AxisX.MajorGrid.Enabled = false;
            chartAreaVolume.AxisY.MajorGrid.Enabled = false;
            chartAreaVolume.AxisX.LabelStyle.Enabled = false;

            chartAreaVolume.BackColor = Color.WhiteSmoke;
            chartAreaVolume.Position = new ElementPosition(0, 70, 100, 30); // 30% height for volume chart
            chartAreaVolume.AlignWithChartArea = "ChartAreaCandlestick"; // Aligns with main chart area for X-axis

            chart_stocks.ChartAreas.Add(chartAreaVolume);

            // Configure candlestick series
            var candlestickSeries = new Series("Candlestick");
            candlestickSeries.ChartType = SeriesChartType.Candlestick;
            candlestickSeries.XValueType = ChartValueType.DateTime;
            candlestickSeries.YValuesPerPoint = 4;

            candlestickSeries["PriceUpColor"] = "LimeGreen";
            candlestickSeries["PriceDownColor"] = "Red";
            candlestickSeries["PointWidth"] = "1.0";
            candlestickSeries["OpenCloseStyle"] = "Triangle";
            candlestickSeries["ShowOpenClose"] = "Both";
            candlestickSeries["EmptyPointValue"] = "Zero";
            candlestickSeries["MaxPixelPointWidth"] = "60";

            candlestickSeries.ChartArea = "ChartAreaCandlestick";
            chart_stocks.Series.Add(candlestickSeries);

            // Configure volume series to use the new volume chart area
            var volumeSeries = new Series("Volume");
            volumeSeries.ChartType = SeriesChartType.Column;
            volumeSeries.XValueType = ChartValueType.DateTime;
            volumeSeries.YAxisType = AxisType.Primary;
            volumeSeries.Color = Color.LimeGreen; // Semi-transparent blue
            volumeSeries["PointWidth"] = "0.8";
            volumeSeries["EmptyPointValue"] = "Zero";

            volumeSeries.ChartArea = "ChartAreaVolume"; // Assign to the volume chart area
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

            double maxPrice = double.MinValue;
            double minPrice = double.MaxValue;
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

                if (high > maxPrice) maxPrice = high;
                if (low < minPrice) minPrice = low;

                // Add candlestick data point using index for X-axis
                candlestickSeries.Points.AddXY(index, high, low, open, close);
                candlestickSeries.Points[index].AxisLabel = date.ToString("MMM dd");

                // Add volume data point and color it based on price movement
                DataPoint volumePoint = new DataPoint();
                volumePoint.XValue = index;
                volumePoint.YValues = new double[] { volume };

                // Set color based on whether the price went up or down
                volumePoint.Color = close >= open ? Color.Green : Color.Red;

                volumeSeries.Points.Add(volumePoint);
                index++;
            }

            double yAxisMax = maxPrice * 1.03;
            double yAxisMin = minPrice * 0.97;

            var chartArea = chart_stocks.ChartAreas["ChartAreaCandlestick"];
            chartArea.AxisY.Minimum = yAxisMin;
            chartArea.AxisY.Maximum = yAxisMax;
            chartArea.AxisY.LabelStyle.Format = "C2";

            var volumeChartArea = chart_stocks.ChartAreas["ChartAreaVolume"];
            volumeChartArea.AxisY2.LabelStyle.Format = "N0";
            volumeChartArea.AxisY2.Title = "Volume";
            volumeChartArea.AxisY2.TitleFont = new Font("Arial", 8, FontStyle.Regular);

            chartArea.AxisX.Interval = 1;
            chartArea.AxisX.LabelStyle.IsEndLabelVisible = true;
            chartArea.AxisX.ScaleView.Zoomable = false;
            chartArea.AxisX.LabelStyle.Angle = -45;

            chartArea.RecalculateAxesScale();
        }


    }
}
