import React, { useState } from 'react';
import {
    Container,
    CssBaseline,
    Grid
} from '@material-ui/core';
import translations from '../../services/translations';
import PageTitle from '../../components/page-title/page-title';

import * as responses from '../../models/responses';
import * as enums from '../../enums';
import AvailabilityTable from '../../components/availability/availability-table';
import AvailabilityTeachersCard from '../../components/availability/availability-teachers-card';
import {  getLaboralDays } from '../../utils/academic-hours';

//TODO: IN THE BACKEND, VALIDATE THAT THE LUNCH HOUR IS NOT SET IN AN AVAILABILITY
const availabilities: responses.ITeacherAvailabilityDetailsResponseDto[] = [
    {
        asignedHours: 5,
        hoursToComplete: 8,
        availability: [
            {
                id: 0,
                day: enums.Day.monday,
                startHourId: 10,
                endHourId: 12,
                periodId: 2
            },
            {
                id: 1,
                day: enums.Day.friday,
                startHourId: 1,
                endHourId: 5,
                periodId: 2
            },
        ]
    },
    {
        asignedHours: 7,
        hoursToComplete: 10,
        availability: [
            {
                id: 2,
                day: enums.Day.saturday,
                startHourId: 1,
                endHourId: 4,
                periodId: 2
            },
            {
                id: 3,
                day: enums.Day.wednesday,
                startHourId: 2,
                endHourId: 6,
                periodId: 2
            },
        ]
    },
    {
        asignedHours: 0,
        hoursToComplete: 12,
        availability: [
            {
                id: 4,
                day: enums.Day.tuesday,
                startHourId: 1,
                endHourId: 6,
                periodId: 2
            },
            {
                id: 5,
                day: enums.Day.tuesday,
                startHourId: 8,
                endHourId: 13,
                periodId: 2
            },
        ]
    }
];

const teachers: responses.ITeacherResponseDto[] = [
    {
        name: 'Efrain',
        lastName: 'Bastidas'
    },
    {
        name: 'Maria',
        lastName: 'Berrios'
    },
    {
        name: 'Luz',
        lastName: 'Marina'
    }
];

interface State {
    selectedTeacher?: responses.ITeacherResponseDto;
    hoursToComplete: number,
    remainingHours: number,
    availability: responses.ITeacherAvailabilityResponseDto[],
}

function Availability() {
    const [state, setState] = useState<State>({
        hoursToComplete: 0,
        remainingHours: 0,
        availability: []
    });

    const calculateRemainingHours = (hoursToComplete: number, availability: responses.ITeacherAvailabilityResponseDto[]): number => {
        const asignedHours = availability.map(a => {
            const hours = a.endHourId - a.startHourId;
            return hours + 1;
        }).reduce((a, b) => a + b);

        const remainingHours = availability.length === 0
            ? hoursToComplete
            : hoursToComplete - asignedHours;

        return remainingHours;
    };

    const handleTeacherChange = (newValue: responses.ITeacherResponseDto) => {
        const something = newValue.name === 'Efrain'
            ? availabilities[0]
            : newValue.name === "Maria"
                ? availabilities[1]
                : availabilities[2];
        const remainingHours = calculateRemainingHours(something.hoursToComplete, something.availability);
        setState({
            ...state,
            selectedTeacher: newValue,
            hoursToComplete: something.hoursToComplete,
            remainingHours: remainingHours,
            availability: something.availability,
        });
    };

    const handleAvailabilityChange = (newValue: responses.ITeacherAvailabilityResponseDto[]) => {
        const remainingHours = calculateRemainingHours(state.hoursToComplete, newValue);
        if (remainingHours < 0) {
            return
        }
        setState({
            ...state,
            availability: newValue,
            remainingHours: remainingHours
        });
    };

    const isAvailabilityValid = () : boolean => {
        if (state.availability.length === 0)
            return false;

        const laboralDays = getLaboralDays();
        for (let i = 0; i <= laboralDays.length; i++) {
            const day = laboralDays[i];
            const availabilityForToday = state.availability.filter(a => a.day === day);
            let ocurrences = 0;
            if (availabilityForToday.length === 0)
                continue;
            
            for (let j = 0; j < availabilityForToday.length; j++) {
                const availability = availabilityForToday[j];
                const diff = availability.endHourId - availability.startHourId + 1;
                //TODO: MOVE THIS TO A SETTING
                if (diff >= 2)
                    ocurrences++;
            }

            if (ocurrences !== availabilityForToday.length)
                return false;
        }
        return true;
    }

    const handleSaveChanges = () => {
        const isValid = isAvailabilityValid();
        console.log("IsValid", isValid);
    };

    const isSaveBtnEnabled = state.selectedTeacher !== undefined && state.remainingHours === 0;

    return <Container>
        <CssBaseline />
        <PageTitle title={translations.loadAvailability} />

        <Grid container justify="center">
            <Grid item xs={12} md={4}>
                <AvailabilityTeachersCard
                    hoursToComplete={state.hoursToComplete}
                    remainingHours={state.remainingHours}
                    teachers={teachers}
                    onTeacherChange={handleTeacherChange}
                    onSaveChanges={handleSaveChanges}
                    isSaveButtonEnabled={isSaveBtnEnabled} />
            </Grid>
            <Grid item xs={12} md={8}>
                <AvailabilityTable
                    availability={state.availability}
                    onAvailabilityChange={handleAvailabilityChange} />
            </Grid>
        </Grid>
    </Container>;
}

export default Availability;