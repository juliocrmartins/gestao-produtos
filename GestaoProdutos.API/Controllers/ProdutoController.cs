using GestaoProdutos.Dominio.Interfaces;
using GestaoProdutos.Dominio.Modelos.DTO;
using GestaoProdutos.Dominio.Modelos.Entidades;
using GestaoProdutos.Dominio.Requisicoes;
using GestaoProdutos.Dominio.Servicos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GestaoProdutos.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly IRepositorio<Produto> _produtoRepositorio;
        private readonly ProdutoServico _produtoServico;


        private static readonly string[] Produtos = new[]
        {
            "Lanterna C3", "Pastilha de freio Ford", "Calota Peugeot 206", "Protetor de Cárter Montana"
        };

        public ProdutoController(IRepositorio<Produto> produtoRepositorio, ProdutoServico produtoServico)
        {
            _produtoRepositorio = produtoRepositorio;
            _produtoServico = produtoServico;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get(FiltraProdutoRequisicao requisicao)
        {
            var lista = _produtoRepositorio.ListarTodos();

            var rng = new Random();
            return Enumerable.Range(1, 3).Select(index => new Produto
            {
                Descricao = Produtos[rng.Next(Produtos.Length)]
            })
            .ToArray();
        }

        [HttpGet("{produtoId}")]
        public ActionResult<ProdutoDto> Get(int produtoId)
        {
            var resultado = _produtoServico.ObterPorId(produtoId);
            return resultado;
        }

        [HttpPost]
        public ActionResult Post([FromBody] InsereProdutoRequisicao requisicao)
        {
            var resultado = _produtoServico.Inserir(requisicao);

            return (!resultado.Sucesso)
                ? BadRequest(resultado)
                : CreatedAtAction(nameof(Post), resultado);
        }

        [HttpPut("{produtoId}")]
        public ActionResult Put(int produtoId, [FromBody] AtualizaProdutoRequisicao requisicao)
        {
            var resultado = _produtoServico.Atualizar(produtoId, requisicao);

            return (!resultado.Sucesso)
                ? BadRequest(resultado)
                : Ok(resultado);
        }

        [HttpDelete("{produtoId}")]
        public ActionResult Delete(int produtoId)
        {
            var resultado = _produtoServico.Excluir(produtoId);

            return (!resultado.Sucesso)
                ? BadRequest(resultado)
                : Ok(resultado);
        }
    }
}
