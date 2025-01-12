namespace PokerEstimate.Models
{
    public class Usuario(string nome)
    {
        public string Nome { get; set; } = nome;
        public string? Ponto { get; set; }
    }
}
