// <copyright file="Tour.cs" company="Самофалов А.П.">
// Copyright (c) Самофалов А.П. All rights reserved.
// </copyright>

using Staff;
using Staff.Extensions;

namespace Domain
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Тур.
    /// </summary>
    public class Tour
    {

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Tour"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="tourists"></param>
        public Tour(int id, string title, params Tourist[] tourists) : this(id, title, new HashSet<Tourist>(tourists))
        { }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Tour"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nameTour"></param>
        /// <param name="tourists"></param>
        public Tour(int id, string nameTour, ISet<Tourist> tourists = null, ISet<Hotel> hotels = null)
        {
            this.Id = id;
            this.NameTour = nameTour.TrimOrNull() ?? throw new ArgumentOutOfRangeException(nameof(nameTour));

            if (tourists != null)
            {
                foreach (var tourist in tourists)
                {
                    this.Tourists.Add(tourist);
                    tourist.AddTour(this);
                }
            }

            if (hotels != null)
            {
                foreach (var hotel in hotels)
                {
                    this.Hotels.Add(hotel);
                    hotel.AddTour(this);
                }
            }
        }

        /// <summary>
        /// Уникальный идентификатор.
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// Gets or sets название тура.
        /// </summary>
        public string NameTour { get; set; }

        /// <summary>
        /// Список туристов
        /// </summary>
        public ISet<Tourist> Tourists { get; protected set; } = new HashSet<Tourist>();

        public ISet<Hotel> Hotels { get; protected set; } = new HashSet<Hotel>();

        /// <inheritdoc/>
        public override string ToString() => $"{this.NameTour} {this.Tourists.Join()} {this.Hotels.Join()}";
    }

}
