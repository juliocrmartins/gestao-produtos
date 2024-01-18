using GestaoProdutos.Dominio.Modelos.Enums;
using System;

namespace GestaoProdutos.Dominio.Requisicoes
{
    public class InsereProdutoRequisicao
    {
        public string Descricao { get; set; }
        public EnumSituacao Situacao { get; set; }
        public DateTime DataFabricacao { get; set; }
        public DateTime DataValidade { get; set; }
        public int FornecedorId { get; set; }
    }
}
