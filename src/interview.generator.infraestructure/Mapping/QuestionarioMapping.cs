using Azure;
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
                .Property(x => x.DataCriacao)
                .IsRequired();


            builder
                .HasMany(x => x.Perguntas)
                .WithMany(x => x.Questionarios)
                .UsingEntity<Dictionary<Guid, Guid>>(
                    "QuestionarioPergunta",
                    l => l.HasOne<Pergunta>().WithMany().HasForeignKey("PerguntaId"),
                    r => r.HasOne<Questionario>().WithMany().HasForeignKey("QuestionarioId"));

            builder
                .HasMany(x => x.Avaliacoes)
                .WithOne(x => x.Questionario)
                .HasForeignKey("QuestionarioId");
        }
    }
}
