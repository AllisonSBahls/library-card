using LibraryCardAPI.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;

namespace LibraryCardAPI.Repository.Context
{
    public class SeedingData
    {
        private readonly LibraryCardContext _context;
        private readonly UserManager<User> _userManager;
        public SeedingData(LibraryCardContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public void SeedUsers()
        {
            if (_userManager.FindByNameAsync("informatica").Result == null)
            {
                User user = new User()
                {
                    UserName = "informatica",
                    FullName = "TI - FAMA"
                };
                IdentityResult result = _userManager.CreateAsync
                (user, "pqzmal123").Result;
            }

            if (_userManager.FindByNameAsync("biblioteca").Result == null)
            {
                User user = new User()
                {
                    UserName = "biblioteca",
                    FullName = "Biblioteca - FAMA"
                };
                IdentityResult result = _userManager.CreateAsync
                (user, "123456").Result;
            }

            if (_context.Students.Any())
            {
                return;
            }

            Student student = new Student { Name = "Ronan o Guardião", Course = "Agronomia", Photo = "ronan.jpg", RegistrationNumber = 20202132, Validate = new DateTime(2021, 06, 17) };
            Student student1 = new Student { Name = "Elises Vermillion", Course = "Zootecnia", Photo = "elises.jpg", RegistrationNumber = 20202112, Validate = new DateTime(2021, 06, 17) };
            Student student2 = new Student { Name = "Lass Scarlet", Course = "Agronomia", Photo = "lass.jpg", RegistrationNumber = 20202431, Validate = new DateTime(2021, 06, 17) };
            Student student3 = new Student { Name = "Sieghart Vermillion", Course = "Psicologia", Photo = "sieg.jpg", RegistrationNumber = 20204221, Validate = new DateTime(2021, 06, 17) };
            Student student4 = new Student { Name = "Jin Mãos de Prata", Course = "Zootencia", Photo = "Jin.jpg", RegistrationNumber = 2020234, Validate = new DateTime(2021, 06, 17) };
            Student student5 = new Student { Name = "Mary a Tecnomaga", Course = "Serviço Social", Photo = "mary.png", RegistrationNumber = 202343021, Validate = new DateTime(2021, 06, 17) };
            Student student6 = new Student { Name = "Ryan o Druida", Course = "Agronomia", Photo = "ryan.png", RegistrationNumber = 20202341, Validate = new DateTime(2021, 06, 17) };
            Student student7 = new Student { Name = "Lire a Arqueira", Course = "Psicologia", Photo = "lire.jpg", RegistrationNumber = 20203221, Validate = new DateTime(2021, 06, 17) };

            _context.AddRange(student1, student, student2, student3, student4, student5, student6, student7);

            _context.SaveChanges();
        }

        
        
    }
}