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
    public partial class fSalary : Form
    {
        public fSalary()
        {
            InitializeComponent();
        }

        private string connectionString = "Data Source = DESKTOP-DAME84Q; Initial Catalog = programming; Persist Security Info=True;User ID = sa; Password=shaamil@";

        private void fSalary_Load(object sender, EventArgs e)
        {

        }

        private void btnAddLv_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the current month's last day
                DateTime lastDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));

                int empID = int.Parse(txtEmpID.Text);
                int leave = int.Parse(txtLv.Text);
                int otHour = int.Parse(txtOT.Text);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Retrieve leave from employee_year table
                    string selectLeaveQuery = "SELECT leave FROM employee_year WHERE empID = @EmpID";
                    SqlCommand selectLeaveCommand = new SqlCommand(selectLeaveQuery, connection);
                    selectLeaveCommand.Parameters.AddWithValue("@EmpID", empID);
                    int leaveFromYear = Convert.ToInt32(selectLeaveCommand.ExecuteScalar());

                    // Update leave in employee_year table
                    string updateLeaveQuery = "UPDATE employee_year SET leave = @Leave WHERE empID = @EmpID";
                    SqlCommand updateLeaveCommand = new SqlCommand(updateLeaveQuery, connection);
                    updateLeaveCommand.Parameters.AddWithValue("@Leave", leaveFromYear - leave);
                    updateLeaveCommand.Parameters.AddWithValue("@EmpID", empID);
                    updateLeaveCommand.ExecuteNonQuery();

                    // Calculate noPayDay based on the difference between updated leave and entered leave
                    int difference = leaveFromYear - leave;
                    int noPayDay = Math.Max(0, -difference);

                    // Insert entered leave and OT hours into employee_month table
                    string insertQuery = "INSERT INTO employee_month (empID, salMonth, leave, otHour, noPayDay) " +
                                         "VALUES (@EmpID, @SalMonth, @Leave, @OTHour, @NoPayDay)";
                    SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                    insertCommand.Parameters.AddWithValue("@EmpID", empID);
                    insertCommand.Parameters.AddWithValue("@SalMonth", lastDayOfMonth);
                    insertCommand.Parameters.AddWithValue("@Leave", leave);
                    insertCommand.Parameters.AddWithValue("@OTHour", otHour);
                    insertCommand.Parameters.AddWithValue("@NoPayDay", noPayDay);
                    insertCommand.ExecuteNonQuery();
                }

                MessageBox.Show("Leave and OT hours added successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding leave and OT hours: " + ex.Message);
            }   

        }

        private decimal CalculateNoPay(SqlConnection connection, int empID, DateTime salMonth)
        {
            string selectNoPayQuery = "SELECT (e.salary / s.workingDays) * em.noPayDay " +
                              "FROM employee e " +
                              "JOIN employee_month em ON e.empID = em.empID " +
                              "JOIN setting s ON em.salMonth = @SalMonth";
            SqlCommand selectNoPayCommand = new SqlCommand(selectNoPayQuery, connection);
            selectNoPayCommand.Parameters.AddWithValue("@SalMonth", salMonth);
            decimal noPay = Convert.ToDecimal(selectNoPayCommand.ExecuteScalar());
            return noPay;
        }

        private decimal CalculateOverTime(SqlConnection connection, int empID, DateTime salMonth)
        {
            string selectOverTimeQuery = "SELECT em.otHour * e.otRate " +
                                  "FROM employee e " +
                                  "JOIN employee_month em ON e.empID = em.empID " +
                                  "WHERE e.empID = @EmpID AND em.salMonth = @SalMonth";
            SqlCommand selectOverTimeCommand = new SqlCommand(selectOverTimeQuery, connection);
            selectOverTimeCommand.Parameters.AddWithValue("@EmpID", empID);
            selectOverTimeCommand.Parameters.AddWithValue("@SalMonth", salMonth);
            decimal overTime = Convert.ToDecimal(selectOverTimeCommand.ExecuteScalar());
            return overTime;
        }

        private decimal RetrieveAllowance(SqlConnection connection, int empID)
        {
            string selectAllowanceQuery = "SELECT allowance FROM employee WHERE empID = @EmpID";
            SqlCommand selectAllowanceCommand = new SqlCommand(selectAllowanceQuery, connection);
            selectAllowanceCommand.Parameters.AddWithValue("@EmpID", empID);
            decimal allowance = Convert.ToDecimal(selectAllowanceCommand.ExecuteScalar());
            return allowance;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the last day of the current month
                DateTime lastDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Retrieve list of existing employee IDs from employee table
                    List<int> empIDs = new List<int>();
                    string selectEmpIDsQuery = "SELECT empID FROM employee";
                    SqlCommand selectEmpIDsCommand = new SqlCommand(selectEmpIDsQuery, connection);
                    SqlDataReader reader = selectEmpIDsCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        empIDs.Add(reader.GetInt32(0));
                    }
                    reader.Close();

                    // Iterate through each employee ID
                    foreach (int empID in empIDs)
                    {
                        // Calculate noPay
                        decimal noPay = CalculateNoPay(connection, empID, lastDayOfMonth);

                        // Calculate overTime
                        decimal overTime = CalculateOverTime(connection, empID, lastDayOfMonth);

                        // Retrieve allowance
                        decimal allowance = RetrieveAllowance(connection, empID);

                        // Insert record into employee_salary table
                        string insertQuery = "INSERT INTO employee_salary (salMonth, empID, noPay, overTime, allowance, status) " +
                                             "VALUES (@SalMonth, @EmpID, @NoPay, @OverTime, @Allowance, 'Y')";
                        SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                        insertCommand.Parameters.AddWithValue("@SalMonth", lastDayOfMonth);
                        insertCommand.Parameters.AddWithValue("@EmpID", empID);
                        insertCommand.Parameters.AddWithValue("@NoPay", noPay);
                        insertCommand.Parameters.AddWithValue("@OverTime", overTime);
                        insertCommand.Parameters.AddWithValue("@Allowance", allowance);
                        insertCommand.ExecuteNonQuery();

                        string selectQuery = "SELECT * FROM employee_salary WHERE salMonth = @SalMonth";
                        SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
                        selectCommand.Parameters.AddWithValue("@SalMonth", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)));

                        SqlDataAdapter adapter = new SqlDataAdapter(selectCommand);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataG2.DataSource = dataTable;
                    }
                }

                MessageBox.Show("Monthly salary records inserted successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting monthly salary records: " + ex.Message);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        

        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the last day of the current month
                DateTime salMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Query to calculate basePay, grossPay, and noPay and insert into salary_report table
                    string query = @"INSERT INTO salary_report (salMonth, empID, noPay, basePay, grossPay)
                             SELECT @SalMonth, e.empID, es.noPay, e.salary + e.allowance + e.otRate AS basePay,
                                    (e.salary + e.allowance + e.otRate) - (es.noPay + (e.salary + e.allowance + e.otRate) * 0.3) AS grossPay
                             FROM employee e
                             INNER JOIN employee_salary es ON e.empID = es.empID
                             WHERE es.salMonth = @SalMonth";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@SalMonth", salMonth);
                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Salary report generated successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating salary report: " + ex.Message);
            }
        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {
            try
            {
                // Parse start and end date strings
                DateTime? startDate = null;
                DateTime? endDate = null;
                if (!string.IsNullOrEmpty(txtStart.Text))
                {
                    if (!DateTime.TryParse(txtStart.Text, out DateTime tempStartDate))
                    {
                        MessageBox.Show("Invalid start date format. Please provide dates in MM/DD/YYYY format.");
                        return;
                    }
                    startDate = tempStartDate;
                }
                if (!string.IsNullOrEmpty(txtEnd.Text))
                {
                    if (!DateTime.TryParse(txtEnd.Text, out DateTime tempEndDate))
                    {
                        MessageBox.Show("Invalid end date format. Please provide dates in MM/DD/YYYY format.");
                        return;
                    }
                    endDate = tempEndDate;
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Construct the SQL query based on provided date range or query all records if both text boxes are empty
                    string query = "SELECT salMonth, empID, noPay, basePay, grossPay FROM salary_report";
                    if (startDate.HasValue || endDate.HasValue)
                    {
                        query += " WHERE 1 = 1";
                        if (startDate.HasValue)
                            query += " AND salMonth >= @StartDate";
                        if (endDate.HasValue)
                            query += " AND salMonth <= @EndDate";
                    }

                    SqlCommand command = new SqlCommand(query, connection);
                    if (startDate.HasValue)
                        command.Parameters.AddWithValue("@StartDate", startDate.Value);
                    if (endDate.HasValue)
                        command.Parameters.AddWithValue("@EndDate", endDate.Value);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Populate the data grid view with the results
                    dataG2.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving salary report: " + ex.Message);
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {

        }
    }
}
