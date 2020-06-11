using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using GemBox.Spreadsheet;

namespace FBLA_Application
{
    public partial class MemberReport : Form
    {
        private string grade;

        public MemberReport()
        {
            InitializeComponent();
            SQLiteConnection conn = new SQLiteConnection("Data Source=FBLADatabase.sqlite;Version=3;");
            conn.Open();
            SQLiteCommand command = new SQLiteCommand("Select * From Members where AmountOwed != 0.00", conn);
            using (SQLiteDataReader read = command.ExecuteReader())
            {
                while (read.Read())
                {
                    if ((Int32)read.GetValue(5) == 2012)
                    {
                        grade = "12th";
                    }
                    if ((Int32)read.GetValue(5) == 2013)
                    {
                        grade = "11th";
                    }
                    if ((Int32)read.GetValue(5) == 2014)
                    {
                        grade = "10th";
                    }
                    if ((Int32)read.GetValue(5) == 2015)
                    {
                        grade = "9th";
                    }
                    dataGridView1.Rows.Add(new object[] {
            read.GetValue(0),
            read.GetValue(1),
            read.GetValue(8),
            read.GetValue(5),
            grade,
            read.GetValue(7),
            });
                }
            }
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fname = "MemberReport";
            CommonFunctions.exportToExcel(dataGridView1, fname, true);
            
            var print = ExcelFile.Load("MemberReport.xls");
            print.Print();
            MessageBox.Show("Saved under file name 'MemberReport.xls'");
        }
    }
}
