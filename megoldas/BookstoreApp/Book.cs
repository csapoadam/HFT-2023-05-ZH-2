using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        public string Title { get; set; }

        [Year]
        public int Year { get; set; }

        public int AuthorId { get; set; }

        public virtual Author Author { get; set; }

        [NotMapped]
        public virtual ICollection<BookAndBookstore> BooksAndBookstores{ get; set; }

    }
}
