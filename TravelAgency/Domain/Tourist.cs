// <copyright file="Tourist.cs" company="Самофалов А.П.">
// Copyright (c) Самофалов А.П. All rights reserved.
// </copyright>

namespace Domain
{
    using System;
    using System.Collections.Generic;
    using Staff;

    public class Tourist
    {

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Tourist"/>.
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <param name="lastName">Фамилия</param>
        /// <param name="firstName">Имя</param>
        public Tourist(int id, string lastName, string firstName)
        {
            this.Id = id;
            this.LastName = lastName ?? throw new ArgumentOutOfRangeException(nameof(lastName));
            this.FirstName = firstName ?? throw new ArgumentOutOfRangeException(nameof(firstName));
        }

        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        public int Id { get; protected set; }


        /// <summary>
        /// Фамилия.
        /// </summary>
        public string LastName { get; protected set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; protected set; }

        /// <summary>
        /// Полное имя.
        /// </summary>
        public string FullName => $"{this.LastName} {this.FirstName[0]}. ".Trim();

        /// <summary>
        /// Туры.
        /// </summary>
        public ISet<Tour> Tours { get; protected set; } = new HashSet<Tour>();


        /// <summary>
        /// Добавить тур туристу.
        /// </summary>
        /// <param name="tour">Добавляемый тур</param>
        /// <returns>
        /// <see langword="true"/> если тур был добавлен.
        /// </returns>
        public bool AddTour(Tour tour) => this.Tours.TryAdd(tour) ?? throw new ArgumentNullException(nameof(tour));

        public override string ToString() => this.FullName;
    }
}
