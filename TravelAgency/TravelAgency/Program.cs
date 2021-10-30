// <copyright file="Program.cs" company="Самофалов А.П.">
// Copyright (c) Самофалов А.П.. All rights reserved.
// </copyright>

namespace TravelAgency
{
    using System;

    internal class Program
    {
        private static void Main(string[] args)
        {
            var tourist = new Domain.Tourist(1, "Самофалов", "Антон");
            var tour = new Domain.Tour(1, "Планета Тур", tourist);

            Console.WriteLine($"{tour} {tourist}");
        }
    }
}
