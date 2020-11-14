import {
    CircularProgress,
    FormControl,
    TextField
} from '@material-ui/core';
import { Autocomplete } from '@material-ui/lab';
import { useSnackbar } from 'notistack';
import React, { useCallback, useEffect, useState } from 'react';
import { buildPaginatedRequest, IGetAllClassroomTypesResponseDto, IPaginatedRequestDto } from '../../models';
import { getAllClassroomTypes, getClassroomType } from '../../services/classroom.service';
import translations, { getErrorCodeTranslation } from '../../services/translations';

interface State {
    loaded: boolean;
    canSearch: boolean;
    searchTerm: string;
    classroomsTypes: IGetAllClassroomTypesResponseDto[];
}

interface Props {
    isInEditMode: boolean;
    canSearch: boolean;
    selectedValue: number;
    onClassroomTypesLoaded: () => void;
    onClassroomTypeSelected: (newVal: IGetAllClassroomTypesResponseDto | null) => void;
}

const initialState: State = {
    loaded: false,
    canSearch: false,
    classroomsTypes: [],
    searchTerm: '',
};

function ClassroomtypeAutocomplete(props: Props) {
    const [state, setState] = useState<State>(initialState);

    const { enqueueSnackbar } = useSnackbar();

    const { isInEditMode, selectedValue, onClassroomTypesLoaded } = props;

    const refreshClassroomTypes = useCallback(async (request: IPaginatedRequestDto) => {
        const response = await getAllClassroomTypes(request);
        if (!response.succeed) {
            enqueueSnackbar(getErrorCodeTranslation(response.errorMessageId), { variant: 'error' });
            setState(s => ({ ...s, loaded: true }));
            return;
        }

        let types = response.result;
        const concat = isInEditMode && !types.find(t => t.id === selectedValue);

        setState(s => ({
            ...s,
            loaded: true,
            currentPage: response.currentPage,
            itemsPerPage: response.take,
            classroomsTypes: concat ? types.concat(s.classroomsTypes) : types,
            totalPages: response.totalPages,
            totalRecords: response.totalRecords
        }));
        onClassroomTypesLoaded();
    }, [selectedValue, isInEditMode, onClassroomTypesLoaded, enqueueSnackbar]);

    //TODO: WHEN YOU SELECT AN ITEM, A REFRESH HAPPENS BECAUSE THE SEARCH TERM CHANGED.....
    useEffect(() => {
        if (!state.canSearch && isInEditMode)
            return;

        const timeout = setTimeout(async () => {
            console.log("getting all", isInEditMode);

            const request = buildPaginatedRequest(1, 10, state.searchTerm, 'Name', true);
            await refreshClassroomTypes(request);
        }, 500);

        return () => clearTimeout(timeout);
    }, [state.canSearch, isInEditMode, state.searchTerm, refreshClassroomTypes]);

    useEffect(() => {
        if (state.loaded || !isInEditMode || selectedValue <= 0)
            return;
        console.log("loading one");
        getClassroomType(selectedValue).then(response => {
            if (!response.succeed) {
                enqueueSnackbar(getErrorCodeTranslation(response.errorMessageId), { variant: 'error' });
                setState(s => ({ ...s, loaded: true }));
                return;
            }

            //Here we need to set the search term, otherwise the onInputChange will be triggered
            //becuase state.searchterm = '' && newVal = response.result.name
            setState(s => ({
                ...s,
                loaded: true,
                classroomsTypes: [response.result],
                searchTerm: response.result.name
            }));
            onClassroomTypesLoaded();
        });
    }, [state.loaded, selectedValue, isInEditMode, onClassroomTypesLoaded, enqueueSnackbar]);

    const onInputChange = (newVal: string) => {
        if (state.searchTerm !== newVal) {
            setState(s => ({ ...s, searchTerm: newVal, canSearch: true }));
        }
    };

    const onChange = (event: React.ChangeEvent<{}>, newValue: IGetAllClassroomTypesResponseDto | null) => {
        if (selectedValue !== newValue?.id) {
            setState(s => ({ ...s, canSearch: false }));
            props.onClassroomTypeSelected(newValue);
        }
    };

    if (!state.loaded) {
        return <CircularProgress />;
    }

    const currentSelectedValue = state.classroomsTypes.find(t => t.id === selectedValue) ?? null;

    return <FormControl fullWidth margin="normal">
        <Autocomplete
            fullWidth
            size="small"
            onInputChange={(e, v) => onInputChange(v)}
            value={currentSelectedValue}
            onChange={onChange}
            options={state.classroomsTypes}
            getOptionLabel={(item: IGetAllClassroomTypesResponseDto) => `${item.name}`}
            renderInput={(params: any) => <TextField {...params} label={translations.classroomType} variant="outlined" />} />
    </FormControl>;
}

export default ClassroomtypeAutocomplete;
