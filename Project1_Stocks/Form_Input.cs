using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace Project1_Stocks
{
    public partial class Form_Input : Form
    {
        public Form_Input()
        {
            InitializeComponent();
            // Set default date for date pickers
            dateTimePicker_startDate.Value = DateTime.Today.AddMonths(-1);
            dateTimePicker_endDate.Value = DateTime.Today;

            // Event handler for Load Stock button
            button_loadStock.Click += Button_loadStock_Click;
        }

        private void Button_loadStock_Click(object sender, EventArgs e)
        {
            // Open file dialog to select CSV file
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                DateTime startDate = dateTimePicker_startDate.Value;
                DateTime endDate = dateTimePicker_endDate.Value;

                // Load data and open display form
                Form_Stock_Display displayForm = new Form_Stock_Display();
                displayForm.LoadStockData(filePath, startDate, endDate);
                displayForm.Show();
            }
        }
    }
}
