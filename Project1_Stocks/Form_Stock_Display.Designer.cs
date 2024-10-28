namespace Project1_Stocks
{
    partial class Form_Stock_Display
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart_stocks = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label_stockName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chart_stocks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart_stocks
            // 
            this.chart_stocks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea2.Name = "ChartArea1";
            this.chart_stocks.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart_stocks.Legends.Add(legend2);
            this.chart_stocks.Location = new System.Drawing.Point(0, 87);
            this.chart_stocks.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chart_stocks.Name = "chart_stocks";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            series2.YValuesPerPoint = 4;
            this.chart_stocks.Series.Add(series2);
            this.chart_stocks.Size = new System.Drawing.Size(1690, 581);
            this.chart_stocks.TabIndex = 0;
            this.chart_stocks.Text = "chart1";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(0, 672);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.Size = new System.Drawing.Size(1690, 297);
            this.dataGridView1.TabIndex = 1;
            // 
            // label_stockName
            // 
            this.label_stockName.AutoSize = true;
            this.label_stockName.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_stockName.Location = new System.Drawing.Point(672, 20);
            this.label_stockName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_stockName.Name = "label_stockName";
            this.label_stockName.Size = new System.Drawing.Size(270, 52);
            this.label_stockName.TabIndex = 2;
            this.label_stockName.Text = "Stock Name";
            // 
            // Form_Stock_Display
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1692, 970);
            this.Controls.Add(this.label_stockName);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.chart_stocks);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form_Stock_Display";
            this.Text = "Stock Display";
            ((System.ComponentModel.ISupportInitialize)(this.chart_stocks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart_stocks;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label_stockName;
    }
}