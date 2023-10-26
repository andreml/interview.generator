using interview.generator.domain.Entidade;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace interview.generator.infraestructure.Mapping
{
    public class QuestionarioMapping : IEntityTypeConfiguration<Questionario>
    {
        public void Configure(EntityTypeBuilder<Questionario> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Nome)
                .IsRequired()
                .HasColumnType("VARCHAR(200)");

            builder
                .Property(x => x.UsuarioCriacaoId)
                .IsRequired();

            builder
                .Property(x => x.TipoQuestionarioId)
                .IsRequired();

            builder
                .Property(x => x.DataCriacao)
                .IsRequired();

            builder
               .HasMany(x => x.PerguntasQuestionario)
               .WithOne(x => x.Questionario)
               .HasForeignKey(x => x.QuestionarioId);
        }
    }
}
