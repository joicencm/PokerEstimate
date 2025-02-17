using Microsoft.AspNetCore.Mvc;
using PokerEstimate.Models;

namespace PokerEstimate.Controllers
{
    public class HomeController : Controller
    {
        private readonly SalaService _salaService;

        // Construtor
        public HomeController(SalaService salaService)
        {
            _salaService = salaService;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Método que irá renderizar a página SobreNddEstima
        public IActionResult SobreNddEstima()
        {
            return View();  // Renderiza a view "SobreNddEstima.cshtml"
        }

        [HttpPost]
        public IActionResult CriarSala(string nomeCriador)
        {
            if (!string.IsNullOrEmpty(nomeCriador))
            {
                var sala = new Sala(nomeCriador);
                var usuario = new Usuario(nomeCriador, TipoUsuario.CRIADOR);

                sala.AdicionarUsuario(usuario);
                _salaService.SalvarSala(sala);

                return RedirectToAction("Painel", new { id = sala.Id, nome = usuario.Nome });
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult EntrarSala(string id, string nome, string tipoUsuario)
        {
            if (!int.TryParse(tipoUsuario, out var tipo))
            {
                return BadRequest("Tipo de usuário inválido");
            }

            if (!Enum.IsDefined(typeof(TipoUsuario), tipo))
            {
                return BadRequest("Tipo de usuário inválido");
            }

            var sala = _salaService.ObterSala(id.Trim());

            if (sala != null && !string.IsNullOrEmpty(nome))
            {
                var usuario = new Usuario(nome, (TipoUsuario)tipo);
                var foiIncluido = sala.AdicionarUsuario(usuario);

                if (!foiIncluido)
                {
                    return RedirectToAction("ErrorPage", new { mensagem = $"A sala já possui um membro com o nome {nome}" });
                }

                return RedirectToAction("Painel", new { id = sala.Id, nome = usuario.Nome });
            }

            return RedirectToAction("ErrorPage", new { mensagem = $"Sala não encontrada" });
        }

        [HttpPost]
        public IActionResult RegistrarEstimativa(string id, string nome, string ponto)
        {
            var sala = _salaService.ObterSala(id);
            var usuario = sala.ObterUsuario(nome);
            usuario?.AdicionarPonto(ponto);

            return RedirectToAction("Painel", new { id, nome });
        }

        [HttpPost]
        public IActionResult DeletarVotos(string id, string nomeCriador)
        {
            var sala = _salaService.ObterSala(id);

            if (sala != null && sala.Criador == nomeCriador)
            {
                sala.DeletarVotos();
            }
            return RedirectToAction("Painel", new { id, nome = nomeCriador });
        }

        [HttpPost]
        public IActionResult ExibirResultados(string id, string nomeCriador)
        {
            var sala = _salaService.ObterSala(id);

            if (sala != null && sala.Criador == nomeCriador)
            {
                sala.CalcularEstimativa();
            }
            return RedirectToAction("Painel", new { id, nome = nomeCriador });
        }

        public IActionResult ErrorPage(string mensagem)
        {
            ViewBag.MensagemErro = mensagem;
            return View();
        }

        public IActionResult Painel(string id, string nome)
        {
            var sala = _salaService.ObterSala(id);

            if (sala == null)
            {
                return NotFound("Sala não encontrada.");
            }

            var usuario = sala.ObterUsuario(nome);

            if (usuario == null)
            {
                return NotFound("Usuário não encontrado na sala.");
            }

            ViewBag.Sala = sala;
            ViewBag.NomeUsuario = usuario.Nome;
            ViewBag.Usuario = usuario;
            ViewBag.EhCriador = sala.Criador == usuario.Nome;
            ViewBag.ExibirResultados = sala.ExibirResultados;

            return View();
        }
    }
}
