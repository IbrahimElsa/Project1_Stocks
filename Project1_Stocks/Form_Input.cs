using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Project1_Stocks
{
    public partial class Form_Input : Form
    {
        private int formOffset = 30;  // Offset for each new form
        private int initialFormX = 100;  // Initial X position for the first form
        private int initialFormY = 100;  // Initial Y position for the first form

        public Form_Input()
        {
            InitializeComponent();

            // Set the default date range for the start and end date pickers
            dateTimePicker_startDate.Value = new DateTime(2022, 1, 1);
            dateTimePicker_endDate.Value = DateTime.Today;

            // Configure OpenFileDialog to allow multiple file selections
            openFileDialog.Multiselect = true;

            // Attach an event handler for the "Load Stock" button click event
            button_loadStock.Click += Button_loadStock_Click;
        }

        // Event handler for loading stock data when the "Load Stock" button is clicked
        private void Button_loadStock_Click(object sender, EventArgs e)
        {
            // Show the open file dialog to select multiple files
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                DateTime startDate = dateTimePicker_startDate.Value;
                DateTime endDate = dateTimePicker_endDate.Value;

                // Track initial positions for cascading effect
                int currentFormX = initialFormX;
                int currentFormY = initialFormY;

                // Loop through each selected file and open a new form
                foreach (string filePath in openFileDialog.FileNames)
                {
                    // Create and open a new stock display form, passing the selected dates and file path
                    Form_Stock_Display displayForm = new Form_Stock_Display(startDate, endDate);
                    displayForm.SetStartAndEndDates(startDate, endDate);
                    displayForm.LoadStockData(filePath, startDate, endDate);

                    // Set the position of the form with cascading effect
                    displayForm.StartPosition = FormStartPosition.Manual;
                    displayForm.Location = new Point(currentFormX, currentFormY);

                    // Show the form for this stock
                    displayForm.Show();

                    // Update position for the next form in the cascade
                    currentFormX += formOffset;
                    currentFormY += formOffset;
                }
            }
        }
    }
}
