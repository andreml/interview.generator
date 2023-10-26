using interview.generator.domain.Entidade;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace interview.generator.infraestructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public readonly IConfiguration _configuration;

        public ApplicationDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Alternativa> Alternativa { get; set; }
        public DbSet<AreaConhecimento> AreaConhecimento { get; set; }
        public DbSet<Avaliacao> Avaliacao { get; set; }
        public DbSet<Pergunta> Pergunta { get; set; }
        public DbSet<PerguntaQuestionario> PerguntaQuestionario { get; set; }
        public DbSet<Questionario> Questionario { get; set; }
        public DbSet<RespostaAvaliacao> RespostaAvaliacao { get; set; }
        public DbSet<TipoQuestionario> TipoQuestionario { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Test")
                optionsBuilder.UseInMemoryDatabase("InMemoryEmployeeTest");
            else
                optionsBuilder.UseSqlServer(_configuration.GetValue<string>("ConnectionStrings:DataBase"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
