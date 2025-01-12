using Microsoft.AspNetCore.Mvc;
using PokerEstimate.Models;
using System.Collections.Generic;
using System.Linq;

namespace PokerEstimate.Controllers
{
    public class HomeController : Controller
    {
        private static readonly List<Sala> salas = [];

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CriarSala(string nomeCriador)
        {
            if (!string.IsNullOrEmpty(nomeCriador))
            {
                var sala = new Sala(nomeCriador);
                sala.Usuarios.Add(new Usuario(nomeCriador));
                salas.Add(sala);

                return RedirectToAction("Painel", new { id = sala.Id, nome = nomeCriador });
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult EntrarSala(string id, string nome)
        {
            var sala = salas.FirstOrDefault(s => s.Id == id);
            if (sala != null && !string.IsNullOrEmpty(nome))
            {
                if (!sala.Usuarios.Any(u => u.Nome == nome))
                {
                    sala.Usuarios.Add(new Usuario(nome));
                }
                return RedirectToAction("Painel", new { id = sala.Id, nome });
            }
            return RedirectToAction("Index");
        }

        public IActionResult Painel(string id, string nome)
        {
            var sala = salas.FirstOrDefault(s => s.Id == id);
            if (sala == null)
            {
                return NotFound("Sala n�o encontrada.");
            }

            var usuario = sala.Usuarios.FirstOrDefault(x => x.Nome == nome);
            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            ViewBag.Sala = sala;
            ViewBag.NomeUsuario = nome;
            ViewBag.Usuario = usuario;
            ViewBag.EhCriador = sala.Criador == nome;
            ViewBag.ExibirResultados = sala.ExibirResultados;
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarEstimativa(string id, string nome, string ponto)
        {
            var sala = salas.FirstOrDefault(s => s.Id == id);
            var usuario = sala?.Usuarios.FirstOrDefault(u => u.Nome == nome);
            if (usuario != null)
            {
                usuario.Ponto = ponto;
            }
            return RedirectToAction("Painel", new { id, nome });
        }

        [HttpPost]
        public IActionResult DeletarVotos(string id, string nomeCriador)
        {
            var sala = salas.FirstOrDefault(s => s.Id == id);
            if (sala != null && sala.Criador == nomeCriador)
            {
                foreach (var usuario in sala.Usuarios)
                {
                    usuario.Ponto = null;
                }
                sala.ExibirResultados = false;  // Oculta os resultados ap�s a limpeza
            }
            return RedirectToAction("Painel", new { id, nome = nomeCriador });
        }

        [HttpPost]
        public IActionResult ExibirResultados(string id, string nomeCriador)
        {
            var sala = salas.FirstOrDefault(s => s.Id == id);
            if (sala != null && sala.Criador == nomeCriador)
            {
                sala.ExibirResultados = true;
            }
            return RedirectToAction("Painel", new { id, nome = nomeCriador });
        }
    }
}
