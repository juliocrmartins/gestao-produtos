using GestaoProdutos.Dominio.Modelos.Enums;
using System;

namespace GestaoProdutos.Dominio.Modelos.DTO
{
    public class ProdutoDto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public EnumSituacao Situacao { get; set; }
        public DateTime DataFabricacao { get; set; }
        public DateTime DataValidade { get; set; }
        public int FornecedorId { get; set; }
        public FornecedorDto Fornecedor { get; set; }
    }
}
