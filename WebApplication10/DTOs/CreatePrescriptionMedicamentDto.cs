namespace WebApplication10.DTOs
{
    public class CreatePrescriptionMedicamentDto
    {
        public int IdMedicament { get; set; }
        public int Dose { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}