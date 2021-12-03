// <copyright file="Country.cs" company="Самофалов А.П.">
// Copyright (c) Самофалов А.П. All rights reserved.
// </copyright>
namespace Domain
{
    using System;
    using System.Collections.Generic;
    using Staff;
    using Staff.Extensions;

    /// <summary>
    /// Класс страна
    /// </summary>
    public class Country
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Country"/>.
        /// </summary>
        /// <param name="nameCountry">Название страны</param>
        public Country(string nameCountry)
        {
            this.NameCountry = nameCountry.TrimOrNull() ?? throw new ArgumentOutOfRangeException(nameof(nameCountry));
            this.Cities = new HashSet<City>();
            this.Hotels = new HashSet<Hotel>();
        }

        /// <summary>
        /// Получает или задает уникальный идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает или задает название страны.
        /// </summary>
        public string NameCountry { get; set; }

        /// <summary>
        /// Получает или задает список отелей в стране.
        /// </summary>
        public virtual ISet<Hotel> Hotels { get; set; } = new HashSet<Hotel>();

        /// <summary>
        /// Получает или задает список городов в стране.
        /// </summary>
        public virtual ISet<City> Cities { get; set; } = new HashSet<City>();

        /// <summary>
        /// Добавить город в страну.
        /// </summary>
        /// <param name="city">Добавляемый город</param>
        /// <returns>
        /// <see langword="true"/> если город был добавлен.
        /// </returns>
        public bool AddCity(City city) => this.Cities.TryAdd(city) ?? throw new ArgumentNullException(nameof(city));

        /// <summary>
        /// Добавить отель в страну.
        /// </summary>
        /// <param name="hotel">Добавляемый отель</param>
        /// <returns>
        /// <see langword="true"/> если отель был добавлен.
        /// </returns>
        public bool AddHotel(Hotel hotel) => this.Hotels.TryAdd(hotel) ?? throw new ArgumentNullException(nameof(hotel));


        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() => $"{this.NameCountry}";
    }
}