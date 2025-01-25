using System.Globalization;

namespace PokerEstimate.Models
{
    public class Estimativa
    {
        public double ValorFinal { get; private set; }
        public double ValorFinalDevs { get; private set; }
        public double ValorFinalQas { get; private set; }

        public Estimativa(List<Usuario> usuarios)
        {
            var usuariosValidos = ValidarUsuarios(usuarios);

            var (devs, qas) = SepararUsuariosPorTipo(usuariosValidos);

            ValorFinalDevs = CalcularValorFinal(ObterPontos(devs));
            ValorFinalQas = CalcularValorFinal(ObterPontos(qas));

            ValorFinal = ValorFinalDevs + ValorFinalQas;
        }

        //valida se a lista de usuários é nula ou vazia
        //retorna uma nova lista removendo os usuarios com voto "coffee" e "?"
        private static List<Usuario> ValidarUsuarios(List<Usuario> usuarios)
        {
            if (usuarios == null || usuarios.Count == 0)
            {
                throw new ArgumentException("A lista de usuários não pode ser nula ou vazia.");
            }

            return [.. usuarios.Where(u => double.TryParse(u.Ponto, out _))];
        }

        private static (List<Usuario> devs, List<Usuario> qas) SepararUsuariosPorTipo(List<Usuario> usuarios)
        {
            var devs = usuarios.Where(u => u.TipoUsuario == TipoUsuario.DEV).ToList();
            var qas = usuarios.Where(u => u.TipoUsuario == TipoUsuario.QA).ToList();

            return (devs, qas);
        }

        private static List<double> ObterPontos(List<Usuario> usuarios)
        {
            // Como o 'voto do usuario' está em string, é necessário tratar a cultura e substituir a vírgula como separador decimal
            return [.. usuarios.Select(u => double.Parse(u.Ponto!.Replace(",", "."), CultureInfo.InvariantCulture))];
        }


        //Ordena os votos por valor(double "key") e frequência(int "value")
        private static List<KeyValuePair<double, int>> OrdenarVotos(List<double> votos)
        {
            return [.. votos
                .GroupBy(v => v)
                .Select(g => new KeyValuePair<double, int>(g.Key, g.Count()))
                .OrderByDescending(x => x.Value)];
        }

        private static double CalcularValorFinal(List<double> pontos)
        {
            if (pontos.Count == 0)
            {
                return 0;
            }

            var valoresOrdenados = OrdenarVotos(pontos);
            var maiorFrequencia = valoresOrdenados.First().Value;

            if (maiorFrequencia == 1) // Todos os votos são diferentes
            {
                return pontos.Min();
            }

            var maisVotados = valoresOrdenados.Where(x => x.Value == maiorFrequencia);
            return maisVotados.Count() > 1
                ? maisVotados.Min(x => x.Key) // Menor valor mais votado
                : valoresOrdenados.First().Key; // Valor mais votado
        }
    }
}
