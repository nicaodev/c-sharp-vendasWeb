using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendasWeb.Data;
using VendasWeb.Models;
using VendasWeb.Services.Exceptions;

namespace VendasWeb.Services
{
    public class SellerService
    {
        private readonly VendasWebContext _context;

        public SellerService(VendasWebContext context)
        {
            _context = context;
        }

        public async Task<List<Seller>> FindAll()
        {
            return await _context.Seller.ToListAsync();
        }

        public async Task Insert(Seller obj) // era void no lugar de Task
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindById(int id)
        {
            return await _context.Seller.Include(d => d.Department).FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task Remove(int id)
        {
            try
            {
                var obj = await _context.Seller.FindAsync(id);

                _context.Seller.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message + "\n Vendedor(a) não pode ser deletado há vendas relacionadas.");
            }
        }

        public async Task Update(Seller obj)
        {
            if (!(await _context.Seller.AnyAsync(x => x.Id == obj.Id)))
                throw new NotFoundException("Não existe no banco");
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}