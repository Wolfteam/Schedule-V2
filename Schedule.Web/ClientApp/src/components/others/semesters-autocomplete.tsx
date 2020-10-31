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
    IGetAllSemestersResponseDto
} from '../../models';
import { getAllSemesters } from '../../services/semester.service';

interface State {
    loaded: boolean;
    semesters: IGetAllSemestersResponseDto[];
}

interface Props {
    selectedValue: number;
    onSemesterLoaded: () => void;
    onSemesterSelected: (newVal: IGetAllSemestersResponseDto | null) => void;
}

function SemestersAutocomplete(props: Props) {
    const [state, setState] = useState<State>({
        loaded: false,
        semesters: []
    });
    const { enqueueSnackbar } = useSnackbar();

    useEffect(() => {
        getAllSemesters().then(resp => {
            if (!resp.succeed) {
                enqueueSnackbar(getErrorCodeTranslation(resp.errorMessageId), { variant: 'error' });
                setState({ ...state, loaded: true });
                return;
            }

            setState({ loaded: true, semesters: resp.result });
            props.onSemesterLoaded();
        });
    }, [])

    const onChange = (event: React.ChangeEvent<{}>, newValue: IGetAllSemestersResponseDto | null) => {
        props.onSemesterSelected(newValue);
    };

    if (!state.loaded) {
        return <CircularProgress />;
    }

    const selectedVal = state.semesters.find(s => s.id == props.selectedValue) ?? null;
    return <FormControl fullWidth margin="normal">
        <Autocomplete
            fullWidth
            size="small"
            onChange={onChange}
            value={selectedVal}
            options={state.semesters}
            getOptionLabel={item => `${item.name}`}
            renderInput={(params: any) => <TextField {...params} label={translations.semester} variant="outlined" />} />
    </FormControl>;
}

export default React.memo(SemestersAutocomplete);
