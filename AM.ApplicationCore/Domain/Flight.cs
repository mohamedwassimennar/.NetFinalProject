using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.ApplicationCore.Domain
{
    public class Flight
    {
        public int FlightId { get; set; }
        public string Pilot { get; set; }
        public string Destination { get; set; }
        public string Departure { get; set; }
        public DateTime FlightDate { get; set; }
        public DateTime EffectiveArrival { get; set; }
        public int EstimatedDuration { get; set; }
        public string AirlineLogo { get; set; }
        //objets de navigation
        public virtual Plane Plane { get; set; }
        [ForeignKey("Plane")]
        public int PlaneFK { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
       // public ICollection<Passenger> Passengers { get; set; }
        public override string ToString()
        {
            return "Departure: " + Departure + " Destination: " + Destination
                + " Flight Date: " + FlightDate+" EstimatedDuration "+EstimatedDuration;
        }


    }
}
