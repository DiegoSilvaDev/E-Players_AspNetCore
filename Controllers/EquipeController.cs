using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Eplayers.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Eplayers.Controllers
{
    public class EquipeController : Controller
    {
        Equipe equipeModel = new Equipe();
        /// <summary>
        /// Aponta para a index da minha view
        /// </summary>
        /// <returns>A própria View</returns>
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
            // Upload da Imagem
            var file    = form.Files[0];

            // PastaA, PastaB, PastaC, Arquivo.pdf
            // PastaA/PastaB/Pastac/Arquivo.pdf
            var folder  = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Equipes");

            if(file != null)
            {
                if(!Directory.Exists(folder)){
                    Directory.CreateDirectory(folder);
                }

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/", folder, file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))  
                {  
                    file.CopyTo(stream);  
                }
                equipe.Imagem   = file.FileName;
            }
            else
            {
                equipe.Imagem   = "padrao.png";
            }
            // Fim - Upload da Imagem

            equipeModel.Create(equipe);

            return LocalRedirect("~/Equipe"); 
        }

        [Route("[controller]/{id}")]

        public IActionResult Excluir(int id){
            equipeModel.Delete(id);
            return LocalRedirect("~/Equipe");
        }

    }
}