using System;
using System.ComponentModel.DataAnnotations;

namespace MyVaccine.WebApi.Models
{
    public class Dose
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public int Number { get; set; }
        public DateTime DateApplied { get; set; }

        // 🔹 Relación con Vaccine (esto es lo que te falta)
        public int VaccineId { get; set; }          // clave foránea
        public Vaccine? Vaccine { get; set; }       // navegación
    }
}
