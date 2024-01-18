using System.Collections.Generic;

namespace GestaoProdutos.Dominio.Respostas
{
    public class ListaPaginadaResposta<TDto>
    {
        public int ItemsPorPagina { get; set; }
        public int Pagina { get; set; }
        public int TotalRegistros { get; set; }
        public IEnumerable<TDto> Registros { get; set; }
    }
}
