using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoProdutos.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private static readonly string[] Produtos = new[]
        {
            "Lanterna C3", "Pastilha de freio Ford", "Calota Peugeot 206", "Protetor de Cárter Montana"
        };

        public ProdutoController()
        {
            
        }

        [HttpGet]
        public IEnumerable<Produto> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 3).Select(index => new Produto
            {
                Descricao = Produtos[rng.Next(Produtos.Length)]
            })
            .ToArray();
        }
    }
}
