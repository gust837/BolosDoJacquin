using BolosDoJacquin.Models;

namespace BolosDoJacquin.Interfaces
{
    public interface IProduto
    {
        Task Cadastrar(Produto produto);

        Task<List<Produto>> Listar();

        Task Deletar(Guid id);

        Task Atualizar(Guid id, Produto produto);

        Task<Produto?> BuscarPorId(Guid id);

        Task<List<Produto>> Filtrar(Guid? idCategoria, decimal? precoMin, decimal? precoMax, string termoBusca);

        Task AtualizarDisponibilidade(Guid id, bool disponibilidade);

        Task AtualizarSituacao(Guid id, string situacao);
    }
}
