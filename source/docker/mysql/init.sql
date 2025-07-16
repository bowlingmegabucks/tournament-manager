-- Switch to the desired database
USE bowling_megabucks;

GRANT ALL PRIVILEGES ON bowling_megabucks.* TO 'local_user'@'%';

FLUSH PRIVILEGES;