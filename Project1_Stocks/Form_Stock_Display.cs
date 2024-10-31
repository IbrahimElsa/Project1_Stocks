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

        // Loads stock data from the selected file, filtering by the specified date range
        public void LoadStockData(string filePath, DateTime startDate, DateTime endDate)
        {
            this.filePath = filePath; // Store file path for reuse
            DataTable stockData = new DataTable();

            // Extract stock ticker and timeframe from the file name
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            string[] nameParts = fileName.Split('-');
            if (nameParts.Length == 2)
            {
                string ticker = nameParts[0];
                string timeFrame = nameParts[1];
                label_stockNameAndTimeFrame.Text = $"{ticker} - {timeFrame}";
            }
            else
            {
                label_stockNameAndTimeFrame.Text = "Invalid file name format";
            }

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

            dataGridView_stocks.DataSource = stockData; // Display data in the grid view
            PopulateChart(stockData);            // Populate chart with filtered data
        }

        // Populates the chart with stock data, including candlestick and volume series
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

                // Track max and min prices for chart scaling
                if (high > maxPrice) maxPrice = high;
                if (low < minPrice) minPrice = low;

                // Add data points to candlestick series
                candlestickSeries.Points.AddXY(index, high, low, open, close);
                candlestickSeries.Points[index].AxisLabel = date.ToString("MMM dd");

                // Add volume points, color based on price movement
                DataPoint volumePoint = new DataPoint
                {
                    XValue = index,
                    YValues = new double[] { volume },
                    Color = close >= open ? Color.Green : Color.Red
                };

                volumeSeries.Points.Add(volumePoint);
                index++;
            }

            // Set Y-axis range for price data
            var chartArea = chart_stocks.ChartAreas["ChartAreaCandlestick"];
            chartArea.AxisY.Minimum = minPrice * 0.97;
            chartArea.AxisY.Maximum = maxPrice * 1.03;
            chartArea.AxisY.LabelStyle.Format = "C2";
            chartArea.AxisX.Interval = 1;
            chartArea.AxisX.LabelStyle.IsEndLabelVisible = true;
            chartArea.AxisX.LabelStyle.Angle = -45;
            chartArea.RecalculateAxesScale();
        }
    }
}