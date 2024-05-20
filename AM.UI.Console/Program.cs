// See https://aka.ms/new-console-template for more information

using AM.ApplicationCore.Domain;
using AM.ApplicationCore.Interfaces;
using AM.ApplicationCore.Services;
using AM.Infrastructure;

Console.WriteLine("Hello, World!");
//création d'un objet à travers le constructeur par défaut
Plane p = new Plane();
p.Capacity = 100;
Console.WriteLine(p.Capacity);
p.ManufactureDate = DateTime.Now;
p.PlaneType = PlaneType.Boeing;

//création d'un objet à travers l'initialiseur d'objets
Flight f = new Flight() { Departure = "Tunis" ,
                          Destination = "Paris",
                          EstimatedDuration=120,
                          FlightDate= new DateTime(2024, 10, 3, 12, 1, 1),
                          EffectiveArrival= new DateTime(2024, 10, 3, 13, 1, 1)
                        };
Console.WriteLine(f);
Console.WriteLine("*********CheckProfile**********");
Passenger p1 = new Passenger() { FullName=new FullName { FirstName = "amina", LastName = "aoun" }
                                ,EmailAdress="amina.aoun@esprit.tn"
};
Console.WriteLine(p1.CheckProfile("Amina", "Aoun"));
Console.WriteLine(p1.CheckProfile("Amina", "Aoun","abc"));
Console.WriteLine("*********PassengerType**********");
Staff s1 = new Staff();
Traveller t1 = new Traveller();
p1.PassengerType();
t1.PassengerType();
s1.PassengerType();
Console.WriteLine("*********GetFlightDates*******");
FlightMethods fm = new FlightMethods();
fm.Flights = TestData.listFlights;
foreach(DateTime d in fm.GetFlightDates("Paris"))
    Console.WriteLine(d);
Console.WriteLine("*********GetFlights*******");
fm.GetFlights("Destination", "Madrid");
fm.GetFlights("EstimatedDuration", "105");
Console.WriteLine("********ShowFlightDetails*****");
fm.ShowFlightDetails(TestData.BoingPlane);
fm.FlightDetailsDel(TestData.BoingPlane);
Console.WriteLine("************ProgrammedFlightNumber*********");
Console.WriteLine(fm.ProgrammedFlightNumber(new DateTime(2021,12,31)));
Console.WriteLine("**********DurationAverage******");
Console.WriteLine(fm.DurationAverage("Madrid"));
Console.WriteLine(fm.DurationAverageDel("Madrid"));
Console.WriteLine("**********OrderedDuration******");
foreach(Flight fl in fm.OrderedDurationFlights())
    Console.WriteLine(fl);
//Console.WriteLine("*******SeniorTravellers*******");
//foreach(var t in fm.SeniorTravellers(TestData.flight1))
//    Console.WriteLine(t.BirthDate);
Console.WriteLine("*******Destination Grouped Flight*******");
    fm.DestinationGroupedFlights();
Console.WriteLine("***********Extension Method");
p1.UpperFullName();
Console.WriteLine(p1.FullName.FirstName+" "+p1.FullName.LastName);
//Insertion dans les tables Planes et Flights
AMContext ctx = new AMContext();
IUnitOfWork uow = new UnitOfWork(ctx);
IServiceFlight sf = new ServiceFlight(uow);
IServicePlane sp = new ServicePlane(uow);
sp.Add(TestData.Airbusplane);
sp.Add(TestData.BoingPlane);
sf.Add(TestData.flight1);
sf.Add(TestData.flight2);
sf.Commit();
Console.WriteLine("Ajout avec succès");
//Afficher le contenu de la table Flights
foreach(Flight fl in sf.GetMany())
Console.WriteLine("Date:"+fl.FlightDate+" Destination: "+fl.Destination+"Plane Capacity: "+fl.Plane.Capacity);



