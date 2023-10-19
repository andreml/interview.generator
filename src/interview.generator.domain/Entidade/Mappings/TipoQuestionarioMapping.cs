using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace interview.generator.domain.Entidade.Mappings
{
    public class TipoQuestionarioMapping : IEntityTypeConfiguration<TipoQuestionario>
    {
        public void Configure(EntityTypeBuilder<TipoQuestionario> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Descricao)
                .IsRequired()
                .HasColumnType("VARCHAR(100)");
        }
    }
}
