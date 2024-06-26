﻿using InterviewGenerator.Application.Dto;
using InterviewGenerator.Application.Interfaces;
using InterviewGenerator.Application.ViewModels;
using InterviewGenerator.Domain.Entidade;
using InterviewGenerator.Domain.Entidade.Common;
using InterviewGenerator.Domain.Repositorio;
using InterviewGenerator.Domain.Utils;

namespace InterviewGenerator.Application.Services;

public class QuestionarioService : IQuestionarioService
{
    private readonly IQuestionarioRepositorio _questionarioRepositorio;
    private readonly IPerguntaRepositorio _perguntaRepositorio;

    public QuestionarioService(
                IQuestionarioRepositorio questionarioRepositorio, 
                IPerguntaRepositorio perguntaRepositorio)
    {
        _questionarioRepositorio = questionarioRepositorio;
        _perguntaRepositorio = perguntaRepositorio;
    }

    public async Task<ResponseBase> AlterarQuestionario(AlterarQuestionarioDto questionarioDto)
    {
        var response = new ResponseBase();


        var questionario = await _questionarioRepositorio.ObterPorIdComAvaliacoesEPerguntas(questionarioDto.UsuarioId, questionarioDto.Id);
        if (questionario == null)
        {
            response.AddErro("Questionario não encontrado");
            return response;
        }

        if (questionario.Avaliacoes != null && questionario.Avaliacoes.Any())
        {
            response.AddErro("Não é possível alterar o questionário, existem avaliações feitas");
            return response;
        }

        if (questionarioDto.Nome != questionario.Nome)
        {
            var questionarioPorNome = await _questionarioRepositorio.ObterPorNome(questionarioDto.UsuarioId, questionarioDto.Nome);
            if (questionarioPorNome != null && questionarioPorNome.Id != questionarioDto.Id)
            {
                response.AddErro("Já existe um questionário com este nome");
                return response;
            }
        }

        questionario.RemoverPerguntas();
        questionario.Nome = questionarioDto.Nome;

        foreach (var pergunta in questionarioDto.Perguntas)
        {
            var perguntaExistente = await _perguntaRepositorio.ObterPerguntaPorId(questionarioDto.UsuarioId, pergunta);
            if (perguntaExistente == null)
            {
                response.AddErro($"Pergunta {pergunta} não encontrada");
                return response;
            }

            questionario.AdicionarPergunta(perguntaExistente);
        }

        await _questionarioRepositorio.Alterar(questionario);

        response.AddData("Questionario alterado com sucesso!");
        return response;
    }

    public async Task<ResponseBase> CadastrarQuestionario(AdicionarQuestionarioDto questionario)
    {
        var response = new ResponseBase();

        var quesitonarioDuplicado = await _questionarioRepositorio.ObterPorNome(questionario.UsuarioId, questionario.Nome);
        if (quesitonarioDuplicado != null)
        {
            response.AddErro("Questionário com esse nome já cadastrado");
            return response;
        }

        var novoQuestionario = new Questionario
        {
            Nome = questionario.Nome,
            UsuarioCriacaoId = questionario.UsuarioId
        };

        foreach (var pergunta in questionario.Perguntas)
        {
            var perguntaExistente = await _perguntaRepositorio.ObterPerguntaPorId(questionario.UsuarioId, pergunta);
            if (perguntaExistente == null)
            {
                response.AddErro($"Pergunta {pergunta} não encontrada");
                return response;
            }

            novoQuestionario.AdicionarPergunta(perguntaExistente);
        }

        await _questionarioRepositorio.Adicionar(novoQuestionario);

        response.AddData(novoQuestionario.Id);
        return response;
    }

    public async Task<ResponseBase> ExcluirQuestionario(Guid usuarioCriacaoId, Guid idQuestionario)
    {
        var response = new ResponseBase();

        var questionario = await _questionarioRepositorio.ObterPorIdComAvaliacoesEPerguntas(usuarioCriacaoId, idQuestionario);
        if (questionario == null)
        {
            response.AddErro("Questionario não encontrado");
            return response;
        }

        if (questionario.Avaliacoes != null && questionario.Avaliacoes.Any())
        {
            response.AddErro("Não é possível excluir o questionário, existem avaliações enviadas");
            return response;
        }

        await _questionarioRepositorio.Excluir(questionario);

        response.AddData("Questionario excluído com sucesso!");
        return response;
    }

    public async Task<ResponseBase<ICollection<QuestionarioViewModelAvaliador>>> ObterQuestionarios(Guid usuarioCriacaoId, Guid questionarioId, string? nome)
    {
        var response = new ResponseBase<ICollection<QuestionarioViewModelAvaliador>>();

        var questionarios = await _questionarioRepositorio.ObterQuestionarios(usuarioCriacaoId, questionarioId, nome);
        if (questionarios.Count() == 0)
            return response;

        var questionariosViewModel = questionarios.Select(x => new QuestionarioViewModelAvaliador()
        {
            Id = x.Id,
            DataCriacao = x.DataCriacao,
            Nome = x.Nome,
            AvaliacoesEnviadas = x.Avaliacoes.Count,
            Perguntas = x.Perguntas.Select(p => new PerguntaQuestionarioViewModelAvaliador()
            {
                Id = p.Id,
                Descricao = p.Descricao,
                Alternativas = p.Alternativas.Select(a => new AlternativaPerguntaQuestionarioViewModelAvaliador()
                {
                    Id = a.Id,
                    Descricao = a.Descricao,
                    Correta = a.Correta
                }).ToList()
            }).ToList()
        }).ToList();

        response.AddData(questionariosViewModel);

        return response;
    }
}
