using System.Collections.Generic;
using System.Linq;
using AssemblyLineTester.Data;
using AssemblyLineTester.Models;
using Microsoft.EntityFrameworkCore;

namespace AssemblyLineTester.Services
{
    public class HarnessService
    {
        public List<ProductHarness> GetAllHarnesses()
        {
            using var db = new AppDbContext();
            return db.ProductHarnesses
                .Include(h => h.ConnectorList)
                .OrderByDescending(h => h.LastUpdate)
                .ToList();
        }
    }
}