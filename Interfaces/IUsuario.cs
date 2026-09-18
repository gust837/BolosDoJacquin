using BolosDoJacquin.Models;

namespace BolosDoJacquin.Interfaces
{
    public interface IUsuario
    {
        Task Cadastrar(Usuario usuario);

        Task<List<Usuario>> Listar();

        Task Atualizar(Guid id, Usuario usuario);

        Task<Usuario?> BuscarPorId(Guid id);

        Task<Usuario?> BuscarPorEmailESenha(string email, string senha);

        Task AtualizarSituacao(Guid id, string situacao);
    }
}
