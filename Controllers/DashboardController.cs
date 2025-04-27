using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class DashboardController : Controller
    {
        private readonly LibraryDbContext _context;

        public DashboardController(LibraryDbContext context)
        {
            _context = context;
        }

        // GET: Dashboard
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Admin Dashboard";

            // Test fetching data directly to see if data is accessible
            var testBooks = await _context.Books.FirstOrDefaultAsync();
            Console.WriteLine($"First Book: {testBooks?.Title ?? "No Books Found"}");

            var testMembers = await _context.Members.FirstOrDefaultAsync();
            Console.WriteLine($"First Member: {testMembers?.Name ?? "No Members Found"}");

            // Original code to fetch counts
            var totalBooks = await _context.Books.CountAsync();
            var totalStudents = await _context.Members.CountAsync();
            var issuedBooks = await _context.Loan.CountAsync(l => l.ReturnDate == null);
            var overdueBooks = await _context.Loan.CountAsync(l => l.DueDate < DateTime.Now && l.ReturnDate == null);

            // Log the counts for further debugging
            Console.WriteLine($"Total Books (fetched): {totalBooks}");
            Console.WriteLine($"Total Students (fetched): {totalStudents}");
            Console.WriteLine($"Issued Books (fetched): {issuedBooks}");
            Console.WriteLine($"Overdue Books (fetched): {overdueBooks}");

            // Create the ViewModel with fetched data
            var viewModel = new DashboardViewModel
            {
                TotalBooks = totalBooks,
                TotalStudents = totalStudents,
                IssuedBooks = issuedBooks,
                OverdueBooks = overdueBooks
            };

            // Pass the ViewModel to the view
            return View(viewModel);
        }

        // Fetch all books from the database
        public async Task<IActionResult> AllBooks()
        {
            var books = await _context.Books.ToListAsync(); // Corrected: fetching all books from the correct table
            return View(books);
        }

        // Fetch all students from the database
        public async Task<IActionResult> AllStudents()
        {
            var students = await _context.Members.ToListAsync();
            return View(students);
        }
    }
}
    