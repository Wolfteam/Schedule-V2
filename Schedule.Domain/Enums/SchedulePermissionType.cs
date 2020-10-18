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
        CreateTeacher = 1,

        [Display(GroupName = "Teacher")]
        UpdateTeacher = 2,

        [Display(GroupName = "Teacher")]
        DeleteTeacher = 4,

        [Display(GroupName = "Teacher")]
        ReadTeacher = 8,

        [Display(GroupName = "All")]
        AllTeacher = CreateTeacher | UpdateTeacher | DeleteTeacher | ReadTeacher,

        [Display(GroupName = "Subject")]
        CreateSubject = 16,

        [Display(GroupName = "Subject")]
        UpdateSubject = 32,

        [Display(GroupName = "Subject")]
        DeleteSubject = 64,

        [Display(GroupName = "Subject")]
        ReadSubject = 128,

        [Display(GroupName = "All")]
        AllSubject = CreateSubject | UpdateSubject | DeleteSubject | ReadSubject,

        [Display(GroupName = "Period")]
        CreatePeriod = 256,

        [Display(GroupName = "Period")]
        UpdatePeriod = 512,

        [Display(GroupName = "Period")]
        DeletePeriod = 1024,

        [Display(GroupName = "Period")]
        ReadPeriod = 2048,

        [Display(GroupName = "All")]
        AllPeriod = CreatePeriod | UpdatePeriod | DeletePeriod | ReadPeriod,

        [Display(GroupName = "Classroom")]
        CreateClassroom = 4096,

        [Display(GroupName = "Classroom")]
        UpdateClassroom = 8192,

        [Display(GroupName = "Classroom")]
        DeleteClassroom = 16384,

        [Display(GroupName = "Classroom")]
        ReadClassroom = 32768,

        [Display(GroupName = "All")]
        AllClassroom = CreateClassroom | UpdateClassroom | DeleteClassroom | ReadClassroom,

        [Display(GroupName = "All")]
        All = AllTeacher | AllSubject | AllPeriod | AllClassroom
    }
}
