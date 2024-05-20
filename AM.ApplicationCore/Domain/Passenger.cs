using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.ApplicationCore.Domain
{
    public class Passenger
    {
        [Key]
        [StringLength(7)]
        public string PassportNumber { get; set; }

        public FullName FullName { get; set; }

        [Display(Name ="Date of birth")]
        [DataType(DataType.Date)] 
        public DateTime BirthDate { get; set; }

        [DataType(DataType.EmailAddress)]
        public string EmailAdress { get; set; }
        [RegularExpression("^[0,9]{8}$")]
        public string PhoneNumber { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }

        // public ICollection<Flight> Flights { get; set; }
        //public Boolean CheckProfile(string prenom,string nom)
        // {
        //     return (prenom == FirstName && nom == LastName);

        // }
        public Boolean CheckProfile(string prenom, string nom,string email=null)
        {
            if(email!=null)  
            return (prenom == FullName.FirstName && nom == FullName.LastName && email==EmailAdress);
            else
            return (prenom == FullName.FirstName && nom == FullName.LastName);

            }

        public virtual void PassengerType()
        {
            Console.WriteLine("I am a passenger"); 
        }
        
    }
}
