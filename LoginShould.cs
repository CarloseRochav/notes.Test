using Microsoft.EntityFrameworkCore;
using notes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using notes.Repository;
using System.ComponentModel.DataAnnotations;
using notes.DTOs;
using Xunit.Abstractions;
using Newtonsoft.Json;
using Moq;
using Microsoft.Extensions.Options;
namespace notes.Tests
{
    public class LoginShould
    {

        //Contexto
        //private readonly DbContextOptions<NotesContext> dbContextOptions;              
        //Aux to show output message
        ITestOutputHelper output;



        //Contexto in-memory-database
        //public LoginShould(ITestOutputHelper output)
        //{
        //    dbContextOptions = new DbContextOptionsBuilder<NotesContext>()
        //         .UseInMemoryDatabase("notes")
        //         .Options;
        //    this.output = output;
        //}


        //USANDO EL CONTEXTO REAL DE LA BASE DE DATOS
        //public LoginShould(ITestOutputHelper output)
        //{
        //    dbContextOptions = new DbContextOptionsBuilder<NotesContext>().UseNpgsql("Host=127.0.0.1;Database=notes;Username=postgres;Password=lost1989").Options;
        //    this.output = output;

        //}

        [Fact]
        public async Task ValidateLogin()
        {
            //Mantener dentro de la misma funcion FACT
            var dbContextOptions = new DbContextOptionsBuilder<NotesContext>()
                 .UseInMemoryDatabase("notes")
                 .Options;



            // Insert seed data into the database using one instance of the context
            await using (var context = new NotesContext(dbContextOptions))
            {
                context.UserModel.Add(new Users { Username = "mat123", Email = "mat123@gmail.com", Password = "123456" });
                context.UserModel.Add(new Users { Username = "mark123", Email = "mark123@gmail.com", Password = "123456" });
                context.UserModel.Add(new Users { Username = "maxt123", Email = "maxt123@gmail.com", Password = "123456" });
                context.SaveChanges();
            }


            // Use a clean instance of the context to run the test
            await using (var context = new NotesContext(dbContextOptions))
            {
                AuthRepository _repository = new AuthRepository(context);                
                List<Users> users = await _repository.GetAllUsers();
                var result = _repository.Login(new() { Email = "max123@gmail.com", Password = "1234567" });

                Assert.NotNull(result);
                //output.WriteLine("Resultado {0} ");
                //Assert.Equal(3, users.Count);
            }            
            
            //Assert.Equal(2, nContext.UserModel.Count());         
            //output.WriteLine("Comparacion {0} ", string.Equals(newUser.Email, loginUser.Email, StringComparison.OrdinalIgnoreCase));            
            //output.WriteLine("Usuario signin , Email {0} & password {1}", result.Email, result.Password);
        }

    }



}
