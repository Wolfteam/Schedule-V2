import {
    CircularProgress,
    FormControl,
    TextField
} from '@material-ui/core';
import { Autocomplete } from '@material-ui/lab';
import { useSnackbar } from 'notistack';
import React, { useEffect, useState } from 'react';
import {
    IGetAllPrioritiesResponseDto
} from '../../models';
import { getAllPriorities } from '../../services/teacher.service';
import translations, { getErrorCodeTranslation } from '../../services/translations';

interface State {
    loaded: boolean;
    priorities: IGetAllPrioritiesResponseDto[];
}

interface Props {
    selectedValue: number;
    onPrioritiesLoaded: () => void;
    onPrioritySelected: (newVal: IGetAllPrioritiesResponseDto | null) => void;
}

function PrioritiesAutocomplete(props: Props) {
    const [state, setState] = useState<State>({
        loaded: false,
        priorities: []
    });
    const { enqueueSnackbar } = useSnackbar();

    const { onPrioritiesLoaded } = props;

    useEffect(() => {
        getAllPriorities().then(resp => {
            if (!resp.succeed) {
                enqueueSnackbar(getErrorCodeTranslation(resp.errorMessageId), { variant: 'error' });
                setState(s => ({ ...s, loaded: true }));
                return;
            }

            setState({ loaded: true, priorities: resp.result });
            onPrioritiesLoaded();
        });
    }, [enqueueSnackbar, onPrioritiesLoaded])

    const onChange = (event: React.ChangeEvent<{}>, newValue: IGetAllPrioritiesResponseDto | null) => {
        props.onPrioritySelected(newValue);
    };

    if (!state.loaded) {
        return <CircularProgress />;
    }

    const selectedVal = state.priorities.find(c => c.id === props.selectedValue) ?? null;

    return <FormControl fullWidth margin="normal">
        <Autocomplete
            fullWidth
            size="small"
            value={selectedVal}
            onChange={onChange}
            options={state.priorities}
            getOptionLabel={item => `${item.name}`}
            renderInput={(params: any) => <TextField {...params} label={translations.priorities} variant="outlined" />} />
    </FormControl>;
}

export default PrioritiesAutocomplete
