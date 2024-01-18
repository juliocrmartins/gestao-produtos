using Bogus.Extensions.Brazil;
using GestaoProdutos.Dominio.Modelos.Entidades;
using GestaoProdutos.Tests.Buiders;

namespace GestaoProdutos.Tests.Construtores
{
    public class FornecedorConstrutor : ConstrutorBase<Fornecedor>
    {
        public override FornecedorConstrutor Padrao(int numeroItens = 1)
        {
            for (int i = 0; i < numeroItens; i++)
            {
                Entidades.Add(new Fornecedor()
                {
                    Id = Faker.Random.Int(),
                    Descricao = Faker.Company.CompanyName(),
                    CNPJ = Faker.Company.Cnpj(true)
                });
            }
            return this;
        }
    }
}
