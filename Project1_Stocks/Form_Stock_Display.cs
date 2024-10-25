using System;
using System.Data;
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

            // Configure candlestick series
            var candlestickSeries = new Series("Candlestick");
            candlestickSeries.ChartType = SeriesChartType.Candlestick;
            candlestickSeries.XValueType = ChartValueType.DateTime;

            // Set how the candlesticks should be displayed
            candlestickSeries.YValuesPerPoint = 4;  // High, Low, Open, Close
            candlestickSeries["OpenCloseStyle"] = "Triangle";
            candlestickSeries["ShowOpenClose"] = "Both";
            candlestickSeries["UpColor"] = "Green";   // Green for bullish (price went up)
            candlestickSeries["DownColor"] = "Red";   // Red for bearish (price went down)

            chart_stocks.Series.Add(candlestickSeries);

            // Configure volume series
            var volumeSeries = new Series("Volume");
            volumeSeries.ChartType = SeriesChartType.Column;
            volumeSeries.XValueType = ChartValueType.DateTime;
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

            // Read CSV into DataTable
            using (var sr = new System.IO.StreamReader(filePath))
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
        /// Populates the chart with stock data.
        /// </summary>
        /// <param name="stockData">The stock data to be displayed.</param>
        private void PopulateChart(DataTable stockData)
        {
            var candlestickSeries = chart_stocks.Series["Candlestick"];
            var volumeSeries = chart_stocks.Series["Volume"];

            foreach (DataRow row in stockData.Rows)
            {
                DateTime date = DateTime.Parse(row["Date"].ToString());
                double open = Convert.ToDouble(row["Open"]);
                double high = Convert.ToDouble(row["High"]);
                double low = Convert.ToDouble(row["Low"]);
                double close = Convert.ToDouble(row["Close"]);
                double volume = Convert.ToDouble(row["Volume"]);

                // Add candlestick data point (Open, High, Low, Close)
                candlestickSeries.Points.AddXY(date, high, low, open, close);

                // Add volume data point
                volumeSeries.Points.AddXY(date, volume);
            }
        }
    }
}
