using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SistemaAutentica.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAutentica.Models
{
    public class TotalDAL
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SistemaAutentica;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        //public string ConnectionString { get => connectionString; set => connectionString = value; }

        //update employee
        public void UpdateTotalNotaFisca(Total total)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_InsertTotalNotaFiscal", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@totalNotaFiscal", total.TotalNotaFiscal);
                

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }

        // get produto by id
        public Total GetTotalNotaFiscal()
        {
            Total total = new Total();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_GetTotalNotaFiscal", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
             
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {

                    total.TotalNotaFiscal = float.Parse((dr["TotalNotaFiscal"].ToString()));
                   

                }
                con.Close();
            }
            return total;
        }
    }

   
}
