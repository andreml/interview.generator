using InterviewGenerator.Application.Dto;
using InterviewGenerator.Application.Interfaces;
using InterviewGenerator.Application.ViewModels;
using InterviewGenerator.Domain.Entidade;
using InterviewGenerator.Domain.Entidade.Common;
using InterviewGenerator.Domain.Repositorio;

namespace InterviewGenerator.Application.Services
{
    public class PerguntaService : IPerguntaService
    {
        private readonly IPerguntaRepositorio _perguntaRepositorio;

        private readonly IAreaConhecimentoService _areaConhecimentoService;

        public PerguntaService(IPerguntaRepositorio perguntaRepositorio, 
                               IAreaConhecimentoService areaConhecimentoService)
        {
            _perguntaRepositorio = perguntaRepositorio;
            _areaConhecimentoService = areaConhecimentoService;
        }

        public async Task<ResponseBase> AlterarPergunta(AlterarPerguntaDto perguntaDto)
        {
            var response = new ResponseBase();

            var pergunta = await _perguntaRepositorio.ObterPerguntaPorId(perguntaDto.UsuarioId, perguntaDto.Id);

            if(pergunta == null)
            {
                response.AddErro("Pergunta não encontrada");
                return response;
            }

            if(pergunta.Questionarios!.Count > 0)
            {
                response.AddErro("Já existem questionários cadastrados com esta pergunta");
                return response;
            }

            var areaConhecimento = await _areaConhecimentoService.ObterOuCriarAreaConhecimento(perguntaDto.UsuarioId, perguntaDto.AreaConhecimento);

            pergunta.Descricao = perguntaDto.Descricao;
            pergunta.AreaConhecimento = areaConhecimento;

            pergunta.RemoverAlternativas();

            foreach (var alternativa in perguntaDto.Alternativas)
                pergunta.AdicionarAlternativa(new Alternativa(alternativa.Descricao, alternativa.Correta));

            await _perguntaRepositorio.Alterar(pergunta);

            response.AddData("Pergunta alterada com sucesso!");
            return response;
        }

        public async Task<ResponseBase> CadastrarPergunta(AdicionarPerguntaDto pergunta)
        {
            var response = new ResponseBase();

            var perguntaDuplicada = await _perguntaRepositorio.ExistePorDescricao(pergunta.UsuarioId, pergunta.Descricao);
            if (perguntaDuplicada)
            {
                response.AddErro("Pergunta já cadastrada");
                return response;
            }

            var areaConhecimento = await _areaConhecimentoService.ObterOuCriarAreaConhecimento(pergunta.UsuarioId, pergunta.AreaConhecimento);

            var novaPergunta = new Pergunta(areaConhecimento, pergunta.Descricao, pergunta.UsuarioId);

            foreach (var alternativa in pergunta.Alternativas)
                novaPergunta.AdicionarAlternativa(new Alternativa(alternativa.Descricao, alternativa.Correta));

            await _perguntaRepositorio.Adicionar(novaPergunta);

            response.AddData("Pergunta adicionada com sucesso!");
            return response;
        }

        public ResponseBase<IEnumerable<PerguntaViewModel>> ListarPerguntas(Guid usuarioCriacaoId, Guid perguntaId, string? areaConhecimento, string? descricao)
        {
            var response = new ResponseBase<IEnumerable<PerguntaViewModel>>();

            var perguntas = _perguntaRepositorio.ObterPerguntas(usuarioCriacaoId, perguntaId, areaConhecimento, descricao);

            if(perguntas.Count() == 0)
                return response;

            var perguntasViewModel =
                perguntas
                    .Select(p => new PerguntaViewModel()
                    {
                        Id = p.Id,
                        Areaconhecimento = p.AreaConhecimento.Descricao,
                        Descricao = p.Descricao,
                        Alternativas = p.Alternativas.Select(a => new AlternativaViewModel(a.Id, a.Descricao, a.Correta)).ToList()
                    });

            response.AddData(perguntasViewModel);

            return response;
        }

        public async Task<ResponseBase> ExcluirPergunta(Guid usuarioCriacaoId, Guid perguntaId)
        {
            var response = new ResponseBase();

            var pergunta = await _perguntaRepositorio.ObterPerguntaPorId(usuarioCriacaoId, perguntaId);
            if (pergunta == null)
            {
                response.AddErro("Pergunta não encontrada");
                return response;
            }

            if (pergunta.Questionarios!.Count > 0)
            {
                response.AddErro("Já existem questionários cadastrados com esta pergunta");
                return response;
            }

            await _perguntaRepositorio.Excluir(pergunta);

            response.AddData("Pergunta excluída com sucesso!");
            return response;
        }

        
    }
}
