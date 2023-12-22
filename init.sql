BEGIN;
--Create towns table
CREATE TABLE IF NOT EXISTS towns (
    Id INTEGER PRIMARY KEY,
    name varchar(40)
);
--Create towns advertcategories
CREATE TABLE IF NOT EXISTS advertcategories (
    Id INTEGER PRIMARY KEY,
    name varchar(100)
);
--Create towns vehiclemodels
CREATE TABLE IF NOT EXISTS vehiclemodels (
    Id INTEGER PRIMARY KEY,
    name varchar(100)
);
--Create towns adverts
CREATE TABLE IF NOT EXISTS adverts (
    Id INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    MemberId INTEGER NOT NULL,
    CityId INTEGER NOT NULL,
    CityName VARCHAR(14) NOT NULL,
    TownId INTEGER NOT NULL,
    TownName VARCHAR(40) NOT NULL,
    ModelId INTEGER NOT NULL,
    ModelName VARCHAR(100) NOT NULL,
    Year INTEGER NOT NULL,
    Price INTEGER NOT NULL,
    Title VARCHAR(250) UNIQUE NOT NULL,
    Date VARCHAR(23) NOT NULL,
    CategoryId INTEGER NOT NULL,
    CategoryName VARCHAR(100) NOT NULL,
    KM INTEGER NOT NULL,
    Color VARCHAR(15),
    Gear VARCHAR(13) NOT NULL,
    Fuel VARCHAR(12) NOT NULL,
    FirstPhoto VARCHAR(135) NOT NULL,
    SecondPhoto VARCHAR(135) NOT NULL,
    UserInfo VARCHAR(19) NOT NULL,
    UserPhone VARCHAR(10),
    Text TEXT
);


---adverts fk towns
ALTER TABLE IF EXISTS adverts
ADD CONSTRAINT fk_adverts_towns FOREIGN KEY (TownId) REFERENCES towns(Id);

---adverts fk advertcategories
ALTER TABLE IF EXISTS adverts
ADD CONSTRAINT fk_adverts_categories FOREIGN KEY (CategoryId) REFERENCES advertcategories(Id);

---adverts fk vehiclemodels
ALTER TABLE IF EXISTS adverts
ADD CONSTRAINT fk_adverts_vehiclemodels FOREIGN KEY (ModelId) REFERENCES vehiclemodels(Id);


CREATE TABLE IF NOT EXISTS advertvisits (
    id INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    advertid int not null,
    ipaddress varchar(15) not null,
    visitdate TIMESTAMP  not null
);
---advertvisits fk adverts
ALTER TABLE IF EXISTS advertvisits
ADD CONSTRAINT fk_advertvisits_adverts FOREIGN KEY (advertid) REFERENCES adverts(Id);


COMMIT;


DROP TABLE IF EXISTS advertCsv;
CREATE TEMP TABLE advertCsv (
    Id INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    MemberId INTEGER NOT NULL,
    CityId INTEGER NOT NULL,
    CityName VARCHAR(14) NOT NULL,
    TownId INTEGER NOT NULL,
    TownName VARCHAR(40) NOT NULL,
    ModelId INTEGER NOT NULL,
    ModelName VARCHAR(100) NOT NULL,
    Year INTEGER NOT NULL,
    Price INTEGER NOT NULL,
    Title VARCHAR(250) NOT NULL,
    Date VARCHAR(23) NOT NULL,
    CategoryId INTEGER NOT NULL,
    CategoryName VARCHAR(100) NOT NULL,
    KM INTEGER NOT NULL,
    Color VARCHAR(15),
    Gear VARCHAR(13) NOT NULL,
    Fuel VARCHAR(12) NOT NULL,
    FirstPhoto VARCHAR(135) NOT NULL,
    SecondPhoto VARCHAR(135) NOT NULL,
    UserInfo VARCHAR(19) NOT NULL,
    UserPhone VARCHAR(10),
    Text TEXT
);

COPY advertCsv FROM '/Adverts.csv' DELIMITER ',' CSV header;
INSERT INTO public.towns (id,name)
SELECT townid,townname FROM advertCsv ON CONFLICT (id) DO NOTHING;

INSERT INTO public.advertcategories (id,name)
SELECT categoryid,categoryname FROM advertCsv ON CONFLICT (id) DO NOTHING;

INSERT INTO public.vehiclemodels (id,name)
SELECT modelid,modelname FROM advertCsv ON CONFLICT (id) DO NOTHING;

INSERT INTO public.adverts (memberid,cityid,cityname,townid,townname,modelid,modelname,year,price,title,date,categoryid,categoryname,km,color,gear,fuel,firstphoto,secondphoto,userinfo,userphone,text)
SELECT memberid,cityid,cityname,townid,townname,modelid,modelname,year,price,title,date,categoryid,categoryname,km,color,gear,fuel,firstphoto,secondphoto,userinfo,userphone,text FROM advertCsv ON CONFLICT (title) DO NOTHING;
---
COPY advertvisits(advertId,iPAdress,visitDate) FROM '/AdvertVisits.csv' DELIMITER ',' CSV HEADER;
DROP TABLE IF EXISTS advertCsv;




