using Microsoft.EntityFrameworkCore;



namespace Marchandise_FCE.Models
{
    public class AnnuaireContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<Mcategorie> Categories { get; set; }
        public DbSet<Menvoi> Envois { get; set; }
        public DbSet<Mexpediteur> Expediteurs { get; set; }
        public DbSet<Mgare> Gares { get; set; }
        public DbSet<Mmarchandise> Marchandises { get; set; }

        public DbSet<Mzone> Zones { get; set; }

        public DbSet<Mtarif> Tarifs { get; set; }

        public AnnuaireContext(DbContextOptions<AnnuaireContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            // Récupérer la chaîne de connexion à partir de appsettings.json
            string connectionString = _configuration.GetConnectionString("Gestion_marchandise");

            // Utiliser la méthode UseMySql avec la chaîne de connexion
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("La chaîne de connexion 'Gestion_marchandise' est manquante ou vide.");
            }

            optionsBuilder.UseMySQL(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuration de l'entité Mcategorie
            modelBuilder.Entity<Mcategorie>()
                .Property(m => m.codcat)
                .IsRequired()
                .HasMaxLength(4);
            modelBuilder.Entity<Mcategorie>()
                .Property(m => m.designcat)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Mcategorie>()
                .HasKey(m => m.codcat);

            // Configuration de l'entité Menvoi

            modelBuilder.Entity<Menvoi>()
                .Property(e => e.numenvoi)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Menvoi>()
                .Property(e => e.idexp)
                .IsRequired()
                .HasMaxLength(6);

            modelBuilder.Entity<Menvoi>()
                .Property(e => e.codegare)
                .IsRequired()
                .HasMaxLength(3);
            modelBuilder.Entity<Menvoi>()
                .Property(e => e.refmarch)
                .IsRequired()
                .HasMaxLength(6);
            modelBuilder.Entity<Menvoi>()
                .Property(e => e.date)
                .IsRequired();
            modelBuilder.Entity<Menvoi>()
                .Property(e => e.poids)
                .IsRequired();
            modelBuilder.Entity<Menvoi>()
                .Property(e => e.nbenvoi)
                .IsRequired();
            modelBuilder.Entity<Menvoi>()
                .Property(e => e.type_embal)
                .IsRequired()
                .HasMaxLength(30);

            modelBuilder.Entity<Menvoi>()
               .HasKey(e => e.numenvoi);
            /*modelBuilder.Entity<Menvoi>()
                .HasKey(e => e.idexp);
            modelBuilder.Entity<Menvoi>()
                .HasKey(e => e.codegare);
            modelBuilder.Entity<Menvoi>()
                .HasKey(e => e.refmarch);*/
            modelBuilder.Entity<Menvoi>()
                 .HasOne(e => e.Expediteur)
                 .WithMany(exp => exp.Envois)
                 .HasForeignKey(e => e.idexp);

            modelBuilder.Entity<Menvoi>()
                .HasOne(e => e.Gare)
                .WithMany(g => g.Envois)
                .HasForeignKey(e => e.codegare);

            modelBuilder.Entity<Menvoi>()
                .HasOne(e => e.Marchandise)
                .WithMany(ma => ma.Envois)
                .HasForeignKey(e => e.refmarch);




            // Configuration de l'entité Mexpediteur
            modelBuilder.Entity<Mexpediteur>()
                .Property(exp => exp.idexp)
                .HasMaxLength(6); ;

            modelBuilder.Entity<Mexpediteur>()
                .Property(exp => exp.nomexp)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Mexpediteur>()
                .Property(exp => exp.cin)
                .IsRequired()
                .HasMaxLength(12);

            modelBuilder.Entity<Mexpediteur>()
                .Property(exp => exp.tel)
                .HasMaxLength(10);

            modelBuilder.Entity<Mexpediteur>()
                .HasKey(exp => exp.idexp);

            // Configuration de l'entité Mzone
            modelBuilder.Entity<Mzone>()
                .Property(z => z.codezone)
                .HasMaxLength(3);

            modelBuilder.Entity<Mzone>()
                .Property(z => z.nomzone)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Mzone>()
                .Property(z => z.descriptzone)
                .HasMaxLength(20);

            modelBuilder.Entity<Mzone>()
                .HasKey(z => z.codezone);

            // Configuration de l'entité Mmarchandise
            modelBuilder.Entity<Mmarchandise>()
                .Property(ma => ma.refmarch)
                .HasMaxLength(4);

            modelBuilder.Entity<Mmarchandise>()
                .Property(ma => ma.nature)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Mmarchandise>()
                .Property(ma => ma.codcat)
                .IsRequired()
                .HasMaxLength(4);

            modelBuilder.Entity<Mmarchandise>()
                .HasKey(ma => ma.refmarch);

            modelBuilder.Entity<Mmarchandise>()
                .HasOne(ma => ma.Categorie)
                .WithMany(c => c.Marchandises)
                .HasForeignKey(ma => ma.codcat);






            // Configuration de l'entité Mgare
            modelBuilder.Entity<Mgare>()
                .Property(g => g.codegare)
                .HasMaxLength(3);

            modelBuilder.Entity<Mgare>()
                .Property(g => g.nomgare)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Mgare>()
                .Property(g => g.abrevnom)
                .IsRequired()
                .HasMaxLength(3);

            modelBuilder.Entity<Mgare>()
                .Property(g => g.kilometrage)
                .IsRequired();

            modelBuilder.Entity<Mgare>()
                 .Property(g => g.codezone)
                 .HasMaxLength(3);

            modelBuilder.Entity<Mgare>()
                .HasKey(g => g.codegare);

            modelBuilder.Entity<Mgare>()
                .HasOne(g => g.Zones)
                .WithMany(z => z.Gares)
                .HasForeignKey(g => g.codezone);

            // Configuration de l'entité Mtarif
            modelBuilder.Entity<Mtarif>()
                .Property(t => t.idtarif)
                .HasMaxLength(3);

            modelBuilder.Entity<Mtarif>()
                .Property(g => g.prixkilo)
                .IsRequired();

            modelBuilder.Entity<Mtarif>()
                 .Property(t => t.codcat)
                 .HasMaxLength(4);

            modelBuilder.Entity<Mtarif>()
                 .Property(t => t.codezone)
                 .HasMaxLength(3);

            modelBuilder.Entity<Mtarif>()
                .HasKey(t => t.idtarif);
           /* modelBuilder.Entity<Mtarif>()
                .HasKey(t => t.codcat);
            modelBuilder.Entity<Mtarif>()
                .HasKey(t => t.codezone);*/

            modelBuilder.Entity<Mtarif>()
                .HasOne(t => t.Categorie)
                .WithMany(c => c.Tarifs)
                .HasForeignKey(t => t.codcat);

            modelBuilder.Entity<Mtarif>()
                .HasOne(t => t.Zone)
                .WithMany(z => z.Tarifs)
                .HasForeignKey(t => t.codezone);

        }
        
    }
}
