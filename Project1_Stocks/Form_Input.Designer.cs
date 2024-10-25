namespace Project1_Stocks
{
    partial class Form_Input
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
            this.dateTimePicker_endDate = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker_startDate = new System.Windows.Forms.DateTimePicker();
            this.Label_startDate = new System.Windows.Forms.Label();
            this.Label_endDate = new System.Windows.Forms.Label();
            this.button_loadStock = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // dateTimePicker_endDate
            // 
            this.dateTimePicker_endDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker_endDate.Location = new System.Drawing.Point(515, 91);
            this.dateTimePicker_endDate.Name = "dateTimePicker_endDate";
            this.dateTimePicker_endDate.Size = new System.Drawing.Size(405, 34);
            this.dateTimePicker_endDate.TabIndex = 0;
            // 
            // dateTimePicker_startDate
            // 
            this.dateTimePicker_startDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker_startDate.Location = new System.Drawing.Point(12, 91);
            this.dateTimePicker_startDate.Name = "dateTimePicker_startDate";
            this.dateTimePicker_startDate.Size = new System.Drawing.Size(386, 34);
            this.dateTimePicker_startDate.TabIndex = 1;
            // 
            // Label_startDate
            // 
            this.Label_startDate.AutoSize = true;
            this.Label_startDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_startDate.Location = new System.Drawing.Point(146, 34);
            this.Label_startDate.Name = "Label_startDate";
            this.Label_startDate.Size = new System.Drawing.Size(118, 29);
            this.Label_startDate.TabIndex = 2;
            this.Label_startDate.Text = "Start Date";
            // 
            // Label_endDate
            // 
            this.Label_endDate.AutoSize = true;
            this.Label_endDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_endDate.Location = new System.Drawing.Point(662, 34);
            this.Label_endDate.Name = "Label_endDate";
            this.Label_endDate.Size = new System.Drawing.Size(112, 29);
            this.Label_endDate.TabIndex = 3;
            this.Label_endDate.Text = "End Date";
            // 
            // button_loadStock
            // 
            this.button_loadStock.AllowDrop = true;
            this.button_loadStock.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_loadStock.Location = new System.Drawing.Point(345, 219);
            this.button_loadStock.Name = "button_loadStock";
            this.button_loadStock.Size = new System.Drawing.Size(220, 71);
            this.button_loadStock.TabIndex = 4;
            this.button_loadStock.Text = "Load Stock";
            this.button_loadStock.UseVisualStyleBackColor = true;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // Form_Input
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(937, 391);
            this.Controls.Add(this.button_loadStock);
            this.Controls.Add(this.Label_endDate);
            this.Controls.Add(this.Label_startDate);
            this.Controls.Add(this.dateTimePicker_startDate);
            this.Controls.Add(this.dateTimePicker_endDate);
            this.Name = "Form_Input";
            this.Text = "Input Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker_endDate;
        private System.Windows.Forms.DateTimePicker dateTimePicker_startDate;
        private System.Windows.Forms.Label Label_startDate;
        private System.Windows.Forms.Label Label_endDate;
        private System.Windows.Forms.Button button_loadStock;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}

