using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp
{
    public class BookAndBookstore
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int BookId { get; set; }

        [NotMapped]
        public virtual Book Book { get; set; }

        public int BookstoreId { get; set; }

        [NotMapped]
        public virtual Bookstore Bookstore { get; set; }
    }
}
