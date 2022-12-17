using Microsoft.EntityFrameworkCore;

namespace BlogSitesi.Models
{
    public class DatabaseContext  : DbContext
    // "DbContext" entity frameworkcore'nin bir class'ıdır. Biz buradan miras alırız artık "DatabaseContext" adında oluşturduğumuz class için 
    {
        public DbSet<Category> Categories { get; set; }//Veritabanımızdaki tabloyu simgeler
        public DbSet<Post> Posts{ get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // OnConfiguring metodu entity framework core içerisinden gelir ve veritabanı ayarlarını yapabilmemizi sağlar.
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB; database=BlogSitesi; integrated security=true"); // Burada uygulamamızda sql server kullanacağımızı entity framework core a belirttik. UseSqlServer metoduna () içerisine connection string ile veritabanı bilgilerimizi parametre olarak gönderebiliyoruz.
            // "integrated security=true" şifre sormaması içim
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // aşağıdaki metot uygulama ilk defa çalıştığında veritabanı oluştuktan sonra admin paneline giriş yapabilmek için veritabanındaki users tablosuna 1 tane kayıt ekler.
            // Bu kayıtı eklemezsek veritabanına kendimiz eklememiz lazım
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id= 1,
                    CreateDate= DateTime.Now,
                    Email="admin@blogsitesi.com",
                    IsActive= true,
                    IsAdmin= true,
                    Name="admin",
                    SurName="admin",
                    Password="123"
                }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
