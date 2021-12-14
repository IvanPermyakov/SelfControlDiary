using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfControlDiary.Models
{
    public class DiaryContext : DbContext
    {
        public DbSet<People> Peoples { get; set; }
        public DbSet<Activ> Activs { get; set; }
        public DbSet<Sex> Sexs { get; set; }
        public DbSet<FoodDiary> FoodDiaries {get; set;}
        public DbSet<Food> Foods { get; set; }
        public DbSet<PhysicalDiary> PhysicalDiaries { get; set; }
        public DbSet<Physical> Physicals { get; set; }
        public DbSet<SleepDiary> SleepDiaries { get; set; }
        public DbSet<User> Users { get; set; }

        public DiaryContext(DbContextOptions<DiaryContext> options):base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
