// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

using CAS_LINQ.Data;
using CAS_LINQ.Models;
using System.Data.Entity.Core.Metadata.Edm;

namespace CAS_LINQ;

/// <summary>
/// Querying();
/// </summary>

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Search: ");
        string? search;
        //search = Console.ReadLine();
        search = "Formaldehyde";
        //search = "borax";
        
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
            //IQueryable<Ca>? cas = db.Cas?
            //var results = db.Cas.FromSql($"SELECT * FROM CAS WHERE ChemName LIKE '{search}% LIMIT 30'").ToList();
            var results = db.Cas.FromSql($"SELECT * FROM CAS WHERE ChemName LIKE {search} LIMIT 30").ToList();
            Console.WriteLine("Activity, Chemname, CAS");
            foreach (var item in results)
            {
                Console.WriteLine($"{item.Activity} , {item.ChemName}, {item.Casrn}");
            }
        }
        using (CasContext db = new())
        {
            var q =
            from a in db.Cas
            where a.Id == "5"
            select a;
            
            //var results = q.ToList();
            var results = q;

            foreach (var result in results)
            {
                Console.WriteLine(result);
            }
            Console.WriteLine($"{results}");
            
        }
    }
}
