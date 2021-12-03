// <copyright file="Program.cs" company="Самофалов А.П.">
// Copyright (c) Самофалов А.П.. All rights reserved.
// </copyright>

using System.Collections.Generic;
using Domain;
using ORM;

namespace TravelAgency
{
    using System;

    internal class Program
    {
        private static void Main(string[] args)
        {
            //var hotel = new Domain.Hotel(1, "Radisson", 5);
            //var tourist = new Domain.Tourist(1, "Самофалов", "Антон", "+7(915)-356-08-98", "samofalov@gmail.com", DateTime.Now);
            //var transport = new Transport(1, "Airplane");
            //ISet<Tourist> tourists = new HashSet<Tourist>();
            //tourists.Add(tourist);
            //ISet<Hotel> hotels = new HashSet<Hotel>();
            //hotels.Add(hotel);
            //ISet<Transport> transports = new HashSet<Transport>();
            //transports.Add(transport);
            //var emp = new Employee(1, "Samofalov", "Anton", "+7(915)-356-08-98", "samofalov@gmail.com", DateTime.Now, "CEO");

            ////var tour = new Domain.Tour(1, "Планета Тур", DateTime.Now, 500, DateTime.Now, 23, tourists , hotels,emp ,transports);
            //var tour = new Tour(1, "Планета Тур", DateTime.Now, 500, DateTime.Now, 23);
            //Console.WriteLine(tour);

            using (var db = new TourContext())
            {

            }
        }
    }
}
