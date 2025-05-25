using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication10.Data;
using WebApplication10.DTOs;
using WebApplication10.Models;
using WebApplication10.Services;
using Xunit;

namespace WebApplication10.Tests
{
    public class PatientServiceTests
    {
        private AppDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task GetPatientAsync_ShouldReturnNull_IfPatientNotFound()
        {
            var context = GetDbContext();
            var service = new PatientService(context);

            var result = await service.GetPatientAsync(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetPatientAsync_ShouldReturnPatientDto_IfPatientExists()
        {
            var context = GetDbContext();

            var patient = new Patient
            {
                IdPatient = 1,
                FirstName = "John",
                LastName = "Doe",
                Birthdate = "2000-01-01",
                Prescriptions = new List<Prescription>
                {
                    new Prescription
                    {
                        IdPrescription = 1,
                        Date = new DateTime(2024, 1, 1),
                        DueDate = new DateTime(2024, 2, 1),
                        IdDoctor = 1,
                        Doctor = new Doctor
                        {
                            IdDoctor = 1,
                            FirstName = "Greg",
                            LastName = "House",
                            Email = "house@clinic.com"
                        },
                        PrescriptionMedicaments = new List<PrescriptionMedicament>
                        {
                            new PrescriptionMedicament
                            {
                                IdMedicament = 1,
                                Medicament = new Medicament
                                {
                                    IdMedicament = 1,
                                    Name = "Ibuprofen",
                                    Description = "Painkiller",
                                    Type = "Tablet"
                                },
                                Dose = 2,
                                Description = "Take after meal"
                            }
                        }
                    }
                }
            };

            context.Patients.Add(patient);
            await context.SaveChangesAsync();

            var service = new PatientService(context);

            var result = await service.GetPatientAsync(1);

            Assert.NotNull(result);
            Assert.Equal("John", result.FirstName);
            Assert.Single(result.Prescriptions);
            Assert.Equal("Ibuprofen", result.Prescriptions[0].Medicaments[0].Name);
            Assert.Equal("Greg", result.Prescriptions[0].Doctor.FirstName);
        }
    }
}
