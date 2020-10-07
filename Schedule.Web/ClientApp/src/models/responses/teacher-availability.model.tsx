import * as enums from '../../enums';

export interface ITeacherAvailabilityResponseDto {
    id: number;
    day: enums.Day;
    startHourId: number;
    endHourId: number;
    periodId: number;
}


export interface ITeacherAvailabilityDetailsResponseDto {
    availability: ITeacherAvailabilityResponseDto[];
    hoursToComplete: number;
    asignedHours: number;
}
