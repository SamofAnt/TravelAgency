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
        public Tourist(int id, string lastName)
        {
            Id = id;
            LastName = lastName;
        }

        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        public int Id { get; protected set; }


        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; protected set; }

        /// <summary>
        /// 
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
    }
}
