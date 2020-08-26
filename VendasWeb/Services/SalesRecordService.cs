using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VendasWeb.Data;
using VendasWeb.Models;

namespace VendasWeb.Services
{
    public class SalesRecordService
    {
        private readonly VendasWebContext _context;
        public SalesRecordService(VendasWebContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDate(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecords select obj;

            if (minDate.HasValue)
                result = result.Where(x => x.Date >= minDate.Value);

            if (maxDate.HasValue)
                result = result.Where(x => x.Date <= maxDate.Value);

            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date).ToListAsync();
        }
    }
}
