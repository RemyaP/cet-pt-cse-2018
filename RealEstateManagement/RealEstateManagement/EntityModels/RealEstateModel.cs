namespace RealEstateManagement.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class RealEstateModel : DbContext
    {
        public RealEstateModel()
            : base( "name=RealEstateModel" )
        {
        }

        public virtual DbSet<bid> bids { get; set; }
        public virtual DbSet<bid_price> bid_price { get; set; }
        public virtual DbSet<buyer> buyers { get; set; }
        public virtual DbSet<category> categories { get; set; }
        public virtual DbSet<landmark> landmarks { get; set; }
        public virtual DbSet<landmarktype> landmarktypes { get; set; }
        public virtual DbSet<min_price> min_price { get; set; }
        public virtual DbSet<property> properties { get; set; }
        public virtual DbSet<seller> sellers { get; set; }
        public virtual DbSet<status> status { get; set; }
        public virtual DbSet<property_status> property_status { get; set; }
        public virtual DbSet<user> users { get; set; }
        public virtual DbSet<image> images { get; set; }

        protected override void OnModelCreating( DbModelBuilder modelBuilder )
        {
            modelBuilder.Entity<bid>()
                .HasOptional( e => e.bid_price )
                .WithRequired( e => e.bid );

            modelBuilder.Entity<buyer>()
                .HasMany( e => e.bids )
                .WithRequired( e => e.buyer )
                .WillCascadeOnDelete( false );

            modelBuilder.Entity<category>()
                .Property( e => e.type )
                .IsUnicode( false );

            modelBuilder.Entity<category>()
                .HasMany( e => e.properties )
                .WithRequired( e => e.category1 )
                .HasForeignKey( e => e.category )
                .WillCascadeOnDelete( false );

            modelBuilder.Entity<property_status>()
                .Property( e => e.status1 )
                .IsUnicode( false );

            modelBuilder.Entity<property_status>()
                .HasMany( e => e.properties )
                .WithRequired( e => e.status1 )
                .HasForeignKey( e => e.status )
                .WillCascadeOnDelete( false );

            modelBuilder.Entity<landmark>()
                .Property( e => e.name )
                .IsUnicode( false );

            modelBuilder.Entity<landmarktype>()
                .Property( e => e.type_name )
                .IsUnicode( false );

            modelBuilder.Entity<landmarktype>()
                .HasMany( e => e.landmarks )
                .WithRequired( e => e.landmark_type )
                .HasForeignKey( e => e.landmarktype )
                .WillCascadeOnDelete( false );

            modelBuilder.Entity<property>()
                .HasMany( e => e.bids )
                .WithRequired( e => e.property )
                .WillCascadeOnDelete( false );

            modelBuilder.Entity<property>()
                .HasOptional( e => e.min_price )
                .WithRequired( e => e.property );

            modelBuilder.Entity<property>()
                .HasMany( e => e.images )
                .WithRequired( e => e.property )
                .WillCascadeOnDelete( false );

            modelBuilder.Entity<property>()
                .HasMany( e => e.landmarks )
                .WithMany( e => e.properties )
                .Map( m => m.ToTable( "property_has_landmark", "real_estate" ).MapLeftKey( "property_id" ).MapRightKey( "landmark_id" ) );

            modelBuilder.Entity<seller>()
                .HasMany( e => e.properties )
                .WithRequired( e => e.seller )
                .WillCascadeOnDelete( false );

            modelBuilder.Entity<status>()
                .Property( e => e.status1 )
                .IsUnicode( false );

            modelBuilder.Entity<status>()
                .HasMany( e => e.users )
                .WithRequired( e => e.status )
                .WillCascadeOnDelete( false );

            modelBuilder.Entity<user>()
                .Property( e => e.user_name )
                .IsUnicode( false );

            modelBuilder.Entity<user>()
                .Property( e => e.password )
                .IsUnicode( false );

            modelBuilder.Entity<user>()
                .Property( e => e.first_name )
                .IsUnicode( false );

            modelBuilder.Entity<user>()
                .Property( e => e.last_name )
                .IsUnicode( false );

            modelBuilder.Entity<user>()
                .Property( e => e.address )
                .IsUnicode( false );

            modelBuilder.Entity<user>()
                .Property( e => e.pan_no )
                .IsUnicode( false );

            modelBuilder.Entity<user>()
                .Property( e => e.mob_no )
                .IsUnicode( false );

            modelBuilder.Entity<user>()
                .Property( e => e.email )
                .IsUnicode( false );

            modelBuilder.Entity<user>()
                .HasMany( e => e.buyers )
                .WithRequired( e => e.user )
                .WillCascadeOnDelete( false );

            modelBuilder.Entity<user>()
                .HasMany( e => e.sellers )
                .WithRequired( e => e.user )
                .WillCascadeOnDelete( false );
        }
    }
}
