using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FBLA_Application
{
    public partial class EditMember : Form
    {
        private string memberNumber = "";
        public int currentMember;
        public int returnValue = 2;

        //Just a variable to check if to show the form or not
        public int showClose = 0;
        private int ugh;

        public EditMember()
        {
            memberNumber = Microsoft.VisualBasic.Interaction.InputBox("Enter the Membership Number", "Edit Record", "");
            InitializeComponent();
            //Load the data into the DataGridView
            SQLiteConnection connection = new SQLiteConnection("Data Source = FBLADatabase.sqlite;Version=3;");
            connection.Open();
            
            if (memberNumber == "")
            {
                MessageBox.Show("You did not enter a Member ID");
                return;
            }

            showClose = 1;

            CommonFunctions.checkIfRowExists(memberNumber.ToString());
            if (returnValue == 0)
            {
                MessageBox.Show("There is not a record with the ID you entered");
                this.Close();
            }
            int.TryParse(memberNumber, out currentMember);
            SQLiteCommand command = new SQLiteCommand($"Select * from Members where MemberNumber={memberNumber}", connection);
            using (SQLiteDataReader read = command.ExecuteReader())
            {
                while (read.Read())
                {
                    textBox1.Text = read.GetValue(0).ToString();
                    textBox2.Text = read.GetValue(1).ToString();
                    textBox3.Text = read.GetValue(2).ToString();
                    textBox4.Text = read.GetValue(3).ToString();
                    textBox5.Text = read.GetValue(4).ToString();
                    textBox6.Text = read.GetValue(5).ToString();
                    textBox7.Text = read.GetValue(6).ToString();
                    textBox8.Text = read.GetValue(7).ToString();
                    textBox9.Text = read.GetValue(8).ToString();
                }
            }
            if (memberNumber == null)
            {
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Change
            SQLiteConnection connection = new SQLiteConnection("Data Source = FBLADatabase.sqlite;Version=3;");
            SQLiteCommand command = null;
            connection.Open();
            if (textBox9.Text == currentMember.ToString())
            {
                ugh = 1;
                goto label123r;
            }
            int check = CommonFunctions.checkIfRowExists(textBox9.Text);
            if ((check == 1) || (textBox9.Text == currentMember.ToString()))
            {
                MessageBox.Show("There is already a member with the same ID! Do things right");
            }
            else {
                ugh = 1;
            }

            label123r:
            if (ugh == 1)
            {
                command = new SQLiteCommand($"Update Members set FirstName='{textBox1.Text}', LastName='{textBox2.Text}', School='{textBox3.Text}', State='{textBox4.Text}', Email='{textBox5.Text}', YearJoined='{textBox6.Text}', Active='{textBox7.Text}', AmountOwed='{textBox8.Text}', MemberNumber='{textBox9.Text}' where MemberNumber={memberNumber}", connection);
            }
            if (command != null)
            {
                command.ExecuteNonQuery();
                connection.Close();
                this.Close();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
