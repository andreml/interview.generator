using InterviewGenerator.Domain.Entidade;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewGenerator.Infra.Mapping;

public class AlternativaMapping : IEntityTypeConfiguration<Alternativa>
{
    public void Configure(EntityTypeBuilder<Alternativa> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.PerguntaId)
            .IsRequired();

        builder.Property(x => x.Correta)
            .IsRequired();

        builder.Property(x => x.Descricao)
            .IsRequired()
            .HasColumnType("VARCHAR(1000)");

        builder
            .HasMany(x => x.RespostasAvaliacao)
            .WithOne(x => x.AlternativaEscolhida)
            .IsRequired(false);
    }
}
