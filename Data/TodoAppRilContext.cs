using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoAppRil.Models;

namespace TodoAppRil.Data
{
    public class TodoAppRilContext : DbContext
    {
        public TodoAppRilContext (DbContextOptions<TodoAppRilContext> options)
            : base(options)
        {
        }

        public DbSet<TodoAppRil.Models.TodoAppModel> TodoAppModel { get; set; } = default!;
    }
}
