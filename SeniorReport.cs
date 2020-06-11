using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using GemBox.Spreadsheet;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GemBox.Spreadsheet.WinFormsUtilities;

namespace FBLA_Application
{
    public partial class SeniorReport : Form
    {
        public SeniorReport()
        {
            InitializeComponent();
            //Load the database into the DataGridView
            SQLiteConnection conn = new SQLiteConnection("Data Source=FBLADatabase.sqlite;Version=3;");
            conn.Open();
            SQLiteCommand command = new SQLiteCommand("Select * From Members where YearJoined = '2012'", conn);
            using (SQLiteDataReader read = command.ExecuteReader())
            {
                while (read.Read())
                {
                    dataGridView1.Rows.Add(new object[] {
            read.GetValue(0),
            read.GetValue(1),
            read.GetValue(4),
            read.GetValue(3)
            });
                }
            }
            conn.Close();
        }

        private void SeniorReport_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Print the report
            Process process = new Process();
            process.StartInfo.FileName = "SeniorReport.xls";
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.Verb = "Print";
            process.Start();
            process.WaitForExit();
            process.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Export to Excel
            CommonFunctions.exportToExcel(dataGridView1, "SeniorReport", false);
        }
    }
}
