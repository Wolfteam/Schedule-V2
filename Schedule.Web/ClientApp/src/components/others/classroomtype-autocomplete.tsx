import React, { useEffect, useState } from 'react'
import {
    CircularProgress,
    FormControl,
    TextField,
} from '@material-ui/core'
import { Autocomplete } from '@material-ui/lab';
import { useSnackbar } from 'notistack';
import translations, { getErrorCodeTranslation } from '../../services/translations';
import {
    IGetAllClassroomTypesResponseDto, IPaginatedRequestDto, buildPaginatedRequest
} from '../../models';
import { getAllClassroomTypes, getClassroomType } from '../../services/classroom.service';


interface State {
    loaded: boolean;
    currentPage: number;
    totalPages: number;
    itemsPerPage: number;
    totalRecords: number;
    orderBy: string;
    orderByAsc: boolean;
    searchTerm: string;
    searchTimeout: number;
    classroomsTypes: IGetAllClassroomTypesResponseDto[];
}

interface Props {
    isInEditMode: boolean;
    canSearch: boolean;
    selectedValue: number;
    onClassroomTypesLoaded: () => void;
    onClassroomTypeSelected: (newVal: IGetAllClassroomTypesResponseDto | null) => void;
}

function ClassroomtypeAutocomplete(props: Props) {
    const [state, setState] = useState<State>({
        loaded: false,
        classroomsTypes: [],
        currentPage: 1,
        totalPages: 0,
        itemsPerPage: 5,
        totalRecords: 0,
        orderBy: 'Name',
        orderByAsc: true,
        searchTerm: '',
        searchTimeout: 0
    });
    const { enqueueSnackbar } = useSnackbar();

    const refreshClassroomTypes = async (request: IPaginatedRequestDto) => {
        const response = await getAllClassroomTypes(request);
        if (!response.succeed) {
            enqueueSnackbar(getErrorCodeTranslation(response.errorMessageId), { variant: 'error' });
            setState({ ...state, loaded: true });
            return;
        }

        let types = response.result;
        if (props.selectedValue > 0 && !types.find(t => t.id === props.selectedValue)) {
            types = types.concat(state.classroomsTypes);
        }
        setState({
            ...state,
            loaded: true,
            currentPage: response.currentPage,
            itemsPerPage: response.take,
            classroomsTypes: types,
            totalPages: response.totalPages,
            totalRecords: response.totalRecords
        });
        props.onClassroomTypesLoaded();
    };
    //TODO: FIX THIS CRAP
    useEffect(() => {
        if (!props.canSearch)
            return;
        const timeout = setTimeout(async () => {
            const request = buildPaginatedRequest(state.currentPage, state.itemsPerPage, state.searchTerm, state.orderBy, state.orderByAsc);
            await refreshClassroomTypes(request);
        }, 500);

        return () => clearTimeout(timeout);
    }, [state.searchTerm]);

    useEffect(() => {
        if (!props.isInEditMode)
            return;

        getClassroomType(props.selectedValue).then(response => {
            if (!response.succeed) {
                enqueueSnackbar(getErrorCodeTranslation(response.errorMessageId), { variant: 'error' });
                setState({ ...state, loaded: true });
                return;
            }

            const types = state.classroomsTypes.concat(response.result);
            setState({ ...state, classroomsTypes: types, loaded: true });
            props.onClassroomTypesLoaded();
        });
    }, [props.selectedValue]);

    const onInputChange = (newVal: string) => {
        if (state.searchTerm !== newVal)
            setState({ ...state, searchTerm: newVal });
    };

    const onChange = (event: React.ChangeEvent<{}>, newValue: IGetAllClassroomTypesResponseDto | null) => {
        if (props.selectedValue !== newValue?.id)
            props.onClassroomTypeSelected(newValue);
    };

    if (!state.loaded) {
        return <CircularProgress />;
    }

    const selectedValue = state.classroomsTypes.find(t => t.id == props.selectedValue) ?? null;

    return <FormControl fullWidth margin="normal">
        <Autocomplete
            fullWidth
            size="small"
            onInputChange={(e, v) => onInputChange(v)}
            value={selectedValue}
            onChange={onChange}
            options={state.classroomsTypes}
            getOptionLabel={(item: IGetAllClassroomTypesResponseDto) => `${item.name}`}
            renderInput={(params: any) => <TextField {...params} label={translations.classroomType} variant="outlined" />} />
    </FormControl>;
}

export default ClassroomtypeAutocomplete;
