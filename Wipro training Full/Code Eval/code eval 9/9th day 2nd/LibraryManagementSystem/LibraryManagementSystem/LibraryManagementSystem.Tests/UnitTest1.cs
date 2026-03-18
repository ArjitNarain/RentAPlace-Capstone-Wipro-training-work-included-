using NUnit.Framework;
using LibraryManagementSystem;
using NUnit.Framework.Legacy;

namespace LibraryManagementSystem.Tests
{
    public class LibraryTests
    {
        private Library library;

        [SetUp]
        public void Setup()
        {
            library = new Library();
        }

        [Test]
        public void AddBook_ShouldAddBook()
        {
            var book = new Book("C# Basics", "John", "123");
            library.AddBook(book);

            ClassicAssert.AreEqual(1, library.Books.Count);
        }

        [Test]
        public void RegisterBorrower_ShouldAddBorrower()
        {
            var borrower = new Borrower("Aditya", "CARD1");
            library.RegisterBorrower(borrower);

            ClassicAssert.AreEqual(1, library.Borrowers.Count);
        }

        [Test]
        public void BorrowBook_ShouldWork()
        {
            var book = new Book("C#", "John", "123");
            var borrower = new Borrower("Aditya", "CARD1");

            library.AddBook(book);
            library.RegisterBorrower(borrower);

            library.BorrowBook("123", "CARD1");

            ClassicAssert.IsTrue(book.IsBorrowed);
            ClassicAssert.AreEqual(1, borrower.BorrowedBooks.Count);
        }

        [Test]
        public void ReturnBook_ShouldWork()
        {
            var book = new Book("C#", "John", "123");
            var borrower = new Borrower("Aditya", "CARD1");

            library.AddBook(book);
            library.RegisterBorrower(borrower);
            library.BorrowBook("123", "CARD1");

            library.ReturnBook("123", "CARD1");

            ClassicAssert.IsFalse(book.IsBorrowed);
            ClassicAssert.AreEqual(0, borrower.BorrowedBooks.Count);
        }
    }
}