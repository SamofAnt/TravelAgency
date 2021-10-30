// <copyright file="Hotel.cs" company="Самофалов А.П.">
// Copyright (c) Самофалов А.П.. All rights reserved.
// </copyright>

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
        }

        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// Названия отеля
        /// </summary>
        public string NameHotel { get; protected set; }

        /// <summary>
        /// Класс отеля
        /// </summary>
        public int Class { get; protected set; }

        /// <summary>
        /// Коллекция туров
        /// </summary>
        public ISet<Tour> Tours { get; protected set; } = new HashSet<Tour>();

        /// <summary>
        /// Добавить тур отелю.
        /// </summary>
        /// <param name="tour">Добавляемый тур</param>
        /// <returns>
        /// <see langword="true"/> если тур был добавлен.
        /// </returns>
        public bool AddTour(Tour tour) => this.Tours.TryAdd(tour) ?? throw new ArgumentNullException(nameof(tour));

    }
}