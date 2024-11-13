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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            this.chart_stocks = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label_stockNameAndTimeFrame = new System.Windows.Forms.Label();
            this.label_startAndEndDates = new System.Windows.Forms.Label();
            this.Label_endDate = new System.Windows.Forms.Label();
            this.Label_startDate = new System.Windows.Forms.Label();
            this.dateTimePicker_startDateUpdate = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker_endDateUpdate = new System.Windows.Forms.DateTimePicker();
            this.button_updateDates = new System.Windows.Forms.Button();
            this.toolTip_candlestick = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.chart_stocks)).BeginInit();
            this.SuspendLayout();
            // 
            // chart_stocks
            // 
            this.chart_stocks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea2.Name = "ChartArea1";
            this.chart_stocks.ChartAreas.Add(chartArea2);
            this.chart_stocks.Location = new System.Drawing.Point(-28, 96);
            this.chart_stocks.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chart_stocks.Name = "chart_stocks";
            this.chart_stocks.Size = new System.Drawing.Size(1719, 909);
            this.chart_stocks.TabIndex = 0;
            this.chart_stocks.Text = "chart1";
            // 
            // label_stockNameAndTimeFrame
            // 
            this.label_stockNameAndTimeFrame.Dock = System.Windows.Forms.DockStyle.Top;
            this.label_stockNameAndTimeFrame.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_stockNameAndTimeFrame.Location = new System.Drawing.Point(0, 0);
            this.label_stockNameAndTimeFrame.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_stockNameAndTimeFrame.Name = "label_stockNameAndTimeFrame";
            this.label_stockNameAndTimeFrame.Size = new System.Drawing.Size(1692, 48);
            this.label_stockNameAndTimeFrame.TabIndex = 2;
            this.label_stockNameAndTimeFrame.Text = "Stock Name";
            this.label_stockNameAndTimeFrame.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_startAndEndDates
            // 
            this.label_startAndEndDates.Dock = System.Windows.Forms.DockStyle.Top;
            this.label_startAndEndDates.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_startAndEndDates.Location = new System.Drawing.Point(0, 48);
            this.label_startAndEndDates.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_startAndEndDates.Name = "label_startAndEndDates";
            this.label_startAndEndDates.Size = new System.Drawing.Size(1692, 46);
            this.label_startAndEndDates.TabIndex = 3;
            this.label_startAndEndDates.Text = "Start and end dates";
            this.label_startAndEndDates.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label_endDate
            // 
            this.Label_endDate.AutoSize = true;
            this.Label_endDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_endDate.Location = new System.Drawing.Point(676, 1049);
            this.Label_endDate.Name = "Label_endDate";
            this.Label_endDate.Size = new System.Drawing.Size(159, 39);
            this.Label_endDate.TabIndex = 7;
            this.Label_endDate.Text = "End Date";
            // 
            // Label_startDate
            // 
            this.Label_startDate.AutoSize = true;
            this.Label_startDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_startDate.Location = new System.Drawing.Point(45, 1050);
            this.Label_startDate.Name = "Label_startDate";
            this.Label_startDate.Size = new System.Drawing.Size(169, 39);
            this.Label_startDate.TabIndex = 6;
            this.Label_startDate.Text = "Start Date";
            // 
            // dateTimePicker_startDateUpdate
            // 
            this.dateTimePicker_startDateUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker_startDateUpdate.Location = new System.Drawing.Point(218, 1053);
            this.dateTimePicker_startDateUpdate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dateTimePicker_startDateUpdate.Name = "dateTimePicker_startDateUpdate";
            this.dateTimePicker_startDateUpdate.Size = new System.Drawing.Size(385, 34);
            this.dateTimePicker_startDateUpdate.TabIndex = 5;
            // 
            // dateTimePicker_endDateUpdate
            // 
            this.dateTimePicker_endDateUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker_endDateUpdate.Location = new System.Drawing.Point(841, 1054);
            this.dateTimePicker_endDateUpdate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dateTimePicker_endDateUpdate.Name = "dateTimePicker_endDateUpdate";
            this.dateTimePicker_endDateUpdate.Size = new System.Drawing.Size(405, 34);
            this.dateTimePicker_endDateUpdate.TabIndex = 4;
            // 
            // button_updateDates
            // 
            this.button_updateDates.AllowDrop = true;
            this.button_updateDates.Enabled = false;
            this.button_updateDates.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_updateDates.Location = new System.Drawing.Point(1349, 1037);
            this.button_updateDates.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_updateDates.Name = "button_updateDates";
            this.button_updateDates.Size = new System.Drawing.Size(251, 52);
            this.button_updateDates.TabIndex = 8;
            this.button_updateDates.Text = "Update Dates";
            this.button_updateDates.UseVisualStyleBackColor = true;
            // 
            // Form_Stock_Display
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1692, 1103);
            this.Controls.Add(this.button_updateDates);
            this.Controls.Add(this.Label_endDate);
            this.Controls.Add(this.Label_startDate);
            this.Controls.Add(this.dateTimePicker_startDateUpdate);
            this.Controls.Add(this.dateTimePicker_endDateUpdate);
            this.Controls.Add(this.label_startAndEndDates);
            this.Controls.Add(this.label_stockNameAndTimeFrame);
            this.Controls.Add(this.chart_stocks);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form_Stock_Display";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stock Display";
            ((System.ComponentModel.ISupportInitialize)(this.chart_stocks)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart_stocks;
        private System.Windows.Forms.Label label_stockNameAndTimeFrame;
        private System.Windows.Forms.Label label_startAndEndDates;
        private System.Windows.Forms.Label Label_endDate;
        private System.Windows.Forms.Label Label_startDate;
        private System.Windows.Forms.DateTimePicker dateTimePicker_startDateUpdate;
        private System.Windows.Forms.DateTimePicker dateTimePicker_endDateUpdate;
        private System.Windows.Forms.Button button_updateDates;
        private System.Windows.Forms.ToolTip toolTip_candlestick;
    }
}