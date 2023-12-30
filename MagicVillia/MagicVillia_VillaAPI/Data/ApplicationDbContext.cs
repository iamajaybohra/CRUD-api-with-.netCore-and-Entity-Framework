using MagicVillia_VillaAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace MagicVillia_VillaAPI.Data
{
    public class ApplicationDbContext: DbContext
    {
        // configuration 
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options) { 
        
        }
        
        
        //The property name is gets created as the table name in sql server
        public DbSet<Villa> Villas { get; set; }

        // seeding the database with default or sample data 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa()
                {
                    Id = 1,
                    Name = "Royal Palace",
                    Details = "Based in Jaipur",
                    ImageUrl = "https://unsplash.com/photos/tranquil-morning-at-famous-indian-tourist-landmark-jal-mahal-water-palace-at-sunrise-in-jaipur-ducks-and-birds-around-enjoy-the-serene-morning-jaipur-rajasthan-india-hx0IZ3Inw-Q",
                    Occupancy = 5,
                    Rate = 5,
                    Sqft = 5,
                    Amenity = "",
                    CreatedDate = DateTime.Now,
                }
            );
        }
    }
}
 