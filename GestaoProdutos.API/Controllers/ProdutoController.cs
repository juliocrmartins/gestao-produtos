using GestaoProdutos.Dominio.Modelos.DTO;
using GestaoProdutos.Dominio.Requisicoes;
using GestaoProdutos.Dominio.Respostas;
using GestaoProdutos.Dominio.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace GestaoProdutos.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoServico _produtoServico;

        public ProdutoController(ProdutoServico produtoServico)
        {
            _produtoServico = produtoServico;
        }

        [HttpGet]
        public ActionResult<ListaPaginadaResposta<ProdutoDto>> Get([FromQuery] FiltraProdutoRequisicao requisicao)
        {
            var resultado = _produtoServico.ListarPorFiltro(requisicao);
            return resultado;
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
