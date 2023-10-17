using interview.generator.domain.Entidade;
using interview.generator.infraestructure.SqlServer;

namespace interview.generator.test.Infraestructure
{
    public class PerfilTest
    {
        Mock<PerfilRepositorio> mockRepositorio = new Mock<PerfilRepositorio>();
        [Fact]
        public async void AdicionarNovoPerfilEConsultarPorId()
        {
            var Perfis = new Perfil()
            {
                Descricao = "Nova descricao",
                Id = 1
            };

            await mockRepositorio.Object.Adicionar(Perfis);
            var result = await mockRepositorio.Object.ObterPorId(Perfis.Id);

            Assert.True(result != null);
        }

        [Fact]
        public async void AlterarPerfilCadastrado()
        {
            var Perfis = new Perfil()
            {
                Descricao = "Nova descricao",
                Id = 1
            };

            await mockRepositorio.Object.Adicionar(Perfis);
            Perfis.Descricao = "Alterado descricao";
            await mockRepositorio.Object.Alterar(Perfis);
            var result = await mockRepositorio.Object.ObterPorId(Perfis.Id);

            Assert.True(Perfis.Descricao == "Alterado descricao");
        }

        [Fact]
        public async void ConsultarPerfilNaoCadastrado()
        {
            var result = await mockRepositorio.Object.ObterPorId(99999);
            Assert.True(result is null);
        }

        [Fact]
        public async void ListarPerfisCadastrados()
        {
            var Perfis = new Perfil()
            {
                Descricao = "Nova descricao",
                Id = 1
            };

            await mockRepositorio.Object.Adicionar(Perfis);

            var Perfis1 = new Perfil()
            {
                Descricao = "Nova descricao 2",
                Id = 2
            };

            await mockRepositorio.Object.Adicionar(Perfis1);
            var result = await mockRepositorio.Object.ObterTodos();
            Assert.True(result.Count() > 0);
        }
    }
}