CREATE TABLE books(
	Id int(10) AUTO_INCREMENT PRIMARY KEY,
    Author LONGTEXT,
    LaunchDate datetime(6) NOT NULL,
    Price DECIMAL(65,2) NOT NULL,
    Title LONGTEXT
)ENGINE InnoDB DEFAULT CHARSET=latin1;