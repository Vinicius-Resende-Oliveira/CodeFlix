using Bogus;

namespace CodeFlix.Catalog.UnitTest.Commons
{
    public abstract class BaseFixture
    {
        protected BaseFixture() 
            => Faker = new Faker("pt_BR");

        public Faker Faker { get; set; }
    }
}
