
using System.Reflection;
using System.Security.Authentication.ExtendedProtection;

namespace BookstoreApp
{
    public class Program
    {

        static void Main(string[] args)
        {
            BooksDbContext db = new BooksDbContext();

            //var listOfAuthors = db.Authors.Select(x => x.Name);
            //Console.WriteLine("List of all authors:");
            //foreach (var author in listOfAuthors)
            //{
            //    Console.WriteLine("\t" + author);
            //}

            var listOfPublishedAuthors = db.BooksAndBookstores.Select(babs => babs.Book).Select(book => book.Author).Select(au => au.Name).Distinct();
            Console.WriteLine("1.) List of authors with at least 1 book available:");
            foreach (var author in listOfPublishedAuthors)
            {
                Console.WriteLine("\t" + author);
            }

            var booksByWells = db.BooksAndBookstores
                .Select(babs => babs.Book)
                .Where(book => book.Author.Name == "HG Wells")
                .Distinct();
            Console.WriteLine("2.) List of books available by HG Wells:");
            foreach (var book in booksByWells)
            {
                Console.WriteLine("\t" + book.Title);
            }

            var booksWithMoreThan1Store = db.BooksAndBookstores.GroupBy(babs => babs.Book.Title).Select(btit => new
            {
                title = btit.Key,
                numTimes = btit.Count()
            }).Where(record => record.numTimes > 1);
                
            Console.WriteLine("3.) List of books that can be purchased in more than 1 store:");
            foreach (var book in booksWithMoreThan1Store)
            {
                Console.WriteLine("\t" + book.title);
            }

            var storesWithMoreThan2Books = db.BooksAndBookstores.GroupBy(babs => babs.Bookstore.Name).Select(bsnm => new
            {
                name = bsnm.Key,
                numTimes = bsnm.Count()
            }).Where(record => record.numTimes > 2);

            Console.WriteLine("4.) List of Bookstores with more than 2 books:");
            foreach (var storename in storesWithMoreThan2Books)
            {
                Console.WriteLine("\t" + storename.name);
            }


            var validBook = new Book { Id = 1, Title = "Blabla", AuthorId = 1, Year = 2023 };
            var invalidBook1 = new Book { Id = 1, Title = "Blabla", AuthorId = 1, Year = -20};
            var invalidBook2 = new Book { Id = 1, Title = "Blabla", AuthorId = 1, Year = 2032 };

            Validator validator = new Validator();
            Console.WriteLine("validBook is valid: " + validator.Validate(validBook));
            Console.WriteLine("invalidBook1 is valid: " + validator.Validate(invalidBook1));
            Console.WriteLine("invalidBook2 is valid: " + validator.Validate(invalidBook2));

            Console.ReadLine();
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    // a fenti sor nelkul barmire, pl. osztalyra vagy konstruktorra is alkalmazni lehetne...
    public class YearAttribute : Attribute
    { }

    public interface IValidation
    {
        public bool Validate(object instance, PropertyInfo prop);
    }

    public class YearValidation : IValidation
    {
        private YearAttribute attr;

        public YearValidation(YearAttribute attr)
        {

            this.attr = attr;

        }

        public bool Validate(object instance, PropertyInfo prop)
        {
            if (prop.PropertyType == typeof(int))
            {
                if (prop.GetValue(instance) != null)
                {
                    int value = (int)prop.GetValue(instance);
                    return value > 999 && value < 2024;
                }
                return true;
            }
            throw new InvalidOperationException();
        }
    }

    public class Validator
    {
        public bool Validate(object instance)
        {
            foreach (var prop in instance.GetType().GetProperties())
            {
                foreach (var attr in prop.GetCustomAttributes(false))
                {
                    if (attr is YearAttribute)
                    {
                        YearValidation validation = new YearValidation((YearAttribute)attr);
                        if (!validation.Validate(instance, prop))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    }
}