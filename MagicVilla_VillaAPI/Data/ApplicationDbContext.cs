using MagicVilla_VillaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }
        public DbSet<Villa> Villas { get; set; }   
        public DbSet<VillaNumber> VillaNumbers { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa
                {
                    Id = 1,
                    Name= "Royal villa",
                    Details="this villa is very beautiful villa with beautiful surrounding and sunrise view",
                    ImageUrl= "https://stock.adobe.com/in/images/tropical-villa-at-night/309464116",
                    Occupancy=4,
                    Rate=20000,
                    Sqft=2100,
                    Amenity=1,
                    CreatedDate= DateTime.Now
                },
                new Villa
                {
                    Id = 2,
                    Name = "vivek villa",
                    Details = "this villa is very beautiful villa with beautiful surrounding and sunrise view",
                    ImageUrl = "https://stock.adobe.com/in/images/tropical-villa-at-night/309464116",
                    Occupancy = 4,
                    Rate = 20000,
                    Sqft = 2100,
                    Amenity = 1,
                    CreatedDate = DateTime.Now
                },
                new Villa
                {
                    Id = 3,
                    Name = "Royal hill villa",
                    Details = "this villa is very beautiful villa with beautiful surrounding and sunrise view",
                    ImageUrl = "https://stock.adobe.com/in/images/tropical-villa-at-night/309464116",
                    Occupancy = 4,
                    Rate = 20000,
                    Sqft = 2100,
                    Amenity = 1,
                    CreatedDate = DateTime.Now
                },
                new Villa
                {
                    Id = 4,
                    Name = "villa in hills",
                    Details = "this villa is very beautiful villa with beautiful surrounding and sunrise view",
                    ImageUrl = "https://stock.adobe.com/in/images/tropical-villa-at-night/309464116",
                    Occupancy = 4,
                    Rate = 20000,
                    Sqft = 2100,
                    Amenity = 1,
                    CreatedDate = DateTime.Now
                },
                new Villa
                {
                    Id = 5,
                    Name = "villa rosemary",
                    Details = "this villa is very beautiful villa with beautiful surrounding and sunrise view",
                    ImageUrl = "https://stock.adobe.com/in/images/tropical-villa-at-night/309464116",
                    Occupancy = 4,
                    Rate = 20000,
                    Sqft = 2100,
                    Amenity = 1,
                    CreatedDate = DateTime.Now
                }
                );
        }
    }
}
