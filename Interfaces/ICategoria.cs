using BolosDoJacquin.Models;

namespace BolosDoJacquin.Interfaces
{
    public interface ICategoria
    {
        Task Cadastrar(Categoria categoria);

        Task<List<Categoria>> Listar();

        Task Atualizar(Guid id, Categoria categoria);

        Task Deletar(Guid id);

        Task<Categoria?> BuscarPorId(Guid id);
    }
}
