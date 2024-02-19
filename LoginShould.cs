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
namespace notes.Tests
{
    public class LoginShould
    {
        //private readonly DbContextOptions<NotesContext> dbContextOptions;        
        private readonly DbContextOptions<NotesContext> dbContextOptions;        
        private readonly ITestOutputHelper output;//Aux to show output message


        //public LoginShould(ITestOutputHelper output)
        //{
        //    dbContextOptions = new DbContextOptionsBuilder<NotesContext>()
        //         .UseInMemoryDatabase("notes")
        //         .Options;
        //    this.output = output;

        //}

        public LoginShould(ITestOutputHelper output)
        {
            dbContextOptions = new DbContextOptionsBuilder<NotesContext>().UseNpgsql("Host=127.0.0.1;Database=notes;Username=postgres;Password=lost1989").Options;            
            this.output = output;

        }


        [Fact]
        public async Task ValidateLogin()
        {
            //ARRANGE
            //var notesContext = new NotesContext(dbContextOptions);            

            //ACT            
            //var newUser = new Users()
            //{
            //    Username = "mark12345",
            //    Email = "mark123@gmail.com",
            //    Password = "123456"
            //};           

            //using (var nContext = new NotesContext(dbContextOptions)){//crear un diferente scope


            //    object userInserted = await nContext.UserModel.AddAsync(newUser);
            //    await nContext.SaveChangesAsync();                

            //}

            //AuthRepository _authRepository = new AuthRepository(NotesContext):
            

            using (var nContext = new NotesContext(dbContextOptions))
            {//crear un diferente scope

                //LoginDto loginUser = new LoginDto { Email = newUser.Email, Password = newUser.Password };
                LoginDto loginUser = new LoginDto { Email = "mat123@gmail.com", Password = "123456" };

                //ACT              
                AuthRepository repository = new AuthRepository(nContext);
                var result = await repository.Login(loginUser);
                //var result = await repository.Login(new LoginDto { Email = "mat123@gmail.com", Password = "123456" });                

                var obj1Str = JsonConvert.SerializeObject(result);//Serializado para poder mostrarlo en output

                //Assert                
                //Assert.NotNull(userInserted);
                Assert.Equal(2, nContext.UserModel.Count());
                output.WriteLine("Records in the db {0} ", nContext.UserModel.Count());
                //output.WriteLine("Objeto logged {0} ", obj1Str.ToString());
                output.WriteLine("Objeto logged  : {0} ", result.Email);
                //output.WriteLine("Comparacion {0} ", string.Equals(newUser.Email, loginUser.Email, StringComparison.OrdinalIgnoreCase));
                //output.WriteLine("Usuario nuevo , Email {0} & password {1}", newUser.Email, newUser.Password);
                output.WriteLine("Usuario signin , Email {0} & password {1}", loginUser.Email, loginUser.Password);                

            }



        }
        
    }
}
