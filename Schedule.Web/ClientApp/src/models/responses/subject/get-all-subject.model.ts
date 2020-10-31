export interface IGetAllSubjectResponseDto {
    id: number;
    code: number;
    name: string;
    totalAcademicHours: number;
    academicHoursPerWeek: number;
    careerId: number;
    career: string;
    semesterId: number;
    semester: string;
    classroomTypePerSubjectId: number;
    classroomType: string;
}