using AutoMapper;
using GestaoProdutos.Dominio.Interfaces;
using GestaoProdutos.Dominio.Modelos.DTO;
using GestaoProdutos.Dominio.Modelos.Entidades;
using GestaoProdutos.Dominio.Modelos.Enums;
using GestaoProdutos.Dominio.Requisicoes;
using GestaoProdutos.Dominio.Respostas;
using System.Collections.Generic;

namespace GestaoProdutos.Dominio.Servicos
{
    public class ProdutoServico
    {
        private readonly IRepositorio<Produto> _produtoRepositorio;
        private readonly IMapper _mapper;

        public ProdutoServico(IRepositorio<Produto> produtoRepositorio, IMapper mapper)
        {
            _produtoRepositorio = produtoRepositorio;
            _mapper = mapper;
        }

        public ProdutoDto ObterPorId(int produtoId)
        {
            var produto = _produtoRepositorio.ObterPorId(produtoId);

            var produtoDTO = _mapper.Map<ProdutoDto>(produto);

            return produtoDTO;
        }

        public IEnumerable<ProdutoDto> ListarPorFiltro(FiltraProdutoRequisicao requisicao)
        {

        }

        public InsereProdutoResposta Inserir(InsereProdutoRequisicao requisicao)
        {
            if (requisicao?.DataValidade <= requisicao?.DataFabricacao)
                return new InsereProdutoResposta { Sucesso = false, MensagemErro = "A data de validade não pode ser menor ou igual a data de fabricação" };

            var produto = new Produto
            {
                DataFabricacao = requisicao.DataFabricacao,
                DataValidade = requisicao.DataValidade,
                Descricao = requisicao.Descricao,
                FornecedorId = requisicao.FornecedorId,
                Situacao = requisicao.Situacao
            };

            var id = _produtoRepositorio.Inserir(produto);

            return new InsereProdutoResposta { Sucesso = true, Id = id };
        }

        public AtualizaProdutoResposta Atualizar(int produtoId, AtualizaProdutoRequisicao requisicao)
        {
            if (requisicao?.DataValidade <= requisicao?.DataFabricacao)
                return new AtualizaProdutoResposta { Sucesso = false, MensagemErro = "A data de validade não pode ser menor ou igual a data de fabricação" };

            var produto = _produtoRepositorio.ObterPorId(produtoId);

            if (produto is null)
                return new AtualizaProdutoResposta { Sucesso = false, MensagemErro = "O produto não existe" };

            produto.Descricao = requisicao.Descricao;
            produto.DataFabricacao = requisicao.DataFabricacao;
            produto.DataValidade = requisicao.DataValidade;
            produto.Descricao = requisicao.Descricao;
            produto.FornecedorId = requisicao.FornecedorId;
            produto.Situacao = requisicao.Situacao;

            _produtoRepositorio.Atualizar(produto);

            return new AtualizaProdutoResposta { Sucesso = true };
        }

        public ExcluiProdutoResposta Excluir(int produtoId)
        {
            var produto = _produtoRepositorio.ObterPorId(produtoId);

            if (produto is null)
                return new ExcluiProdutoResposta { Sucesso = false, MensagemErro = "O produto não existe" };

            produto.Situacao = EnumSituacao.Inativo;

            _produtoRepositorio.Atualizar(produto);

            return new ExcluiProdutoResposta { Sucesso = true };
        }
    }
}
