using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProdutos.Dominio.Interfaces
{
    public interface IRepositorio<TEntidade>
    {
        IEnumerable<TEntidade> ListarTodos();
        int Inserir(TEntidade entidade);
        void Atualizar(TEntidade entidade);
        TEntidade ObterPorId(int id);
    }
}
