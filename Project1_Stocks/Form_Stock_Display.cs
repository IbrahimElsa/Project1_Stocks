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

        public Form_Stock_Display(DateTime startDate, DateTime endDate)
        {
            InitializeComponent();
            SetupChart();
            CenterLabels();

            // Initialize date pickers with the passed-in dates
            dateTimePicker_startDateUpdate.Value = startDate;
            dateTimePicker_endDateUpdate.Value = endDate;

            // Initialize button as disabled
            button_updateDates.Enabled = false;

            // Event handlers to enable the update button on date change
            dateTimePicker_startDateUpdate.ValueChanged += DatePicker_ValueChanged;
            dateTimePicker_endDateUpdate.ValueChanged += DatePicker_ValueChanged;

            button_updateDates.Click += Button_updateDates_Click;
            this.Resize += (s, e) => CenterLabels(); // Re-center on form resize
        }

        private void CenterLabels()
        {
            label_stockNameAndTimeFrame.Left = (this.ClientSize.Width - label_stockNameAndTimeFrame.Width) / 2;
            label_startAndEndDates.Left = (this.ClientSize.Width - label_startAndEndDates.Width) / 2;
        }

        private void DatePicker_ValueChanged(object sender, EventArgs e)
        {
            button_updateDates.Enabled = true;
        }

        private void Button_updateDates_Click(object sender, EventArgs e)
        {
            DateTime newStartDate = dateTimePicker_startDateUpdate.Value;
            DateTime newEndDate = dateTimePicker_endDateUpdate.Value;

            // Update the label with the new date range
            SetStartAndEndDates(newStartDate, newEndDate);

            // Reload stock data with the new dates
            LoadStockData(filePath, newStartDate, newEndDate);

            // Disable the button again after updating
            button_updateDates.Enabled = false;
        }

        public void SetStartAndEndDates(DateTime startDate, DateTime endDate)
        {
            label_startAndEndDates.Text = $"{startDate:MMM dd, yyyy} - {endDate:MMM dd, yyyy}";
        }

        private void SetupChart()
        {
            chart_stocks.Series.Clear();
            chart_stocks.ChartAreas.Clear();

            var chartAreaCandlestick = new ChartArea("ChartAreaCandlestick");
            chartAreaCandlestick.AxisX.MajorGrid.Enabled = false;
            chartAreaCandlestick.AxisY.MajorGrid.Enabled = true;
            chartAreaCandlestick.AxisX.LabelStyle.Format = "MMM dd";
            chartAreaCandlestick.BackColor = Color.White;
            chartAreaCandlestick.BorderColor = Color.LightGray;
            chartAreaCandlestick.BorderWidth = 1;
            chartAreaCandlestick.AxisX.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartAreaCandlestick.AxisY.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartAreaCandlestick.Position = new ElementPosition(0, 0, 100, 70);
            chart_stocks.ChartAreas.Add(chartAreaCandlestick);

            var chartAreaVolume = new ChartArea("ChartAreaVolume");
            chartAreaVolume.AxisX.MajorGrid.Enabled = false;
            chartAreaVolume.AxisY.MajorGrid.Enabled = false;
            chartAreaVolume.AxisX.LabelStyle.Enabled = false;
            chartAreaVolume.BackColor = Color.WhiteSmoke;
            chartAreaVolume.Position = new ElementPosition(0, 70, 100, 30);
            chartAreaVolume.AlignWithChartArea = "ChartAreaCandlestick";
            chart_stocks.ChartAreas.Add(chartAreaVolume);

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

            var volumeSeries = new Series("Volume");
            volumeSeries.ChartType = SeriesChartType.Column;
            volumeSeries.XValueType = ChartValueType.DateTime;
            volumeSeries.YAxisType = AxisType.Primary;
            volumeSeries.Color = Color.LimeGreen;
            volumeSeries["PointWidth"] = "0.8";
            volumeSeries["EmptyPointValue"] = "Zero";
            volumeSeries.ChartArea = "ChartAreaVolume";
            chart_stocks.Series.Add(volumeSeries);
        }

        public void LoadStockData(string filePath, DateTime startDate, DateTime endDate)
        {
            this.filePath = filePath; // Store file path for reuse
            DataTable stockData = new DataTable();

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

            dataGridView1.DataSource = stockData;
            PopulateChart(stockData);
        }

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

                candlestickSeries.Points.AddXY(index, high, low, open, close);
                candlestickSeries.Points[index].AxisLabel = date.ToString("MMM dd");

                DataPoint volumePoint = new DataPoint
                {
                    XValue = index,
                    YValues = new double[] { volume },
                    Color = close >= open ? Color.Green : Color.Red
                };

                volumeSeries.Points.Add(volumePoint);
                index++;
            }

            double yAxisMax = maxPrice * 1.03;
            double yAxisMin = minPrice * 0.97;
            var chartArea = chart_stocks.ChartAreas["ChartAreaCandlestick"];
            chartArea.AxisY.Minimum = yAxisMin;
            chartArea.AxisY.Maximum = yAxisMax;
            chartArea.AxisY.LabelStyle.Format = "C2";
            chartArea.AxisX.Interval = 1;
            chartArea.AxisX.LabelStyle.IsEndLabelVisible = true;
            chartArea.AxisX.ScaleView.Zoomable = false;
            chartArea.AxisX.LabelStyle.Angle = -45;
            chartArea.RecalculateAxesScale();
        }
    }
}
