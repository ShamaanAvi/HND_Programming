using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Programming_Assign
{
    public partial class fSetting : Form
    {
        public fSetting()
        {
            InitializeComponent();
        }

        private string connectionString = "Data Source = DESKTOP-DAME84Q; Initial Catalog = programming; Persist Security Info=True;User ID = sa; Password=shaamil@";



        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Get the specified date range from the DateTimePickers
                    DateTime startDate = dtStart.Value;
                    DateTime endDate = dtEnd.Value;

                    // Iterate through each month in the date range
                    DateTime currentDate = new DateTime(startDate.Year, startDate.Month, 1); // Start from the first day of the start month
                    while (currentDate <= endDate)
                    {
                        // Calculate the end of the current month
                        DateTime endOfMonth = currentDate.AddMonths(1).AddDays(-1);

                        // Update the records in the setting table for the current month
                        string query = @"IF NOT EXISTS (SELECT 1 FROM setting WHERE salMonth = @SalMonth)
                        BEGIN
                            INSERT INTO setting (salMonth, salStart, salEnd, workingDays) 
                            VALUES (@SalMonth, @SalStart, @SalEnd, @WorkingDays)
                        END
                        ELSE
                        BEGIN
                            UPDATE setting 
                            SET salStart = @SalStart, 
                                salEnd = @SalEnd, 
                                workingDays = @WorkingDays
                            WHERE salMonth = @SalMonth
                        END";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@WorkingDays", Convert.ToInt32(txtDateRange.Text)); // Assuming txtDateRange contains working days
                        command.Parameters.AddWithValue("@SalStart", startDate);
                        command.Parameters.AddWithValue("@SalEnd", endDate);
                        command.Parameters.AddWithValue("@SalMonth", currentDate);

                        command.ExecuteNonQuery();

                        // Move to the next month
                        currentDate = currentDate.AddMonths(1);
                    }

                    MessageBox.Show("Settings updated successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating settings: " + ex.Message);
            }
        }

        private void btnEoyTable_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Check if the current number of records is a multiple of 12
                    string countQuery = "SELECT COUNT(*) FROM setting";
                    SqlCommand countCommand = new SqlCommand(countQuery, connection);
                    int recordCount = (int)countCommand.ExecuteScalar();

                    if (recordCount % 12 == 0)
                    {
                        // Check if none of the fields in the current records are NULL
                        string nullCheckQuery = "SELECT COUNT(*) FROM setting WHERE workingDays IS NULL OR salStart IS NULL OR salEnd IS NULL";
                        SqlCommand nullCheckCommand = new SqlCommand(nullCheckQuery, connection);
                        int nullCount = (int)nullCheckCommand.ExecuteScalar();

                        if (nullCount == 0)
                        {
                            // Insert salMonth for each month of the next year
                            DateTime currentDate = DateTime.Now.AddYears(1).AddMonths(1); // Start from January of next year
                            while (currentDate.Month != 1 || currentDate.Year != DateTime.Now.AddYears(2).Year) // Stop at December of next year
                            {
                                DateTime startOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);

                                // Insert record for the current month with salMonth filled and other fields NULL
                                string insertQuery = "INSERT INTO setting (salMonth) VALUES (@SalMonth)";
                                SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                                insertCommand.Parameters.AddWithValue("@SalMonth", startOfMonth);
                                insertCommand.ExecuteNonQuery();

                                // Move to the next month
                                currentDate = currentDate.AddMonths(1);
                            }

                            MessageBox.Show("Next year's records inserted successfully.");
                        }
                        else
                        {
                            MessageBox.Show("Cannot insert records. Some fields in the current records are NULL.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Cannot insert records. At least one month has not been included this year.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting records: " + ex.Message);
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Query to select all records from the setting table
                    string query = "SELECT * FROM setting";
                    SqlCommand command = new SqlCommand(query, connection);

                    // Create a DataTable to hold the result
                    DataTable dataTable = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);

                    // Bind the DataTable to the DataGridView
                    dataG1.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving data: " + ex.Message);
            }
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the current year
                int currentYear = DateTime.Now.Year;

                // Get the value typed in txtLeave
                int leaveValue = Convert.ToInt32(txtLeave.Text);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Update leave field for existing records in employee_year table
                    string updateQuery = "UPDATE employee_year SET leave = @Leave WHERE empID IN (SELECT empID FROM employee)";
                    SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@Leave", leaveValue);
                    updateCommand.ExecuteNonQuery();

                    // Insert new records for empIDs that don't already exist in employee_year table
                    string insertQuery = "INSERT INTO employee_year (empID, empYear, leave) " +
                                         "SELECT empID, @EmpYear, @Leave FROM employee " +
                                         "WHERE empID NOT IN (SELECT empID FROM employee_year)";
                    SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                    insertCommand.Parameters.AddWithValue("@EmpYear", currentYear);
                    insertCommand.Parameters.AddWithValue("@Leave", leaveValue);
                    insertCommand.ExecuteNonQuery();

                    MessageBox.Show("Records updated/inserted successfully into employee_year table.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating/inserting records: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Open the fEmployee form for all role IDs
            fEmployee employeeForm = new fEmployee();
            employeeForm.Show();

            // Hide the login form (optional)
            this.Hide();
        }

        private void fSetting_Load(object sender, EventArgs e)
        {

        }
    }
}
