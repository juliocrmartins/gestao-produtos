using System.Collections.Generic;

namespace GestaoProdutos.Dominio.Modelos.Entidades
{
    public class Fornecedor : EntidadeBase
    {
        public string Descricao { get; set; }
        public string CNPJ { get; set; }
        public ICollection<Produto> Produtos { get; }
    }
}
