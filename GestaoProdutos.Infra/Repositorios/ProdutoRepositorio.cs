using GestaoProdutos.Dominio.Interfaces;
using GestaoProdutos.Dominio.Modelos;
using GestaoProdutos.Dominio.Modelos.Entidades;
using GestaoProdutos.Infra.Contexto;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GestaoProdutos.Infra.Repositorios
{
    public class ProdutoRepositorio : IProdutoRepositorio
    {
        protected readonly ContextoBD _contexto;

        public ProdutoRepositorio(ContextoBD contexto)
        {
            _contexto = contexto;
        }

        public EntidadePaginada<Produto> ListarPorFiltro(ProdutoFiltro filtro)
        {
            var query = _contexto.Produtos.Include(p => p.Fornecedor).AsEnumerable();

            if (!string.IsNullOrEmpty(filtro.TextoBusca))
                query = query.Where(q => 
                       q.Descricao.Contains(filtro.TextoBusca) ||
                       q.Fornecedor.Descricao.Contains(filtro.TextoBusca))
                    .AsEnumerable();

            if (filtro.Situacao != null)
                query = query.Where(q => q.Situacao == filtro.Situacao);

            if (filtro.FornecedorId != null)
                query = query.Where(q => q.FornecedorId == filtro.FornecedorId);

            var registros = query.Count();

            var skip = filtro.ItemsPorPagina * (filtro.Pagina - 1);
            var take = filtro.ItemsPorPagina;

            return new EntidadePaginada<Produto>
            {
                Registros = query.Skip(skip).Take(take),
                ItemsPorPagina = filtro.ItemsPorPagina,
                Pagina = filtro.Pagina,
                TotalRegistros = registros
            };
        }

        public Produto ObterPorId(int id)
            => _contexto.Produtos
                    .Include(p => p.Fornecedor)
                    .FirstOrDefault(p => p.Id == id);

        public virtual int Inserir(Produto entidade)
        {
            _contexto.Set<Produto>().Add(entidade);
            _contexto.SaveChanges();
            return entidade.Id;
        }

        public virtual void Atualizar(Produto entidade)
        {
            _contexto.Set<Produto>().Update(entidade);
            _contexto.SaveChanges();
        }
    }
}
