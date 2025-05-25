using WebApplication10.Models;
using WebApplication10.DTOs;
using WebApplication10.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApplication10.Services
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly AppDbContext _context;

        public PrescriptionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> AddPrescriptionAsync(CreatePrescriptionRequest request)
        {
            
            if (request.Medicaments.Count > 10)
            {
                return "A prescription can include a maximum of 10 medications.";
            }

            
            if (request.DueDate < request.Date)
            {
                return "DueDate must be greater than or equal to Date.";
            }

            
            var medicamentIds = request.Medicaments.Select(m => m.IdMedicament).ToList();
            var existingMedicaments = await _context.Medicaments
                .Where(m => medicamentIds.Contains(m.IdMedicament))
                .Select(m => m.IdMedicament)
                .ToListAsync();

            var missing = medicamentIds.Except(existingMedicaments).ToList();
            if (missing.Any())
            {
                return $"Some medicaments were not found: {string.Join(", ", missing)}";
            }

            
            Patient? patient;
            if (request.IdPatient.HasValue)
            {
                patient = await _context.Patients.FindAsync(request.IdPatient.Value);
                if (patient == null)
                    return $"Patient with Id {request.IdPatient.Value} not found.";
            }
            else
            {
                
                if (string.IsNullOrWhiteSpace(request.FirstName) || string.IsNullOrWhiteSpace(request.LastName) || string.IsNullOrWhiteSpace(request.Birthdate))
                {
                    return "New patient requires FirstName, LastName and Birthdate.";
                }

                patient = new Patient
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Birthdate = request.Birthdate
                };
                _context.Patients.Add(patient);
                await _context.SaveChangesAsync();
            }

            
            var doctor = await _context.Doctors.FindAsync(request.IdDoctor);
            if (doctor == null)
                return $"Doctor with Id {request.IdDoctor} not found.";

            
            var prescription = new Prescription
            {
                Date = request.Date,
                DueDate = request.DueDate,
                Patient = patient,
                Doctor = doctor,
                PrescriptionMedicaments = request.Medicaments.Select(m => new PrescriptionMedicament
                {
                    IdMedicament = m.IdMedicament,
                    Dose = m.Dose,
                    Description = m.Description
                }).ToList()
            };

            _context.Prescriptions.Add(prescription);
            await _context.SaveChangesAsync();

            return "Prescription created successfully.";
        }
    }
}
