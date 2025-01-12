using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace PokerEstimate.Models
{
    public class SalaHub : Hub
    {
        // Dicionário de salas, onde cada sala contém uma lista de usuários
        private static readonly ConcurrentDictionary<string, List<Usuario>> _salas = new();

        public async Task EntrarSala(Usuario usuario, string idSala)
        {
            if (usuario == null || string.IsNullOrWhiteSpace(idSala))
                return;

            // Adiciona a sala se não existir
            _salas.AddOrUpdate(idSala,
                [usuario],
                (key, usuarios) =>
                {
                    // Adiciona o usuário à sala, se ainda não estiver nela
                    var usuarioExistente = usuarios.FirstOrDefault(u => u.Nome == usuario.Nome);
                    if (usuarioExistente == null)
                    {
                        usuarios.Add(usuario);
                    }

                    Clients.Group(idSala).SendAsync("UsuarioEntrou", usuario);

                    return usuarios;
                });

            // Adiciona a conexão ao grupo SignalR
            await Groups.AddToGroupAsync(Context.ConnectionId, idSala);
        }

        public async Task AtualizarVoto(string nomeUsuario, string? ponto, string idSala)
        {
            if (string.IsNullOrWhiteSpace(nomeUsuario) || string.IsNullOrWhiteSpace(idSala))
                return;

            // Verifica se a sala existe
            if (_salas.TryGetValue(idSala, out var usuarios))
            {
                // Encontra o usuário e atualiza os pontos
                var usuario = usuarios.FirstOrDefault(u => u.Nome == nomeUsuario);
                if (usuario != null)
                {
                    usuario.Ponto = ponto;

                    // Envia a atualização para todos os usuários da sala
                    await Clients.Group(idSala).SendAsync("AtualizarVoto", usuario);
                }
            }
        }

        public async Task ExibirResultados(string idSala)
        {
            if (string.IsNullOrWhiteSpace(idSala))
                return;

            // Verifica se a sala existe
            if (_salas.TryGetValue(idSala, out var usuarios))
            {
                // Envia os resultados para todos os usuários da sala
                await Clients.Group(idSala).SendAsync("ExibirResultados", usuarios);
            }
        }

        public async Task LimparVotos(string idSala)
        {
            if (string.IsNullOrWhiteSpace(idSala))
                return;

            // Verifica se a sala existe
            if (_salas.TryGetValue(idSala, out var usuarios))
            {
                usuarios.ForEach(usuario => usuario.Ponto = null);

                // Envia os resultados para todos os usuários da sala
                await Clients.Group(idSala).SendAsync("LimparVotos", usuarios);
            }
        }
    }
}