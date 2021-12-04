// <copyright file="Program.cs" company="Самофалов А.П.">
// Copyright (c) Самофалов А.П.. All rights reserved.
// </copyright>

namespace TravelAgency
{
    using System;
    using System.Collections.Generic;
    using Domain;
    using ORM;
    using ORM.Repositories;

    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var db = new TourContext())
            {
                CountryRepository rep = new CountryRepository(db);
                rep.Create(new Country("USA"));
                Console.WriteLine(db.Country.Find(6).NameCountry);
            }
        }
    }
}
