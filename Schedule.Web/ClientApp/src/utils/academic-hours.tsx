import { Day } from "../enums";

export const academicHours = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13] as const;

type academicHour = typeof academicHours[number];

export const getHourText = (hour: academicHour): string => {
    switch (hour) {
        case 1:
            return '7:00am a 7:50am';
        case 2:
            return '7:50am a 8:40am';
        case 3:
            return '8:40am a 9:30am';
        case 4:
            return '9:30am a 10:20am';
        case 5:
            return '10:20am a 11:10am';
        case 6:
            return '11:10am a 12:00pm';
        case 7:
            return '12:00pm a 1:00pm';
        case 8:
            return '1:00pm a 1:50pm';
        case 9:
            return '1:50pm a 2:40pm';
        case 10:
            return '2:40pm a 3:30pm';
        case 11:
            return '3:30pm a 4:20pm';
        case 12:
            return '4:20pm a 5:10pm';
        case 13:
            return '5:10pm a 6:00pm';
    }
    return '';
};

export const isLunchHour = (hour: number): boolean => {
    return hour === 7;  
};

export const isLastAcademicHour = (hour: number): boolean => {
    const maxHour = Math.max.apply(academicHours);
    return maxHour === hour;
};

export const isAcademicHourBetweenRange = (start: number, end: number, selectedHour: number): boolean => {
    return end >= selectedHour && selectedHour >= start;
};

export const getLaboralDays = (): number[] => {
    const keys = Object.keys(Day).filter(k => typeof Day[k as any] === "number"); // ["A", "B"]
    const values = keys.map(k => +Day[k as any]);// [0, 1]
    return values;
};