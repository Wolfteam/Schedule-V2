using System;
using System.ComponentModel.DataAnnotations;

namespace Schedule.Domain.Enums
{
    [Flags]
    public enum SchedulePermissionType
    {
        [Display(GroupName = "None")]
        None = 0,

        [Display(GroupName = "Teacher")]
        CreateTeacher = 1 << 0,

        [Display(GroupName = "Teacher")]
        UpdateTeacher = 1 << 1,

        [Display(GroupName = "Teacher")]
        DeleteTeacher = 1 << 2,

        [Display(GroupName = "Teacher")]
        ReadTeacher = 1 << 3,

        [Display(GroupName = "All")]
        AllTeacher = CreateTeacher | UpdateTeacher | DeleteTeacher | ReadTeacher,

        [Display(GroupName = "Subject")]
        CreateSubject = 1 << 4,

        [Display(GroupName = "Subject")]
        UpdateSubject = 1 << 5,

        [Display(GroupName = "Subject")]
        DeleteSubject = 1 << 6,

        [Display(GroupName = "Subject")]
        ReadSubject = 1 << 7,

        [Display(GroupName = "All")]
        AllSubject = CreateSubject | UpdateSubject | DeleteSubject | ReadSubject,

        [Display(GroupName = "Period")]
        CreatePeriod = 1 << 8,

        [Display(GroupName = "Period")]
        UpdatePeriod = 1 << 9,

        [Display(GroupName = "Period")]
        DeletePeriod = 1 << 10,

        [Display(GroupName = "Period")]
        ReadPeriod = 1 << 11,

        [Display(GroupName = "All")]
        AllPeriod = CreatePeriod | UpdatePeriod | DeletePeriod | ReadPeriod,

        [Display(GroupName = "Classroom")]
        CreateClassroom = 1 << 12,

        [Display(GroupName = "Classroom")]
        UpdateClassroom = 1 << 13,

        [Display(GroupName = "Classroom")]
        DeleteClassroom = 1 << 14,

        [Display(GroupName = "Classroom")]
        ReadClassroom = 1 << 15,

        [Display(GroupName = "All")]
        AllClassroom = CreateClassroom | UpdateClassroom | DeleteClassroom | ReadClassroom,

        [Display(GroupName = "Career")]
        CreateCareer = 1 << 16,

        [Display(GroupName = "Career")]
        UpdateCareer = 1 << 17,

        [Display(GroupName = "Career")]
        DeleteCareer = 1 << 18,

        [Display(GroupName = "Career")]
        ReadCareer = 1 << 19,

        [Display(GroupName = "All")]
        AllCareer = CreateCareer | UpdateCareer | DeleteCareer | ReadCareer,

        [Display(GroupName = "Semester")]
        CreateSemester = 1 << 20,

        [Display(GroupName = "Semester")]
        UpdateSemester = 1 << 21,

        [Display(GroupName = "Semester")]
        DeleteSemester = 1 << 22,

        [Display(GroupName = "Semester")]
        ReadSemester = 1 << 23,

        [Display(GroupName = "All")]
        AllSemester = CreateSemester | UpdateSemester | DeleteSemester | ReadSemester,

        [Display(GroupName = "All")]
        All = AllTeacher | AllSubject | AllPeriod | AllClassroom | AllCareer | AllSemester
    }
}
