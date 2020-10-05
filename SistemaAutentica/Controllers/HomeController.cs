using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SistemaAutentica.Areas.Identity.Data;
using SistemaAutentica.Models;

namespace SistemaAutentica.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<SistemaAutenticaUser> userManager;
        public HomeController(UserManager<SistemaAutenticaUser> userManager, ILogger<HomeController> logger)
        {
            this.userManager = userManager;
            _logger = logger;
        }

        UserDAL userDAL = new UserDAL();
        NotaFiscalDAL notaFiscalDAL = new NotaFiscalDAL();
        ProdutoDAL produtoDAL = new ProdutoDAL();
        TotalDAL totalDAL = new TotalDAL();

        List<SistemaAutenticaUser> listaUser = new List<SistemaAutenticaUser>();
        List<NotaFiscal> listaNota = new List<NotaFiscal>();
        List<Produto> listaProduto = new List<Produto>();

        

        private readonly ILogger<HomeController> _logger;
        /*
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var result = await userManager.DeleteAsync(user);
            return RedirectToAction("Index");
        } */

        public IActionResult DeleteUser(string id)
        {
            ViewBag.id = id;
            userDAL.DeleteUserById(id);
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            return View();
           
        }

        [HttpGet]
        public IActionResult ListaUsuarios()
        {
            listaUser = userDAL.GetAllUsers().ToList();
            return View(listaUser);
        }

        [HttpGet]
        public IActionResult ListaNotaFiscal()
        {

            listaNota = notaFiscalDAL.GetAllNotasFiscais().ToList();
            
           
            return View(listaNota);
        }

        [HttpGet]
        public IActionResult CadastraNotaFiscal()
        {
            return View();
        }

        [HttpGet]
        public IActionResult EditaProduto(int? id)
        {
            if (id == null)
            {
                return NotFound();

            }
            Produto produto = produtoDAL.GetProdutoById(id);
            
            if (produto == null)
            {
                return NotFound();
            }
            return View(produto);
        }

        [HttpGet]
        public IActionResult EditaNotaFiscal(int? id)
        {
            if (id == null)
            {
                return NotFound();

            }
            NotaFiscal notaFiscal = notaFiscalDAL.GetNotaFiscalById(id);

            if (notaFiscal == null)
            {
                return NotFound();
            }
            return View(notaFiscal);
        }

        [HttpGet]
        public IActionResult Totais()
        {
            Total total = totalDAL.GetTotalNotaFiscal();
            return View(total);
        }

        [HttpGet]
        public IActionResult DeleteProduto(int? id)
        {
            Produto produto = produtoDAL.GetProdutoById(id);
            NotaFiscal notafiscal = notaFiscalDAL.GetNotaFiscalById(produto.NotaID);
            notafiscal.Valor -= (produto.Custo * produto.Quantidade);
            notaFiscalDAL.UpdateNotaFiscal(notafiscal);
            produtoDAL.DeleteProduto(id);
            
            return RedirectToAction("Index");
        }


       [HttpGet]
        public IActionResult DeleteNotaFiscal(int? id)
        {
            notaFiscalDAL.DeleteNotaFiscal(id);
            return RedirectToAction("Index");
        }

       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditaNotaFiscal(int? id, [Bind] NotaFiscal nota)
        {
            if (id == null)
            {
                return NotFound();

            }
            if (ModelState.IsValid)
            {
                notaFiscalDAL.UpdateNotaFiscal(nota);
                return RedirectToAction("Index");
            }

            return View(notaFiscalDAL);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditaProduto(int? id, [Bind] Produto produto)
        {
            if (id == null)
            {
                return NotFound();

            }
            if (ModelState.IsValid)
            {
                produtoDAL.UpdateProduto(produto);
                var total = produto.Custo * produto.Quantidade;

                Produto produto1 = produtoDAL.GetProdutoById(id);

                produto1.Total = total;


                produtoDAL.UpdateProduto(produto1);


                NotaFiscal notaFiscal = notaFiscalDAL.GetNotaFiscalById(produto1.NotaID);


                notaFiscal.Valor = produto1.Total;
                //ViewBag.nota = notaFiscal.Valor;
                notaFiscalDAL.UpdateNotaFiscal(notaFiscal);
                
                //EditaNotaFiscal(notaFiscal.NotaID, notaFiscal);
                return RedirectToAction("Index");
            }

            return View(produtoDAL);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CadastraNotaFiscal([Bind] NotaFiscal notafiscal)
        {
            if (ModelState.IsValid)
            {
                notaFiscalDAL.AddNotaFiscal(notafiscal);
                return RedirectToAction("Index");
            }
            return View(notafiscal);
        }

        [HttpGet]
        public IActionResult ListaProduto()
        {

            listaProduto = produtoDAL.GetAllProdutos().ToList();

            return View(listaProduto);
        }

        [HttpGet]
        public IActionResult CadastraProduto()
        {
            listaNota = notaFiscalDAL.GetAllNotasFiscais().ToList();
            ViewBag.LISTA = listaNota;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CadastraProduto([Bind] Produto produto)
        {
            
            if (ModelState.IsValid)
            {

                produto.Total = produto.getTotal();
                ViewBag.total = produto.Total;

                
                Produto prodResult = produtoDAL.AddProduto(produto);
                ViewBag.prod = prodResult.Total;

               
                NotaFiscal nf = notaFiscalDAL.GetNotaFiscalById(prodResult.NotaID);
                ViewBag.res = prodResult.NotaID;
                ViewBag.nf = nf.Numero;

                NotaFiscal notaObj = notaFiscalDAL.GetNotaFiscalById(prodResult.NotaID);
                ViewBag.notaId = notaObj.NotaID;
                ViewBag.notaNumero = notaObj.Numero;
                notaObj.Valor += (prodResult.Custo * prodResult.Quantidade);
                notaFiscalDAL.UpdateNotaFiscal(notaObj);

                var total = totalDAL.GetTotalNotaFiscal();

                total.TotalNotaFiscal += prodResult.Total;

                totalDAL.UpdateTotalNotaFisca(total);

                return RedirectToAction("Index");


            }
            return View(produto);
        }

        public IActionResult SaidaProduto()
        {
            listaNota = notaFiscalDAL.GetAllNotasFiscais().ToList();
            

            return View(listaNota);
        }

        [HttpGet]
        public IActionResult ListaProdutosPorNotaId(int id)
        {

            listaProduto = produtoDAL.GetAllProdutosByNotaId(id).ToList();
            ViewBag.lista = listaProduto;
            ViewBag.id = id;
            return View(listaProduto);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
