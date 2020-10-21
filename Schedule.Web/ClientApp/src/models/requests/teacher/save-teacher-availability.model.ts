import { Day } from '../../../enums'

export interface ISaveTeacherAvailabilityRequestDto {
    availability: ITeacherAvailabilityRequestDto[];
}

export interface ITeacherAvailabilityRequestDto {
    day: Day;
    startHour: number;
    endHour: number;
}