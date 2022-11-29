-- SQL code lines to Select all columns in a specific table
SELECT * FROM librarian;
SELECT * FROM book;
SELECT * FROM library;
SELECT * FROM account;
SELECT * FROM author;
SELECT * FROM media;
SELECT * FROM permission;
SELECT * FROM loan;
SELECT * FROM author_to_book;

-- Select account with admin permission and show what library they work at
SELECT lib.name AS 'library_name', a.first_name, a.last_name, p.name AS 'role'
FROM librarian lib_emp
         INNER JOIN account a ON lib_emp.account_id = a.id
         INNER JOIN library lib ON lib_emp.library_id = lib.id
         INNER JOIN permission p ON a.permission_id = p.id;

-- Select all books and show media name and library city
SELECT b.title AS 'book_title', m.name AS 'format', l.city AS 'location', b.available
FROM book b
         INNER JOIN media m on b.media_id = m.id
         INNER JOIN library l on b.library_id = l.id;

-- Select all audio books and show media name and library city
SELECT b.title AS 'book_title', m.name AS 'format', l.city AS 'location', b.available
FROM book b
         INNER JOIN media m on b.media_id = m.id
         INNER JOIN library l on b.library_id = l.id
WHERE m.id = 2;


-- Select all books from specific library
SELECT b.title AS 'book_title', m.name AS 'format', l.city AS 'location', b.available
FROM book b
         INNER JOIN media m on b.media_id = m.id
         INNER JOIN library l on b.library_id = l.id
WHERE l.id = 1;


-- Select sum of all books
SELECT COUNT(b.id) AS 'amount_of_books'
FROM book b;

-- Select sum of all physical books and audio books
SELECT COUNT(b.id) AS 'amount_of_audio_and_physical_books'
FROM book b
WHERE b.media_id = 1 OR b.media_id = 2;

-- Select sum of all books in a specific library
SELECT COUNT(b.id) AS 'amount_of_books'
FROM book b
WHERE b.library_id = 1;

-- Select all book and author
SELECT a.name AS 'Author', b.title AS 'book_title'
FROM author a
         INNER JOIN author_to_book atb ON a.id = atb.author_id
         INNER JOIN book b ON atb.book_id = b.id;

-- Select a specific book title simply with the name of 'harry'
SELECT *
FROM book b
WHERE b.title LIKE '%harry%';


-- Select a specific book title simply with the name of 'harry' that is also available to loan out
SELECT *
FROM book b
WHERE b.title LIKE '%harry%' AND b.available = 1;