using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;
using System.Linq;


public class DataAccess
{
    private readonly string _connectionString;

    public DataAccess(string connectionString)
    {
        _connectionString = connectionString;
    }
    public async Task<int> RegisterUserAsync(string fullName, string login, string password)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var cmd = new NpgsqlCommand("SELECT public.register_user(@p_full_name, @p_login, @p_password)", connection))
            {
                cmd.Parameters.AddWithValue("p_full_name", fullName);
                cmd.Parameters.AddWithValue("p_login", login);
                cmd.Parameters.AddWithValue("p_password", password);
                return Convert.ToInt32(await cmd.ExecuteScalarAsync());
            }
        }
    }
    public async Task<List<Book>> GetBooksAsync(string searchTerm = null, string searchBy = null)
    {
        var books = new List<Book>();
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new NpgsqlCommand("SELECT isbn, title, description, authors FROM public.get_books(@p_search_term, @p_search_by)", connection))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("p_search_term", string.IsNullOrEmpty(searchTerm) ? DBNull.Value : (object)searchTerm);
                command.Parameters.AddWithValue("p_search_by", string.IsNullOrEmpty(searchBy) ? DBNull.Value : (object)searchBy);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var book = new Book
                        {
                            ISBN = reader.GetString(reader.GetOrdinal("isbn")),
                            Title = reader.GetString(reader.GetOrdinal("title")),
                            Description = reader.IsDBNull(reader.GetOrdinal("description")) ? string.Empty : reader.GetString(reader.GetOrdinal("description")),
                            Authors = reader.IsDBNull(reader.GetOrdinal("authors")) ? new List<Author>() : ((string[])reader.GetValue(reader.GetOrdinal("authors"))).Select(fullName => new Author { FullName = fullName }).ToList()
                        };
                        books.Add(book);
                    }
                }
            }
        }
        return books;
    }

    public async Task<Book> GetBookDetailsAsync(string isbn)
    {
        Book book = null;
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new NpgsqlCommand("SELECT * FROM public.get_book_details(@p_isbn)", connection))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("p_isbn", isbn);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    List<Review> reviews = new List<Review>();
                    while (await reader.ReadAsync())
                    {
                        string currentIsbn = reader.GetString(reader.GetOrdinal("isbn"));

                        if (book == null)
                        {
                            book = new Book
                            {
                                ISBN = currentIsbn,
                                Title = reader.GetString(reader.GetOrdinal("title")),
                                Description = reader.IsDBNull(reader.GetOrdinal("description")) ? string.Empty : reader.GetString(reader.GetOrdinal("description")),
                                Authors = reader.IsDBNull(reader.GetOrdinal("authors")) ? new List<Author>() : ((string[])reader.GetValue(reader.GetOrdinal("authors"))).Select(fullName => new Author { FullName = fullName }).ToList(),
                                Reviews = new List<Review>()
                            };
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("review_id")))
                        {
                            reviews.Add(new Review
                            {
                                ReviewId = reader.GetInt32(reader.GetOrdinal("review_id")),
                                ReaderId = reader.GetInt32(reader.GetOrdinal("reader_id")),
                                Text = reader.GetString(reader.GetOrdinal("review_text")),
                                Rating = reader.GetInt32(reader.GetOrdinal("rating")),
                                Date = reader.GetDateTime(reader.GetOrdinal("review_date"))
                            });
                        }
                    }

                    if (book != null)
                    {
                        book.Reviews = reviews;
                    }
                }
            }
        }
        return book;
    }

    public async Task<List<Copy>> GetCopiesAsync(string isbn)
    {
        var copies = new List<Copy>();
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new NpgsqlCommand("SELECT copy_id, date_published, publish_country, returned FROM public.get_copies(@p_isbn)", connection))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("p_isbn", isbn);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        copies.Add(new Copy
                        {
                            CopyId = reader.GetInt32(reader.GetOrdinal("copy_id")),
                            DatePublished = reader.GetDateTime(reader.GetOrdinal("date_published")),
                            PublishCountry = reader.GetString(reader.GetOrdinal("publish_country")),
                            Returned = reader.GetBoolean(reader.GetOrdinal("returned"))
                        });
                    }
                }
            }
            return copies;
        }
    }

    public async Task<List<Reader>> GetReadersAsync(string searchTerm = null)
    {
        var readers = new List<Reader>();
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new NpgsqlCommand("SELECT * FROM public.get_readers(@p_search_term)", connection))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("p_search_term", string.IsNullOrEmpty(searchTerm) ? DBNull.Value : (object)searchTerm);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        readers.Add(new Reader
                        {
                            ReaderId = reader.GetInt32(reader.GetOrdinal("reader_id")),
                            FullName = reader.GetString(reader.GetOrdinal("full_name")),
                            PhoneNumber = reader.GetString(reader.GetOrdinal("phone_number")),
                            Address = reader.GetString(reader.GetOrdinal("address"))
                        });
                    }
                }
            }
        }
        return readers;
    }

    public async Task<bool> IssueBookAsync(int copyId, int readerId)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new NpgsqlCommand("SELECT public.issue_book(@p_copy_id, @p_reader_id)", connection))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("p_copy_id", copyId);
                command.Parameters.AddWithValue("p_reader_id", readerId);

                var result = await command.ExecuteScalarAsync();
                return result != null && (bool)result;
            }
        }
    }

    public async Task<string> GetBookTitleByCopyIdAsync(int copyId)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new NpgsqlCommand("SELECT public.get_book_title_by_copy_id(@p_copy_id)", connection))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("p_copy_id", copyId);
                var result = await command.ExecuteScalarAsync();
                return result?.ToString();
            }
        }
    }

    public async Task<string> GetReaderFullNameByIdAsync(int readerId)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new NpgsqlCommand("SELECT public.get_reader_full_name_by_id(@p_reader_id)", connection))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("p_reader_id", readerId);
                var result = await command.ExecuteScalarAsync();
                return result?.ToString();
            }
        }
    }

    public async Task<bool> ReturnBookByCopyIdAsync(int copyId)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new NpgsqlCommand("SELECT public.return_book(@p_copy_id)", connection))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("p_copy_id", copyId);
                var result = await command.ExecuteScalarAsync();
                return result != null && (bool)result;
            }
        }
    }

    public async Task<bool> ReturnBookAsync(int borrowingId)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new NpgsqlCommand("SELECT public.return_book_by_borrowing_id(@p_borrowing_id)", connection))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("p_borrowing_id", borrowingId);
                var result = await command.ExecuteScalarAsync();
                return result != null && (bool)result;
            }
        }
    }

    public async Task<List<Borrowing>> SearchBorrowedBooksAsync(string readerSearchTerm = null, string bookSearchTerm = null)
    {
        var borrowings = new List<Borrowing>();
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new NpgsqlCommand("SELECT * FROM public.search_borrowed_books(@p_reader_search_term, @p_book_search_term)", connection))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("p_reader_search_term", string.IsNullOrEmpty(readerSearchTerm) ? DBNull.Value : (object)readerSearchTerm);
                command.Parameters.AddWithValue("p_book_search_term", string.IsNullOrEmpty(bookSearchTerm) ? DBNull.Value : (object)bookSearchTerm);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        borrowings.Add(new Borrowing
                        {
                            BorrowingId = reader.GetInt32(reader.GetOrdinal("borrowing_id")),
                            CopyId = reader.GetInt32(reader.GetOrdinal("copy_id")),
                            ReaderId = reader.GetInt32(reader.GetOrdinal("reader_id")),
                            BorrowDate = reader.GetDateTime(reader.GetOrdinal("borrow_date")),
                            ReturnDate = reader.IsDBNull(reader.GetOrdinal("return_date")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("return_date"))
                        });
                    }
                }
            }
        }
        return borrowings;
    }
    public async Task<List<User>> GetUsersAsync(string searchTerm = null)
    {
        var users = new List<User>();
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new NpgsqlCommand("SELECT * FROM public.get_users(@p_search_term)", connection))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("p_search_term", string.IsNullOrEmpty(searchTerm) ? DBNull.Value : (object)searchTerm);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        users.Add(new User
                        {
                            UserId = reader.GetInt32(reader.GetOrdinal("user_id")),
                            FullName = reader.GetString(reader.GetOrdinal("full_name")),
                            Role = reader.GetString(reader.GetOrdinal("role")),
                            Login = reader.GetString(reader.GetOrdinal("login"))
                        });
                    }
                }
            }
        }
        return users;
    }
    public async Task<bool> UpdateUserRoleAsync(int userId, string newRole)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new NpgsqlCommand("SELECT public.update_user_role(@userId, @newRole)", connection))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@newRole", newRole);

                var result = await command.ExecuteScalarAsync();
                return result != null && (bool)result;
            }
        }
    }

    public async Task<List<BookReportItem>> GetBookReportAsync(string titleFilter = null)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandType = CommandType.Text;

                if (string.IsNullOrEmpty(titleFilter))
                {
                    cmd.CommandText = "SELECT * FROM generate_report()";
                }
                else
                {
                    cmd.CommandText = "SELECT * FROM generate_report(@title_filter)";
                    cmd.Parameters.AddWithValue("title_filter", titleFilter);
                }

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    var reportData = new List<BookReportItem>();
                    while (await reader.ReadAsync())
                    {
                        reportData.Add(new BookReportItem
                        {
                            Title = reader.GetString(0),
                            TotalCopies = reader.GetInt64(1),
                            ReturnedCopies = reader.GetInt64(2),
                            NotReturnedCopies = reader.GetInt64(3),
                            NextReturnDate = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4)
                        });
                    }
                    return reportData;
                }
            }
        }
    }

    public async Task AddBookAsync(string isbn, string title, string description)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var cmd = new NpgsqlCommand("CALL public.add_book(@p_isbn, @p_title, @p_description)", connection))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("p_isbn", isbn);
                cmd.Parameters.AddWithValue("p_title", title);
                cmd.Parameters.AddWithValue("p_description", description);
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task AddCopyAsync(string isbn, DateTime datePublished, string publishCountry)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var cmd = new NpgsqlCommand("CALL public.add_copy(@p_isbn, @p_date_published, @p_publish_country)", connection))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("p_isbn", isbn);
                cmd.Parameters.AddWithValue("p_date_published", datePublished);
                cmd.Parameters.AddWithValue("p_publish_country", publishCountry);
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }


    public async Task<int> AddAuthorAsync(string fullName, string penName, DateTime birthDate, string isbn)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var cmd = new NpgsqlCommand("SELECT public.add_author(@p_full_name, @p_pen_name, @p_birth_date, @p_isbn)", connection))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("p_full_name", fullName);
                cmd.Parameters.AddWithValue("p_pen_name", string.IsNullOrEmpty(penName) ? DBNull.Value : (object)penName);
                cmd.Parameters.AddWithValue("p_birth_date", birthDate);
                cmd.Parameters.AddWithValue("p_isbn", isbn);
                return Convert.ToInt32(await cmd.ExecuteScalarAsync());
            }
        }
    }

    public async Task DeleteBookAsync(string isbn)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var cmd = new NpgsqlCommand("CALL public.delete_book(@p_isbn)", connection))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("p_isbn", isbn);
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task DeleteCopyAsync(int copyId)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var cmd = new NpgsqlCommand("CALL public.delete_copy(@p_copy_id)", connection))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("p_copy_id", copyId);
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task DeleteAuthorAsync(int authorId)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var cmd = new NpgsqlCommand("CALL public.delete_author(@p_author_id)", connection))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("p_author_id", authorId);
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task AddBookAuthorAsync(string isbn, int authorId)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var cmd = new NpgsqlCommand("CALL public.add_book_author(@p_isbn, @p_author_id)", connection))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("p_isbn", isbn);
                cmd.Parameters.AddWithValue("p_author_id", authorId);
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task DeleteAuthorFromBookAsync(string isbn, int authorId)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var cmd = new NpgsqlCommand("CALL public.delete_author_from_book(@p_isbn, @p_author_id)", connection))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("p_isbn", isbn);
                cmd.Parameters.AddWithValue("p_author_id", authorId);
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task<List<Author>> GetAuthorsByISBNAsync(string isbn, string searchText = null)
    {
        var authors = new List<Author>();
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var cmd = new NpgsqlCommand("SELECT * FROM public.get_authors_by_isbn(@p_isbn, @p_search_text)", connection))
            {
                cmd.Parameters.AddWithValue("p_isbn", isbn);
                cmd.Parameters.AddWithValue("p_search_text", string.IsNullOrEmpty(searchText) ? DBNull.Value : (object)searchText);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var author = new Author
                        {
                            AuthorId = reader.GetInt32(0),
                            FullName = reader.GetString(1),
                            PenName = reader.IsDBNull(2) ? null : reader.GetString(2),
                            BirthDate = reader.GetDateTime(3)
                        };
                        authors.Add(author);
                    }
                }
            }
        }
        return authors;
    }


}