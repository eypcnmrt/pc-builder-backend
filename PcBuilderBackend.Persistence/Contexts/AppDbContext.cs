using Microsoft.EntityFrameworkCore;
using PcBuilderBackend.Domain.Entities;

namespace PcBuilderBackend.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Processor> Processors => Set<Processor>();
        public DbSet<Motherboard> Motherboards => Set<Motherboard>();
        public DbSet<Gpu> Gpus => Set<Gpu>();
        public DbSet<Ram> Rams => Set<Ram>();
        public DbSet<Storage> Storages => Set<Storage>();
        public DbSet<Psu> Psus => Set<Psu>();
        public DbSet<PcCase> PcCases => Set<PcCase>();
        public DbSet<Cooler> Coolers => Set<Cooler>();
        public DbSet<Build> Builds => Set<Build>();
        public DbSet<BuildActivity> BuildActivities => Set<BuildActivity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Username).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Email).HasMaxLength(200).IsRequired();
                entity.Property(e => e.PasswordHash).IsRequired();
                entity.HasIndex(e => e.Email).IsUnique();
            });

            modelBuilder.Entity<Processor>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Brand).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Model).HasMaxLength(200).IsRequired();
                entity.Property(e => e.Socket).HasMaxLength(50).IsRequired();
            });

            modelBuilder.Entity<Motherboard>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Brand).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Model).HasMaxLength(200).IsRequired();
                entity.Property(e => e.Socket).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Chipset).HasMaxLength(100).IsRequired();
                entity.Property(e => e.FormFactor).HasMaxLength(20).IsRequired();
                entity.Property(e => e.SupportedRamType).HasMaxLength(10).IsRequired();
            });

            modelBuilder.Entity<Gpu>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Brand).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Model).HasMaxLength(200).IsRequired();
                entity.Property(e => e.MemoryType).HasMaxLength(20).IsRequired();
            });

            modelBuilder.Entity<Ram>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Brand).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Model).HasMaxLength(200).IsRequired();
                entity.Property(e => e.Type).HasMaxLength(10).IsRequired();
            });

            modelBuilder.Entity<Storage>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Brand).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Model).HasMaxLength(200).IsRequired();
                entity.Property(e => e.Type).HasMaxLength(10).IsRequired();
                entity.Property(e => e.Interface).HasMaxLength(50).IsRequired();
            });

            modelBuilder.Entity<Psu>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Brand).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Model).HasMaxLength(200).IsRequired();
                entity.Property(e => e.EfficiencyRating).HasMaxLength(20).IsRequired();
                entity.Property(e => e.Modular).HasMaxLength(10).IsRequired();
            });

            modelBuilder.Entity<PcCase>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Brand).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Model).HasMaxLength(200).IsRequired();
                entity.Property(e => e.FormFactor).HasMaxLength(10).IsRequired();
            });

            modelBuilder.Entity<Cooler>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Brand).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Model).HasMaxLength(200).IsRequired();
                entity.Property(e => e.Type).HasMaxLength(10).IsRequired();
                entity.Property(e => e.CompatibleSockets).HasMaxLength(200).IsRequired();
            });

            modelBuilder.Entity<Build>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Name).HasMaxLength(200).IsRequired();
                entity.HasOne(e => e.User).WithMany().HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Processor).WithMany().HasForeignKey(e => e.ProcessorId).OnDelete(DeleteBehavior.SetNull);
                entity.HasOne(e => e.Motherboard).WithMany().HasForeignKey(e => e.MotherboardId).OnDelete(DeleteBehavior.SetNull);
                entity.HasOne(e => e.Gpu).WithMany().HasForeignKey(e => e.GpuId).OnDelete(DeleteBehavior.SetNull);
                entity.HasOne(e => e.Ram).WithMany().HasForeignKey(e => e.RamId).OnDelete(DeleteBehavior.SetNull);
                entity.HasOne(e => e.Storage).WithMany().HasForeignKey(e => e.StorageId).OnDelete(DeleteBehavior.SetNull);
                entity.HasOne(e => e.Psu).WithMany().HasForeignKey(e => e.PsuId).OnDelete(DeleteBehavior.SetNull);
                entity.HasOne(e => e.PcCase).WithMany().HasForeignKey(e => e.PcCaseId).OnDelete(DeleteBehavior.SetNull);
                entity.HasOne(e => e.Cooler).WithMany().HasForeignKey(e => e.CoolerId).OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<BuildActivity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.ComponentType).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Action).HasMaxLength(20).IsRequired();
                entity.Property(e => e.Description).HasMaxLength(500).IsRequired();
                entity.HasOne(e => e.Build).WithMany(b => b.Activities).HasForeignKey(e => e.BuildId).OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
