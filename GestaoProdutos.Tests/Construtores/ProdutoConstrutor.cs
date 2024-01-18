using GestaoProdutos.Dominio.Modelos.Entidades;
using GestaoProdutos.Dominio.Modelos.Enums;
using GestaoProdutos.Tests.Construtores;

namespace GestaoProdutos.Tests.Buiders
{
    public class ProdutoConstrutor : ConstrutorBase<Produto>
    {
        public override ProdutoConstrutor Padrao(int numeroItens = 1)
        {
            for (int i = 0; i < numeroItens; i++)
            {
                var produto = new Produto()
                {
                    Id = Faker.Random.Int(),
                    Descricao = Faker.Commerce.ProductName(),
                    DataFabricacao = Faker.Date.Past(),
                    DataValidade = Faker.Date.Future(),
                    Situacao = Faker.Random.ArrayElement(new[] { EnumSituacao.Ativo, EnumSituacao.Inativo }),
                    FornecedorId = Faker.Random.Int()
                };

                produto.Fornecedor = new FornecedorConstrutor()
                    .Padrao(1)
                    .Construir();

                Entidades.Add(produto);
            }
            return this;
        }
    }
}
