using CollectionManager.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CollectionManager.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Estado> Estados { get; set; }
    public DbSet<Franquia> Franquias { get; set; }
    public DbSet<Marca> Marcas { get; set; }
    public DbSet<Plataforma> Plataformas { get; set; }
    public DbSet<Editora> Editoras { get; set; }
    public DbSet<Status> Status { get; set; }
    public DbSet<Item> Itens { get; set; }
    public DbSet<Controle> Controles { get; set; }
    public DbSet<Jogo> Jogos { get; set; }
    public DbSet<Videogame> Videogames { get; set; }
    public DbSet<Leitura> Leituras { get; set; }
    public DbSet<ColecaoLeitura> ColecaoLeitura { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Controle>()
            .HasKey(c => c.ItemId);

        modelBuilder.Entity<Controle>()
            .HasOne(c => c.Item)
            .WithOne()
            .HasForeignKey<Controle>(c => c.ItemId);

        modelBuilder.Entity<Jogo>()
            .HasKey(j => j.ItemId);

        modelBuilder.Entity<Jogo>()
            .HasOne(j => j.Item)
            .WithOne()
            .HasForeignKey<Jogo>(j => j.ItemId);

        modelBuilder.Entity<Videogame>()
            .HasKey(i => i.ItemId);

        modelBuilder.Entity<Videogame>()
            .HasOne(i => i.Item)
            .WithOne()
            .HasForeignKey<Videogame>(i => i.ItemId);

        modelBuilder.Entity<Leitura>()
            .HasKey(l => l.Id);

        modelBuilder.Entity<Leitura>()
            .HasOne(l => l.ColecaoLeitura)
            .WithMany()
            .HasForeignKey(l => l.ColecaoLeituraId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    public override async Task<int> SaveChangesAsync(
    CancellationToken cancellationToken = default)
    {
        var entradas = ChangeTracker
            .Entries<EntidadeBase>();

        foreach (var entrada in entradas)
        {
            if (entrada.State == EntityState.Added)
            {
                entrada.Entity.DataCadastro = DateTime.UtcNow;
                entrada.Entity.DataAtualizacao = DateTime.UtcNow;
            }

            if (entrada.State == EntityState.Modified)
            {
                entrada.Entity.DataAtualizacao = DateTime.UtcNow;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}