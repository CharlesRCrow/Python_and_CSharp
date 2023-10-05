using CASAPP.Models;

namespace CASAPP
{
    internal class Search
    {
        static void CasSearch(string searchWord)
        {
            using(Data db = new())
            {
                try
                {
                    IQueryable<Ca>? cas = db.Cas?.Where(p => EF.Functions.Like(p.ChemName, $"%{search}%"))
                        .Where(p => p.Activity == "ACTIVE")
                        .Where(p => p.Flag == "")
                        .OrderBy(p => p.ChemName)
                        .Take(100);

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
}
