using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication10.Models
{
    public class PrescriptionMedicament
    {
        [ForeignKey("Medicament")]
        public int IdMedicament { get; set; }
        public Medicament Medicament { get; set; }

        [ForeignKey("Prescription")]
        public int IdPrescription { get; set; }
        public Prescription Prescription { get; set; }

        [Required]
        public int Dose { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }
    }
}