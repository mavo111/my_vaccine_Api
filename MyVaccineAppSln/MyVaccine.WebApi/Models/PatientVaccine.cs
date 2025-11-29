namespace MyVaccine.WebApi.Models
{
    public class PatientVaccine
    {
        public int Id { get; set; }

        public int PatientId { get; set; }
        public int VaccineId { get; set; }
        public int DoseId { get; set; }

        public DateTime ApplicationDate { get; set; }

        public Patient Patient { get; set; }
        public Vaccine Vaccine { get; set; }
        public Dose Dose { get; set; }
    }
}
