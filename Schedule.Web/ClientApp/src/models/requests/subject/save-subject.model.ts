export interface ISaveSubjectRequestDto {
    code: number;
    name: string;
    totalAcademicHours: number;
    academicHoursPerWeek: number;

    careerId: number;
    semesterId: number;
    classroomTypePerSubjectId: number;
}