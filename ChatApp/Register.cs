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
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void lblLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // if login link clicked, reg form wil close, open login for
            frmChat frmLogin = new frmChat();
            frmLogin.Show();
            this.Hide();
        }

        //ensures application is closed, rather then hidden. 
        private void Register_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit(); 
        }

        //Sends userinput to sql parameters then sends to database via stored procudure
        private void txtRegister_Click(object sender, EventArgs e)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter("Email", txtEmail.Text));//email
            sqlParams.Add(new SqlParameter("Password", txtPass.Text));//password
            sqlParams.Add(new SqlParameter("First", txtFirst.Text));//first Name
            sqlParams.Add(new SqlParameter("Last", txtLast.Text));// Last Name

            DAL.ExecSP("CreateUser", sqlParams);//Sends to user table
            MessageBox.Show("Registration Successful","Success!");//alerts user registration successfull 


            frmChat frmLogin = new frmChat();
            frmLogin.Show();// brings user back to login form automatically

            this.Hide();//closer reg form 
        }
    }
}
