using System.Collections.Generic;

namespace WebApplication10.DTOs
{
    public class PatientDto
    {
        public int IdPatient { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Birthdate { get; set; } = string.Empty;

        public List<PrescriptionDto> Prescriptions { get; set; } = new();
    }
}