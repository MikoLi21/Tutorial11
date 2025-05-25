using System;
using System.Collections.Generic;

namespace WebApplication10.DTOs
{
    public class PrescriptionDto
    {
        public int IdPrescription { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }

        public DoctorSimpleDto Doctor { get; set; } = new();
        public List<MedicamentDto> Medicaments { get; set; } = new();
    }

    public class DoctorSimpleDto
    {
        public int IdDoctor { get; set; }
        public string FirstName { get; set; } = string.Empty;
    }
}