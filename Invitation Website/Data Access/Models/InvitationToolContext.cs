namespace Data_Access.Models
{
    public partial class InvitationToolContext : DbContext
    {
        public InvitationToolContext()
        {
        }

        public InvitationToolContext(DbContextOptions<InvitationToolContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Person> People { get; set; } = null!;
        public virtual DbSet<Request> Requests { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;database=InvitationTool;user=root;password=Test1234", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.24-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8_general_ci")
                .HasCharSet("utf8");

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("person");

                entity.Property(e => e.PersonId)
                    .HasColumnType("int(11)")
                    .HasColumnName("person_id");

                entity.Property(e => e.PersonName)
                    .HasMaxLength(45)
                    .HasColumnName("person_name");

                entity.Property(e => e.PersonNameHashed)
                    .HasMaxLength(45)
                    .HasColumnName("person_name_hashed");

                entity.Property(e => e.PersonRequestState)
                    .HasColumnType("int(11)")
                    .HasColumnName("person_request_state");

                entity.HasMany(d => d.RequestRequests)
                    .WithMany(p => p.PersonPeople)
                    .UsingEntity<Dictionary<string, object>>(
                        "PersonHasRequest",
                        l => l.HasOne<Request>().WithMany().HasForeignKey("RequestRequestId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("fk_Person_has_Request_Request1"),
                        r => r.HasOne<Person>().WithMany().HasForeignKey("PersonPersonId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("fk_Person_has_Request_Person"),
                        j =>
                        {
                            j.HasKey("PersonPersonId", "RequestRequestId").HasName("PRIMARY").HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                            j.ToTable("person_has_request");

                            j.HasIndex(new[] { "PersonPersonId" }, "fk_Person_has_Request_Person_idx");

                            j.HasIndex(new[] { "RequestRequestId" }, "fk_Person_has_Request_Request1_idx");

                            j.IndexerProperty<int>("PersonPersonId").HasColumnType("int(11)").HasColumnName("Person_person_id");

                            j.IndexerProperty<int>("RequestRequestId").HasColumnType("int(11)").HasColumnName("Request_request_id");
                        });
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.ToTable("request");

                entity.Property(e => e.RequestId)
                    .HasColumnType("int(11)")
                    .HasColumnName("request_id");

                entity.Property(e => e.RequestAmount)
                    .HasColumnType("int(11)")
                    .HasColumnName("request_amount")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.RequestArticle)
                    .HasMaxLength(45)
                    .HasColumnName("request_article");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
