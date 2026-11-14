using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using prog_ing_azrv.Server.Models;
using prog_ing_azrv.Server.Models.Enums;

namespace prog_ing_azrv.Server.Data
{
    public class AzrvContext:IdentityDbContext<User>
    {

        public AzrvContext(DbContextOptions<AzrvContext>options):base(options){}

        public DbSet<Skill> Skills{ get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //////////////////////// DB CONFIGURATION ////////////////////////
            builder.Entity<Skill>()
                .HasOne(s => s.SkillCategory)
                .WithMany(sc => sc.Skills)
                .HasForeignKey(s => s.SkillCategoryID);


            //////////////////////// ENUM INSERTION ////////////////////////
            builder.Entity<FriendshipStatus>()
                .HasData(
                    new FriendshipStatus() { Id = 1, Name = "Pending" },
                    new FriendshipStatus() { Id = 2, Name = "Accepted" },
                    new FriendshipStatus() { Id = 3, Name = "Blocked" }
                );

            builder.Entity<WantedStatus>()
                .HasData(
                new WantedStatus() { Id=1, Name="Want"},
                new WantedStatus() { Id=2, Name="Offer"}
                );

            builder.Entity<SkillLevel>()
                .HasData(
                new SkillLevel() { Id=1, Name="Beginner"},
                new SkillLevel() { Id=2, Name="Intermediate"},
                new SkillLevel() { Id=3, Name="Pro"}
                );
        }
    }
}
