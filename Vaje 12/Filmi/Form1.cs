using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;


namespace Filmi
{
    public partial class Form1 : Form
    {
        private DataTable filmiTable;

        public Form1()
        {
            InitializeComponent();
            LoadFilmiData();
            PopulateComboBox();
        }

        private void LoadFilmiData()
        {
            try
            {
                string connectionString = "Data Source=filmi.sqlite";
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM filmi";
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection);

                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "filmi");

                    filmiTable = dataSet.Tables["filmi"];
                    dataGridView1.DataSource = filmiTable;

                    // After filling the dataset
                    filmiTable = dataSet.Tables["filmi"];

                    // Add a new column to hold decoded descriptions
                    filmiTable.Columns.Add("opis_decoded", typeof(string));

                    // Fill decoded values from the BLOB
                    foreach (DataRow row in filmiTable.Rows)
                    {
                        if (row["opis"] != DBNull.Value)
                        {
                            byte[] blob = (byte[])row["opis"];
                            string decoded = Encoding.UTF8.GetString(blob); // or Encoding.Default
                            row["opis_decoded"] = decoded;
                        }
                    }

                    // Show everything in the DataGridView, including the decoded version
                    dataGridView1.DataSource = filmiTable;

                    // Optional: hide the original BLOB column
                    if (dataGridView1.Columns.Contains("opis"))
                    {
                        dataGridView1.Columns["opis"].Visible = false;
                    }

                    

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka z nalaganjem podatkov: " + ex.Message);
            }
        }

        private void PopulateComboBox()
        {
            comboBoxColumn.Items.Add("Naslov");
            comboBoxColumn.Items.Add("Leto");
            comboBoxColumn.Items.Add("Reziser");
            comboBoxColumn.Items.Add("Dodaj novi film...");
            comboBoxColumn.Items.Add("Spremeni ocene za leto...");

            comboBoxColumn.SelectedIndexChanged += comboBoxColumn_SelectedIndexChanged;
            comboBoxColumn.SelectedIndex = 0;
        }

        private void comboBoxColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = comboBoxColumn.SelectedItem?.ToString();

            bool addingMovie = selected == "Dodaj novi film...";
            bool changingRatings = selected == "Spremeni ocene za leto...";

            textBoxSearch.Visible = !addingMovie && !changingRatings;
            buttonSearch.Visible = !addingMovie && !changingRatings;

            labelTitle.Visible = addingMovie;
            textBoxTitle.Visible = addingMovie;
            labelYear.Visible = addingMovie;
            textBoxYear.Visible = addingMovie;
            labelDirector.Visible = addingMovie;
            textBoxDirector.Visible = addingMovie;
            buttonAddMovie.Visible = addingMovie;

            labelChangeYear.Visible = changingRatings;
            textBoxChangeYear.Visible = changingRatings;
            labelChangeDelta.Visible = changingRatings;
            textBoxChangeDelta.Visible = changingRatings;
            buttonApplyChange.Visible = changingRatings;
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string selectedColumn = comboBoxColumn.SelectedItem.ToString();
            string searchText = textBoxSearch.Text.Trim().Replace("'", "''");

            if (string.IsNullOrWhiteSpace(searchText))
            {
                dataGridView1.DataSource = filmiTable;
                return;
            }

            DataView view = new DataView(filmiTable);
            if (selectedColumn == "leto")
            {
                // leto is an integer, so do exact match
                if (int.TryParse(searchText, out int year) && year > 0)
                {
                    view.RowFilter = $"Convert(leto, 'System.String') LIKE '%{year}%'";
                }
                else
                {
                    MessageBox.Show("Prosim vnesite pravilno številko za leto.");
                    return;
                }
            }
            else
            {
                view.RowFilter = $"{selectedColumn} LIKE '%{searchText}%'";
            }

            dataGridView1.DataSource = view;
        }

        private void buttonAddMovie_Click(object sender, EventArgs e)
        {
            string title = textBoxTitle.Text.Trim();
            string yearText = textBoxYear.Text.Trim();
            string director = textBoxDirector.Text.Trim();

            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(yearText))
            {
                MessageBox.Show("Prosim vnesite ime filma in leto.");
                return;
            }

            if (!int.TryParse(yearText, out int year))
            {
                MessageBox.Show("Leto more biti celo stevilo.");
                return;
            }

            if (year < 0)
            {
                MessageBox.Show("Leto more biti pozitivno");
                return;
            }

            try
            {
                string connectionString = "Data Source=filmi.sqlite";
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string insertQuery = "INSERT INTO filmi (naslov, leto, reziser) VALUES (@naslov, @leto, @reziser)";
                    using (SQLiteCommand command = new SQLiteCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@naslov", title);
                        command.Parameters.AddWithValue("@leto", year);
                        command.Parameters.AddWithValue("@reziser", string.IsNullOrWhiteSpace(director) ? DBNull.Value : (object)director);

                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Film uspesno dodan!");
                LoadFilmiData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri dodajanju filma: " + ex.Message);
            }
        }

        private void buttonApplyChange_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBoxChangeYear.Text.Trim(), out int year))
            {
                MessageBox.Show("Nepravilno leto.");
                return;
            }

            if (year < 0)
            {
                MessageBox.Show("Leto more biti pozitivno");
                return;
            }

            if (!double.TryParse(textBoxChangeDelta.Text.Trim(), out double delta))
            {
                MessageBox.Show("Nepravilna sprememba.");
                return;
            }

            int changed = 0;

            foreach (DataRow row in filmiTable.Rows)
            {
                if (row["leto"] != DBNull.Value && row["ocena"] != DBNull.Value &&
                    Convert.ToInt32(row["leto"]) == year)
                {
                    double oldRating = Convert.ToDouble(row["ocena"]);
                    row["ocena"] = oldRating + delta;
                    changed++;
                }
            }

            MessageBox.Show($"{changed} ocen je spremenjeno.");
            dataGridView1.DataSource = filmiTable;
        }
    }
}
