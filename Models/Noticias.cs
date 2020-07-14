using System;
using System.Collections.Generic;
using System.IO;
using Eplayers.Interfaces;

namespace Eplayers.Models
{
    public class Noticias : EplayersBase, INoticias
    {
        public int IdNoticia { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }
        public string Imagem { get; set; }
        private const string PATH = "Database/noticia.csv";  

        /// <summary>
        /// Cria o diretorio e o arquivo caso não existam
        /// </summary>
        public Noticias(){
            CreateFolderAndFile(PATH);
        }
        /// <summary>
        /// Cria/Cadastra a noticia
        /// </summary>
        /// <param name="n">Noticia</param>
        public void Create(Noticias n)
        {
            string[] linha = {PrepararLinha(n)};
            File.AppendAllLines(PATH, linha);  
        }
        /// <summary>
        /// Prepara a impressão da linha
        /// </summary>
        /// <param name="n">Noticia</param>
        /// <returns>A noticia separada</returns>
        private string PrepararLinha(Noticias n){
            return $"{n.IdNoticia};{n.Titulo};{n.Texto};{n.Imagem}  ";
        }
        /// <summary>
        /// Exclui a noticia
        /// </summary>
        /// <param name="IdNoticia">Noticia que será excluida</param>
        public void Delete(int IdNoticia)
        {
            List<string> linhas = ReadAllLinesCSV(PATH);
            linhas.RemoveAll(x => x.Split(";")[0] == IdNoticia.ToString());
            RewriteCSV(PATH, linhas);
        }
        /// <summary>
        /// Lê o CSV
        /// </summary>
        /// <returns>Retorna todas as linhas do CSV Separadas</returns>
        public List<Noticias> ReadAll()
        {
            List<Noticias> noticias = new List<Noticias>();
            string[] linhas = File.ReadAllLines(PATH);
            foreach (var item in linhas)
            {
                string[] linha = item.Split(";");
                Noticias noticia = new Noticias();
                noticia.IdNoticia = Int32.Parse(linha[0]);
                noticia.Titulo = linha[1];
                noticia.Texto = linha[2];
                noticia.Imagem = linha[3];

                noticias.Add(noticia);
            }
            return noticias;
        }
        /// <summary>
        /// Altera ou atualiza a noticia
        /// </summary>
        /// <param name="n">Noticia</param>
        public void Update(Noticias n)
        {
            List<string> linhas = ReadAllLinesCSV(PATH);
            linhas.RemoveAll( x => x.Split(";")[0] == n.IdNoticia.ToString());
            linhas.Add(PrepararLinha(n) );
            RewriteCSV(PATH, linhas);
        }
    }
}