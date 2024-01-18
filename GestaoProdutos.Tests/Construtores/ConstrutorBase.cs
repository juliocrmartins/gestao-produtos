using Bogus;
using System.Collections.Generic;
using System.Linq;

namespace GestaoProdutos.Tests.Buiders
{
    public abstract class ConstrutorBase<T> where T : class
    {
        protected List<T> Entidades { get; set; } = new List<T>();

        protected Faker Faker { get; set; } = new Faker("pt_BR");

        public abstract ConstrutorBase<T> Padrao(int numeroItens = 1);

        public T Construir()
            => Entidades.FirstOrDefault();

        public List<T> ConstruirLista()
            => Entidades;

        public static T EntidadeNula
        { get { return null; } }

    }
}
