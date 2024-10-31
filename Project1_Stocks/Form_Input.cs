using System;
using System.IO;
using System.Windows.Forms;

namespace Project1_Stocks
{
    public partial class Form_Input : Form
    {
        public Form_Input()
        {
            InitializeComponent();

            // Set the default date range for the start and end date pickers
            dateTimePicker_startDate.Value = DateTime.Today.AddMonths(-1);
            dateTimePicker_endDate.Value = DateTime.Today;

            // Attach an event handler for the "Load Stock" button click event
            button_loadStock.Click += Button_loadStock_Click;
        }

        // Event handler for loading stock data when the "Load Stock" button is clicked
        private void Button_loadStock_Click(object sender, EventArgs e)
        {
            // Show the open file dialog to select a file
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                DateTime startDate = dateTimePicker_startDate.Value;
                DateTime endDate = dateTimePicker_endDate.Value;

                // Create and open a new stock display form, passing the selected dates and file path
                Form_Stock_Display displayForm = new Form_Stock_Display(startDate, endDate);
                displayForm.SetStartAndEndDates(startDate, endDate);
                displayForm.LoadStockData(filePath, startDate, endDate);
                displayForm.Show();
            }
        }
    }
}