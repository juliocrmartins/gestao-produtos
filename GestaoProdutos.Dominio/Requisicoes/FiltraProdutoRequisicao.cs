using GestaoProdutos.Dominio.Modelos.Enums;

namespace GestaoProdutos.Dominio.Requisicoes
{
    public class FiltraProdutoRequisicao
    {
        public string TextoBusca { get; set; }
        public int FornecedorId { get; set; }
        public EnumSituacao Situacao { get; set; }
    }
}
