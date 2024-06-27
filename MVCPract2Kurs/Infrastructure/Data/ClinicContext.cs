using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.Data
{
    public class ClinicContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }

        public ClinicContext(DbContextOptions<ClinicContext> options) : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
    }
}
