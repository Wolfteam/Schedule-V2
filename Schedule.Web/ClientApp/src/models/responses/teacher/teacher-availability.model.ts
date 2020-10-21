import { Day } from '../../../enums';

export interface ITeacherAvailabilityResponseDto {
    id: number;
    day: Day;
    startHour: number;
    endHour: number;
    periodId: number;
    teacherId: number;
}


export interface ITeacherAvailabilityDetailResponseDto {
    teacherId: number;
    availability: ITeacherAvailabilityResponseDto[];
    hoursToComplete: number;
    asignedHours: number;
}
