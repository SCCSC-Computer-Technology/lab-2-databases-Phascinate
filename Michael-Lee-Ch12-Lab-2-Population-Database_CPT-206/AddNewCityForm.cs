using System;
using System.Data.SqlClient;
using System.Windows.Forms;

/*
 * Michael Lee
 * CPT-206
 * Lab 2
 */

namespace Michael_Lee_Ch12_Lab_2_Population_Database_CPT_206
{
    public partial class AddCityForm : Form
    {
        private MainForm mainForm;
        private string connectionString;

        public AddCityForm(MainForm mainForm, string connectionString)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.connectionString = connectionString;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddCityForm_Load(object sender, EventArgs e)
        {
            // Add states to combo box
            foreach (string state in StateHelper.states)
            {
                stateComboBox.Items.Add(state);
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(cityNameTextBox.Text))
            {
                MessageBox.Show("Please enter a city name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (stateComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a state.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(populationTextBox.Text, out int population) || population < 0)
            {
                MessageBox.Show("Please enter a valid population number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();

                string query = "INSERT INTO City (City, State, Population) VALUES (@City, @State, @Population)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@City", cityNameTextBox.Text);
                cmd.Parameters.AddWithValue("@State", stateComboBox.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@Population", population);

                int rowsAffected = cmd.ExecuteNonQuery();
                conn.Close();
                cmd.Dispose();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("City added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Refresh the main form DataGridView
                    mainForm.RefreshData();

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error: City was not added.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding city: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
