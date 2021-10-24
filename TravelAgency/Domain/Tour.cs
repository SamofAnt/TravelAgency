// <copyright file="Tour.cs" company="Самофалов А.П.">
// Copyright (c) Самофалов А.П. All rights reserved.
// </copyright>

namespace Domain
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Тур.
    /// </summary>
    public class Tour
    {
        public Tour(int id, string nameTour, ISet<Tourist> tourists = null)
        {
            Id = id;
            var trimmedNameTour = nameTour?.Trim();

            this.NameTour = trimmedNameTour ?? throw new ArgumentOutOfRangeException(nameof(nameTour));

            if(tourists != null)
            {
                foreach (var tourist in tourists)
                {
                    Tourists.Add(tourist);
                    tourist.AddTour(this);
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

        public override string ToString() => $"{this.NameTour} {this.NameTour}";
    }

}
