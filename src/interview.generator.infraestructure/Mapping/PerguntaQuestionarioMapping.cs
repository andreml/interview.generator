using interview.generator.domain.Entidade;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interview.generator.infraestructure.Mapping
{
    public class PerguntaQuestionarioMapping : IEntityTypeConfiguration<PerguntaQuestionario>
    {
        public void Configure(EntityTypeBuilder<PerguntaQuestionario> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Peso)
                .IsRequired();

            builder
                .Property(x => x.OrdemApresentacao)
                .IsRequired();

            builder
                .Property(x => x.PerguntaId)
                .IsRequired();

            builder
                .Property(x => x.QuestionarioId)
                .IsRequired();
        }
    }
}
