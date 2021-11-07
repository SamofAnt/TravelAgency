using System;
using System.Collections.Generic;
using Staff;

namespace Domain
{
    public class City
    {
        /// <summary>
        /// Получает или задает уникальный идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает или задает название города
        /// </summary>
        public string NameCity { get; set; }

        /// <summary>
        /// Получает или задает идентификатор страны
        /// </summary>
        public int CountryId { get; set; }

        /// <summary>
        /// Получает или задает объект страны.
        /// </summary>
        public virtual Country Country { get; set; }

        public virtual ISet<Attraction> Attractions { get; set; } = new HashSet<Attraction>();

        /// <summary>
        /// Добавить достопримечательность городу.
        /// </summary>
        /// <param name="attraction">Добавляемая достопримечательность</param>
        /// <returns>
        /// <see langword="true"/> если достопримечательность была добавлен.
        /// </returns>
        public bool AddAttraction(Attraction attraction) => this.Attractions.TryAdd(attraction) ?? throw new ArgumentNullException(nameof(attraction));

        public override string ToString() => $"{this.NameCity} {this.Country.NameCountry} {this.Attractions}";
    }
}