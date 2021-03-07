using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using StartWarsRestfulService.Models;

namespace StartWarsRestfulService.DataContext
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext():base(nameOrConnectionString: "MyConnection")
        {

        }

        public virtual DbSet<CharactersClass> CharactersObj { get; set; }
    }
}