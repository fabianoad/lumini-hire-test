using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SistemaAutentica.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAutentica.Models
{
    public class ProdutoDAL
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SistemaAutentica;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        //public string ConnectionString { get => connectionString; set => connectionString = value; }

        public IEnumerable<Produto> GetAllProdutos()
        {
            List<Produto> produtoList = new List<Produto>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_GetAllProdutos", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while(dr.Read())
                {
                    Produto produto = new Produto();

                    produto.ProdutoID = Convert.ToInt32(dr["ProdutoID"].ToString());
                    produto.Nome = dr["Nome"].ToString();
                    produto.Quantidade = Convert.ToInt32(dr["Quantidade"].ToString());
                    produto.Custo = float.Parse((dr["Custo"].ToString()));
                    produto.Unidade = dr["Unidade"].ToString();
                    produto.NumeroNotaFiscal = dr["Numero"].ToString();
                    produto.Total = produto.Custo * produto.Quantidade;
                    produto.NotaID = Convert.ToInt32(dr["NotaID"].ToString());



                    produtoList.Add(produto);
                }
                con.Close();
            }
            return produtoList;
        }

        public IEnumerable<Produto> GetAllProdutosByNotaId(int? notaId)
        {
            List<Produto> produtoList = new List<Produto>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_GetAllProdutosByNotaId", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NotaId", notaId);
                con.Open();
                
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Produto produto = new Produto();

                    produto.ProdutoID = Convert.ToInt32(dr["ProdutoID"].ToString());
                    produto.Nome = dr["Nome"].ToString();
                    produto.Quantidade = Convert.ToInt32(dr["Quantidade"].ToString());
                    produto.Custo = float.Parse((dr["Custo"].ToString()));
                    produto.Unidade = dr["Unidade"].ToString();
                    produto.Total = produto.Custo * produto.Quantidade;
                    produto.NotaID = Convert.ToInt32(dr["NotaID"].ToString());



                    produtoList.Add(produto);
                }
                con.Close();
            }
            return produtoList;
        }





        //insert employee
        public Produto AddProduto(Produto produto)
        {
            produto.Total = produto.Custo * produto.Quantidade;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                

                SqlCommand cmd = new SqlCommand("SP_InsertProduto", con);
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.AddWithValue("@NotaID", produto.NotaID);
                cmd.Parameters.AddWithValue("@Nome", produto.Nome);
                cmd.Parameters.AddWithValue("@Quantidade", produto.Quantidade);
                cmd.Parameters.AddWithValue("@Custo", produto.Custo);
                cmd.Parameters.AddWithValue("@Unidade", produto.Unidade);
                cmd.Parameters.AddWithValue("@Total", produto.Total);


                try
                {
                    con.Open();
                    Int32 rowsAffected = cmd.ExecuteNonQuery();
                    Console.WriteLine("RowsAffected: {0}", rowsAffected);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                con.Close();
                return produto;
               
            }
        }

        //update employee
        public void UpdateProduto(Produto produto)
        {
            
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_UpdateProduto", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProdutoId", produto.ProdutoID);
                cmd.Parameters.AddWithValue("@Nome", produto.Nome);
                cmd.Parameters.AddWithValue("@Quantidade", produto.Quantidade);
                cmd.Parameters.AddWithValue("@Custo", produto.Custo);
                cmd.Parameters.AddWithValue("@Unidade", produto.Unidade);
                cmd.Parameters.AddWithValue("@Total", produto.Total);
                




                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                
            }

        }

        public void DeleteProduto(int? produtoId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_DeleteProduto", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProdutoId", produtoId);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

           

        }

        // get produto by id
        public Produto GetProdutoById(int? produtoId)
        {
            Produto produto = new Produto();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_GetProdutoById", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProdutoId", produtoId);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
               
                while (dr.Read())
                {
                    
                    produto.ProdutoID = Convert.ToInt32(dr["ProdutoID"].ToString());
                    produto.Nome = dr["Nome"].ToString();
                    produto.Quantidade = Convert.ToInt32(dr["Quantidade"].ToString());
                    produto.Custo = float.Parse((dr["Custo"].ToString()));
                    produto.Unidade = dr["Unidade"].ToString();
                    produto.Total = produto.Custo * produto.Quantidade;
                    produto.NotaID = Convert.ToInt32(dr["NotaID"].ToString());

                }
                con.Close();
            }
            return produto;
        }


        

    }

   
}
