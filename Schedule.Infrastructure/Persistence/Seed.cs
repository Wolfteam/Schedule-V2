using Schedule.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.Infrastructure.Persistence
{
    public static class Seed
    {
        public static async Task Init(AppDbContext dbContext)
        {
            var periods = new List<Period>
            {
                new Period
                {
                    Name = "2016-II"
                },
                new Period
                {
                    Name = "2017-I"
                },
                new Period
                {
                    Name = "2017-II",
                    IsActive = true
                }
            };

            dbContext.Periods.AddRange(periods);

            var classroomTypes = new[]
            {
                "Teoria", "Laboratorio de Sistemas Eléctricos", "Laboratorio de Convertidores Eléctricos", "Laboratorio de Sistemas de Comunicación",
                "Laboratorio de Electrónica Industrial", "Laboratorio de Sistemas Digitales II",
                "Laboratorio de Sistemas Digitales I", "Laboratorio de Computación", "Laboratorio de Sistemas Electrónicos I y II",
                "Laboratorio de Sistemas Digitales III", "Laboratorio de Sistemas de Control II",
                "Laboratorio de Sistemas de Control III"
            }.Select(t => new ClassroomTypePerSubject { Name = t }).ToList();
            dbContext.ClassroomTypePerSubject.AddRange(classroomTypes);

            var careers = new[] { "Sistemas", "Mecanica", "Industrial" }.Select(c => new Career { Name = c }).ToList();
            dbContext.Careers.AddRange(careers);

            var priorities = new List<Priority>
            {
                new Priority{ Name = "C-H", HoursToComplete = 7},
                new Priority{ Name = "C-MT", HoursToComplete = 12},
                new Priority{ Name = "C-TC", HoursToComplete = 16},
                new Priority{ Name = "MT", HoursToComplete = 12},
                new Priority{ Name = "TC", HoursToComplete = 16},
                new Priority{ Name = "DE", HoursToComplete = 16},
            };
            dbContext.Priorities.AddRange(priorities);

            var semesters = new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "Electiva Cod-41", "Electiva Cod-43", "Electiva Cod-44", "Electiva Cod-46", "Otros" }
                .Select(s => new Semester { Name = s })
                .ToList();
            dbContext.Semesters.AddRange(semesters);

            var classRooms = new List<Classroom>
            {
                new Classroom{Name = "2201", Capacity = 45, ClassroomTypePerSubjectId = 1},
                new Classroom{Name = "Lab. Sistemas Eléctricos I y II", Capacity = 16, ClassroomTypePerSubjectId = 2},
                new Classroom{Name = "Lab. Convertidores Eléctricos", Capacity = 15, ClassroomTypePerSubjectId = 3},
                new Classroom{Name = "2407", Capacity = 45, ClassroomTypePerSubjectId = 1},
                new Classroom{Name = "2409", Capacity = 35, ClassroomTypePerSubjectId = 1},
                new Classroom{Name = "2411", Capacity = 35, ClassroomTypePerSubjectId = 1},
                new Classroom{Name = "2412", Capacity = 35, ClassroomTypePerSubjectId = 1},
                new Classroom{Name = "2414", Capacity = 45, ClassroomTypePerSubjectId = 1},
                new Classroom{Name = "Lab. Sistemas de Comunicaciones", Capacity = 10, ClassroomTypePerSubjectId = 4},
                new Classroom{Name = "Lab. Sistemas Electrónicos II", Capacity = 16, ClassroomTypePerSubjectId = 9},
                new Classroom{Name = "Lab. Electrónica Industrial", Capacity = 16, ClassroomTypePerSubjectId = 5},
                new Classroom{Name = "Lab. Sistemas Digitales II", Capacity = 12, ClassroomTypePerSubjectId = 6},
                new Classroom{Name = "Lab. Computacion", Capacity = 35, ClassroomTypePerSubjectId = 8},
                new Classroom{Name = "Lab. Sistemas Electrónicos I", Capacity = 12, ClassroomTypePerSubjectId = 9},
                new Classroom{Name = "Lab. Sistemas Digitales III", Capacity = 15, ClassroomTypePerSubjectId = 10},
                new Classroom{Name = "Lab. Sistemas de Control II", Capacity = 15, ClassroomTypePerSubjectId = 11},
                new Classroom{Name = "Lab. Sistemas de Control III", Capacity = 15, ClassroomTypePerSubjectId = 12},
                new Classroom{Name = "Lab. Sistemas Digitales I", Capacity = 15, ClassroomTypePerSubjectId = 7},
            };
            dbContext.Classrooms.AddRange(classRooms);

            var subjects = new List<Subject>
            {
                Subject.NewSubject(41144, "Electrotecnia", 14, 1, 3, 72, 4),
                Subject.NewSubject(41151, "Lab. Electrotecnia", 14, 3, 3, 54, 3),
                Subject.NewSubject(41514, "Sistemas Eléctricos I", 3, 1, 1, 90, 5),
                Subject.NewSubject(41521, "Lab. Sistemas Eléctricos I", 3, 2, 1, 54, 3),
                Subject.NewSubject(41534, "Sistemas Eléctricos II", 4, 1, 1, 90, 5),
                Subject.NewSubject(41541, "Lab. Sistemas Eléctricos II", 4, 2, 1, 54, 3),
                Subject.NewSubject(41553, "Convertidores Eléctricos", 5, 1, 1, 72, 4),
                Subject.NewSubject(41561, "Lab. Convertidores Eléctricos", 5, 3, 1, 54, 3),
                Subject.NewSubject(41594, "Canalizaciones", 10, 1, 1, 90, 5),
                Subject.NewSubject(42513, "Sistemas Digitales I", 5, 1, 1, 72, 4),
                Subject.NewSubject(42521, "Lab. Sistemas Digitales I", 5, 7, 1, 54, 3),
                Subject.NewSubject(42533, "Sistemas Digitales II", 6, 1, 1, 72, 4),
                Subject.NewSubject(42541, "Lab. Sistemas Digitales II", 6, 6, 1, 54, 3),
                Subject.NewSubject(42553, "Sistemas Digitales III", 7, 1, 1, 72, 4),
                Subject.NewSubject(42561, "Lab. Sistemas Digitales III", 7, 10, 1, 54, 3),
                Subject.NewSubject(43113, "Controles Industriales", 11, 1, 1, 72, 4),
                Subject.NewSubject(43514, "Sistemas de Control I", 5, 1, 1, 90, 5),
                Subject.NewSubject(43523, "Instrumentación Industrial", 6, 1, 1, 72, 4),
                Subject.NewSubject(43531, "Lab. Instrumentación Industrial", 6, 1, 1, 54, 3),
                Subject.NewSubject(43545, "Modelaje y Simulación Digital", 6, 1, 1, 108, 6),
                Subject.NewSubject(43554, "Sistemas de Control II", 7, 1, 1, 90, 5),
                Subject.NewSubject(43561, "Lab. Sistemas de Control II", 7, 11, 1, 54, 3),
                Subject.NewSubject(43573, "Sistemas de Control III", 8, 1, 1, 72, 4),
                Subject.NewSubject(43581, "Lab. Sistemas de Control III", 8, 12, 1, 54, 3),
                Subject.NewSubject(43592, "Proyectos Industriales", 9, 1, 1, 72, 4),
                Subject.NewSubject(43623, "Control Óptimo", 11, 1, 1, 72, 4),
                Subject.NewSubject(44023, "Sistemas de Comunicación", 8, 1, 1, 72, 4),
                Subject.NewSubject(44031, "Lab. Sistemas de Comunicación", 8, 4, 1, 54, 3),
                Subject.NewSubject(44043, "Transmisión de Datos", 9, 1, 1, 72, 4),
                Subject.NewSubject(44103, "Antenas", 12, 1, 1, 72, 4),
                Subject.NewSubject(44203, "Telefonía", 12, 1, 1, 72, 4),
                Subject.NewSubject(44303, "Fibra Óptica", 12, 1, 1, 72, 4),
                Subject.NewSubject(44423, "Sistemas de Comunicaciones Móviles", 12, 1, 1, 72, 4),
                Subject.NewSubject(44513, "Sistemas de Señales", 7, 1, 1, 72, 4),
                Subject.NewSubject(44623, "Redes Digitales de Comunicaciones", 12, 1, 1, 72, 4),
                Subject.NewSubject(45514, "Sistemas Eléctronicos I", 4, 1, 1, 90, 5),
                Subject.NewSubject(45521, "Lab. Sistemas Eléctronicos I", 4, 9, 1, 54, 3),
                Subject.NewSubject(45534, "Sistemas Eléctronicos II", 5, 1, 1, 90, 5),
                Subject.NewSubject(45541, "Lab. Sistemas Eléctronicos II", 5, 9, 1, 54, 3),
                Subject.NewSubject(45553, "Electrónica Industrial", 6, 1, 1, 72, 4),
                Subject.NewSubject(45561, "Lab. Electrónica Industrial", 6, 5, 1, 54, 3),
                Subject.NewSubject(46513, "Programación Digital", 3, 1, 1, 90, 5),
                Subject.NewSubject(46523, "Procesamiento de Datos", 7, 8, 1, 72, 4),
                Subject.NewSubject(46533, "Sistemas de Información I", 13, 8, 1, 72, 4),
                Subject.NewSubject(46543, "Sistemas de Información II", 13, 8, 1, 72, 4),
                Subject.NewSubject(46573, "Base de Datos", 13, 8, 1, 72, 4),
                Subject.NewSubject(46583, "Programación Orientada a Objetos", 13, 8, 1, 72, 4),
                Subject.NewSubject(46593, "Ingeniería de Software", 13, 1, 1, 72, 4)
            };
            dbContext.Subjects.AddRange(subjects);

            var teachers = new List<Teacher>
            {
                Teacher.NewTeacher(12688253, "Jenny", "Cruz", 5),
                Teacher.NewTeacher(8309660, "Jesús", "Noriega", 6),
                Teacher.NewTeacher(4884447, "Pedro", "Márquez", 6),
                Teacher.NewTeacher(6869357, "Walter", "Rivero", 5),
                Teacher.NewTeacher(15800780, "María", "Franco", 6),
                Teacher.NewTeacher(6867877, "Henry", "Arias", 6),
                Teacher.NewTeacher(10345341, "José", "Pinzon", 5),
                Teacher.NewTeacher(6163902, "Francisco", "Ledo", 6),
                Teacher.NewTeacher(13383418, "Manuel", "Fajardo", 5),
                Teacher.NewTeacher(12642255, "Wilson", "Mendoza", 5),
                Teacher.NewTeacher(10379875, "Alexis", "Cabello", 6),
                Teacher.NewTeacher(10110448, "Jorge", "Lara", 6),
                Teacher.NewTeacher(6255322, "Ángel", "Ramos", 6),
                Teacher.NewTeacher(6343581, "Oswaldo", "Fornerino", 5),
                Teacher.NewTeacher(5432639, "Federico", "Ochoa", 6),
                Teacher.NewTeacher(14196635, "Frank", "Capote", 4),
                Teacher.NewTeacher(6334158, "Carlos", "Bethancourt", 6),
                Teacher.NewTeacher(4254143, "Luis", "León", 2),
                Teacher.NewTeacher(23144433, "Alexander", "Tinoco", 5),
                Teacher.NewTeacher(81394456, "Margarita", "Aguayo", 5),
                Teacher.NewTeacher(6728390, "David", "Jaén", 6),
                Teacher.NewTeacher(23, "Jesús", "Guaregua", 1),
                Teacher.NewTeacher(6373667, "Nando", "Vitti", 4),
                Teacher.NewTeacher(4119381, "José", "Tovar", 6),
                Teacher.NewTeacher(3223864, "Miguel", "Contreras", 3),
                Teacher.NewTeacher(13945188, "Kelly", "Pérez", 4),
                Teacher.NewTeacher(28, "Fernando", "Dávila", 1),
                Teacher.NewTeacher(11201707, "Mixaida", "Delgado", 5),
                Teacher.NewTeacher(12069935, "Wilfredo", "Márquez", 3),
                Teacher.NewTeacher(13944531, "Luis", "Romero", 2),
                Teacher.NewTeacher(21255727, "Efrain", "Bastidas", 2),
            };
            dbContext.Teachers.AddRange(teachers);

            await dbContext.SaveChangesAsync();
        }
    }
}
