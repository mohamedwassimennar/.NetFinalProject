using AM.ApplicationCore.Domain;
using AM.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Plane = AM.ApplicationCore.Domain.Plane;

namespace AM.ApplicationCore.Services
{
    public class FlightMethods:IFlightMethods
    {
        public Action<Plane> FlightDetailsDel;
        public Func<string, double> DurationAverageDel;
        public FlightMethods()
        {
            FlightDetailsDel = pl=> {
                var req = from f in Flights
                          where f.Plane == pl
                          select new { f.Destination, f.FlightDate };
                foreach (var f in req)
                    Console.WriteLine(f);
            } ;
            DurationAverageDel = dest => {
                var req = from f in Flights
                          where f.Destination == dest
                          select f.EstimatedDuration;
                return req.Average();
            };
        }

       public List<Flight> Flights=new List<Flight>();

        public IEnumerable<IGrouping<string, Flight>> DestinationGroupedFlights()
        {
            var req = from f in Flights
                      group f by f.Destination;

            var req2 = Flights.GroupBy(f => f.Destination);
            
            foreach (var g in req)
            {
                Console.WriteLine(g.Key);
                foreach(var f in g)
                    Console.WriteLine(f);
            }
            return req;
        }

        public double DurationAverage(string destination)
        {
            var req = (from f in Flights
                      where f.Destination == destination
                      select f.EstimatedDuration).Average();
           
            var req2 = Flights.Where(f => f.Destination == destination)
                .Average(f=>f.EstimatedDuration);

            return req;
        }

        public List<DateTime> GetFlightDates(string destination)
        {
            List<DateTime> dates = (from f in Flights
                                   where f.Destination == destination
                                   select f.FlightDate).ToList();

            var req2 = Flights.Where(f => f.Destination == destination)
                .Select(f => f.FlightDate);

            //foreach (Flight f in Flights)
            //{
            //    if (f.Destination == destination)
            //        dates.Add(f.FlightDate);
            //}
            return dates;
        }

        public void GetFlights(string filterType, string filterValue)
        {
            switch(filterType)
            {
                case "Destination":
                    foreach(Flight f in Flights)
                        if(f.Destination.Equals( filterValue))
                            Console.WriteLine(f);
                break;
                case "Departure":
                    foreach (Flight f in Flights)
                        if (f.Departure.Equals(filterValue))
                            Console.WriteLine(f);
                    break;
                case "FlightDate":
                    foreach (Flight f in Flights)
                        if (f.FlightDate.Equals(DateTime.Parse(filterValue)))
                            Console.WriteLine(f);
                    break;
                case "EstimatedDuration":
                    foreach (Flight f in Flights)
                        if (f.EstimatedDuration.Equals(int.Parse(filterValue)))
                            Console.WriteLine(f);
                    break;
            }
        }

        public IEnumerable<Flight> OrderedDurationFlights()
        {
            var req=from f in Flights
                    orderby f.EstimatedDuration descending
                    select f;
            var req2 = Flights.OrderByDescending(f => f.EstimatedDuration);
            return req;
        }

        public int ProgrammedFlightNumber(DateTime startDate)
        {
            var req=from f in Flights
                    where DateTime.Compare(startDate, f.FlightDate)<0 
                    && (f.FlightDate-startDate).TotalDays<7
                    select f;
            var req2 = Flights.Where(f => DateTime.Compare(startDate, f.FlightDate) < 0
                    && (f.FlightDate - startDate).TotalDays < 7);
            return req.Count();
        }

        //public IEnumerable<Traveller> SeniorTravellers(Flight flight)
        //{
        //    var req = from t in flight.Passengers.OfType<Traveller>()
        //              orderby t.BirthDate
        //              select t;
        //    var req2 = flight.Passengers.OfType<Traveller>().OrderBy(t => t.BirthDate);
        //    return req.Take(3);
        //    //pour ignorer les 3 premiers
        //    //Skip(3)
        //}

        public void ShowFlightDetails(Plane plane)
        {
           var req= from f in Flights
                             where f.Plane==plane
                             select new { f.Destination, f.FlightDate };
            var req2 = Flights.Where(f => f.Plane == plane)
                .Select(f => new { f.Destination, f.FlightDate });
            foreach(var f in req)
                Console.WriteLine(f);
        }
    }
}
