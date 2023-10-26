﻿using interview.generator.domain.Entidade.Common;

namespace interview.generator.domain.Entidade
{
    public class Questionario : EntidadeBase
    {
        public string Nome { get; set; }
        public Guid UsuarioCriacaoId { get; set; }
        public Guid TipoQuestionarioId { get; set; }
        public DateTime DataCriacao { get; set; }
        public List<PerguntaQuestionario> PerguntasQuestionario { get; set; } = new();

        public Questionario()
        {
        }

        public void AdicionarPergunta(PerguntaQuestionario perguntaQuestionario)
        {
            PerguntasQuestionario.Add(perguntaQuestionario);
        }
    }
}
