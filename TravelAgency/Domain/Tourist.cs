// <copyright file="Tourist.cs" company="Самофалов А.П.">
// Copyright (c) Самофалов А.П. All rights reserved.
// </copyright>

using Staff.Extensions;

namespace Domain
{
    using System;
    using System.Collections.Generic;
    using Staff;

    /// <summary>
    /// Класс турист
    /// </summary>
    public class Tourist
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Tourist"/>.
        /// </summary>
        /// /// <param name="id">Уникальный идентификатор.</param>
        /// <param name="lastName">Фамилия</param>
        /// <param name="firstName">Имя</param>
        /// <param name="phone">Номер телефона</param>
        /// <param name="email">Почта</param>
        /// <param name="birthday">Дата рождения</param>
        public Tourist(int id, string lastName, string firstName, string phone, string email, DateTime birthday)
        {
            this.Id = id;
            this.Phone = phone;
            this.Email = email;
            this.Birthday = birthday;
            this.LastName = lastName.TrimOrNull() ?? throw new ArgumentOutOfRangeException(nameof(lastName));
            this.FirstName = firstName.TrimOrNull() ?? throw new ArgumentOutOfRangeException(nameof(firstName));
            this.Tours = new HashSet<Tour>();
        }

        /// <summary>
        /// Получает или задает уникальный идентификатор
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// Получает или задает фамилию.
        /// </summary>
        public string LastName { get; protected set; }

        /// <summary>
        /// Получает или задает имя
        /// </summary>
        public string FirstName { get; protected set; }

        /// <summary>
        /// Получает полное имя.
        /// </summary>
        public string FullName => $"{this.LastName} {this.FirstName[0]}. ".Trim();

        /// <summary>
        /// Получает или задает номер телефона
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Получает или задает почту
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Получает или задает дату рождения
        /// </summary>
        public DateTime Birthday { get; set; }
        /// <summary>
        /// Получает или задает туры.
        /// </summary>
        public virtual ISet<Tour> Tours { get; protected set; } = new HashSet<Tour>();

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
        public override string ToString() => $"Полное имя: {this.FullName}\nEmail: {this.Email}\nДата рождения: {this.Birthday}\nНомер телефона: {this.Phone}";
    }
}
