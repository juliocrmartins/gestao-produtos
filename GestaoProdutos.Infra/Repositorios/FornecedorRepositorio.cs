using GestaoProdutos.Dominio.Interfaces;
using GestaoProdutos.Dominio.Modelos.Entidades;
using GestaoProdutos.Infra.Contexto;
using System.Linq;

namespace GestaoProdutos.Infra.Repositorios
{
    public class FornecedorRepositorio : IFornecedorRepositorio
    {
        protected readonly ContextoBD _contexto;

        public FornecedorRepositorio(ContextoBD contexto)
        {
            _contexto = contexto;
        }

        public Fornecedor ObterPorId(int id)
            => _contexto.Fornecedores
                    .FirstOrDefault(p => p.Id == id);
    }
}
