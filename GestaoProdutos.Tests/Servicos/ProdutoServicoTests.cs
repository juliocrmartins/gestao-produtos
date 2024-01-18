using AutoMapper;
using Bogus;
using GestaoProdutos.Dominio.Interfaces;
using GestaoProdutos.Dominio.Modelos;
using GestaoProdutos.Dominio.Modelos.DTO;
using GestaoProdutos.Dominio.Modelos.Entidades;
using GestaoProdutos.Dominio.Modelos.Enums;
using GestaoProdutos.Dominio.Servicos;
using GestaoProdutos.Tests.Buiders;
using GestaoProdutos.Tests.Construtores;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;

namespace GestaoProdutos.Tests.Servicos
{
    [TestClass]
    public class ProdutoServicoTests
    {
        private readonly ProdutoServico _produtoServico;
        private readonly Mock<IProdutoRepositorio> _produtoRepositorio;
        private readonly Mock<IFornecedorRepositorio> _fornecedorRepositorio;
        private readonly Mock<IMapper> _mapper;

        public ProdutoServicoTests()
        {
            _produtoRepositorio = new Mock<IProdutoRepositorio>();
            _fornecedorRepositorio = new Mock<IFornecedorRepositorio>();
            _mapper = new Mock<IMapper>();

            _produtoServico = new ProdutoServico(
                _produtoRepositorio.Object,
                _fornecedorRepositorio.Object,
                _mapper.Object);
        }

        [TestMethod]
        public void CriarProduto_Sucesso()
        {
            var requisicao = new InsereProdutoRequisicaoConstrutor()
                .Padrao(1)
                .Construir();

            var fornecedor = new FornecedorConstrutor()
                .Padrao(1)
                .Construir();

            _fornecedorRepositorio
                .Setup(r => r.ObterPorId(requisicao.FornecedorId))
                .Returns(fornecedor);

            var produtoCadastradoId = new Faker().Random.Int();

            var produtoCadastrado = new Produto();
            _produtoRepositorio.Setup(r => r.Inserir(It.IsAny<Produto>())).Callback((Produto p) =>
            {
                produtoCadastrado = p;
            }).Returns(produtoCadastradoId);

            var resultado = _produtoServico.Inserir(requisicao);

            _produtoRepositorio
                .Verify(r => r.Inserir(It.IsAny<Produto>()),
                    Times.Once);

            Assert.IsTrue(resultado.Sucesso);
            Assert.IsNull(resultado.MensagemErro);
            Assert.AreEqual(resultado.Id, produtoCadastradoId);

            Assert.AreEqual(produtoCadastrado.DataFabricacao, requisicao.DataFabricacao);
            Assert.AreEqual(produtoCadastrado.DataValidade, requisicao.DataValidade);
            Assert.AreEqual(produtoCadastrado.Descricao, requisicao.Descricao);
            Assert.AreEqual(produtoCadastrado.FornecedorId, requisicao.FornecedorId);
            Assert.AreEqual(produtoCadastrado.Situacao, requisicao.Situacao);
        }

        [TestMethod]
        public void CriarProduto_DataValidade_Erro()
        {
            var requisicao = new InsereProdutoRequisicaoConstrutor()
                .Padrao(1)
                .ComValidadeMenorQueFabricacao()
                .Construir();

            var resultado = _produtoServico.Inserir(requisicao);

            Assert.IsFalse(resultado.Sucesso);
            Assert.AreEqual("A data de validade não pode ser menor ou igual a data de fabricação", resultado.MensagemErro);
        }

        [TestMethod]
        public void CriarProduto_FornecedorInexistente_Erro()
        {
            var requisicao = new InsereProdutoRequisicaoConstrutor()
                .Padrao(1)
                .Construir();

            var fornecedorInexistente = FornecedorConstrutor.EntidadeNula;

            _fornecedorRepositorio
                .Setup(r => r.ObterPorId(requisicao.FornecedorId))
                .Returns(fornecedorInexistente);

            var resultado = _produtoServico.Inserir(requisicao);

            Assert.IsFalse(resultado.Sucesso);
            Assert.AreEqual("O fornecedor não existe", resultado.MensagemErro);
        }

        [TestMethod]
        public void AtualizarProduto_Sucesso()
        {
            var produtoId = new Faker().Random.Int();
            var requisicao = new AtualizaProdutoRequisicaoConstrutor()
                .Padrao(1)
                .Construir();

            var fornecedor = new FornecedorConstrutor()
                .Padrao(1)
                .Construir();

            var produto = new ProdutoConstrutor()
                .Padrao(1)
                .Construir();

            _fornecedorRepositorio
                .Setup(r => r.ObterPorId(requisicao.FornecedorId))
                .Returns(fornecedor);

            _produtoRepositorio
                .Setup(r => r.ObterPorId(produtoId))
                .Returns(produto);

            var produtoAtualizado = new Produto();
            _produtoRepositorio.Setup(r => r.Atualizar(It.IsAny<Produto>())).Callback((Produto p) =>
            {
                produtoAtualizado = p;
            });

            var resultado = _produtoServico.Atualizar(produtoId, requisicao);

            _produtoRepositorio
                .Verify(r => r.Atualizar(It.IsAny<Produto>()),
                    Times.Once);

            Assert.IsTrue(resultado.Sucesso);
            Assert.IsNull(resultado.MensagemErro);

            Assert.AreEqual(produtoAtualizado.DataFabricacao, requisicao.DataFabricacao);
            Assert.AreEqual(produtoAtualizado.DataValidade, requisicao.DataValidade);
            Assert.AreEqual(produtoAtualizado.Descricao, requisicao.Descricao);
            Assert.AreEqual(produtoAtualizado.FornecedorId, requisicao.FornecedorId);
            Assert.AreEqual(produtoAtualizado.Situacao, requisicao.Situacao);
        }

