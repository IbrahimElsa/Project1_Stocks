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
            this.chart_stocks = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label_stockNameAndTimeFrame = new System.Windows.Forms.Label();
            this.label_startAndEndDates = new System.Windows.Forms.Label();
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
            this.chart_stocks.Location = new System.Drawing.Point(-21, 78);
            this.chart_stocks.Margin = new System.Windows.Forms.Padding(2);
            this.chart_stocks.Name = "chart_stocks";
            this.chart_stocks.Size = new System.Drawing.Size(1289, 517);
            this.chart_stocks.TabIndex = 0;
            this.chart_stocks.Text = "chart1";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(0, 596);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.Size = new System.Drawing.Size(1268, 241);
            this.dataGridView1.TabIndex = 1;
            // 
            // label_stockNameAndTimeFrame
            // 
            this.label_stockNameAndTimeFrame.Dock = System.Windows.Forms.DockStyle.Top;
            this.label_stockNameAndTimeFrame.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_stockNameAndTimeFrame.Location = new System.Drawing.Point(0, 0);
            this.label_stockNameAndTimeFrame.Name = "label_stockNameAndTimeFrame";
            this.label_stockNameAndTimeFrame.Size = new System.Drawing.Size(1269, 39);
            this.label_stockNameAndTimeFrame.TabIndex = 2;
            this.label_stockNameAndTimeFrame.Text = "Stock Name";
            this.label_stockNameAndTimeFrame.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_startAndEndDates
            // 
            this.label_startAndEndDates.Dock = System.Windows.Forms.DockStyle.Top;
            this.label_startAndEndDates.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_startAndEndDates.Location = new System.Drawing.Point(0, 39);
            this.label_startAndEndDates.Name = "label_startAndEndDates";
            this.label_startAndEndDates.Size = new System.Drawing.Size(1269, 37);
            this.label_startAndEndDates.TabIndex = 3;
            this.label_startAndEndDates.Text = "Start and end dates";
            this.label_startAndEndDates.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form_Stock_Display
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1269, 836);
            this.ControlBox = false;
            this.Controls.Add(this.label_startAndEndDates);
            this.Controls.Add(this.label_stockNameAndTimeFrame);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.chart_stocks);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form_Stock_Display";
            this.Text = "Stock Display";
            ((System.ComponentModel.ISupportInitialize)(this.chart_stocks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart_stocks;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label_stockNameAndTimeFrame;
        private System.Windows.Forms.Label label_startAndEndDates;
    }
}