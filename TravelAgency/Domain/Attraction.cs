// <copyright file="Attraction.cs" company="Самофалов А.П.">
// Copyright (c) Самофалов А.П.. All rights reserved.
// </copyright>
namespace Domain
{
    using System;
    using Staff;

    /// <summary>
    /// Класс достопримечательность
    /// </summary>
    public class Attraction
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Attraction"/>.
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <param name="nameAttraction">Название достопримечательности</param>
        public Attraction(int id, string nameAttraction)
        {
            this.Id = id;
            this.NameAttraction =
                nameAttraction.TrimOrNull() ?? throw new ArgumentOutOfRangeException(nameof(nameAttraction));
        }

        /// <summary>
        /// Получает или задает уникальный идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает или задает название достопримечательности
        /// </summary>
        public string NameAttraction { get; set; }

        /// <summary>
        /// Получает или задает идентификатор города
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// Получает или задает объект города
        /// </summary>
        public virtual City City { get; set; }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() => this.NameAttraction;
    }
}