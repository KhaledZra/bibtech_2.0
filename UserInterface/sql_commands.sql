-- Start setup DB
START TRANSACTION;

CREATE TABLE library
(
    id INT AUTO_INCREMENT,
    name VARCHAR(32) NOT NULL,
    adress VARCHAR(32) NOT NULL,
    city VARCHAR(32) NOT NULL,

    PRIMARY KEY (id)
);

CREATE TABLE permission
(
    id INT AUTO_INCREMENT,
    name VARCHAR(32) NOT NULL,

    PRIMARY KEY (id)
);


CREATE TABLE account
(
    id INT AUTO_INCREMENT,
    user_name VARCHAR(32) NOT NULL,
    password VARCHAR(32) NOT NULL,
    first_name VARCHAR(32) NOT NULL,
    last_name VARCHAR(32) NOT NULL,
    email VARCHAR(32) NOT NULL,
    phone_number VARCHAR(32) NOT NULL,
    permission_id INT NOT NULL,

    PRIMARY KEY (id),
    FOREIGN KEY (permission_id) REFERENCES permission(id)
);

CREATE TABLE librarian
(
    id INT AUTO_INCREMENT,
    library_id INT NOT NULL,
    account_id INT NOT NULL,

    PRIMARY KEY (id),
    FOREIGN KEY (library_id) REFERENCES library(id),
    FOREIGN KEY (account_id) REFERENCES account(id)
);

CREATE TABLE media
(
    id INT AUTO_INCREMENT,
    name VARCHAR(32) NOT NULL,

    PRIMARY KEY (id)
);

CREATE TABLE book
(
    id INT AUTO_INCREMENT,
    title VARCHAR(32) NOT NULL,
    media_id INT NOT NULL,
    library_id INT NOT NULL,

    PRIMARY KEY (id),
    FOREIGN KEY (media_id) REFERENCES media(id),
    FOREIGN KEY (library_id) REFERENCES library(id)
);

CREATE TABLE loan
(
    id INT AUTO_INCREMENT,
    start_date date NOT NULL,
    due_date date NULL,
    book_id INT NOT NULL,
    user_id INT NOT NULL,

    PRIMARY KEY (id),
    FOREIGN KEY (book_id) REFERENCES book(id),
    FOREIGN KEY (user_id) REFERENCES account(id)
);


CREATE TABLE author
(
    id INT AUTO_INCREMENT,
    name VARCHAR(32) NOT NULL,

    PRIMARY KEY (id)
);

CREATE TABLE author_to_book
(
    author_id INT NOT NULL,
    book_id INT NOT NULL,

    PRIMARY KEY (author_id, book_id),
    FOREIGN KEY (author_id) REFERENCES author(id),
    FOREIGN KEY (book_id) REFERENCES book(id)
);

COMMIT;


-- Insert media
START TRANSACTION;
INSERT INTO media(name) VALUES ('Physical'), ('Audio book'), ('E-book');
COMMIT;


-- Insert Permission
START TRANSACTION;
INSERT INTO permission(name) VALUES ('Admin'), ('User');
COMMIT;


-- Insert Library
START TRANSACTION;
INSERT INTO library(name, adress, city) VALUES ('Bobloteket', 'Street 11 51162', 'Boras'),
                                               ('Gotateket', 'Street 51 71222', 'Stockholm'),
                                               ('Stockateket', 'Street 99 11345', 'Gothenberg');
COMMIT;


-- Insert Author
START TRANSACTION;
INSERT INTO author(name) VALUES ('J.K Rowling'), ('George R.R. Martin'), ('J.R.R. Tolkien');
COMMIT;


-- Insert Book
START TRANSACTION;
INSERT INTO book(title, media_id, library_id) VALUES ('Harry Potter', 1, 1);
INSERT INTO book(title, media_id, library_id) VALUES ('Harry Potter', 2, 1);
INSERT INTO book(title, media_id, library_id) VALUES ('Harry Potter', 3, 1);

INSERT INTO book(title, media_id, library_id) VALUES ('Song of Ice and Fire', 1, 2);
INSERT INTO book(title, media_id, library_id) VALUES ('Song of Ice and Fire', 2, 2);
INSERT INTO book(title, media_id, library_id) VALUES ('Song of Ice and Fire', 3, 2);

INSERT INTO book(title, media_id, library_id) VALUES ('The Lord of the Rings', 1, 3);
INSERT INTO book(title, media_id, library_id) VALUES ('The Lord of the Rings', 2, 3);
INSERT INTO book(title, media_id, library_id) VALUES ('The Lord of the Rings', 3, 3);
COMMIT;


-- Insert author_to_book
START TRANSACTION;
INSERT INTO author_to_book(author_id, book_id) VALUES (1, 1), (1, 2), (1, 3),
                                                      (2, 4), (2, 5), (2, 6),
                                                      (3, 7), (3, 8), (3, 9);
COMMIT;


-- Insert Author
START TRANSACTION;
INSERT INTO account(user_name, password, first_name, last_name, email, phone_number, permission_id)
VALUES ('kalle', 'pw123', 'Khaled', ' Zraiqi', 'khaled@gmail.com', '0736972075', 1),
       ('shah', 'pw123', 'Shahin', ' Zraiqi', 'shahin@gmail.com', '0724572122', 2),
       ('rally', 'pw123', 'Tariq', ' Zraiqi', 'tariq@gmail.com', '0711111111', 2),
       ('konstans', 'pw123', 'Tinos', ' Zraiqi', 'tinos@gmail.com', '0722222222', 2);
COMMIT;


-- Insert librarian
START TRANSACTION;
INSERT INTO librarian(library_id, account_id) VALUES (1, 1);
COMMIT;


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