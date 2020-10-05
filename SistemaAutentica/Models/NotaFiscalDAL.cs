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
    public class NotaFiscalDAL
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SistemaAutentica;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        //public string ConnectionString { get => connectionString; set => connectionString = value; }

        public IEnumerable<NotaFiscal> GetAllNotasFiscais()
        {
            List<NotaFiscal> notasList = new List<NotaFiscal>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_GetAllNotasFiscais", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    NotaFiscal nf = new NotaFiscal();
                    nf.NotaID = Convert.ToInt32(dr["NotaID"].ToString());
                    nf.Numero = dr["Numero"].ToString();
                    nf.Valor = float.Parse(dr["Valor"].ToString());
                    nf.DataInsertSistema = Convert.ToDateTime(dr["DataInsertSistema"].ToString());
                    
                    notasList.Add(nf);
                }
                con.Close();
            }
            return notasList;
        }


        
        public void AddNotaFiscal(NotaFiscal notafiscal)
        {
          
            
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_InsertNotaFiscal", con);
                
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Numero", notafiscal.Numero);
                cmd.Parameters.AddWithValue("@Valor", notafiscal.Valor);
                cmd.Parameters.AddWithValue("@DataInsertSistema", DateTime.Now.ToString("MM/dd/yyyy h:mm tt"));
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public NotaFiscal GetNotaFiscalById(int? notaId)
        {
            NotaFiscal notaFiscal = new NotaFiscal();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_GetNotaFiscalById", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NotaId", notaId);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    notaFiscal.NotaID = Convert.ToInt32(dr["NotaID"].ToString());
                    notaFiscal.Numero = dr["Numero"].ToString();
                    notaFiscal.Valor = float.Parse(dr["Valor"].ToString());
                    

                }
                con.Close();
            }
            return notaFiscal;
        }



        //update employee
        public void UpdateNotaFiscal(NotaFiscal notaFiscal)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_UpdateNotaFiscal", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@NotaId", notaFiscal.NotaID);
                cmd.Parameters.AddWithValue("@Numero", notaFiscal.Numero);
                cmd.Parameters.AddWithValue("@Valor", notaFiscal.Valor);
                cmd.Parameters.AddWithValue("@DataInsertSistema", DateTime.Now.ToString("MM/dd/yyyy h:mm tt"));

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }

        public void DeleteNotaFiscal(int? notaId)
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_DeleteNotaFiscal", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NotaId", notaId);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }


    }

   
}
