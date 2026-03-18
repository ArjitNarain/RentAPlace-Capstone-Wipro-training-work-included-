using System;

namespace LibraryManagementSystem
{
    public class Book
    {
        // Properties
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public bool IsBorrowed { get; private set; }

        // Constructor
        public Book(string title, string author, string isbn)
        {
            Title = title;
            Author = author;
            ISBN = isbn;
            IsBorrowed = false;
        }

        // Method to borrow book
        public void Borrow()
        {
            if (IsBorrowed)
                throw new InvalidOperationException("Book is already borrowed.");

            IsBorrowed = true;
        }

        // Method to return book
        public void Return()
        {
            if (!IsBorrowed)
                throw new InvalidOperationException("Book is not borrowed.");

            IsBorrowed = false;
        }
    }
}