// <copyright file="Hotel.cs" company="Самофалов А.П.">
// Copyright (c) Самофалов А.П. All rights reserved.
// </copyright>

using Staff.Extensions;

namespace Domain
{
    using System;
    using System.Collections.Generic;
    using Staff;

    /// <summary>
    /// Класс отель
    /// </summary>
    public class Hotel
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Hotel"/>.
        /// </summary>
        public Hotel()
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Hotel"/>.
        /// </summary>
        /// /// <param name="id">Уникальный идентификатор.</param>
        /// <param name="nameHotel">Названия отеля</param>
        /// <param name="classHotel">Класс отеля</param>
        public Hotel(int id, string nameHotel, int classHotel)
        {
            this.Id = id;
            this.NameHotel = nameHotel.TrimOrNull() ?? throw new ArgumentOutOfRangeException(nameof(nameHotel));
            this.ClassHotel = classHotel;
            this.Tours = new HashSet<Tour>();
        }

        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        public int Id { get;  set; }

        /// <summary>
        /// Получает или задает названия отеля
        /// </summary>
        public string NameHotel { get;  set; }

        /// <summary>
        /// Получает или задает класс отеля
        /// </summary>
        public int ClassHotel { get;  set; }


        /// <summary>
        /// Получает или задает объект страны
        /// </summary>
        public virtual Country Country { get; set; }

        /// <summary>
        /// Получает или задает коллекцию туров
        /// </summary>
        public virtual ISet<Tour> Tours { get;  set; } = new HashSet<Tour>();

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
        public override string ToString() => $"{this.NameHotel} {this.ClassHotel}";
    }
}