using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.ApplicationCore.Domain
{
    public class Staff:Passenger
    {
        public string Function { get; set; }
        public DateTime EmploymentDate { get; set; }
        [DataType(DataType.Currency)]
        public double Salary { get; set; }
        public override void PassengerType()
        {
            base.PassengerType();
            Console.WriteLine("i am a staff");
        }
    }
}
