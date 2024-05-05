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
    public partial class fEmployee : Form
    {
        public fEmployee()
        {
            InitializeComponent();

            // Define columns for the DataGridView
            dataG.Columns.Add("empName", "Employee Name");
            dataG.Columns.Add("salary", "Monthly Salary");
            dataG.Columns.Add("otRate", "Overtime Rate");
            dataG.Columns.Add("allowance", "Allowance");
        }

        private string connectionString = "Data Source = DESKTOP-DAME84Q; Initial Catalog = programming; Persist Security Info=True;User ID = sa; Password=shaamil@";

        private void fEmployee_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Add data to the DataGridView
            string[] row = {
            txtEmpName.Text,
            txtMonthSal.Text,
            txtOTR.Text,
            txtAll.Text
                            };
            dataG.Rows.Clear(); // Clear the DataGridView
            dataG.Rows.Add(row); // Add the new row
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            // Insert data from DataGridView to database
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    foreach (DataGridViewRow dgvRow in dataG.Rows)
                    {
                        if (!dgvRow.IsNewRow)
                        {
                            string empName = dgvRow.Cells["empName"].Value.ToString();
                            decimal salary = Convert.ToDecimal(dgvRow.Cells["salary"].Value);
                            int otRate = Convert.ToInt32(dgvRow.Cells["otRate"].Value);
                            int allowance = Convert.ToInt32(dgvRow.Cells["allowance"].Value);

                            // Insert into employee table
                            string query = "INSERT INTO employee (empName, salary, otRate, allowance) VALUES (@EmpName, @Salary, @OTRate, @Allowance)";
                            SqlCommand command = new SqlCommand(query, connection);
                            command.Parameters.AddWithValue("@EmpName", empName);
                            command.Parameters.AddWithValue("@Salary", salary);
                            command.Parameters.AddWithValue("@OTRate", otRate);
                            command.Parameters.AddWithValue("@Allowance", allowance);
                            command.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Data inserted successfully!");

                    // Clear textboxes after insertion
                    txtEmpName.Clear();
                    txtMonthSal.Clear();
                    txtOTR.Clear();
                    txtAll.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting data: " + ex.Message);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Define SQL query to fetch records matching the keyword
                    string query = "SELECT empName, salary, otRate, allowance FROM employee WHERE empName LIKE @Keyword OR CONVERT(varchar, salary) LIKE @Keyword OR CONVERT(varchar, otRate) LIKE @Keyword OR CONVERT(varchar, allowance) LIKE @Keyword";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Keyword", "%" + txtKeyword.Text + "%");

                    // Create a DataTable to hold the results
                    DataTable dataTable = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);

                    // Clear existing columns of the DataGridView
                    dataG.Columns.Clear();

                    // Display the results in the DataGridView
                    dataG.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving data: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // Confirm record deletion
                DialogResult result = MessageBox.Show("Are you sure you want to delete this employee record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    int empID = int.Parse(txtEmpID.Text);

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Step 1: Disable foreign key constraints
                        SqlCommand disableConstraintsCommand = new SqlCommand("ALTER TABLE employee_year NOCHECK CONSTRAINT FK_employee_year; ALTER TABLE salary_report NOCHECK CONSTRAINT FK_salary_report_employee;", connection);
                        disableConstraintsCommand.ExecuteNonQuery();

                        // Step 2: Delete record from employee table
                        string deleteEmployeeQuery = "DELETE FROM employee WHERE empID = @EmpID";
                        SqlCommand deleteEmployeeCommand = new SqlCommand(deleteEmployeeQuery, connection);
                        deleteEmployeeCommand.Parameters.AddWithValue("@EmpID", empID);
                        deleteEmployeeCommand.ExecuteNonQuery();

                        // Step 3: Delete related records from employee_year and salary_report tables
                        string deleteEmployeeYearQuery = "DELETE FROM employee_year WHERE empID = @EmpID";
                        SqlCommand deleteEmployeeYearCommand = new SqlCommand(deleteEmployeeYearQuery, connection);
                        deleteEmployeeYearCommand.Parameters.AddWithValue("@EmpID", empID);
                        deleteEmployeeYearCommand.ExecuteNonQuery();

                        string deleteSalaryReportQuery = "DELETE FROM salary_report WHERE empID = @EmpID";
                        SqlCommand deleteSalaryReportCommand = new SqlCommand(deleteSalaryReportQuery, connection);
                        deleteSalaryReportCommand.Parameters.AddWithValue("@EmpID", empID);
                        deleteSalaryReportCommand.ExecuteNonQuery();

                        // Step 4: Re-enable foreign key constraints
                        SqlCommand enableConstraintsCommand = new SqlCommand("ALTER TABLE employee_year CHECK CONSTRAINT FK_employee_year; ALTER TABLE salary_report CHECK CONSTRAINT FK_salary_report_employee;", connection);
                        enableConstraintsCommand.ExecuteNonQuery();
                    }

                    MessageBox.Show("Employee record deleted successfully.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting employee record: " + ex.Message);
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            // Get the selected row index
            int rowIndex = dataG.CurrentCell.RowIndex;

            // Update DataGridView cell values with textbox values
            DataGridViewColumn colEmpName = dataG.Columns["empName"]; // assuming the column name is "empName"
            DataGridViewColumn colMonthSal = dataG.Columns["salary"]; // assuming the column name is "salary"
            DataGridViewColumn colOTR = dataG.Columns["otRate"]; // assuming the column name is "otRate"
            DataGridViewColumn colAll = dataG.Columns["allowance"]; // assuming the column name is "allowance"

            dataG.Rows[rowIndex].Cells[colEmpName.Index].Value = txtUpEmpName.Text;
            dataG.Rows[rowIndex].Cells[colMonthSal.Index].Value = txtUpSal.Text;
            dataG.Rows[rowIndex].Cells[colOTR.Index].Value = txtUpOTR.Text;
            dataG.Rows[rowIndex].Cells[colAll.Index].Value = txtUpAll.Text;
        }

        private void btnInsertUp_Click(object sender, EventArgs e)
        {
            // Update database with values from DataGridView
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    foreach (DataGridViewRow dgvRow in dataG.Rows)
                    {
                        if (!dgvRow.IsNewRow)
                        {
                            int empID = Convert.ToInt32(dgvRow.Cells["empID"].Value); // Get empID from DataGridView

                            DataGridViewColumn colEmpName = dataG.Columns["empName"]; 
                            DataGridViewColumn colMonthSal = dataG.Columns["salary"]; 
                            DataGridViewColumn colOTR = dataG.Columns["otRate"]; 
                            DataGridViewColumn colAll = dataG.Columns["allowance"]; 

                            string empName = dgvRow.Cells[colEmpName.Index].Value != null ? dgvRow.Cells[colEmpName.Index].Value.ToString() : null;
                            decimal salary = dgvRow.Cells[colMonthSal.Index].Value != null ? Convert.ToDecimal(dgvRow.Cells[colMonthSal.Index].Value) : 0;
                            int otRate = dgvRow.Cells[colOTR.Index].Value != null ? Convert.ToInt32(dgvRow.Cells[colOTR.Index].Value) : 0;
                            int allowance = dgvRow.Cells[colAll.Index].Value != null ? Convert.ToInt32(dgvRow.Cells[colAll.Index].Value) : 0;

                            // Update employee table in the database
                            string query = "UPDATE employee SET empName = @EmpName, salary = @Salary, otRate = @OTRate, allowance = @Allowance WHERE empID = @EmpID";
                            SqlCommand command = new SqlCommand(query, connection);
                            command.Parameters.AddWithValue("@EmpID", empID);
                            command.Parameters.AddWithValue("@EmpName", empName);
                            command.Parameters.AddWithValue("@Salary", salary);
                            command.Parameters.AddWithValue("@OTRate", otRate);
                            command.Parameters.AddWithValue("@Allowance", allowance);
                            command.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Data updated successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating data: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Open the fUser form for all role IDs
            fSetting settingForm = new fSetting();
            settingForm.Show();

            // Hide the login form (optional)
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Open the fUser form for all role IDs
            fSalary salaryForm = new fSalary();
            salaryForm.Show();

            // Hide the login form (optional)
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Prompt the user for confirmation
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Check if the user clicked Yes
            if (result == DialogResult.Yes)
            {
                // Open the fLogin form
                fLogin loginForm = new fLogin();
                loginForm.Show();

                // Hide the current form (fEmployee)
                this.Hide();
            }
            // No action needed if the user clicked No
        }
    }
}
