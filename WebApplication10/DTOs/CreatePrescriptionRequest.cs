using System;
using System.Collections.Generic;

namespace WebApplication10.DTOs
{
    public class CreatePrescriptionRequest
    {
        
        public int IdDoctor { get; set; }
        public int? IdPatient { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Birthdate { get; set; }

        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }

        public List<CreatePrescriptionMedicamentDto> Medicaments { get; set; } = new();
    }
}