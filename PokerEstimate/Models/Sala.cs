namespace PokerEstimate.Models
{
    public class Sala(string criador)
    {
        public Sala() : this(string.Empty) { }
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Criador { get; set; } = criador;
        public Estimativa? Estimativa { get; set; }
        public List<Usuario> Usuarios { get; set; } = [];
        public bool ExibirResultados { get; set; }

        public void CalcularEstimativa()
        {
            Estimativa = new Estimativa(Usuarios);

            ExibirResultados = true;
        }

        public Usuario ObterUsuario(string nome)
        {
            return Usuarios.FirstOrDefault(u => u.Nome == nome)!;
        }

        public bool AdicionarUsuario(Usuario usuario)
        {
            if (!Usuarios.Contains(usuario))
            {
                Usuarios.Add(usuario);

                return true;
            }

            return false;
        }

        public void DeletarVotos()
        {
            Usuarios.ForEach(u => u.Ponto = null);

            Estimativa = null;

            ExibirResultados = false;
        }
    }
}
