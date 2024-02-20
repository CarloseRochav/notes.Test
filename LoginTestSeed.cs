using Microsoft.EntityFrameworkCore;
using notes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace notes.Tests
{
    public static class LoginTestSeed
    {        
            public static void Seed(NotesContext context)
            {
            context.UserModel.Add(new() { Username = "mark12345", Email = "mark123@gmail.com", Password = "123456" });
            context.SaveChanges();
            }
        
    }
}
