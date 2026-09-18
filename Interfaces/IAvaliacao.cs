using BolosDoJacquin.Models;

namespace BolosDoJacquin.Interfaces
{
    public interface IAvaliacao
    {
        Task<Avaliacao?> BuscarPorUsuarioEProduto(Guid idUsuario, Guid idProduto);
        Task Cadastrar(Avaliacao avaliacao);

        Task Deletar(Guid id);

        Task Atualizar(Guid id, Avaliacao avaliacao);

        Task<List<Avaliacao>> ListarPorProduto(Guid idProduto);

        Task<List<Avaliacao>> ListarPorUsuario(Guid idUsuario);

        Task<List<Avaliacao>> ListarTodasAdmin();

        Task<Avaliacao?> BuscarPorId(Guid id);

        Task AlterarSituacao(Guid id, bool situacao, string motivoOcultacao);
    }
}

