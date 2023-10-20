using interview.generator.domain.Entidade;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace interview.generator.infraestructure.Mapping
{
    public class AreaConhecimentoMapping : IEntityTypeConfiguration<AreaConhecimento>
    {
        public void Configure(EntityTypeBuilder<AreaConhecimento> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Descricao)
                .IsRequired()
                .HasColumnType("VARCHAR(100)");

            builder
                .HasMany(x => x.Perguntas)
                .WithOne(x => x.AreaConhecimento)
                .HasForeignKey(x => x.AreaConhecimentoId);
        }
    }
}
