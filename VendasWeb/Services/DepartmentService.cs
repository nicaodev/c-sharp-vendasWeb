using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendasWeb.Data;
using VendasWeb.Models;

namespace VendasWeb.Services
{
    public class DepartmentService
    {
        private readonly VendasWebContext _context;

        public DepartmentService(VendasWebContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> FindAll()
        {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
        }
    }
}