        [TestMethod]
        public void AtualizarProduto_DataValidade_Erro()
        {
            var produtoId = new Faker().Random.Int();
            var requisicao = new AtualizaProdutoRequisicaoConstrutor()
                .Padrao(1)
                .ComValidadeMenorQueFabricacao()
                .Construir();

            var resultado = _produtoServico.Atualizar(produtoId, requisicao);

            Assert.IsFalse(resultado.Sucesso);
            Assert.AreEqual("A data de validade não pode ser menor ou igual a data de fabricação", resultado.MensagemErro);
        }

        [TestMethod]
        public void AtualizarProduto_ProdutoInexistente_Erro()
        {
            var produtoId = new Faker().Random.Int();
            var requisicao = new AtualizaProdutoRequisicaoConstrutor()
                .Padrao(1)
                .Construir();

            var produtoInexistente = ProdutoConstrutor.EntidadeNula;

            _produtoRepositorio
                .Setup(r => r.ObterPorId(produtoId))
                .Returns(produtoInexistente);

            var resultado = _produtoServico.Atualizar(produtoId, requisicao);

            Assert.IsFalse(resultado.Sucesso);
            Assert.AreEqual("O produto não existe", resultado.MensagemErro);
        }

        [TestMethod]
        public void AtualizarProduto_FornecedorInexistente_Erro()
        {
            var produtoId = new Faker().Random.Int();
            var requisicao = new AtualizaProdutoRequisicaoConstrutor()
                .Padrao(1)
                .Construir();

            var produto = new ProdutoConstrutor()
                .Padrao(1)
                .Construir();

            _produtoRepositorio
                .Setup(r => r.ObterPorId(produtoId))
                .Returns(produto);

            var fornecedorInexistente = FornecedorConstrutor.EntidadeNula;

            _fornecedorRepositorio
                .Setup(r => r.ObterPorId(requisicao.FornecedorId))
                .Returns(fornecedorInexistente);

            var resultado = _produtoServico.Atualizar(produtoId, requisicao);

            Assert.IsFalse(resultado.Sucesso);
            Assert.AreEqual("O fornecedor não existe", resultado.MensagemErro);
        }

        [TestMethod]
        public void ExcluirProduto_Sucesso()
        {
            var produtoId = new Faker().Random.Int();

            var produto = new ProdutoConstrutor()
                .Padrao(1)
                .Construir();

            _produtoRepositorio
                .Setup(r => r.ObterPorId(produtoId))
                .Returns(produto);

            var situacaoProduto = default(EnumSituacao);
            _produtoRepositorio.Setup(r => r.Atualizar(It.IsAny<Produto>())).Callback((Produto p) =>
            {
                situacaoProduto = p.Situacao;
            });

            var resultado = _produtoServico.Excluir(produtoId);

            _produtoRepositorio
                .Verify(r => r.Atualizar(It.IsAny<Produto>()),
                    Times.Once);

            Assert.AreEqual(EnumSituacao.Inativo, situacaoProduto);

            Assert.IsTrue(resultado.Sucesso);
            Assert.IsNull(resultado.MensagemErro);
        }

        [TestMethod]
        public void ExcluirProduto_ProdutoInexistente_Erro()
        {
            var produtoId = new Faker().Random.Int();

            var produtoInexistente = ProdutoConstrutor.EntidadeNula;

            _produtoRepositorio
                .Setup(r => r.ObterPorId(produtoId))
                .Returns(produtoInexistente);

            var resultado = _produtoServico.Excluir(produtoId);

            Assert.IsFalse(resultado.Sucesso);
            Assert.AreEqual("O produto não existe", resultado.MensagemErro);
        }

