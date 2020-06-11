using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data.SQLite;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FBLA_Application
{
    public partial class AddMember : Form
    {
        private TextBox firstNameTextBox;
        private TextBox lastNameTextBox;
        private TextBox schoolTextBox;
        private TextBox stateTextBox;
        private TextBox emailTextBox;
        private TextBox yearJoinedTextBox;
        private RadioButton yesButton;
        private RadioButton noButton;
        private TextBox amountOwed;
        private TextBox memberNumber;

        //Extra variables
        private float i = 0;
        private int k = 0;
        private int l = 0;
        private bool amountResult;
        private bool memberResult;
        private string activeBool;
        private bool yearResult;

        public AddMember()
        {
            InitializeComponent();
            firstNameTextBox = textBox1;
            lastNameTextBox = textBox2;
            schoolTextBox = textBox3;
            stateTextBox = textBox4;
            emailTextBox = textBox5;
            yearJoinedTextBox = textBox6;
            yesButton = radioButton1;
            noButton = radioButton2;
            amountOwed = textBox8;
            memberNumber = textBox9;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Add clicked
            amountResult = float.TryParse(amountOwed.Text, out i);
            memberResult = int.TryParse(memberNumber.Text, out l);
            yearResult = int.TryParse(yearJoinedTextBox.Text, out k);
            if (amountResult == false)
            {
                MessageBox.Show("The amount owed value is not a number. Are you sure that you inserted a proper value?");
                return;
            }

            if (memberResult == false)
            {
                MessageBox.Show("The member number you entered is not an integer. Are you sure that you properly entered a value?");
            }

            if (yearResult == false)
            {
                MessageBox.Show("The value you entered for the year joined is not valid. Please re-enter a value");
                return;
            }

            if (radioButton1.Checked == true)
            {
                activeBool = "Yes";
            }else if (radioButton2.Checked == true)
            {
                activeBool = "No";
            }else
            {
                MessageBox.Show("Are you sure you checked a value for Active?");
            }

            SQLiteConnection connection = new SQLiteConnection("Data Source = FBLADatabase.sqlite;Version=3;");
            connection.Open();
            SQLiteCommand command = new SQLiteCommand();
            command.Connection = connection;
            command.CommandText = $"Select count(*) from Members where MemberNumber={memberNumber.Text}";
            int count = Convert.ToInt32(command.ExecuteScalar());
            if (count == 0)
            {
                command.CommandText = $"insert into Members values('{firstNameTextBox.Text}', '{lastNameTextBox.Text}', '{schoolTextBox.Text}', '{stateTextBox.Text}', '{emailTextBox.Text}', '{k}', '{activeBool}', '{i}', '{l}'); ";
                command.ExecuteNonQuery();
            }
            else
            {
                MessageBox.Show("A member with that Membership Number already exists");
                return;
            }
            connection.Close();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Cancel clicked
            this.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                radioButton2.Checked = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                radioButton1.Checked = false;
            }
        }
    }
}
