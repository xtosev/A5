CREATE TABLE Nastavnik(
	NastavnikID INT NOT NULL PRIMARY KEY,
	Ime VARCHAR(50) NOT NULL
);
 
ALTER TABLE Aktivnost
ADD NastavnikID INT,
CONSTRAINT FK_Aktivnost_Nastavnik FOREIGN KEY (NastavnikID) REFERENCES Nastavnik(NastavnikID);

ALTER TABLE Dete
ADD DatumPrijema DATE,
CONSTRAINT CHK_DatumPrijema CHECK(DatumPrijema > DatumRodjenja)