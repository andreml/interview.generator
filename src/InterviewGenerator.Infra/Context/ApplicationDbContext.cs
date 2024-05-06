using InterviewGenerator.Domain.Entidade;
using Microsoft.EntityFrameworkCore;

namespace InterviewGenerator.Infra.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Alternativa> Alternativa { get; set; }
    public DbSet<AreaConhecimento> AreaConhecimento { get; set; }
    public DbSet<Avaliacao> Avaliacao { get; set; }
    public DbSet<Pergunta> Pergunta { get; set; }
    public DbSet<Questionario> Questionario { get; set; }
    public DbSet<RespostaAvaliacao> RespostaAvaliacao { get; set; }
    public DbSet<Usuario> Usuario { get; set; }
    public DbSet<ControleImportacaoPerguntas> ControleImportacao { get; set; }
    public DbSet<LinhaArquivo> LinhasArquivo { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
