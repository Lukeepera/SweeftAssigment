create table Teacher (
	TeacherID int identity(1,1) primary key ,
	FirstName varchar(50),
	LastName varchar(50),
	Gender char(1),
	Subject varchar(50)
)

create table Pupil (
	PupilID int identity(1,1) primary key,
	FirstName varchar(50),
	LastName varchar(50),
	Gender char(1),
	Class varchar(50)
)

create table TeacherToPupil (
    TeacherID int,    
    PupilID int,
    foreign key (TeacherID) references Teacher(TeacherID),
    foreign key (PupilID) references Pupil(PupilID),
    primary key (TeacherID, PupilID)
) 
--ვქმნი მესამე ცხრილს რომ ჩამოვაყალიბო "Many to many relationship" მოსწავლეებს და მასწავლებლებს შორის.

select t.FirstName, t.LastName
from Teacher t
join TeacherToPupil tp on t.TeacherID = tp.TeacherID
join Pupil p on tp.PupilID = p.PupilID
where p.FirstName = 'Giorgi'


