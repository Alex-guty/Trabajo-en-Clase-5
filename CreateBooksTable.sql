CREATE TABLE Libros (
  Id           INT           PRIMARY KEY IDENTITY(1,1),
  OriginalName NVARCHAR(200) NOT NULL,
  SpanishName  NVARCHAR(200) NOT NULL,
  Edition      NVARCHAR(50)  NULL,
  Year         INT           NULL,
  Editor       NVARCHAR(200) NULL
);
GO

INSERT INTO Libros (OriginalName, SpanishName, Edition, Year, Editor) VALUES
('Don Quixote',      'Don Quijote',      '1ª', 1605, 'Francisco de Robles'),
('Pride and Prejudice','Orgullo y Prejuicio','1ª',1813,'T. Egerton'),
('Moby Dick',         'Moby Dick',         '1ª',1851,'Richard Bentley'),
('The Odyssey',       'La Odisea',         '1ª',-700,'Homero'),
('The Divine Comedy', 'La Divina Comedia', '1ª',1320,'Johannes Numeister'),
('Hamlet',            'Hamlet',            '1ª',1603,'Nicholas Ling'),
('War and Peace',     'Guerra y Paz',      '1ª',1869,'The Russian Messenger'),
('Don Juan',          'Don Juan',          '1ª',1630,'Francisco de Robles'),
('Ulysses',           'Ulises',            '1ª',1922,'Sylvia Beach'),
('One Hundred Years of Solitude','Cien Años de Soledad','1ª',1967,'Sudamericana');
GO

