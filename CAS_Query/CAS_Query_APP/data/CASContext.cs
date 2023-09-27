using CAS_Query_APP.Models;
using Microsoft.EntityFrameworkCore;
//using System.Data.SQLite.Core;


namespace CAS_Query_APP.Data;

public class CASContext : DbContext
{
    public DbSet<CAS> CAS { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(@"Data Source=/workspaces/chemistry_tools/CAS_Query/CAS_Query_APP/CAS.db");
        //string path = Path.Combine(Environment.CurrentDirectory, "CAS.db");
        //optionsBuilder.UseSqlite($"Filename={path}");
    }
}