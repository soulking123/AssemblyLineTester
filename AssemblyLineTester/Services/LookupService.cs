using System.Collections.Generic;
using System.Linq;
using AssemblyLineTester.Data;
using Microsoft.EntityFrameworkCore;

namespace AssemblyLineTester.Services
{
    // The reference lists the filter dropdowns are built from.
    public static class LookupService
    {
        public static List<string> GetProductModelNames()
        {
            using var db = new AppDbContext();
            return db.ProductModels.AsNoTracking().OrderBy(p => p.Name).Select(p => p.Name).ToList();
        }

        public static List<string> GetAreaNames()
        {
            using var db = new AppDbContext();
            return db.Areas.AsNoTracking().OrderBy(a => a.Name).Select(a => a.Name).ToList();
        }
    }
}
