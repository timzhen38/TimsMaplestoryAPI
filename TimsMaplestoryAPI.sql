CREATE DATABASE MapleStoryPlayerData;
USE MapleStoryPlayerData;
CREATE TABLE Players(
	PlayerId INT NOT NULL AUTO_INCREMENT,
    PlayerName VARCHAR(50) NOT NULL,
    PlayerLevel INT NOT NULL,
    PRIMARY KEY (PlayerId),
    UNIQUE(PlayerId,PlayerName)
);
CREATE TABLE Classes(
	ClassId INT NOT NULL AUTO_INCREMENT,
    ClassName VARCHAR(50) NOT NULL,
    PRIMARY KEY(ClassId),
    UNIQUE(ClassId,ClassName)
);
ALTER TABLE Players ADD COLUMN ClassId INT;
ALTER TABLE Players ADD CONSTRAINT FK_PlayerClass FOREIGN KEY (ClassId) REFERENCES Classes(ClassId);
SELECT * FROM Classes;
SELECT * FROM Players;
INSERT INTO Classes(ClassName) VALUES ("BEGINNER");			#id = 1		
INSERT INTO Classes(ClassName) VALUES ("HERO");				#2	
INSERT INTO Classes(ClassName) VALUES ("PALADIN");			#3
INSERT INTO Classes(ClassName) VALUES ("DARK_KNIGHT");		#4
INSERT INTO Classes(ClassName) VALUES ("FIRE_POISON");		#5
INSERT INTO Classes(ClassName) VALUES ("ICE_LIGHTNING");	#6
INSERT INTO Classes(ClassName) VALUES ("BISHOP");			#7
INSERT INTO Classes(ClassName) VALUES ("BOWMASTER");		#8
INSERT INTO Classes(ClassName) VALUES ("MARKSMAN");			#9
INSERT INTO Classes(ClassName) VALUES ("NIGHT_LORD");		#10
INSERT INTO Classes(ClassName) VALUES ("SHADOWER");			#11
INSERT INTO Classes(ClassName) VALUES ("DUAL_BLADE");		#12	
INSERT INTO Classes(ClassName) VALUES ("BUCCANEER");		#13
INSERT INTO Classes(ClassName) VALUES ("CORSAIR");			#14
INSERT INTO Classes(ClassName) VALUES ("CANNON_MASTER");	#15
INSERT INTO Players(PlayerName,ClassId,PlayerLevel) VALUES ("mayo", 1, 10);
INSERT INTO Players(PlayerName,ClassId,PlayerLevel) VALUES ("tim", 1, 200);
INSERT INTO Players(PlayerName,ClassId,PlayerLevel) VALUES ("ketchup", 3, 140);
INSERT INTO Players(PlayerName,ClassId,PlayerLevel) VALUES ("mustard", 10, 210);
DROP TABLE Players;
DROP TABLE Classes;