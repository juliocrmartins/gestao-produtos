using GestaoProdutos.Dominio.Modelos.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GestaoProdutos.Infra.Contexto
{
    public class ContextoBD : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }

        public ContextoBD(DbContextOptions<ContextoBD> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DefineRegrasBD(modelBuilder);
            CadastraFornecedores(modelBuilder);
        }

        private void DefineRegrasBD(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fornecedor>()
                .HasMany(e => e.Produtos)
                .WithOne(e => e.Fornecedor)
                .HasForeignKey(e => e.FornecedorId)
                .HasPrincipalKey(e => e.Id);

            modelBuilder.Entity<Produto>()
                .Property(p => p.Descricao)
                .IsRequired();
        }

        private void CadastraFornecedores(ModelBuilder modelBuilder)
        {
            var fornecedores = new List<Fornecedor>
            {
                new Fornecedor { Id = 1, CNPJ = "55.758.816/0001-70", Descricao = "Bosch"    },
                new Fornecedor { Id = 2, CNPJ = "78.738.236/0001-17", Descricao = "Moura"    },
                new Fornecedor { Id = 3, CNPJ = "89.752.353/0001-51", Descricao = "Sachs"    },
                new Fornecedor { Id = 4, CNPJ = "98.821.249/0001-62", Descricao = "Osram"    },
                new Fornecedor { Id = 5, CNPJ = "80.905.484/0001-57", Descricao = "Goodyear" },
                new Fornecedor { Id = 6, CNPJ = "40.232.831/0001-03", Descricao = "Dunlop"   },
                new Fornecedor { Id = 7, CNPJ = "55.784.902/0001-58", Descricao = "Valeo"    }
            };

            foreach (var fornecedor in fornecedores)
            {
                modelBuilder.Entity<Fornecedor>().HasData(fornecedor);
            }
        }
    }
}
