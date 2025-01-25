using FluentAssertions;
using PokerEstimate.Models;

namespace estimativa_tests;

public class EstimativaUnitTests
{
    static List<Usuario> listaPontosDiferentes;
    static List<Usuario> listaMaisVotadoUnico;
    static List<Usuario> listaMaisVotadoRepetido;

    [SetUp]
    public void Setup()
    {
        //lista com pontos diferentes
        listaPontosDiferentes = new List<Usuario>
        { 
            new Usuario { Nome = "Usuário 0", Ponto = "0.5", TipoUsuario = TipoUsuario.DEV },
            new Usuario { Nome = "Usuário 1", Ponto = "1", TipoUsuario = TipoUsuario.DEV },
            new Usuario { Nome = "Usuário 2", Ponto = "2", TipoUsuario = TipoUsuario.DEV },

            new Usuario { Nome = "Usuário 3", Ponto = "3", TipoUsuario = TipoUsuario.QA },
            new Usuario { Nome = "Usuário 4", Ponto = "4", TipoUsuario = TipoUsuario.QA },
            new Usuario { Nome = "Usuário 5", Ponto = "5", TipoUsuario = TipoUsuario.QA },
        };

        // Lista com mais votado unico
        listaMaisVotadoUnico = new List<Usuario>
        {
            new Usuario { Nome = "Usuário 9", Ponto = "1", TipoUsuario = TipoUsuario.DEV },
            new Usuario { Nome = "Usuário 10", Ponto = "2", TipoUsuario = TipoUsuario.DEV },
            new Usuario { Nome = "Usuário 11", Ponto = "2", TipoUsuario = TipoUsuario.DEV },
            new Usuario { Nome = "Usuário 12", Ponto = "3", TipoUsuario = TipoUsuario.DEV },
            new Usuario { Nome = "Usuário 13", Ponto = "4", TipoUsuario = TipoUsuario.DEV },

            new Usuario { Nome = "Usuário 14", Ponto = "1", TipoUsuario = TipoUsuario.QA },
            new Usuario { Nome = "Usuário 15", Ponto = "1.5", TipoUsuario = TipoUsuario.QA },
            new Usuario { Nome = "Usuário 16", Ponto = "1.5", TipoUsuario = TipoUsuario.QA },
            new Usuario { Nome = "Usuário 17", Ponto = "2", TipoUsuario = TipoUsuario.QA },
            new Usuario { Nome = "Usuário 18", Ponto = "3", TipoUsuario = TipoUsuario.QA },
        };

        // Lista mais votado repetido
        listaMaisVotadoRepetido = new List<Usuario>
        {
            new Usuario { Nome = "Usuário 19", Ponto = "1", TipoUsuario = TipoUsuario.DEV },
            new Usuario { Nome = "Usuário 20", Ponto = "1", TipoUsuario = TipoUsuario.DEV },
            new Usuario { Nome = "Usuário 21", Ponto = "2", TipoUsuario = TipoUsuario.DEV },
            new Usuario { Nome = "Usuário 22", Ponto = "2", TipoUsuario = TipoUsuario.DEV },
            new Usuario { Nome = "Usuário 23", Ponto = "3", TipoUsuario = TipoUsuario.DEV },
            new Usuario { Nome = "Usuário 24", Ponto = "4", TipoUsuario = TipoUsuario.DEV },

            new Usuario { Nome = "Usuário 25", Ponto = "1", TipoUsuario = TipoUsuario.QA },
            new Usuario { Nome = "Usuário 26", Ponto = "2.5", TipoUsuario = TipoUsuario.QA },
            new Usuario { Nome = "Usuário 27", Ponto = "2.5", TipoUsuario = TipoUsuario.QA },
            new Usuario { Nome = "Usuário 28", Ponto = "3", TipoUsuario = TipoUsuario.QA },
            new Usuario { Nome = "Usuário 29", Ponto = "3", TipoUsuario = TipoUsuario.QA },
            new Usuario { Nome = "Usuário 30", Ponto = "4", TipoUsuario = TipoUsuario.QA },
        };
    }

    [Test]
    public void QuandoTodosOsVotos_ForemDiferentes_DeveRetornar_MenorPonto()
    {
        var estimativa = new Estimativa(listaPontosDiferentes);

        estimativa.ValorFinalDevs.Should().Be(0.5);

        estimativa.ValorFinalQas.Should().Be(3);
    }

    [Test]
    public void QuandoTotalMaisVotado_ForUnico_DeveRetornar_PontoMaisVotado()
    {
        var estimativa = new Estimativa(listaMaisVotadoUnico);

        estimativa.ValorFinalDevs.Should().Be(2);

        estimativa.ValorFinalQas.Should().Be(1.5);
    }

    [Test]
    public void QuandoTotalMaisVotado_NaoForUnico_DeveRetornar_OMenorPontoMaisVotado()
    {
        var estimativa = new Estimativa(listaMaisVotadoRepetido);

        estimativa.ValorFinalDevs.Should().Be(1);

        estimativa.ValorFinalQas.Should().Be(2.5);
    }
}
