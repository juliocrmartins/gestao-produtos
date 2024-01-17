using GestaoProdutos.Dominio.Interfaces;
using GestaoProdutos.Dominio.Modelos.Entidades;
using GestaoProdutos.Dominio.Servicos;
using GestaoProdutos.Infra.Contexto;
using GestaoProdutos.Infra.Repositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GestaoProdutos.API
{
    public static class Inicializador
    {
        public static void Configurar(IServiceCollection services, string conection)
        {
            services.AddDbContext<ContextoBD>(options => options.UseSqlServer(conection));

            services.AddScoped(typeof(IRepositorio<Produto>), typeof(ProdutoRepositorio));
            services.AddScoped(typeof(IRepositorio<>), typeof(Repositorio<>));
            services.AddScoped(typeof(ProdutoServico));
            //services.AddScoped(typeof(ContatoService));
            //services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        }
    }
}
