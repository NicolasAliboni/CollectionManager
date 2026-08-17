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
            .HasKey(l => l.ItemId);

        modelBuilder.Entity<Leitura>()
            .HasOne(l => l.Item)
            .WithOne()
            .HasForeignKey<Leitura>(l => l.ItemId);
    }
}