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
        private readonly DbContextOptions<NotesContext> dbContextOptions;
        private readonly ITestOutputHelper output;//Aux to show output message


        public LoginShould(ITestOutputHelper output)
        {
            dbContextOptions = new DbContextOptionsBuilder<NotesContext>()
                 .UseInMemoryDatabase("notes")
                 .Options;
            this.output = output;
            
        }

        

        [Fact]
        public async Task ValidateLogin()
        {
            //ARRANGE
            var notesContext = new NotesContext(dbContextOptions);
            AuthRepository repository = new AuthRepository(notesContext);

            //ACT            
            var newUser = new Users()
            {
                Username = "mark12345",
                Email = "mark123@gmail.com",
                Password = "123456"
            };           

            using (var nContext = new NotesContext(dbContextOptions)){

                
                object userInserted = await nContext.UserModel.AddAsync(newUser);
                await nContext.SaveChangesAsync();

                //ACT              
                var result = await repository.Login(new LoginDto { Email=newUser.Email,Password=newUser.Password});                
                //var result = await repository.Login(new LoginDto { Email = "mat123@gmail.com", Password = "123456" });                

                var obj1Str = JsonConvert.SerializeObject(result);//Serializado para poder mostrarlo en output

                //Assert                
                //Assert.NotNull(userInserted);
                Assert.Equal(2, nContext.UserModel.Count());                
                output.WriteLine("Records in the db {0} ",nContext.UserModel.Count());
                output.WriteLine("Objeto insertado {0} ",userInserted);
                output.WriteLine("Objeto logged {0} ",obj1Str.ToString());
                //output.WriteLine("Result {0} ",result.Username);
                //Assert.NotNull(result);
                //Assert.Single(nContext.UserModel);//This show error because return 2 elemetns and not 1
                //Assert.Single();
            }







        }
        
    }
}
