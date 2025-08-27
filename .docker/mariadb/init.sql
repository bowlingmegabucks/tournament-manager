-- Switch to the desired database
USE tournament_manager;

GRANT ALL PRIVILEGES ON tournament_manager.* TO 'local_user'@'%';

FLUSH PRIVILEGES;
