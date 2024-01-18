using System.Collections.Generic;

namespace GestaoProdutos.Dominio.Modelos
{
    public class EntidadePaginada<TEntidade>
    {
        public int ItemsPorPagina { get; set; }
        public int Pagina { get; set; }
        public int TotalRegistros { get; set; }
        public IEnumerable<TEntidade> Registros { get; set; }
    }
}
