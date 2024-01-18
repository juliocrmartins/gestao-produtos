using GestaoProdutos.Dominio.Modelos.Enums;
using GestaoProdutos.Dominio.Requisicoes;
using GestaoProdutos.Tests.Buiders;
using System.Linq;

namespace GestaoProdutos.Tests.Construtores
{
    public class AtualizaProdutoRequisicaoConstrutor : ConstrutorBase<AtualizaProdutoRequisicao>
    {
        public override AtualizaProdutoRequisicaoConstrutor Padrao(int numeroItens = 1)
        {
            for (int i = 0; i < numeroItens; i++)
            {
                Entidades.Add(new AtualizaProdutoRequisicao()
                {
                    Descricao = Faker.Commerce.ProductName(),
                    DataFabricacao = Faker.Date.Past(),
                    DataValidade = Faker.Date.Future(),
                    Situacao = Faker.Random.ArrayElement(new[] { EnumSituacao.Ativo, EnumSituacao.Inativo }),
                    FornecedorId = Faker.Random.Int()
                });
            }
            return this;
        }

        public AtualizaProdutoRequisicaoConstrutor ComValidadeMenorQueFabricacao()
        {
            var entidade = Entidades.First();

            entidade.DataFabricacao = Faker.Date.Past();
            entidade.DataValidade = entidade.DataFabricacao.AddDays(-1);
            return this;
        }
    }
}
