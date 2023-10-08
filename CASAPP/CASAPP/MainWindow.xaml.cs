using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using CASAPP.Models;
using Microsoft.EntityFrameworkCore;
using MessageBox = System.Windows.Forms.MessageBox;



namespace CASAPP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DataTable ResultsTable { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            //loaded += (s, e) => keyboard.focus(SearchBox);

        }

        private void textChangedEventHandler(object sender, TextChangedEventArgs args)
        {
            
            string search = SearchBox.Text;
            string digits = string.Concat(search.Where(Char.IsDigit));

            System.Data.DataTable table = new DataTable("SearchResults");

            DataColumn Activity = new DataColumn("Activity", typeof(string));
            DataColumn Casrn = new DataColumn("CAS", typeof(string));
            DataColumn ChemName = new DataColumn("Name", typeof(string));

            table.Columns.Add(Activity);
            table.Columns.Add(Casrn);
            table.Columns.Add(ChemName);
            

            if (search.Length < 2)
            {
                DataGridResults.Visibility = Visibility.Hidden;
                btnSaveFile.IsEnabled = false;
                btnSaveFile.Visibility = Visibility.Hidden;
            }
            else if (search.Length > 2) 
            {
                using (Data db = new())
                {
                    try
                    {
                        IQueryable<Ca>? cas = db.Cas?.Where(p => EF.Functions.Like(p.ChemName, $"%{search}%"))

                            .OrderBy(p => p.ChemName)
                            .OrderBy(p => p.Activity)
                            .Take(1000);

                        foreach (Ca p in cas)
                        {
                            DataRow row = table.NewRow();

                            row["Activity"] = p.Activity;
                            row["CAS"] = p.Casrn;
                            row["Name"] = p.ChemName;
                            table.Rows.Add(row);

                        }
                        
                        if (digits.Length > 2 && digits.Length < 11)
                        {
                            IQueryable<Ca>? casDigits = db.Cas?.Where(p => EF.Functions.Like(p.Casregno, $"%{digits}%"))

                                .OrderBy(p => p.ChemName)
                                .OrderBy(p => p.Activity)
                                .Take(1000);
                            
                            foreach (Ca p in casDigits)
                            {
                                DataRow row = table.NewRow();

                                row["Activity"] = p.Activity;
                                row["CAS"] = p.Casrn;
                                row["Name"] = p.ChemName;
                                table.Rows.Add(row);
                            }
                        }
                                                                    
                        DataGridResults.ItemsSource = table.DefaultView;
                        int numberOfRecords = table.Rows.Count;
                        
                        if (numberOfRecords == 1000) 
                        {
                            NumberResults.Text = "Greater than 1000 Results";
                            DataGridResults.Visibility= Visibility.Visible;
                            btnSaveFile.IsEnabled = true;
                            btnSaveFile.Visibility = Visibility.Visible;
                        }
                        
                        else if (numberOfRecords > 0)
                        {
                            NumberResults.Text = "Number of Results: " + numberOfRecords.ToString();
                            DataGridResults.Visibility = Visibility.Visible;
                            btnSaveFile.IsEnabled = true;
                            btnSaveFile.Visibility = Visibility.Visible;
                        }
                        
                        else
                        {
                            NumberResults.Text= "No Results";
                            DataGridResults.Visibility = Visibility.Hidden;
                            btnSaveFile.IsEnabled = false;
                            btnSaveFile.Visibility = Visibility.Hidden;
                        }

                        ResultsTable = table;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);

                    }
                }
            }
        }
        
        public void btnSaveFile_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV (*.CSV;)|*.CSV;|All files (*.*) | *.*";
                saveFileDialog.DefaultExt = "csv";
                saveFileDialog.ShowDialog();
                StringBuilder data = ConvertDataTableToCsvFile(ResultsTable);
                SaveData(data, saveFileDialog.FileName);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        
        public StringBuilder ConvertDataTableToCsvFile(DataTable dtData)
        {
            StringBuilder data = new StringBuilder();

            //Taking the column names.
            for (int column = 0; column < dtData.Columns.Count; column++)
            {
                //Making sure that end of the line, shoould not have comma delimiter.
                if (column == dtData.Columns.Count - 1)
                    data.Append(dtData.Columns[column].ColumnName.ToString().Replace(",", ";"));
                else
                    data.Append(dtData.Columns[column].ColumnName.ToString().Replace(",", ";") + ',');
            }

            data.Append(Environment.NewLine);//New line after appending columns.

            for (int row = 0; row < dtData.Rows.Count; row++)
            {
                for (int column = 0; column < dtData.Columns.Count; column++)
                {
                    ////Making sure that end of the line, shoould not have comma delimiter.
                    if (column == dtData.Columns.Count - 1)
                        data.Append(dtData.Rows[row][column].ToString().Replace(",", ";"));
                    else
                        data.Append(dtData.Rows[row][column].ToString().Replace(",", ";") + ',');
                }

                //Making sure that end of the file, should not have a new line.
                if (row != dtData.Rows.Count - 1)
                    data.Append(Environment.NewLine);
            }
            return data;
        }

        public void SaveData(StringBuilder data, string filePath)
        {
            using (StreamWriter objWriter = new StreamWriter(filePath))
            {
                objWriter.WriteLine(data);
            }
        }
    }
}
