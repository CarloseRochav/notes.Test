using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using notes.Models;
using notes.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace notes.Tests
{
    public class RegisterShould
    {

        // Arrange
        public readonly DbContextOptions<NotesContext> dbContextOptions;//Set Context   ; We need the context to do operations      
        //Crear un Context para poder realizar ; Conectar a un abase de datos en memoria        
        

        public RegisterShould()//Constructor to set database-in-memory ;  Then you can test your repository logic.
        {
            dbContextOptions = new DbContextOptionsBuilder<NotesContext>()
                .UseInMemoryDatabase(databaseName:"notes")
                .Options;
        }

        //Method to validate User register
        [Fact]//Atributo para hacer metodo de Test
        public async Task ValidateRegister()
        {
            //ARRANGE            
            var notesContext = new NotesContext(dbContextOptions);
            AuthRepository repository = new AuthRepository(notesContext);
            var newUser = new Users()
            {
                Username = "mont12345",
                Email = "mont123@gmail.com",
                Password = "123456"
            };

            // Act
            // Perform repository method calls            
            bool result = await repository.Create(newUser);

            // Assert
            // Make assertions based on expected behavior            
            //Assert.Equal(1,await notesContext.)
            List<Users> users = notesContext.UserModel.ToList();
            Assert.True(result);
            //Assert.Single(users);            
        }

        
    }
}
