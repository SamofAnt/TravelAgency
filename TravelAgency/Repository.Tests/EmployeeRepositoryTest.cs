namespace Repository.Tests
{
    using Domain;
    using ORM;
    using ORM.Repositories;
    using ORM.Repositories.Interfaces;
    using Xunit;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    
    /// <summary>
    /// Тесты на IQ
    /// </summary>
    public class EmployeeRepositoryTest
    {
        [Fact]
        public void Add_WhenHaveNoTour()
        {
            IRepository<Employee> sut = GetInMemoryEmployeeRepository();
            Employee employee = new Employee()
            {
                Id = sut.GetAll().Count() + 1,
                LastName = "Samofalov",
                FirstName = "Anton",
                Phone = "+7(915)-356-08-98",
                Email = "samofalov@gmail.com",
                Birthday = DateTime.Now,
                Position = "CEO"
            };

            Employee savedEmployee = sut.Create(employee);

            Assert.Equal(1, sut.GetAll().Count());
            Assert.Equal("Samofalov A.", savedEmployee.FullName);
            Assert.NotNull(savedEmployee.Tours);
        }
        [Fact]
        public void Add_WhenHaveCountry()
        {
            IRepository<Employee> sut = GetInMemoryEmployeeRepository();
            Employee employee = new Employee()
            {
                Id = sut.GetAll().Count() + 1,
                LastName = "Samofalov",
                FirstName = "Anton",
                Phone = "+7(915)-356-08-98",
                Email = "samofalov@gmail.com",
                Birthday = DateTime.Now,
                Position = "CEO"
            };
            employee.AddTour(new Tour()
            {
                Id = 1,
                NameTour = "Gelengik",
                DateStart = DateTime.Now,
                DateEnd = DateTime.Now.AddDays(1),
                MaxTourists = 5,
                Price = 30000
            });

            Employee savedEmployee = sut.Create(employee);

            Assert.Equal(1, sut.GetAll().Count());
            Assert.Equal("Samofalov A.", savedEmployee.FullName);
            Assert.Equal("Gelengik", savedEmployee.Tours.First().NameTour);
        }
        
        [Fact]
        public void Delete_ValidData_Success()
        {
            IRepository<Employee> sut = GetInMemoryEmployeeRepository();
            sut.Create(GenerateEmployee(1, "Samofalov", "Test", "test", "test@test.su", DateTime.Now.AddDays(1), "testing"));

            Employee deleteEmployee = sut.GetById(1);
            sut.Delete(1);


            Assert.Equal(0, sut.GetAll().Count());
            Assert.Null(sut.GetAll().FirstOrDefault(c => c.FirstName == deleteEmployee.FirstName));
        }

        [Fact]
        public void Update_ValidData_Success()
        {
            IRepository<Employee> sut = GetInMemoryEmployeeRepository();
            sut.Create(GenerateEmployee(1, "Samofalov", "Test", "test", "test@test.su", DateTime.Now.AddDays(1), "testing"));

            Employee updateEmployee = sut.GetById(1);
            updateEmployee.FirstName = "Pasha";
            sut.Update(updateEmployee);

            Assert.Equal("Samofalov P.", sut.GetById(1).FullName);
        }
        
        private IRepository<Employee> GetInMemoryEmployeeRepository()
        {
            DbContextOptions<TourContext> options;
            var builder = new DbContextOptionsBuilder<TourContext>();
            builder.UseInMemoryDatabase(databaseName: "EmployeeDb");
            options = builder.Options;
            TourContext tourContext = new TourContext(options);
            tourContext.Database.EnsureDeleted();
            tourContext.Database.EnsureCreated();
            return new EmployeeRepository(tourContext);
        }
        //modelBuilder.Entity<Employee>().HasData(new Employee()
        //{
        //    Id = 1,
        //    LastName = "Samofalov",
        //    FirstName = "Anton",
        //    Phone = "+7(915)-356-08-98",
        //    Email = "samofalov@gmail.com",
        //    Birthday = DateTime.Now,
        //    Position = "CEO"
        //});
        private Employee GenerateEmployee(int id, string lastName, string firstName, string phone, string email, DateTime birthday, string position)
            => new(id, lastName, firstName, phone, email, birthday, position);
    }
}
