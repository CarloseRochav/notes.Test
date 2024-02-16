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
namespace notes.Tests
{
    public class LoginShould
    {
        private readonly DbContextOptions<NotesContext> dbContextOptions;


        public LoginShould()
        {
            dbContextOptions = new DbContextOptionsBuilder<NotesContext>()
                 .UseInMemoryDatabase("notes")
                 .Options;
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
                Email = "mark@gmail.com",
                Password = "123456"
            };           

            using (notesContext)            {

                //notesContext.Add(newUser);
                await notesContext.UserModel.AddAsync(newUser);
                await notesContext.SaveChangesAsync();

                //ACT              
                var result = await repository.Login(new LoginDto { Email=newUser.Email,Password=newUser.Password});

                //Assert
                Assert.NotNull(result);
        }

            
            

            

            
        }
        
    }
}
