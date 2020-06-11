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

namespace FBLA_Application
{
    public partial class Form1 : Form
    {
        string var;

        public Form1()
        {
            InitializeComponent();
            readInValues();
        }

        private void readInValues()
        {
            CommonFunctions.amountOwedTotal = 0;
            CommonFunctions.numberActive = 0;
            CommonFunctions.numberNonActive = 0;
            CommonFunctions.numberOwing = 0;
            SQLiteConnection conn = new SQLiteConnection("Data Source=FBLADatabase.sqlite;Version=3;");
            conn.Open();
            SQLiteCommand command = new SQLiteCommand("Select * From Members", conn);
            using (SQLiteDataReader read = command.ExecuteReader())
            {
                while (read.Read())
                {
                    if (read.GetValue(6).ToString() == "No")
                    {
                        CommonFunctions.numberNonActive++;
                    }
                    if(read.GetValue(6).ToString() == "Yes")
                    {
                        CommonFunctions.numberActive++;
                    }
                    if (Convert.ToInt32(read.GetValue(7)) > 0)
                    {
                        CommonFunctions.numberOwing++;
                        CommonFunctions.amountOwedTotal += Convert.ToInt32(read.GetValue(7));
                    }

                    dataGridView1.Rows.Add(new object[] {
            read.GetValue(0),
            read.GetValue(1),
            read.GetValue(2),
            read.GetValue(3),
            read.GetValue(4),
            read.GetValue(5),
            read.GetValue(6),
            read.GetValue(7),
            read.GetValue(8)
            });
                }
            }
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            readInValues();
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Create Member Report
            MemberReport mr = new MemberReport();
            mr.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Create Senior Report
            SeniorReport sr = new SeniorReport();
            sr.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Add Record clicked
            AddMember am = new AddMember();
            am.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //Edit Record Clicked
            EditMember em = new EditMember();
            if (em.showClose == 1)
            {
                em.Show();
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            //Delete Record
            var = Microsoft.VisualBasic.Interaction.InputBox("Enter the Membership Number", "Delete Record", "");
            SQLiteConnection conn = new SQLiteConnection("Data Source=FBLADatabase.sqlite;Version=3;");
            conn.Open();

            int check = CommonFunctions.checkIfRowExists(var.ToString());
            if (check == 0)
            {
                MessageBox.Show("There is not a record with the ID you entered");
                return;
            }
            SQLiteCommand command = new SQLiteCommand($"Delete from Members where MemberNumber={var}", conn);
            command.ExecuteNonQuery();
            conn.Close();
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //Help Menu
            HelpMenu hm = new HelpMenu();
            hm.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This application was created by Srikur Kanuparthy for the FBLA Desktop Application Programming event");
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
