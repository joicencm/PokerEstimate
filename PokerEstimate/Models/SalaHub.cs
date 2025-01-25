using Microsoft.AspNetCore.SignalR;
using PokerEstimate.Controllers;

namespace PokerEstimate.Models
{
    public class SalaHub(SalaService salaService) : Hub
    {
        private readonly SalaService SalaService = salaService;


        public async Task EntrarSala(Usuario usuario, string idSala)
        {
            await Clients.Group(idSala).SendAsync("UsuarioEntrou", usuario);

            await Groups.AddToGroupAsync(Context.ConnectionId, idSala);
        }


        public async Task AtualizarVoto(string nomeUsuario, string idSala)
        {
            if (string.IsNullOrWhiteSpace(nomeUsuario) || string.IsNullOrWhiteSpace(idSala))
                return;

            var sala = SalaService.ObterSala(idSala);

            if (sala != null)
            {
                var usuario = sala.ObterUsuario(nomeUsuario);
                
                await Clients.Group(idSala).SendAsync("AtualizarVoto", usuario);
            }

        }

        public async Task ExibirResultados(string idSala)
        {
            if (string.IsNullOrWhiteSpace(idSala))
                return;

            var sala = SalaService.ObterSala(idSala);

            if (sala != null)
            {
                await Clients.Group(idSala).SendAsync("ExibirResultados", sala);
            }
        }

        public async Task LimparVotos(string idSala)
        {
            if (string.IsNullOrWhiteSpace(idSala))
                return;

            var sala = SalaService.ObterSala(idSala);

            if (sala != null)
            {
                await Clients.Group(idSala).SendAsync("LimparVotos", sala.Usuarios);
            }
        }
    }
}