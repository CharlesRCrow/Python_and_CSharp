// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

using CAS_LINQ.Data;
using CAS_LINQ.Models;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Configuration;

namespace CAS_LINQ;

/// <summary>
/// Querying();
/// </summary>

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Search: ");
        
        string search = "hydro";
        
        if (search == null)
        {
            return;
        }
        Querying(search);
                
    }
    
    static void Querying(string search)
    {
        using (CasContext db = new())
        {
            FormattableString input = $"SELECT * FROM CAS WHERE ChemName LIKE {search} LIMIT 30";
            var results = db.Cas.FromSql(input).ToList();
            Console.WriteLine("Activity  |  CAS  |  Chemname");
            foreach (var item in results)
            {
                Console.WriteLine($"{item.Activity} | {item.Casrn} | {item.ChemName}");
            }
        }
        
        using( CasContext db = new())
        {
            try
            {
                IQueryable<Ca>? cas = db.Cas?.Where(p => EF.Functions.Like(p.ChemName, $"%{search}%"))
                    .Where(p => p.Activity == "ACTIVE")
                    .Where(p => p.Flag == "")
                    .OrderBy(p => p.ChemName)
                    .Take(40);

                if (cas == null)
                {
                    Console.WriteLine("None Found");
                    return;
                }

                foreach (Ca p in cas)
                {
                    Console.WriteLine($"{p.Activity} | {p.Casrn} | {p.ChemName}");
                }
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return;
            }
        }
        

    }
}
