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
    public partial class Message : Form
    {
        public Message()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            sendMsg();//calls function when send button is clicked. 
        }

        private void sendMsg()
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>();// Create new sql parameters for sending message to DB
            sqlParams.Add(new SqlParameter("msg", txtSend.Text)); // Message content sent to sql parameter
            sqlParams.Add(new SqlParameter("user", 1)); //Unable To complete
            sqlParams.Add(new SqlParameter("mac", "123123123"));//Unable To complete
            DAL.ExecSP("SendMsg", sqlParams);
            txtSend.Clear();//Clear texbox input
            txtSend.Focus();// set curser to input
        }

        private void loadMsg()

        {

            List<SqlParameter> sqlParams = new List<SqlParameter>();
            DataTable dtLoginResults = DAL.ExecSP("getMsg", sqlParams);

            //Pulls all current messages from database.
            for (int i = dtLoginResults.Rows.Count - 1; i > 0; i--)
            {
                lstChat.Items.Add(dtLoginResults.Rows[i][2].ToString() + "         "+ dtLoginResults.Rows[i][1].ToString());
                
                lstChat.SetSelected(lstChat.Items.Count - 1, true);
            }//loop end, loop will run for however many rows are in the messages table.

        }
        

        
        //function gets new messages from DB , Function will detect if there are new messages 
        //by comparing the line amount in the list box to the row amont in the DB
        //If the row amount in the DB exceeds the Line amount in the list box,
        //Store procuder will be called to update data.
       private void   getMsg()
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            DataTable dtLoginResults = DAL.ExecSP("getMsg", sqlParams);// StoreProdcuder exucted to pul messages from DB. 
            int listCount = lstChat.Items.Count;// Varible declared for amount of entried in listbox. 

            if (dtLoginResults.Rows.Count > listCount)// checking if changes have been made since last update

            {
                dtLoginResults = DAL.ExecSP("getMsg", sqlParams);// Stored procuder pulls new messages from DB.
                lstChat.Items.Add(dtLoginResults.Rows[0][2].ToString() + "         " + dtLoginResults.Rows[0][1].ToString());//writes to list box; formated.
                lstChat.SetSelected(lstChat.Items.Count - 1, true);//highlights last entry, scrolls listbox to end of window/entries.

    }



}

        private void timer1_Tick(object sender, EventArgs e)
        {
            getMsg(); // Retrieves messages from the DB every 1 second, with timer. 
        }

        private void Message_Load(object sender, EventArgs e)
        {
            loadMsg();// Loads exsisting messages from the database upun form load.
        }
    }
}
