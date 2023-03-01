using Microsoft.EntityFrameworkCore;
using mvp_api.Models;


namespace mpv_api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts)
            : base(opts)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UC>()
                .HasOne(uc => uc.Consumidor)
                .WithMany(consumidor => consumidor.UCs)
                .HasForeignKey(uc => uc.Cod_Consumidor);




            builder.Entity<Fatura>()
                .HasOne(fatura => fatura.UC)
                .WithMany(uc => uc.Faturas)
                .HasForeignKey(fatura => fatura.Cod_UC);


            builder.Entity<Fatura>()
                .HasOne(fatura => fatura.Consumidor)
                .WithMany(consumidor => consumidor.Faturas)
                .HasForeignKey(fatura => fatura.Cod_Consumidor);

            builder.Entity<ItemFatura>()
                .HasOne(itemFatura => itemFatura.Fatura)
                .WithMany(fatura => fatura.ItemFatura)
                .HasForeignKey(itemFatura => itemFatura.Cod_Fatura)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ItemFatura>()
                .HasOne(itemFatura => itemFatura.Item)
                .WithMany(item => item.ItemFaturas)
                .HasForeignKey(itemFatura => itemFatura.Cod_Item)
                .OnDelete(DeleteBehavior.Restrict); // retira o pdrão cascate do delete


            builder.Entity<Consumidor>().HasIndex(p => p.Doc_Consumidor).IsUnique();
            builder.Entity<Consumidor>().HasIndex(p => p.Email).IsUnique();

        }

        public DbSet<Consumidor> Consumidores { get; set; }
        public DbSet<UC> UCs { get; set; }
        public DbSet<Fatura> Faturas { get; set; }
        public DbSet<Item> Itens { get; set; }
        public DbSet<ItemFatura> ItensFatura { get; set; }


    }
}
