using System;
using System.Collections.Generic;

public class User
{
    public int UserId { get; set; }
    public string FullName { get; set; }
    public string Role { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
}

public class Author
{
    public int AuthorId { get; set; }
    public string FullName { get; set; }
    public string PenName { get; set; }
    public DateTime BirthDate { get; set; }
}

public class Book
{
    public string ISBN { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<Author> Authors { get; set; } = new List<Author>();
    public List<Copy> Copies { get; set; } = new List<Copy>();
    public List<Review> Reviews { get; set; } = new List<Review>();
}

public class Copy
{
    public int CopyId { get; set; }
    public DateTime DatePublished { get; set; }
    public string PublishCountry { get; set; }
    public string ISBN { get; set; }
    public bool Returned { get; set; }
    public List<Borrowing> Borrowings { get; set; } = new List<Borrowing>();
    public string DisplayString
    {
        get { return $"ID: {CopyId}, Год: {DatePublished.Year}, Страна: {PublishCountry}"; }
    }
}

public class Reader
{
    public int ReaderId { get; set; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public List<Review> Reviews { get; set; } = new List<Review>(); 
    public List<Borrowing> Borrowings { get; set; } = new List<Borrowing>(); 
}

public class Review
{
    public int ReviewId { get; set; }
    public string ISBN { get; set; }
    public int ReaderId { get; set; }
    public string Text { get; set; }
    public int Rating { get; set; }
    public DateTime Date { get; set; }
}

public class Borrowing
{
    public int BorrowingId { get; set; }
    public DateTime BorrowDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public int CopyId { get; set; }
    public int ReaderId { get; set; }
}

public class BookReportItem
{
    public string Title { get; set; }
    public long TotalCopies { get; set; }
    public long ReturnedCopies { get; set; }
    public long NotReturnedCopies { get; set; }
    public DateTime? NextReturnDate { get; set; }
}