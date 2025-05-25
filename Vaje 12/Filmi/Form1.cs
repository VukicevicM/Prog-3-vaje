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

        private Dictionary<string, string> columnMapping;

        private void PopulateComboBox()
        {
                    // Map visible names to actual column names in your table
            columnMapping = new Dictionary<string, string>()
            {
                { "Naslov", "naslov" },
                { "Leto", "leto" },
                { "Reziser", "reziser" },
                { "Dodaj novi film...", "add" },
                { "Spremeni ocene za leto...", "change" }
            };

            comboBoxColumn.Items.AddRange(columnMapping.Keys.ToArray());
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
            string selectedDisplay = comboBoxColumn.SelectedItem.ToString();
            string selectedColumn = columnMapping[selectedDisplay];
            string searchText = textBoxSearch.Text.Trim().Replace("'", "''");

            if (string.IsNullOrWhiteSpace(searchText))
            {
                dataGridView1.DataSource = filmiTable;
                return;
            }

            DataView view = new DataView(filmiTable);

            try
            {
                if (selectedColumn == "leto")
                {
                    if (int.TryParse(searchText, out int year))
                    {
                        // Exact numeric match — no LIKE
                        view.RowFilter = $"leto = {year}";
                    }
                    else
                    {
                        MessageBox.Show("Prosim vnesite pravilno številko za leto.");
                        return;
                    }
                }
                else if (selectedColumn == "naslov" || selectedColumn == "reziser")
                {
                    view.RowFilter = $"{selectedColumn} LIKE '%{searchText}%'";
                }
                else
                {
                    MessageBox.Show("Iskanje ni omogočeno za to možnost.");
                    return;
                }

                dataGridView1.DataSource = view;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri filtriranju: " + ex.Message);
            }
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
                MessageBox.Show("Leto mora biti celo število.");
                return;
            }

            if (year < 0)
            {
                MessageBox.Show("Leto mora biti pozitivno.");
                return;
            }

            try
            {
                DataRow newRow = filmiTable.NewRow();
                newRow["naslov"] = title;
                newRow["leto"] = year;

                if (filmiTable.Columns.Contains("reziser") && !string.IsNullOrWhiteSpace(director))
                {
                    newRow["reziser"] = director;
                }

                // Set defaults for other columns
                if (filmiTable.Columns.Contains("ocena"))
                    newRow["ocena"] = DBNull.Value;

                if (filmiTable.Columns.Contains("opis"))
                    newRow["opis"] = DBNull.Value;

                filmiTable.Rows.Add(newRow);

                MessageBox.Show("Film je bil dodan v lokalno kopijo (ni shranjeno v bazo).");
                dataGridView1.DataSource = filmiTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri dodajanju filma v lokalno kopijo: " + ex.Message);
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
                MessageBox.Show("Leto mora biti pozitivno.");
                return;
            }

            if (!double.TryParse(textBoxChangeDelta.Text.Trim(), out double delta))
            {
                MessageBox.Show("Nepravilna sprememba ocene.");
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

            MessageBox.Show($"{changed} ocen je bilo spremenjenih (lokalno, brez shranjevanja v bazo).");
            dataGridView1.DataSource = filmiTable;
        }
    }
}
