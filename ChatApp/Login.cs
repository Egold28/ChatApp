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

namespace ChatApp
{
    public partial class frmChat : Form
    {
        public frmChat()
        {
            InitializeComponent();
        }

      

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //Stores user inputed email and password into Sql Parameters
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter("Email", txtEmail.Text));
            sqlParams.Add(new SqlParameter("Password", txtPass.Text));
            
            //SP checks to see is email mathes but password doesnt
            DataTable dtInvalidPassword = DAL.ExecSP("InvalidPassword", sqlParams);

            //Stores user inputed email and password into Sql Parameters
            List<SqlParameter> sqlParams2 = new List<SqlParameter>();
            sqlParams2.Add(new SqlParameter("Email", txtEmail.Text));
            sqlParams2.Add(new SqlParameter("Password", txtPass.Text));
            //SP checks is both email and password match
            DataTable dtLoginResults = DAL.ExecSP("ValidateLogin", sqlParams2);

            // INVALID PASSWORD
            // If email matches but password does not, a value of 1 is returned from DT


            if (txtEmail.Text == "" || txtPass.Text == "")// if email or password are left black, alert user
            {
                MessageBox.Show("Fields Can not be left Blank", "Warning!");//message to user.
                
            }

            else 
                
                if (dtInvalidPassword.Rows[0][0].ToString() == "1")
            {
                MessageBox.Show("Invalid Password"); //invalid password message
                txtPass.Clear();//clear field password feild for re-entry
                txtPass.Focus();// set cursor on password field input.
            }


            //VALID LOGIN
            else
                 // Detecting valid email, Valid password
                 if (dtLoginResults.Rows.Count == 1)
            {
                Message frmMessage = new Message(); 
                frmMessage.Show(); // opens chat form

                this.Hide();//hides ogin window
            }

            //REGISTRATION REQUIRED
            else
            MessageBox.Show("Email Not Registered", "Not Found"); // Registration required Message Alert
            txtEmail.Clear();//clear email text input
            txtPass.Clear();//set cursor on email text field.
            

        }

        private void lblRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Closes login form, opens registrations form  if register link is clicked 
            Register frmRegister = new Register();
            frmRegister.Show();//show registation form 
            this.Hide();//hide login form 
        }

        private void frmChat_FormClosing(object sender, FormClosingEventArgs e)
        {
            //ensure application closes, rather then hides
            Application.Exit();
        }
    }
}
