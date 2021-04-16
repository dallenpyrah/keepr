USE finalprojectapi;

-- CREATE TABLE profiles
-- (
--   id VARCHAR(255) NOT NULL,
--   email VARCHAR(255) NOT NULL,
--   name VARCHAR(255),
--   picture VARCHAR(255),
--   PRIMARY KEY (id)
-- );
DROP TABLE keeps;
CREATE TABLE keeps 
(
    id INT NOT NULL AUTO_INCREMENT,
    creatorId VARCHAR(255) NOT NULL,
    name VARCHAR(255) NOT NULL,
    description VARCHAR(255) NOT NULL,
    img VARCHAR(255) NOT NULL,
    views INT DEFAULT 0,
    shares INT DEFAULT 0,
    keeps INT DEFAULT 0,

    PRIMARY KEY (id),

    FOREIGN KEY (creatorId)
    REFERENCES profiles (id)
    ON DELETE CASCADE
)