using GestaoProdutos.Dominio.Modelos;
using GestaoProdutos.Dominio.Modelos.Entidades;

namespace GestaoProdutos.Dominio.Interfaces
{
    public interface IProdutoRepositorio
    {
        EntidadePaginada<Produto> ListarPorFiltro(ProdutoFiltro filtro);
        Produto ObterPorId(int id);
        int Inserir(Produto entidade);
        void Atualizar(Produto entidade);
    }
}
