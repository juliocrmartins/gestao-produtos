using GestaoProdutos.Dominio.Modelos.Entidades;
using GestaoProdutos.Infra.Contexto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GestaoProdutos.Infra.Repositorios
{
    public class ProdutoRepositorio : Repositorio<Produto>
    {
        public ProdutoRepositorio(ContextoBD context) : base(context)
        {
        }

        public override IEnumerable<Produto> ListarTodos()
        {
            var query = _contexto.Produtos
                .Include(n => n.Fornecedor);

            return query.Any() ? query.ToList() : new List<Produto>();
        }

        public override Produto ObterPorId(int id)
            => _contexto.Produtos
                    .Include(p => p.Fornecedor)
                    .FirstOrDefault(p => p.Id == id);

        public IEnumerable<Produto> ListarPorFiltro()
        {
            return ListarTodos();
        }

    }
}
