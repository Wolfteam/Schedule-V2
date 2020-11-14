import {
    Container,
    CssBaseline,
    Grid
} from '@material-ui/core';
import { useSnackbar } from 'notistack';
import React, { useEffect, useState } from 'react';
import AvailabilityTable from '../../components/availability/availability-table';
import AvailabilityTeachersCard from '../../components/availability/availability-teachers-card';
import PageTitle from '../../components/page-title/page-title';
import * as responses from '../../models';
import { getAllTeachers, getTeacherAvailability, saveTeacherAvailability } from '../../services/teacher.service';
import translations, { getErrorCodeTranslation } from '../../services/translations';
import { getLaboralDays } from '../../utils/academic-hours';

interface State {
    isLoadingTeachers: boolean,
    isLoadingAvailability: boolean,
    teachers: responses.IGetAllTeacherResponseDto[],
    selectedTeacher?: responses.IGetAllTeacherResponseDto;
    hoursToComplete: number,
    remainingHours: number,
    availability: responses.ITeacherAvailabilityRequestDto[],
}

const initialState: State = {
    isLoadingTeachers: false,
    isLoadingAvailability: false,
    teachers: [],
    hoursToComplete: 0,
    remainingHours: 0,
    availability: []
};

function Availability() {
    const [state, setState] = useState<State>(initialState);

    const { enqueueSnackbar } = useSnackbar();

    useEffect(() => {
        getAllTeachers().then(response => {
            if (response.succeed) {
                enqueueSnackbar(getErrorCodeTranslation(response.errorMessageId), { variant: 'error', });
                return;
            }
            setState(s => ({ ...s, teachers: response.result }));
        });
    }, [enqueueSnackbar]);

    const calculateRemainingHours = (hoursToComplete: number, availability: responses.ITeacherAvailabilityRequestDto[]): number => {
        const asignedHours = availability.map(a => {
            const hours = a.endHour - a.startHour;
            return hours + 1;
        }).reduce((a, b) => a + b, 0);

        const remainingHours = availability.length === 0
            ? hoursToComplete
            : hoursToComplete - asignedHours;

        return remainingHours;
    };

    const handleTeacherChange = async (newValue: responses.IGetAllTeacherResponseDto | null) => {
        if (!newValue) {
            setState({ ...initialState, teachers: state.teachers });
            return;
        }
        setState({ ...state, isLoadingAvailability: true });
        const response = await getTeacherAvailability(newValue.id);
        if (!response.succeed) {
            enqueueSnackbar(getErrorCodeTranslation(response.errorMessageId), { variant: 'error' });
            setState({ ...state, isLoadingAvailability: false });
            return;
        }
        const remainingHours = calculateRemainingHours(response.result.hoursToComplete, response.result.availability);

        setState({
            ...state,
            selectedTeacher: newValue,
            hoursToComplete: response.result.hoursToComplete,
            remainingHours: remainingHours,
            availability: response.result.availability,
            isLoadingAvailability: false
        });
    };

    const handleAvailabilityChange = (newValue: responses.ITeacherAvailabilityRequestDto[]) => {
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

    const isAvailabilityValid = (): boolean => {
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
                const diff = availability.endHour - availability.startHour + 1;
                //TODO: MOVE THIS TO A SETTING
                if (diff >= 2)
                    ocurrences++;
            }

            if (ocurrences !== availabilityForToday.length)
                return false;
        }
        return true;
    }

    const handleSaveChanges = async () => {
        const isValid = isAvailabilityValid();
        if (!isValid) {
            enqueueSnackbar(translations.availabilitiesAreNotValid, { variant: 'warning' });
            return;
        }
        setState({ ...state, isLoadingAvailability: true });

        const request: responses.ISaveTeacherAvailabilityRequestDto = {
            availability: state.availability
        };
        const response = await saveTeacherAvailability(state.selectedTeacher!.id, request);
        if (response.succeed) {
            enqueueSnackbar(translations.availabilityWasSaved, { variant: 'success' });
        } else {
            enqueueSnackbar(getErrorCodeTranslation(response.errorMessageId), { variant: 'error' });
        }

        setState({ ...state, isLoadingAvailability: false });
    };

    const isSaveBtnEnabled = state.selectedTeacher !== undefined &&
        state.remainingHours === 0 &&
        !state.isLoadingTeachers &&
        !state.isLoadingAvailability;

    return <Container>
        <CssBaseline />
        <PageTitle title={translations.loadAvailability} showLoading={state.isLoadingAvailability} />
        <Grid container justify="center">
            <Grid item xs={12} md={4}>
                <AvailabilityTeachersCard
                    hoursToComplete={state.hoursToComplete}
                    remainingHours={state.remainingHours}
                    teachers={state.teachers}
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