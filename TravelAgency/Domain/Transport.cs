// <copyright file="Transport.cs" company="Самофалов А.П.">
// Copyright (c) Самофалов А.П.. All rights reserved.
// </copyright>


namespace Domain
{
    using System;
    using System.Collections.Generic;
    using Staff;
    using Staff.Extensions;

    /// <summary>
    /// Класс транспорт
    /// </summary>
    public class Transport
    {

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Transport"/>.
        /// </summary>
        /// /// <param name="id">Уникальный идентификатор.</param>
        /// <param name="nameTransport">Название транспорта</param>
        public Transport(int id, string nameTransport)
        {
            this.Id = id;
            this.NameTransport = nameTransport.TrimOrNull() ?? throw new ArgumentOutOfRangeException(nameof(nameTransport));
        }

        /// <summary>
        /// Получает или задает уникальный идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает или задает название транспорта
        /// </summary>
        public string NameTransport { get; set; }

        /// <summary>
        /// Получает или задает список туров
        /// </summary>
        public virtual ISet<Tour> Tours { get; set; } = new HashSet<Tour>();

        /// <summary>
        /// Добавить тур туристу.
        /// </summary>
        /// <param name="tour">Добавляемый тур</param>
        /// <returns>
        /// <see langword="true"/> если тур был добавлен.
        /// </returns>
        public bool AddTour(Tour tour) => this.Tours.TryAdd(tour) ?? throw new ArgumentNullException(nameof(tour));

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() => this.NameTransport;
    }
}