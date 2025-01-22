using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.Common;
using System.Collections;

/*
 * Michael Lee
 * CPT-206
 * Lab 2
 */

namespace Michael_Lee_Ch12_Lab_2_Population_Database_CPT_206
{
    public partial class MainForm : Form
    {

        // Get connection string dynamically
        private string connectionString = ConfigurationManager.ConnectionStrings["Michael_Lee_Ch12_Lab_2_Population_Database_CPT_206.Properties.Settings.CityDBConnectionString"].ConnectionString;

        // Track last sort order
        private string lastSortColumn = "CityID";  // Default column
        private string lastSortOrder = "ASC";  // Default order

        public MainForm()
        {
            InitializeComponent();

            // Hack to prevent the BindingNavigator from trying to create a new row instead of using the AddNewCityForm
            bindingNav.AddNewItem = null;

            bindingNav.DeleteItem = null; // Stops automatic deletion
            bindingNavigatorDeleteItem.Enabled = true;

        }

        public void RefreshData()
        {
            this.cityTableAdapter.Fill(this.cityDBDataSet.City);

            // Preserve sorting after refresh
            if (!string.IsNullOrEmpty(lastSortColumn))
            {
                DataView dv = cityDBDataSet.City.DefaultView;
                dv.Sort = $"{lastSortColumn} {lastSortOrder}";
                cityDataGridView.DataSource = dv;
            }

            UpdateStatistics();

        }

        private void UpdateStatistics()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();

                // Get total population
                string totalQuery = "SELECT SUM(Population) FROM City";
                SqlCommand totalCmd = new SqlCommand(totalQuery, conn);
                object totalResult = totalCmd.ExecuteScalar();
                totalPopulationResultLabel.Text = totalResult != DBNull.Value ? totalResult.ToString() : "0";

                // Get average Population
                string avgQuery = "SELECT AVG(Population) FROM City";
                SqlCommand avgCmd = new SqlCommand(avgQuery, conn);
                object avgResult = avgCmd.ExecuteScalar();
                averagePopulationResultLabel.Text = avgResult != DBNull.Value ? Math.Round(Convert.ToDouble(avgResult)).ToString() : "0";

                // Get highest population
                string maxQuery = "SELECT MAX(Population) FROM City";
                SqlCommand maxCmd = new SqlCommand(maxQuery, conn);
                object maxResult = maxCmd.ExecuteScalar();
                highestPopulationResultLabel.Text = maxResult != DBNull.Value ? maxResult.ToString() : "N/A";

                // Get lowest population
                string minQuery = "SELECT MIN(Population) FROM City";
                SqlCommand minCmd = new SqlCommand(minQuery, conn);
                object minResult = minCmd.ExecuteScalar();
                lowestPopulationResultLabel.Text = minResult != DBNull.Value ? minResult.ToString() : "N/A";

                // Close the connection
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching statistics: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Load initial data
            RefreshData();

            // Set binding sources
            bindingNav.BindingSource = cityBindingSource;
            cityDataGridView.DataSource = cityBindingSource;

            // The event for when a user edits a column
            cityDataGridView.CellEndEdit += cityDataGridView_CellEndEdit;

        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (cityDataGridView.SelectedCells.Count > 0)
            {
                int rowIndex = cityDataGridView.SelectedCells[0].RowIndex;

                // Use the actual column name
                string cityIDColumn = "dataGridViewTextBoxColumn1";
                string cityNameColumn = "dataGridViewTextBoxColumn2";

                // Try to extract CityID. A bit hacky... Probably could do this a better way.
                object cityIDValue = cityDataGridView.Rows[rowIndex].Cells[cityIDColumn].Value;
                object cityNameValue = cityDataGridView.Rows[rowIndex].Cells[cityNameColumn].Value;

                if (cityIDValue != null && int.TryParse(cityIDValue.ToString(), out int cityID))
                {

                    // Delete the selected city from the database
                    DialogResult confirm = MessageBox.Show($"Delete {cityNameValue}?",
                        "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (confirm == DialogResult.Yes)
                    {
                        try
                        {
                            SqlConnection conn = new SqlConnection(connectionString);
                            conn.Open();

                            string query = "DELETE FROM City WHERE CityID = @CityID";
                            SqlCommand cmd = new SqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@CityID", cityID);
                            int rowsAffected = cmd.ExecuteNonQuery();

                            conn.Close();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Record deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("No records were deleted. Check if the CityID exists.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }

                            // Refresh DataGridView
                            RefreshData();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error deleting record: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Invalid CityID. Cannot delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cityDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < cityDataGridView.Rows.Count - 1) // Ignore new row
            {
                try
                {

                    // Get the column values
                    int cityID = Convert.ToInt32(cityDataGridView.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn1"].Value);
                    string cityName = cityDataGridView.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn2"].Value.ToString();
                    string state = cityDataGridView.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn3"].Value.ToString();
                    int population = Convert.ToInt32(cityDataGridView.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn4"].Value);

                    SqlConnection conn = new SqlConnection(connectionString);

                    // Open the connection
                    conn.Open();

                    // Update query
                    string query = "UPDATE City SET City = @City, State = @State, Population = @Population WHERE CityID = @CityID";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@City", cityName);
                    cmd.Parameters.AddWithValue("@State", state);
                    cmd.Parameters.AddWithValue("@Population", population);
                    cmd.Parameters.AddWithValue("@CityID", cityID);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();

                    RefreshData();

                    if (rowsAffected < 0)
                    {
                        MessageBox.Show("Update failed. Check if the CityID exists.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating record: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void bindingNav_RefreshItems(object sender, EventArgs e)
        {

        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            // Cancel the automatic cityDataGridView event
            // I prefer having a new Form window rather than inputting rows manually... Cleaner...
            // Hope that is ok!
            cityBindingSource.CancelEdit();

            // Open the AddCityForm
            AddCityForm addCityForm = new AddCityForm(this, connectionString);
            addCityForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sortByAscendingButton_Click(object sender, EventArgs e)
        {
            lastSortColumn = "Population";
            lastSortOrder = "ASC";

            DataView dataView = cityDBDataSet.City.DefaultView;
            dataView.Sort = $"{lastSortColumn} {lastSortOrder}";
            cityDataGridView.DataSource = dataView;
        }

        private void sortByDescendingButton_Click(object sender, EventArgs e)
        {
            lastSortColumn = "Population";
            lastSortOrder = "DESC";

            DataView dataView = cityDBDataSet.City.DefaultView;
            dataView.Sort = $"{lastSortColumn} {lastSortOrder}";
            cityDataGridView.DataSource = dataView;
        }

        private void sortByNameButton_Click(object sender, EventArgs e)
        {
            lastSortColumn = "City";
            lastSortOrder = "ASC";

            DataView dataView = cityDBDataSet.City.DefaultView;
            dataView.Sort = $"{lastSortColumn} {lastSortOrder}";
            cityDataGridView.DataSource = dataView;
        }

    }
}
