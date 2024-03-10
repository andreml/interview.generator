﻿using FluentValidation;
using System.Text;
using System.Text.Json.Serialization;

namespace InterviewGenerator.Application.Dto
{
    public class AdicionarPerguntaDto
    {
        [JsonIgnore]
        public Guid UsuarioId { get; set; }
        public string Descricao { get; set; } = default!;
        public string AreaConhecimento { get; set; } = default!;
        public ICollection<AlternativaDto> Alternativas { get; set; } = default!;
        public int NumeroLinha { get; set; }

        public static AdicionarPerguntaDto FromCsv(string linhaCsv, Guid usuarioId, int numeroLinha)
        {
            string[] values = linhaCsv.Split(';');

            AdicionarPerguntaDto pergunta = new()
            {
                AreaConhecimento = values[0],
                Descricao = values[1],
                UsuarioId = usuarioId,
                NumeroLinha = numeroLinha,
                Alternativas = new List<AlternativaDto>
                {
                    new(values[2], true),
                    new(values[3], false),
                    new(values[4], false)
                }
            };

            if (!string.IsNullOrEmpty(values[5]))
                pergunta.Alternativas.Add(new(values[5], false));

            if (!string.IsNullOrEmpty(values[6]))
                pergunta.Alternativas.Add(new(values[6], false));

            return pergunta;
        }
    }

    public class AlternativaDto
    {
        public AlternativaDto(string descricao, bool correta)
        {
            Descricao = descricao;
            Correta = correta;
        }

        public string Descricao { get; set; } = default!;
        public bool Correta { get; set; }
    }

    public class AdicionarPerguntaDtoValidator : AbstractValidator<AdicionarPerguntaDto>
    {
        public AdicionarPerguntaDtoValidator()
        {
            RuleFor(x => x.Descricao)
                .NotNull().NotEmpty().WithMessage("Descricao é obrigatório")
                .MaximumLength(1000).WithMessage("Descrição deve ter até 1000 caracteres");

            RuleFor(x => x.AreaConhecimento)
                .NotNull().NotEmpty()
                .WithMessage("AreaConhecimento é obrigatória")
                .MaximumLength(100).WithMessage("AreaConhecimento deve tera até 100 caracteres");


            RuleFor(x => x.Alternativas)
                .NotNull().NotEmpty()
                .WithMessage("Alternativas são obrigatórias");

            RuleFor(x => x.Alternativas)
                .Must(x => x.Count >= 3 && x.Count <= 5)
                .WithMessage("A pergunda teve ter entre 3 e 5 alternativas");

            RuleFor(x => x.Alternativas)
                .Must(x => x.Where(x => x.Correta).Count() == 1)
                .WithMessage("A pergunda teve ter somente uma alternativa correta");

            RuleFor(x => x.Alternativas)
                .Must(x => x.Select(x => x.Descricao).Count() == x.Select(x => x.Descricao).Distinct().Count())
                .WithMessage("Uma ou mais alternativas estão duplicadas")
                .ForEach(alternativa =>
                {
                    RuleFor(alternativa => alternativa.Descricao)
                        .NotNull().NotEmpty().WithMessage("Descrição da alternativa é obrigatória")
                        .MaximumLength(1000).WithMessage("Descrição da alternativa deve ter até 1000 caracteres");
                });
        }
    }
}
