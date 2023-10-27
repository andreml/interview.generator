using interview.generator.domain.Entidade;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace interview.generator.infraestructure.Mapping
{
    public class PerguntaQuestionarioMapping : IEntityTypeConfiguration<PerguntaQuestionario>
    {
        public void Configure(EntityTypeBuilder<PerguntaQuestionario> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.OrdemApresentacao)
                .IsRequired();
        }
    }
}
