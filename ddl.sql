DROP TABLE IF EXISTS authors CASCADE;
CREATE TABLE authors (
    author_id INTEGER PRIMARY KEY NOT NULL UNIQUE,
    full_name VARCHAR(255) NOT NULL,
    pen_name VARCHAR(255),
    birth_date DATE NOT NULL CHECK (birth_date < CURRENT_DATE)
);

DROP TABLE IF EXISTS books CASCADE;
CREATE TABLE books (
    ISBN VARCHAR(13) PRIMARY KEY NOT NULL UNIQUE,
    title VARCHAR(255) NOT NULL,
    description TEXT
);

DROP TABLE IF EXISTS authors_books CASCADE;
CREATE TABLE authors_books (
    author_id INTEGER NOT NULL,
    ISBN VARCHAR(13) NOT NULL,
    PRIMARY KEY (author_id, ISBN),
    FOREIGN KEY (author_id) REFERENCES authors(author_id),
    FOREIGN KEY (ISBN) REFERENCES books(ISBN)
);

DROP TABLE IF EXISTS copies CASCADE;
CREATE TABLE copies (
    copy_id INTEGER PRIMARY KEY NOT NULL UNIQUE,
    date_published DATE NOT NULL CHECK (date_published < CURRENT_DATE),
    publish_country VARCHAR(255) NOT NULL,
    ISBN VARCHAR(13) NOT NULL,
    FOREIGN KEY (ISBN) REFERENCES books(ISBN)
);

DROP TABLE IF EXISTS readers CASCADE;
CREATE TABLE readers (
    reader_id INTEGER PRIMARY KEY NOT NULL UNIQUE,
    full_name VARCHAR(255) NOT NULL,
    phone_number VARCHAR(15) NOT NULL,
    address VARCHAR(511) NOT NULL
);

DROP TABLE IF EXISTS reviews CASCADE;
CREATE TABLE reviews (
    review_id INTEGER PRIMARY KEY NOT NULL UNIQUE,
    ISBN VARCHAR(13) NOT NULL,
    reader_id INTEGER NOT NULL,
    text TEXT NOT NULL,
    rating INTEGER NOT NULL CHECK (rating BETWEEN 1 AND 5),
    date DATE NOT NULL CHECK (date < CURRENT_DATE),
    FOREIGN KEY (ISBN) REFERENCES books(ISBN),
    FOREIGN KEY (reader_id) REFERENCES readers(reader_id)
);

DROP TABLE IF EXISTS borrowing CASCADE;
CREATE TABLE borrowing (
    borrowing_id INTEGER PRIMARY KEY NOT NULL UNIQUE,
    start_date DATE NOT NULL CHECK (start_date < CURRENT_DATE),
    end_date DATE NOT NULL CHECK (end_date < CURRENT_DATE AND end_date >= start_date),
    copy_id INTEGER NOT NULL,
    reader_id INTEGER NOT NULL,
    FOREIGN KEY (copy_id) REFERENCES copies(copy_id),
    FOREIGN KEY (reader_id) REFERENCES readers(reader_id)
);

DROP TABLE IF EXISTS book_statuses CASCADE;
CREATE TABLE book_statuses (
    ISBN VARCHAR(13) NOT NULL,
    date DATE NOT NULL CHECK (date < CURRENT_DATE),
    available INTEGER NOT NULL,
    borrowed INTEGER NOT NULL,
    next_return_date DATE,
    PRIMARY KEY (ISBN, date),
    FOREIGN KEY (ISBN) REFERENCES books(ISBN)
);