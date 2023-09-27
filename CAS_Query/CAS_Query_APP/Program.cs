// See https://aka.ms/new-console-template for more information
﻿using Microsoft.EntityFrameworkCore; // Include extension method
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.ChangeTracking; // CollectionEntry
using Microsoft.EntityFrameworkCore.Storage; // IDbContextTransaction
using CAS_Query_APP.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}