using System;
using System.Collections.Generic;

namespace PokerEstimate.Models
{
    public class Sala(string criador)
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Criador { get; set; } = criador;
        public List<Usuario> Usuarios { get; set; } = [];
        public bool ExibirResultados { get; set; }
    }
}
