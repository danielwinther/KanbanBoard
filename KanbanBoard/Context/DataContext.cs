using KanbanBoard.Models;

namespace KanbanBoard.Context
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class DataContext : DbContext
    {
        // Your context has been configured to use a 'DataContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'KanbanBoard.DataContext.DataContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'DataContext' 
        // connection string in the application configuration file.
        public DataContext()
            : base("name=DataContext")
        {
            Database.SetInitializer(new ContextInitializer());   
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public DbSet<Member> Members { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<Favourite> Favourites { get; set; }
        public DbSet<List> Lists { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Comment> Comments { get; set; }

    }
}