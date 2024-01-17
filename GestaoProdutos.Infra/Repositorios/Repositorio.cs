using GestaoProdutos.Dominio.Interfaces;
using GestaoProdutos.Dominio.Modelos.Entidades;
using GestaoProdutos.Infra.Contexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProdutos.Infra.Repositorios
{
    public class Repositorio<TEntidade> : IRepositorio<TEntidade> where TEntidade : EntidadeBase
    {
        protected readonly ContextoBD _contexto;

        public Repositorio(ContextoBD contexto)
        {
            _contexto = contexto;
        }

        public virtual int Inserir(TEntidade entidade)
        {
            _contexto.Set<TEntidade>().Add(entidade);
            _contexto.SaveChanges();
            return entidade.Id;
        }

        public virtual void Atualizar(TEntidade entidade)
        {
            _contexto.Set<TEntidade>().Update(entidade);
            _contexto.SaveChanges();
        }

        public virtual IEnumerable<TEntidade> ListarTodos()
        {
            var query = _contexto.Set<TEntidade>();

            if (query.Any())
                return query.ToList();

            return new List<TEntidade>();
        }

        public virtual TEntidade ObterPorId(int id)
            => _contexto.Set<TEntidade>()
                .FirstOrDefault(e => e.Id == id);
    }
}
