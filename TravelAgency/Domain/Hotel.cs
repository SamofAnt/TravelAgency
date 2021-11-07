// <copyright file="Hotel.cs" company="Самофалов А.П.">
// Copyright (c) Самофалов А.П. All rights reserved.
// </copyright>

using Staff.Extensions;

namespace Domain
{
    using System;
    using System.Collections.Generic;
    using Staff;

    public class Hotel
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Hotel"/>.
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <param name="nameHotel">Названия отеля</param>
        /// <param name="class">Класс отеля</param>
        public Hotel(int id, string nameHotel, int @class)
        {
            this.Id = id;
            this.NameHotel = nameHotel ?? throw new ArgumentOutOfRangeException(nameof(nameHotel));
            this.Class = @class;
            this.Tours = new HashSet<Tour>();
        }

        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// Получает или задает названия отеля
        /// </summary>
        public string NameHotel { get; protected set; }

        /// <summary>
        /// Получает или задает класс отеля
        /// </summary>
        public int Class { get; protected set; }

        /// <summary>
        /// Получает или задает идентификатор страны
        /// </summary>
        public int CountryId { get; set; }

        /// <summary>
        /// Получает или задает объект страны
        /// </summary>
        public virtual Country Country { get; set; }

        /// <summary>
        /// Получает или задает коллекцию туров
        /// </summary>
        public virtual ISet<Tour> Tours { get; protected set; } = new HashSet<Tour>();

        /// <summary>
        /// Добавить тур отелю.
        /// </summary>
        /// <param name="tour">Добавляемый тур</param>
        /// <returns>
        /// <see langword="true"/> если тур был добавлен.
        /// </returns>
        public bool AddTour(Tour tour) => this.Tours.TryAdd(tour) ?? throw new ArgumentNullException(nameof(tour));

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() => $"{this.NameHotel} {this.Class} {this.Tours.Join()}";
    }
}