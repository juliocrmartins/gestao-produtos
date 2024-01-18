using GestaoProdutos.Dominio.Modelos.Entidades;

namespace GestaoProdutos.Dominio.Interfaces
{
    public interface IFornecedorRepositorio
    {
        Fornecedor ObterPorId(int id);
    }
}
