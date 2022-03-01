--1. Listar los datos de los autores que tengan más de un libro publicado 
select a.Name,a.Nationality, count(ba.bookId) from BookAuthor ba
left join Author a on ba.AuthorId = a.Id
group by ba.AuthorId, a.Name,a.Nationality
having count(ba.bookId)>=2

--2. Listar nombre y edad de los estudiantes
select s.Name,s.Age from Student s

--3. ¿Qué estudiantes pertenecen a la carrera de Informática?
select s.Name,s.Age from Student s where s.Career ='Informatica'

--4. Listar los nombres de los estudiantes cuyo nombre comience con la letra G?
select s.Name,s.Age from Student s where s.Name Like 'G%'

--5. ¿Quiénes son los autores del libro “Visual Studio Net”, listar solamente los nombres?
select s.Name,s.Age from Student s where s.Name Like 'G%'

-- Autores del libro “Visual Studio Net”, listar solamente los nombres

select a.Name from BookAuthor ba
inner join Author a on ba.AuthorId = a.Id
inner join Book b on b.Id = ba.BookId
where b.Name = 'Visual Studio Net'

--6. ¿Qué autores son de nacionalidad USA o Francia?7. ¿Qué libros No Son del Area de Internet?
select a.Name from Author a 
where a.Nationality in ('USA','FRANCIA')

--¿Qué libros No Son del Area de Internet?
select a.Name from Book a 
where a.Area NOT in ('Internet')

--8. ¿Qué libros se prestó el Lector “Felipe Loayza Beramendi”?
select a.Name from Book a 
inner join BookRequest br on a.Id = br.BookId
inner join Student s on s.Id =  br.StudentId
where s.Name = 'Felipe Loayza Beramendi'

--9. Listar el nombre del estudiante de menor edad
select s.Name,s.Age from Student s where s.Age <= 18

--10. Listar los nombres de los estudiantes que se prestaron Libros de Base de Datos
select b.Name from Book b where b.Area = 'Base de datos'

--11. Listar los libros de editorial Alfa y Omega
select b.Name,b.Publisher from Book b where b.Publisher in ('Alfa', 'Omega')

--12. Listar los libros que pertenecen al autor Mario Benedetti
select b.Name,b.Publisher from Book b 
inner join BookAuthor ba on b.Id = ba.BookId
inner join Author a on a.Id =  ba.AuthorId
where a.Name = 'Benedetti, Mario'

--14. Hallar la suma de las edades de los estudiantes
select SUM(s.Age) SumAge from Student  s 

--15. Listar los datos de los estudiantes cuya edad es mayor al promedio
select s.Name , (SUM(s.Age) / COUNT(s.Age))from Student  s 
GROUP BY s.Name, s.Age
HAVING s.Age >= ( select SUM(s.Age)/ Count(s.Age) from Student  s)
