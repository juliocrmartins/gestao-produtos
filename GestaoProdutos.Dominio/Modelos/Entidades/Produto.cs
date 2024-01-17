using GestaoProdutos.Dominio.Modelos.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProdutos.Dominio.Modelos.Entidades
{
    public class Produto : EntidadeBase
    {
        public string Descricao { get; set; }
        public EnumSituacao Situacao { get; set; }
        public DateTime DataFabricacao { get; set; }
        public DateTime DataValidade { get; set; }
        public int FornecedorId { get; set; }
        public Fornecedor Fornecedor { get; set; }
    }
}
