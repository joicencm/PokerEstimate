using PokerEstimate.Models;

namespace PokerEstimate.Controllers
{
    public class SalaService
    {

        private readonly List<Sala> _salas = [];

        public void SalvarSala(Sala sala)
        {
            _salas.Add(sala);
        }

        public Sala ObterSala(string idSala)
        {
            return _salas.FirstOrDefault(s => s.Id == idSala)!;
        }

        public List<Sala> ObterSalas() => _salas;
    }

}

