using interview.generator.domain.Entidade;
using interview.generator.infraestructure.SqlServer;

namespace interview.generator.test.Infraestructure
{
    public class TipoQuestionarioTest
    {
        Mock<TipoQuestionarioRepositorio> mockRepositorio = new Mock<TipoQuestionarioRepositorio>();
        [Fact]
        public async void AdicionarNovoTipoQuestionarioEConsultarPorId()
        {
            var TipoQuestionarios = new TipoQuestionario()
            {
                Descricao = "Nova descricao",
                Id = 1
            };

            await mockRepositorio.Object.Adicionar(TipoQuestionarios);
            var result = await mockRepositorio.Object.ObterPorId(TipoQuestionarios.Id);

            Assert.True(result != null);
        }

        [Fact]
        public async void AlterarTipoQuestionarioCadastrado()
        {
            var TipoQuestionarios = new TipoQuestionario()
            {
                Descricao = "Nova descricao",
                Id = 1
            };

            await mockRepositorio.Object.Adicionar(TipoQuestionarios);
            TipoQuestionarios.Descricao = "Alterado descricao";
            await mockRepositorio.Object.Alterar(TipoQuestionarios);
            var result = await mockRepositorio.Object.ObterPorId(TipoQuestionarios.Id);

            Assert.True(TipoQuestionarios.Descricao == "Alterado descricao");
        }

        [Fact]
        public async void ConsultarTipoQuestionarioNaoCadastrado()
        {
            var result = await mockRepositorio.Object.ObterPorId(99999);
            Assert.True(result is null);
        }

        [Fact]
        public async void ListarTipoQuestionariosCadastrados()
        {
            var TipoQuestionarios = new TipoQuestionario()
            {
                Descricao = "Nova descricao",
                Id = 1
            };
            await mockRepositorio.Object.Adicionar(TipoQuestionarios);

            var TipoQuestionarios1 = new TipoQuestionario()
            {
                Descricao = "Nova descricao",
                Id = 2
            };

            await mockRepositorio.Object.Adicionar(TipoQuestionarios1);
            var result = await mockRepositorio.Object.ObterTodos();
            Assert.True(result.Count() > 0);
        }
    }
}