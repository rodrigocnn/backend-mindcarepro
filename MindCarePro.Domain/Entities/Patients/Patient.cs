using MindCarePro.Domain.Entities.BaseEntitie;
using MindCarePro.Domain.Entities.Users;
using MindCarePro.Domain.Utils;

namespace MindCarePro.Domain.Entities.Patients
{
    public class Patient : BaseEntity
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }
        public string Phone { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string Notes { get; private set; }
        public string Rg { get; private set; }
        public string Gender { get; private set; }
    
        public Guid? UserId { get; private set; }
        public User? User { get; private set; }

        // Construtor usado no código
        public Patient(
            string name,
            string email,
            string cpf,
            string phone,
            DateTime birthDate,
            string notes,
            string rg,
            string gender,
            Guid? userId = null // opcional para não quebrar EF Core
        )
        {
            Name = name;
            Email = email;
            Cpf = cpf;
            Phone = phone;
            BirthDate = birthDate;
            Notes = notes;
            Rg = rg;
            Gender = gender;
            UserId = userId;
        }

        // Construtor sem parâmetros para EF Core
        protected Patient() { }
    }

}