using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp
{
   public static class DAL
    {

        public static DataTable ExecSP(string spName, List<SqlParameter> sqlParams = null)

        {

            string strConnect = "Server=DESKTOP-3MG5MG0\\SQLEXPRESS;Database=DBProgramming2018;Trusted_Connection=True;";

            SqlConnection conn = new SqlConnection();

            DataTable dt = new DataTable();

            try
            {
                //Connect to DB
                conn = new SqlConnection(strConnect);
                conn.Open();

                //Building the query to send to Database
                SqlCommand cmd = new SqlCommand(spName, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(sqlParams.ToArray());

                //Execute Command
                SqlCommand command = conn.CreateCommand();
                SqlDataReader dr = cmd.ExecuteReader();


                //Load Data table with results
                dt.Load(dr);

            }
            catch (Exception ex)
            {

                throw ex;
            }

            finally
            {
                conn.Close();
            }

            return dt;

        }

    }
}
