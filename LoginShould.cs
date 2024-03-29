﻿using Microsoft.EntityFrameworkCore;
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
        //[Theory]
        //[InlineData("mark123@gmail.com","123456")] 
        //[InlineData("mat123@gmail.com","123456")] 
        //[InlineData("max123@gmail.com","123456")]                
        //public async Task ValidateLogin(string email, string password)
        public async Task ValidateLogin()
        {
            //Mantener dentro de la misma funcion FACT
            var dbContextOptions =  new DbContextOptionsBuilder<NotesContext>()
                 .UseInMemoryDatabase("notes")
                 .Options;


            // Insert seed data into the database using one instance of the context
            using (var context = new NotesContext(dbContextOptions))
            {
                context.UserModel.Add(new Users { Username = "mark123", Email = "mark123@gmail.com", Password = "123456" });
                context.UserModel.Add(new Users { Username = "mat123", Email = "mat123@gmail.com", Password = "123456" });
                context.UserModel.Add(new Users { Username = "max123", Email = "max123@gmail.com", Password = "123456" });
                context.SaveChanges();
            }


            // Use a clean instance of the context to run the test
            using (var context = new NotesContext(dbContextOptions))
            {
                AuthRepository _repository = new AuthRepository(context);
                var usersS = await _repository.GetAllUsers();
                //var result = await _repository.Login(new() { Email = email, Password = password }); 
                //var result =  await _repository.Login(new() { Email ="mark123@gmail.com", Password = "123456" });

                //Assert.NotNull(result);                
                //output.WriteLine("Usuario signin : Email {0} & password {1}", result.Email, result.Password);
                //Assert.Equal(3, users.Count);
                usersS.ForEach(user =>
                {
                    output.WriteLine("Usuarios registrados : {0}", user.Email);
                });
                //output.WriteLine("Usuarios registrados : {0}", users.Count);
            }

            //Assert.Equal(2, nContext.UserModel.Count());                     
            //output.WriteLine("Comparacion {0} ", string.Equals(newUser.Email, loginUser.Email, StringComparison.OrdinalIgnoreCase));            
            //output.WriteLine("Usuario signin , Email {0} & password {1}", result.Email, result.Password);
        }

    }



}
