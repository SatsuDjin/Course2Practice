using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public static class DataSeeder
    {
        
        public static async Task SeedAdminUserAsync(ClinicContext context)
        {
            var adminEmail = "adminepta@gmail.com";
            var adminPassword = "adminpF";

            if (!await context.Users.AnyAsync(u => u.Email == adminEmail))
            {
                var adminUser = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    FullName = "Administrator",
                    Email = adminEmail,
                    PasswordHash = HashPassword(adminPassword),
                    IsAdmin = true
                };

                context.Users.Add(adminUser);
                await context.SaveChangesAsync();
            }
        }
        public static async Task SeedCitiesAsync(ClinicContext context)
        {
            if (!await context.Cities.AnyAsync())
            {
                var cities = new List<City>
                {
                    new City { Name = "Город1" },
                    new City { Name = "Город2" }
                };

                context.Cities.AddRange(cities);
                await context.SaveChangesAsync();
            }
        }
        public static async Task SeedClinicsAsync(ClinicContext context)
        {
            if (!await context.Clinics.AnyAsync())
            {
                var clinics = new List<Clinic>
                {
                    new Clinic
                    {
                        Name = "Клиника1",
                        CityId = 1, 
                        Address = "дом2",
                        Photo = "photo1.jpg",
                        Description = "Класс"
                    },
                    new Clinic
                    {
                        Name = "Клиника2",
                        CityId = 2, 
                        Address = "дом1",
                        Photo = "photo2.jpg",
                        Description = "Норм"
                    }
                };

                context.Clinics.AddRange(clinics);
                await context.SaveChangesAsync();
            }
        }
        public static async Task SeedSpecializationsAsync(ClinicContext context)
        {
            if (!await context.Specializations.AnyAsync())
            {
                var specializations = new List<Specialization>
                {
                    new Specialization { Name = "Хирург" },
                    new Specialization { Name = "Окулист" }
                };

                context.Specializations.AddRange(specializations);
                await context.SaveChangesAsync();
            }
        }
        public static async Task SeedDoctorsAsync(ClinicContext context)
        {
            if (!await context.Doctors.AnyAsync())
            {
                var clinicIds = await context.Clinics.Select(c => c.ClinicId).ToListAsync();
                var specializationIds = await context.Specializations.Select(s => s.SpecializationId).ToListAsync();

                var doctors = new List<Doctor>
                {
                    new Doctor
                    {
                        FullName = "Лорка Д.Д.",
                        BirthDate = DateTime.SpecifyKind(new DateTime(1980, 1, 1), DateTimeKind.Utc),
                        Description = "Он просто хорош",
                        Photo = "photo1.jpg",
                        DoctorClinics = clinicIds.Select(cid => new DoctorClinic { ClinicId = cid }).ToList(),
                        DoctorSpecializations = specializationIds.Select(sid => new DoctorSpecialization { SpecializationId = sid }).ToList()
                    },
                    new Doctor
                    {
                        FullName = "Жолв А.А.",
                        BirthDate = DateTime.SpecifyKind(new DateTime(1983, 2, 6), DateTimeKind.Utc),
                        Description = "Любитель",
                        Photo = "photo2.jpg",
                        DoctorClinics = clinicIds.Select(cid => new DoctorClinic { ClinicId = cid }).ToList(),
                        DoctorSpecializations = specializationIds.Select(sid => new DoctorSpecialization { SpecializationId = sid }).ToList()
                    }
                };

                context.Doctors.AddRange(doctors);
                await context.SaveChangesAsync();
            }
        }



        private static string HashPassword(string password)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return $"{Convert.ToBase64String(salt)}.{hashed}";
        }
    }

}
