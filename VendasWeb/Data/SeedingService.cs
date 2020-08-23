using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendasWeb.Models;
using VendasWeb.Models.Enums;

namespace VendasWeb.Data
{
    public class SeedingService
    {
        private VendasWebContext _context;

        public SeedingService(VendasWebContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Department.Any() || _context.Seller.Any() || _context.SalesRecords.Any())
                return; //Ja tem dados.

            //Não funciona.

            Department d1 = new Department(1, "Computadores");
            Department d2 = new Department(2, "Eletronicos");
            Department d3 = new Department(3, "Roupas");
            Department d4 = new Department(4, "Livros");

            Seller s1 = new Seller(1, "Nicolas", "a@gmail.com", new DateTime(1994, 2, 7), 1000.0, d1);
            Seller s2 = new Seller(2, "Alexandre", "b@gmail.com", new DateTime(1994, 3, 7), 2000.0, d2);
            Seller s3 = new Seller(3, "DaSilva", "c@gmail.com", new DateTime(1994, 4, 7), 400.0, d3);
            Seller s4 = new Seller(4, "Pereira", "d@gmail.com", new DateTime(1994, 5, 7), 100.0, d4);

            SalesRecord sr1 = new SalesRecord(1, new DateTime(2020, 8, 23), 12000.0, SaleStatus.Billed, s1);
            SalesRecord sr2 = new SalesRecord(2, new DateTime(2020, 8, 23), 14000.0, SaleStatus.Billed, s2);
            SalesRecord sr3 = new SalesRecord(3, new DateTime(2020, 8, 23), 15000.0, SaleStatus.Billed, s2);
            SalesRecord sr4 = new SalesRecord(4, new DateTime(2020, 8, 23), 11000.0, SaleStatus.Billed, s3);
            SalesRecord sr5 = new SalesRecord(5, new DateTime(2020, 8, 23), 26000.0, SaleStatus.Billed, s4);


            _context.Department.AddRange(d1, d2, d3, d4);
            _context.Seller.AddRange(s1, s2, s3, s4);
            _context.SalesRecords.AddRange(sr1, sr2, sr3, sr4, sr5);

            _context.SaveChanges();
        }

    }
}
