namespace PokerEstimate.Models
{
    public class Usuario(string nome, TipoUsuario tipoUsuario)
    {
        public string Nome { get; set; } = nome;
        public string? Ponto { get; set; }
        public TipoUsuario TipoUsuario { get; set; } = tipoUsuario;

        public Usuario() : this(string.Empty, default) { }

        public void AdicionarPonto(string ponto)
        {
            Ponto = ponto;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Usuario usuario)
            {
                return Nome == usuario.Nome;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Nome.GetHashCode();
        }
    }


    public enum TipoUsuario
    {
        CRIADOR,
        DEV,
        QA
    }
}