        [TestMethod]
        public void ObterProdutoPorId_Sucesso()
        {
            var produtoId = new Faker().Random.Int();

            var produto = new ProdutoConstrutor()
                .Padrao(1)
                .Construir();

            _produtoRepositorio
                .Setup(r => r.ObterPorId(produtoId))
                .Returns(produto);

            _mapper.Setup(m => m.Map<ProdutoDto>(
                    It.Is<Produto>(p => p.Id == produto.Id 
                        && p.Descricao == produto.Descricao
                        && p.Situacao == produto.Situacao
                        && p.DataFabricacao == produto.DataFabricacao
                        && p.DataValidade == produto.DataValidade
                        && p.FornecedorId == produto.FornecedorId
                        && p.Fornecedor.Id == produto.Fornecedor.Id
                        && p.Fornecedor.Descricao == produto.Fornecedor.Descricao
                        && p.Fornecedor.CNPJ == produto.Fornecedor.CNPJ)
                ))
                .Returns(new ProdutoDto 
                    { 
                        Id = produto.Id,
                        Descricao = produto.Descricao,
                        Situacao = produto.Situacao,
                        DataFabricacao = produto.DataFabricacao,
                        DataValidade = produto.DataValidade,
                        FornecedorId = produto.FornecedorId,
                        Fornecedor = new FornecedorDto
                        {
                            Id = produto.Fornecedor.Id,
                            Descricao = produto.Fornecedor.Descricao,
                            CNPJ = produto.Fornecedor.CNPJ
                        }
                    });

            var resultado = _produtoServico.ObterPorId(produtoId);

            Assert.AreEqual(produto.DataFabricacao, resultado.DataFabricacao);
            Assert.AreEqual(produto.DataValidade, resultado.DataValidade);
            Assert.AreEqual(produto.Descricao, resultado.Descricao);
            Assert.AreEqual(produto.FornecedorId, resultado.FornecedorId);
            Assert.AreEqual(produto.Situacao, resultado.Situacao);
            Assert.AreEqual(produto.Fornecedor.Id, resultado.Fornecedor.Id);
            Assert.AreEqual(produto.Fornecedor.Descricao, resultado.Fornecedor.Descricao);
            Assert.AreEqual(produto.Fornecedor.CNPJ, resultado.Fornecedor.CNPJ);
        }

        [TestMethod]
        public void ListarPorFiltro_Sucesso()
        {
            var requisicao = new FiltraProdutoRequisicaoConstrutor()
                .Padrao(1)
                .Construir();

            var produto = new ProdutoConstrutor()
                .Padrao(1)
                .ConstruirLista();
            var produtoItem = produto.First();

            var entidadePaginada = new EntidadePaginada<Produto>
            {
                ItemsPorPagina = requisicao.ItemsPorPagina,
                Pagina = requisicao.Pagina,
                TotalRegistros = 10,
                Registros = produto
            };

            _produtoRepositorio.Setup(r => r.ListarPorFiltro(
                    It.Is<ProdutoFiltro>(f => f.TextoBusca == requisicao.TextoBusca
                        && f.FornecedorId == requisicao.FornecedorId
                        && f.Situacao == requisicao.Situacao
                        && f.ItemsPorPagina == requisicao.ItemsPorPagina
                        && f.Pagina == requisicao.Pagina)
                ))
                .Returns(entidadePaginada);

            _mapper.Setup(m => m.Map<ProdutoDto>(
                    It.Is<Produto>(p => p.Id == produtoItem.Id
                        && p.Descricao == produtoItem.Descricao
                        && p.Situacao == produtoItem.Situacao
                        && p.DataFabricacao == produtoItem.DataFabricacao
                        && p.DataValidade == produtoItem.DataValidade
                        && p.FornecedorId == produtoItem.FornecedorId
                        && p.Fornecedor.Id == produtoItem.Fornecedor.Id
                        && p.Fornecedor.Descricao == produtoItem.Fornecedor.Descricao
                        && p.Fornecedor.CNPJ == produtoItem.Fornecedor.CNPJ)
                ))
                .Returns(new ProdutoDto
                {
                    Id = produtoItem.Id,
                    Descricao = produtoItem.Descricao,
                    Situacao = produtoItem.Situacao,
                    DataFabricacao = produtoItem.DataFabricacao,
                    DataValidade = produtoItem.DataValidade,
                    FornecedorId = produtoItem.FornecedorId,
                    Fornecedor = new FornecedorDto
                    {
                        Id = produtoItem.Fornecedor.Id,
                        Descricao = produtoItem.Fornecedor.Descricao,
                        CNPJ = produtoItem.Fornecedor.CNPJ
                    }
                });

            var resultado = _produtoServico.ListarPorFiltro(requisicao);

            var registro = resultado.Registros.First();

            Assert.AreEqual(produtoItem.DataFabricacao, registro.DataFabricacao);
            Assert.AreEqual(produtoItem.DataValidade, registro.DataValidade);
            Assert.AreEqual(produtoItem.Descricao, registro.Descricao);
            Assert.AreEqual(produtoItem.FornecedorId, registro.FornecedorId);
            Assert.AreEqual(produtoItem.Situacao, registro.Situacao);
            Assert.AreEqual(produtoItem.Fornecedor.Id, registro.Fornecedor.Id);
            Assert.AreEqual(produtoItem.Fornecedor.Descricao, registro.Fornecedor.Descricao);
            Assert.AreEqual(produtoItem.Fornecedor.CNPJ, registro.Fornecedor.CNPJ);
        }
    }
}
