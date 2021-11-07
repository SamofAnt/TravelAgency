// <copyright file="Country.cs" company="Самофалов А.П.">
// Copyright (c) Самофалов А.П. All rights reserved.
// </copyright>
namespace Domain
{
    using System;
    using System.Collections.Generic;
    using Staff;
    using Staff.Extensions;

    public class Country
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Country"/>.
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <param name="nameCountry">Название страны</param>
        public Country(int id, string nameCountry)
        {
            this.Id = id;
            this.NameCountry = nameCountry.TrimOrNull() ?? throw new ArgumentNullException(nameof(nameCountry));
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

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() => $"{this.NameCountry} {this.Cities.Join()} {this.Hotels.Join()}";
    }
}