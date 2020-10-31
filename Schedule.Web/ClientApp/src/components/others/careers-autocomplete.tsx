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
    IGetAllCareersResponseDto
} from '../../models';
import { getAllCareers } from '../../services/career.service';

interface State {
    loaded: boolean;
    careers: IGetAllCareersResponseDto[];
}

interface Props {
    selectedValue: number;
    onCareerLoaded: () => void;
    onCareerSelected: (newVal: IGetAllCareersResponseDto | null) => void;
}

function CareersAutocomplete(props: Props) {
    const [state, setState] = useState<State>({
        loaded: false,
        careers: []
    });
    const { enqueueSnackbar } = useSnackbar();

    useEffect(() => {
        getAllCareers().then(resp => {
            if (!resp.succeed) {
                enqueueSnackbar(getErrorCodeTranslation(resp.errorMessageId), { variant: 'error' });
                setState({ ...state, loaded: true });
                return;
            }

            setState({ loaded: true, careers: resp.result });
            props.onCareerLoaded();
        });
    }, [])

    const onChange = (event: React.ChangeEvent<{}>, newValue: IGetAllCareersResponseDto | null) => {
        props.onCareerSelected(newValue);
    };

    if (!state.loaded) {
        return <CircularProgress />;
    }

    const selectedVal = state.careers.find(c => c.id == props.selectedValue) ?? null;

    return <FormControl fullWidth margin="normal">
        <Autocomplete
            fullWidth
            size="small"
            value={selectedVal}
            onChange={onChange}
            options={state.careers}
            getOptionLabel={(item: IGetAllCareersResponseDto) => `${item.name}`}
            renderInput={(params: any) => <TextField {...params} label={translations.careers} variant="outlined" />} />
    </FormControl>;
}

export default CareersAutocomplete;
