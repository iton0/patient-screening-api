using Microsoft.EntityFrameworkCore;

namespace PatientApi.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }

    public class PatientDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }

    class PatientDb(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Patient> Patients { get; set; } = null!;
    }

    public static class PatientMapper
    {
        public static PatientDto ToDto(this Patient patient)
        {
            return new PatientDto
            {
                Id = patient.Id,
                Name = patient.Name,
            };
        }
    }
}
