-- 1. Подсчет количества книг для каждого автора:
SELECT a.full_name, COUNT(ab.ISBN) as book_count
FROM authors a
JOIN authors_books ab ON a.author_id = ab.author_id
GROUP BY a.full_name
ORDER BY book_count DESC;

-- 2. Книги с высокими оценками (более 4):
SELECT b.title, AVG(r.rating) as average_rating
FROM books b
JOIN reviews r ON b.ISBN = r.ISBN
GROUP BY b.title
HAVING AVG(r.rating) > 4
ORDER BY average_rating DESC;

-- 3. Количество экземпляров каждой книги:
SELECT b.title, COUNT(c.copy_id) AS copy_count
FROM books b
JOIN copies c ON b.ISBN = c.ISBN
GROUP BY b.title
ORDER BY copy_count DESC;

-- 4. Средний рейтинг отзывов каждого читателя:
SELECT r.full_name, AVG(rev.rating) AS avg_rating
FROM readers r
JOIN reviews rev ON r.reader_id = rev.reader_id
GROUP BY r.full_name;

-- 5. Средний рейтинг книг каждого автора:
SELECT a.full_name, AVG(r.rating) AS avg_rating
FROM authors a
JOIN authors_books ab ON a.author_id = ab.author_id
JOIN reviews r ON ab.ISBN = r.ISBN
GROUP BY a.full_name
ORDER BY avg_rating DESC;

-- 6. Читатели, которые брали книги, но не оставили отзыва
SELECT full_name
FROM readers
EXCEPT
SELECT full_name
FROM readers r
JOIN reviews rev ON r.reader_id = rev.reader_id;

-- 7. Книги, которые были опубликованы в России:
SELECT b.ISBN, b.title
FROM books b
JOIN copies c ON b.ISBN = c.ISBN
WHERE c.publish_country = 'Россия';

-- 8. Авторы, которые родились в 20 веке:
SELECT a.author_id, a.full_name
FROM authors a
WHERE to_char(a.birth_date, 'YYYY') BETWEEN '1900' AND '2000';

-- 9. 3 самых длинных отзыва:
SELECT LENGTH(r.text) AS review_length, r.review_id, b.title, r.text
FROM reviews AS r
JOIN books AS b ON r.ISBN = b.ISBN
ORDER BY review_length DESC
LIMIT 3;

-- 10. Cредняя продолжительность аренды книг
SELECT AVG(EXTRACT(DAY FROM age(end_date, start_date)))
FROM borrowing;