using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using notes.Models;
using notes.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace notes.Tests
{
    public class NotesShould
    {

        //output function
        private readonly ITestOutputHelper output;

        public NotesShould(ITestOutputHelper output)
        {
            this.output = output;
        }


        //Test Create Note
        [Theory]
        [InlineData("Validar inventario","Validar incongruencias en assets duplicados",1)]
        [InlineData("Usuario incorrecto","Nombre/Apellido incorrectos",2)]
        [InlineData("Error Codigo de barras","Error al imprimir los codigos de barras",1)]
        [InlineData("Caducidad producto","Producto con caducidad excedida",2)]
        public async Task NotesPost(string title,string content, int idUser)
        {

            //Set Context in-memory-database
            var dbContextOptions = new DbContextOptionsBuilder<NotesContext>()
                .UseInMemoryDatabase("notes").
                Options;           
            
            //List all Users
            using (var nContext = new NotesContext(dbContextOptions))
            {
                var _repository = new AuthRepository(nContext);
                var result = await _repository.GetAllUsers();

                result.ForEach(user => {
                    output.WriteLine("Id : {0} Username : {1} Email : {2} ", user.Id, user.Username, user.Email
                  );
                });
                
                output.WriteLine("Resultados : {0} ",result.ToString() );
                //output.WriteLine("Tipo : {0} ",result.GetType());//Get data type
            }

            using (var nContext = new NotesContext(dbContextOptions))
            {
                var _repository = new NoteRepository(nContext);
                var note = new Note() { Title=title,Content=content};

                var result =await _repository.Create(note, idUser);

                output.WriteLine("Nota {0} recibida : {1} by {2}", result.Id,result.Title, result.UserID);                
            }

        }

        //Test List All Notes by User ID
        [Fact]
        public async Task ListNotesByUser()
        {
            //Set Context in-memory-database
            var dbContextOptions = new DbContextOptionsBuilder<NotesContext>()
                .UseInMemoryDatabase("notes").
                Options;

            using(var nContext = new NotesContext(dbContextOptions))
            {
                var _repository = new NoteRepository(nContext);
                var result = await _repository.ListAll(1);

                result.ForEach(note => {
                    Assert.NotNull(note);
                    output.WriteLine("Nota : {0} de {1 }", note.Title, note.UserID); 
                });

            }
        }


        //Get Note from specific User
        [Fact]
        public async Task GetNoteByIdNote()
        {
            //Set Context in-memory-database
            var dbContextOptions = new DbContextOptionsBuilder<NotesContext>()
                .UseInMemoryDatabase("notes").
                Options;

            using (var nContext = new NotesContext(dbContextOptions))
            {
                var _repository = new NoteRepository(nContext);
                var result = await _repository.GetById(2,2);

                //output.WriteLine("Nota No. {0} ",result.UserID);                
            }            
        }


        //Update Note from some User
        [Fact]
        public async Task UpdateNoteFromUser()
        {
            //Set Context in-memory-database
            var dbContextOptions = new DbContextOptionsBuilder<NotesContext>()
                .UseInMemoryDatabase("notes").
                Options;

            using (var nContext = new NotesContext(dbContextOptions))
            {
                var _repository = new NoteRepository(nContext);
                var result = await _repository.UpdateById(2, 2, new() {Title="Usuario Incorrecto",Content="Pendiente el nombre"});
                output.WriteLine("Nota actualizada : {0} ",result);
            }
        }

        //Delete Note by IdNote
        [Fact]
        public async Task DeleteNoteById()
        {
            //Set Context in-memory-database
            var dbContextOptions = new DbContextOptionsBuilder<NotesContext>()
                .UseInMemoryDatabase("notes").
                Options;


            using (var nContext = new NotesContext(dbContextOptions))
            {
                var _repository = new NoteRepository(nContext);
                var result = await _repository.DeleteById(2, 2);
                output.WriteLine("Nota Removida : {0} ", result);
            }
        }

         
    }
}
