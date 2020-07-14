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
    public class EquipeController : Controller
    {
        Equipe equipeModel = new Equipe();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            ViewBag.Equipes = equipeModel.ReadAll();
            return View();
        }
        /// <summary>
        /// Cadastra o Formulário
        /// </summary>
        /// <param name="form">Formulário</param>
        /// <returns>Retorna o Formulário na index de Equipe</returns>
        public IActionResult Cadastrar(IFormCollection form){
            
            Equipe equipe   = new Equipe();
            equipe.IdEquipe = Int32.Parse(form["IdEquipe"]); 
            equipe.Nome     = form["Nome"];
            equipe.Imagem   = form["Imagem"];

            equipeModel.Create(equipe);

            ViewBag.Equipes = equipeModel.ReadAll();
            return LocalRedirect("~/Equipe"); 
        }
    }
}