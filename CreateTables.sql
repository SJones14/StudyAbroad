CREATE TABLE ADMINS(
	Admin_id INT Primary Key,
	UserName char(100) NOT NULL,
	Password char (128) NOT NULL,
	Salt char(20) NOT NULL
);
CREATE TABLE PROGRAMS(
	Program_ID INT PRIMARY KEY,
	Major char(20) NOT NULL
);
CREATE TABLE UNIVERSITIES(
	University_ID INT PRIMARY KEY,
	Country char(60) NOT NULL,
	Continent char(15) NOT NULL
);
CREATE TABLE HOUSING_TYPE(
	HousingType_ID INT PRIMARY KEY,
	HousingType char(50) NOT NULL,
	HousingDescription char(200) NOT NULL
);