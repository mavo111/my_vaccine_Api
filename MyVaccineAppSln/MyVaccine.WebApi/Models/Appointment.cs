namespace MyVaccine.WebApi.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        // Relación: un paciente tiene muchas citas
        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        // Relación: una vacuna puede estar en muchas citas
        public int VaccineId { get; set; }
        public Vaccine Vaccine { get; set; }
    }
}
