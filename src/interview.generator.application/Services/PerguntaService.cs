using interview.generator.application.Dto;
using interview.generator.application.Interfaces;
using interview.generator.application.ViewModels;
using interview.generator.domain.Entidade;
using interview.generator.domain.Entidade.Common;
using interview.generator.domain.Repositorio;

namespace interview.generator.application.Services
{
    public class PerguntaService : IPerguntaService
    {
        private readonly IPerguntaRepositorio _perguntaRepositorio;
        private readonly IAreaConhecimentoRepositorio _areaConhecimentoRepositorio;

        public PerguntaService(IPerguntaRepositorio perguntaRepositorio, IAreaConhecimentoRepositorio areaConhecimentoRepositorio)
        {
            _perguntaRepositorio = perguntaRepositorio;
            _areaConhecimentoRepositorio = areaConhecimentoRepositorio;
        }

        public async Task<ResponseBase> CadastrarPergunta(AdicionarPerguntaDto pergunta, Guid usuarioId)
        {
            var response = new ResponseBase();

            var perguntaDuplicada = await _perguntaRepositorio.ExistePorDescricao(pergunta.Descricao, usuarioId);
            if (perguntaDuplicada)
            {
                response.AddErro("Pergunta já cadastrada");
                return response;
            }

            var areaConhecimento = await _areaConhecimentoRepositorio.ObterPorIdEUsuarioId(pergunta.AreaConhecimentoId, usuarioId);

            if(areaConhecimento == null)
            {
                response.AddErro("Id de areaConhecimendo inválido");
                return response;
            }

            //verificar se areaConhecimentoExiste

            var novaPergunta = new Pergunta(areaConhecimento, pergunta.Descricao, usuarioId);

            foreach (var alternativa in pergunta.Alternativas)
                novaPergunta.AdicionarAlternativa(new Alternativa(alternativa.Descricao, alternativa.Correta));

            await _perguntaRepositorio.Adicionar(novaPergunta);

            response.AddData("Pergunta adicionada com sucesso!");
            return response;
        }

        public ResponseBase<IEnumerable<PerguntaViewModel>> ListarPerguntas(Guid userId, Guid perguntaId, string? areaConhecimento, string? descricao)
        {
            var response = new ResponseBase<IEnumerable<PerguntaViewModel>>();

            var perguntas = _perguntaRepositorio.ObterPerguntas(userId, perguntaId, areaConhecimento, descricao);

            var perguntasViewModel =
                perguntas
                    .Select(p => new PerguntaViewModel()
                    {
                        Id = p.Id,
                        Areaconhecimento = p.AreaConhecimento.Descricao,
                        Descricao = p.Descricao,
                        Alternativas = p.Alternativas.Select(a => new AlternativaViewModel(a.Descricao, a.Correta)).ToList()
                    });

            response.AddData(perguntasViewModel);

            return response;
        }
    }
}
