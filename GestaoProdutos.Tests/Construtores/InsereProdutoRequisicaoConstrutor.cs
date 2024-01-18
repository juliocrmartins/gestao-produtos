using GestaoProdutos.Dominio.Modelos.Enums;
using GestaoProdutos.Dominio.Requisicoes;
using GestaoProdutos.Tests.Buiders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProdutos.Tests.Construtores
{
    public class InsereProdutoRequisicaoConstrutor : ConstrutorBase<InsereProdutoRequisicao>
    {
        public override InsereProdutoRequisicaoConstrutor Padrao(int numeroItens = 1)
        {
            for (int i = 0; i < numeroItens; i++)
            {
                Entidades.Add(new InsereProdutoRequisicao()
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

        public InsereProdutoRequisicaoConstrutor ComValidadeMenorQueFabricacao()
        {
            var entidade = Entidades.First();

            entidade.DataFabricacao = Faker.Date.Past();
            entidade.DataValidade = entidade.DataFabricacao.AddDays(-1);
            return this;
        }
    }
}
