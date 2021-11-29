// <copyright file="Employee.cs" company="Самофалов А.П.">
// Copyright (c) Самофалов А.П.. All rights reserved.
// </copyright>


namespace Domain
{
    using System;
    using System.Collections.Generic;
    using Staff;
    using Staff.Extensions;

    /// <summary>
    /// Класс сотрудник
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Employee"/>.
        /// </summary>
        /// <param name="lastName">Фамилия.</param>
        /// <param name="firstName">Имя.</param>
        /// <param name="phone">Телефон.</param>
        /// <param name="email">Почта.</param>
        /// <param name="birthday">Дата рождения.</param>
        /// <param name="position"> Позиция.</param>
        public Employee(string lastName, string firstName, string phone, string email, DateTime birthday, string position)
        {
            this.Phone = phone;
            this.Email = email;
            this.Birthday = birthday;
            this.Position = position;
            this.FirstName = firstName.TrimOrNull() ?? throw new ArgumentNullException(nameof(firstName));
            this.LastName = lastName.TrimOrNull() ?? throw new ArgumentNullException(nameof(lastName));
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
        /// Получает или задает позицию сотрудника
        /// </summary>
        public string Position { get; set; }

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
        public override string ToString() => $"{this.FullName} {this.Phone} {this.Birthday} {this.Email} {this.Position}";
    }
}