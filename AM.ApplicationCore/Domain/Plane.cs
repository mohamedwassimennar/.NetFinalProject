using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.ApplicationCore.Domain
{
    public enum PlaneType {
        Boeing,
        Airbus
    }
    public class Plane
    {
        public int PlaneId { get; set; }
        public DateTime ManufactureDate { get; set; }
        [Range(0,int.MaxValue)]
        public int Capacity { get; set; }
        public PlaneType PlaneType { get; set; }
        [NotMapped]
        public string Information { get { return PlaneId + " " + ManufactureDate + " " + Capacity; } }

        //objets de navigation
        public virtual ICollection<Flight> Flights { get; set; }
    }
}
