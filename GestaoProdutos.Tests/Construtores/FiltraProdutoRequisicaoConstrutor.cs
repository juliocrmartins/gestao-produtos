using GestaoProdutos.Dominio.Modelos.Enums;
using GestaoProdutos.Dominio.Requisicoes;
using GestaoProdutos.Tests.Buiders;

namespace GestaoProdutos.Tests.Construtores
{
    public class FiltraProdutoRequisicaoConstrutor : ConstrutorBase<FiltraProdutoRequisicao>
    {
        public override FiltraProdutoRequisicaoConstrutor Padrao(int numeroItens = 1)
        {
            for (int i = 0; i < numeroItens; i++)
            {
                Entidades.Add(new FiltraProdutoRequisicao()
                {
                    FornecedorId = Faker.Random.Int(),
                    Situacao = Faker.Random.ArrayElement(new[] { EnumSituacao.Ativo, EnumSituacao.Inativo }),
                    TextoBusca = Faker.Company.CompanyName(),
                    ItemsPorPagina = Faker.Random.Int(),
                    Pagina = Faker.Random.Int()
                });
            }
            return this;
        }
    }
}
