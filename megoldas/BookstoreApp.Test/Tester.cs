using NUnit.Framework;
using System.ComponentModel.DataAnnotations;

namespace BookstoreApp.Test
{
    [TestFixture]
    public class Tester
    {
        [Test]
        public void NeptunFormatTest1()
        {
            var validBook = new Book { Id = 1, Title = "Blabla", AuthorId = 1, Year = 2023 };
            

            Validator validator = new Validator();

            Assert.That(validator.Validate(validBook), Is.True);
        }

        [Test]
        public void NeptunFormatTest2()
        {
            var invalidBook1 = new Book { Id = 1, Title = "Blabla", AuthorId = 1, Year = -20 };

            Validator validator = new Validator();

            Assert.That(validator.Validate(invalidBook1), Is.False);
        }

        [Test]
        public void NeptunFormatTest3()
        {
            var invalidBook2 = new Book { Id = 1, Title = "Blabla", AuthorId = 1, Year = 2032 };

            Validator validator = new Validator();

            Assert.That(validator.Validate(invalidBook2), Is.False);
        }
    }
}