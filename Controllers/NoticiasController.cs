using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Eplayers.Models;
using Microsoft.AspNetCore.Http;

namespace Eplayers.Controllers
{
    public class NoticiasController : Controller
    {
        Noticias noticiasModel = new Noticias();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            ViewBag.Noticias = noticiasModel.ReadAll();
            return View();
        }

        /// <summary>
        /// Cadastra o Formulário
        /// </summary>
        /// <param name="form">Formulário</param>
        /// <returns>Retorna o Formulário na index de Equipe</returns>
        public IActionResult Cadastrar(IFormCollection form){
            
            Noticias noticia    = new Noticias();
            noticia.IdNoticia   = Int32.Parse(form["IdNoticia"]); 
            noticia.Texto       = form["Texto"];
            noticia.Titulo      = form["Titulo"];
            noticia.Imagem      = form["Imagem"];

            noticiasModel.Create(noticia);

            ViewBag.Noticias = noticiasModel.ReadAll();
            return LocalRedirect("~/Noticias"); 
        }
    }
}