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
    public class PrescriptionServiceTests
    {
        private AppDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task AddPrescriptionAsync_ShouldReturnError_WhenTooManyMedicaments()
        {
            var context = GetDbContext();
            var service = new PrescriptionService(context);

            var request = new CreatePrescriptionRequest
            {
                IdDoctor = 1,
                Medicaments = new List<CreatePrescriptionMedicamentDto>()
            };

            for (int i = 0; i < 11; i++)
                request.Medicaments.Add(new CreatePrescriptionMedicamentDto { IdMedicament = i + 1, Dose = 1, Description = "..." });

            var result = await service.AddPrescriptionAsync(request);

            Assert.Contains("maximum", result);
        }

        [Fact]
        public async Task AddPrescriptionAsync_ShouldReturnError_WhenDueDateBeforeDate()
        {
            var context = GetDbContext();
            var service = new PrescriptionService(context);

            var request = new CreatePrescriptionRequest
            {
                IdDoctor = 1,
                Date = new DateTime(2024, 5, 10),
                DueDate = new DateTime(2024, 5, 1),
                Medicaments = new List<CreatePrescriptionMedicamentDto>()
            };

            var result = await service.AddPrescriptionAsync(request);

            Assert.Contains("DueDate", result);
        }

        [Fact]
        public async Task AddPrescriptionAsync_ShouldCreatePrescription_WhenValidData()
        {
            var context = GetDbContext();

            context.Medicaments.Add(new Medicament { IdMedicament = 1, Name = "Test", Description = "Desc", Type = "Type" });
            context.Doctors.Add(new Doctor { IdDoctor = 1, FirstName = "Test", LastName = "Doc", Email = "doc@test.com" });
            await context.SaveChangesAsync();

            var service = new PrescriptionService(context);

            var request = new CreatePrescriptionRequest
            {
                IdDoctor = 1,
                FirstName = "Anna",
                LastName = "Nowak",
                Birthdate = "2000-02-10",
                Date = new DateTime(2024, 1, 1),
                DueDate = new DateTime(2024, 2, 1),
                Medicaments = new List<CreatePrescriptionMedicamentDto>
                {
                    new CreatePrescriptionMedicamentDto
                    {
                        IdMedicament = 1,
                        Dose = 2,
                        Description = "Test use"
                    }
                }
            };

            var result = await service.AddPrescriptionAsync(request);

            Assert.Equal("Prescription created successfully.", result);
        }
    }
}
