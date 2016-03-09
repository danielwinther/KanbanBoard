using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using KanbanBoard.Models;

namespace KanbanBoard.Context
{
    public class ContextInitializer : DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext dataContext)
        {
            var board = new Board() { Name = "My board", Members = new List<Member>() };

            var daniel = new Member()
            {
                Username = "Daniel",
                Password = "daniel100",
                Email = "daniel@mail.dk",
                FirstName = "Daniel",
                MiddleName = "Winther",
                LastName = "Jensen",
                Bio = "About me...",
                Boards = new List<Board>(),
                Items = new List<Item>(),
            };
            var anders = new Member()
            {
                Username = "Anders",
                Password = "secret1234",
                Email = "anders@mail.dk",
                FirstName = "Anders",
                LastName = "Børjesson",
                Bio = "About Anders...",
                Boards = new List<Board>(),
                Items = new List<Item>(),
            };
            var zuhair = new Member()
            {
                Username = "Zuhair",
                Password = "password100",
                Email = "zuhair@mail.dk",
                FirstName = "Zuhair",
                MiddleName = "Haroon",
                LastName = "Khan",
                Bio = "About Zuhair...",
                Boards = new List<Board>(),
                Items = new List<Item>(),
            };

            dataContext.Boards.Add(board);
            dataContext.Members.Add(daniel);
            dataContext.Members.Add(anders);
            dataContext.Members.Add(zuhair);
            daniel.Boards.Add(board);
            anders.Boards.Add(board);
            zuhair.Boards.Add(board);

            dataContext.Lists.Add(new List() { Name = "To do", BoardId = 1 });
            dataContext.Lists.Add(new List() { Name = "Doing", BoardId = 1 });
            dataContext.Lists.Add(new List() { Name = "Done", BoardId = 1 });

            dataContext.SaveChanges();

            dataContext.Colors.Add(new Color() { Name = "default" });
            dataContext.Colors.Add(new Color() { Name = "success" });
            dataContext.Colors.Add(new Color() { Name = "info" });
            dataContext.Colors.Add(new Color() { Name = "warning" });
            dataContext.Colors.Add(new Color() { Name = "danger" });

            List<Item> items = new List<Item>()
            {
                new Item() { Title = "Item 1", Description = "Here is a description for this item", Deadline = DateTime.Today, ColorId = 5, Index = 1, ListId = 1, Members = new List<Member>() },
                new Item() { Title = "Item 2", Description = "Here is a description for this item", Deadline = DateTime.Today, ColorId = 1, Index = 2, ListId = 1, Members = new List<Member>() },
                new Item() { Title = "Item 3", Description = "Here is a description for this item", Deadline = DateTime.Today, ColorId = 1, Index = 3, ListId = 1, Members = new List<Member>() },
                new Item() { Title = "Item 4", Description = "Here is a description for this item", Deadline = DateTime.Today, ColorId = 1, Index = 4, ListId = 1, Members = new List<Member>() },

                new Item() { Title = "Item 1", Description = "Here is a description for this item", Deadline = DateTime.Today, ColorId = 1, Index = 1, ListId = 2, Members = new List<Member>() },
                new Item() { Title = "Item 2", Description = "Here is a description for this item", Deadline = DateTime.Today, ColorId = 1, Index = 2, ListId = 2, Members = new List<Member>() },

                new Item() { Title = "Item 1", Description = "Here is a description for this item", Deadline = DateTime.Today, ColorId = 1, Index = 1, ListId = 3, Members = new List<Member>() },
            };
            foreach (var item in items)
            {
                dataContext.Items.Add(item);
                daniel.Items.Add(item);
                anders.Items.Add(item);
                zuhair.Items.Add(item);
            }

            dataContext.SaveChanges();

            dataContext.Comments.Add(new Comment() { Title = "Comment 1", Text = "This is a comment", ItemId = 1, MemberId = 1 });
            dataContext.Comments.Add(new Comment() { Title = "Comment 2", Text = "This is another comment", ItemId = 7, MemberId = 1 });

            dataContext.SaveChanges();
        }
    }
}