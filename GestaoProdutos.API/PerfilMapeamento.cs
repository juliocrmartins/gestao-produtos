using AutoMapper;
using GestaoProdutos.Dominio.Modelos.DTO;
using GestaoProdutos.Dominio.Modelos.Entidades;

namespace GestaoProdutos.API
{
    public class PerfilMapeamento : Profile
    {
        public PerfilMapeamento()
        {
            CreateMap<Produto, ProdutoDto>();
            CreateMap<Fornecedor, FornecedorDto>();
        }
    }
}
