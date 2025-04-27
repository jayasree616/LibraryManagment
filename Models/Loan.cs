

namespace WebApplication1.Models
{
           public class Loan
        {
            public int LoanId { get; set; } // Primary key
            public int BookId { get; set; } // Foreign key to Book
            public int MemberId { get; set; } // Foreign key to Member
            public DateTime LoanDate { get; set; }
            public DateTime DueDate { get; set; }
            public DateTime? ReturnDate { get; set; } // Nullable to track unreturned books

            // Navigation properties
            public Book Book { get; set; }
            public Member Member { get; set; }
        }
    }


