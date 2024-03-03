using InterviewGenerator.Domain.Entidade;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewGenerator.Infra.Mapping
{
    public class ControleImportacaoPerguntasMapping : IEntityTypeConfiguration<ControleImportacaoPerguntas>
    {
        public void Configure(EntityTypeBuilder<ControleImportacaoPerguntas> builder)
        {
            builder.ToTable("ControleImportacaoPerguntas");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.StatusImportacao)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(x => x.UsuarioId)
                .IsRequired();

            builder.Property(x => x.DataFimImportacao)
                .HasColumnType("DATETIME");

            builder.Property(x => x.DataUpload)
                .HasColumnType("DATETIME")
                .IsRequired();

            builder.Property(x => x.ErrosImportacao)
                .HasColumnType("VARCHAR(500)");

            builder.Property(x => x.NomeArquivo)
                .HasColumnType("VARCHAR(250)")
                .IsRequired();

            builder.Property(x => x.QuantidadeLinhasImportadas)
                .HasColumnType("INT")
                .IsRequired();

            builder
                .HasMany(x => x.LinhasArquivo)
                .WithOne()
                .HasForeignKey(x => x.IdControleImportacao)
                .IsRequired();


        }
    }
}
