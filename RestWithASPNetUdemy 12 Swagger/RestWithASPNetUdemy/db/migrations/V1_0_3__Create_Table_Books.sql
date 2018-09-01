CREATE TABLE IF NOT EXISTS books(
	Id VARCHAR(127) NOT NULL,
    Author LONGTEXT,
    LaunchDate datetime(6) NOT NULL,
    Price DECIMAL(65,2) NOT NULL,
    Title LONGTEXT,
    PRIMARY KEY(Id)
)ENGINE InnoDB DEFAULT CHARSET=latin1;