import { faFont } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import {
    Button,
    Container,
    createStyles,
    Grid,
    InputAdornment,
    makeStyles,
    TextField,
    Theme
} from '@material-ui/core';
import { useSnackbar } from 'notistack';
import React, { useCallback, useEffect, useState } from 'react';
import { useHistory, useParams } from 'react-router-dom';
import { String } from 'typescript-string-operations';
import validator from 'validator';
import PageTitle from '../../../components/page-title/page-title';
import { ISaveCareerRequestDto } from '../../../models';
import { careersPath } from '../../../routes';
import { createCareer, getCareer, updateCareer } from '../../../services/career.service';
import translations, { getErrorCodeTranslation } from '../../../services/translations';

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        submitButton: {
            margin: 'auto',
            display: 'flex'
        },
    }),
);

interface State {
    isBusy: boolean;
}

interface CareerState {
    name: string;
}

interface CareerValidationState {
    isNameValid: boolean;
    isNameDirty: boolean;
}

interface ParamTypes {
    id: string;
}

const initialState: CareerState = {
    name: ''
};

const initialValidationState: CareerValidationState = {
    isNameDirty: false,
    isNameValid: false
};

function Career() {
    const classes = useStyles();
    const [state, setState] = useState<State>({
        isBusy: false,
    });

    const [career, setCareer] = useState<CareerState>(initialState);

    const [careerValidation, setCareerValidation] = useState<CareerValidationState>(initialValidationState);

    const { enqueueSnackbar } = useSnackbar();
    const history = useHistory();
    const params = useParams<ParamTypes>();
    const isInEditMode = validator.isInt(params.id) && +params.id > 0;

    const loadCareer = useCallback(async () => {
        if (!isInEditMode) {
            return;
        }

        setState(s => ({ ...s, isBusy: true }));
        const response = await getCareer(+params.id);
        if (!response.succeed) {
            enqueueSnackbar(getErrorCodeTranslation(response.errorMessageId), { variant: 'error' });
            setState(s => ({ ...s, isBusy: false }));
            return;
        }
        const career = response.result;
        setCareer({
            name: career.name
        });

        setCareerValidation({
            isNameDirty: true,
            isNameValid: true
        });
        setState(s => ({ ...s, isBusy: false }));
    }, [isInEditMode, params.id, enqueueSnackbar]);

    useEffect(() => {
        loadCareer();
    }, [loadCareer]);

    const handleChange = (prop: keyof CareerState) => (event: React.ChangeEvent<HTMLInputElement>) => {
        let newVal = event.target.value;
        const newState = { ...career };
        const newValidationState = { ...careerValidation };
        switch (prop) {
            case 'name':
                newState[prop] = newVal;
                const isValid = !validator.isEmpty(newVal) && validator.isLength(newVal, { min: 5, max: 50 });
                newValidationState.isNameValid = isValid;
                newValidationState.isNameDirty = true;
                break;
            default:
                break;
        }
        setCareer(newState);
        setCareerValidation(newValidationState);
    };

    const showValidationError = (isValid: keyof CareerValidationState, isDirty: keyof CareerValidationState) => {
        return !careerValidation[isValid] && careerValidation[isDirty];
    };

    const saveChanges = async () => {
        setState({ ...state, isBusy: true });
        const request: ISaveCareerRequestDto = {
            name: career.name
        };

        const response = isInEditMode
            ? await updateCareer(+params.id, request)
            : await createCareer(request);

        setState({ ...state, isBusy: false });
        if (!response.succeed) {
            enqueueSnackbar(getErrorCodeTranslation(response.errorMessageId), { variant: 'error' });
            return;
        }

        enqueueSnackbar(translations.careerWasSaved, { variant: 'success' });
        history.replace(careersPath);
    };

    const isFormValid = Object.values(careerValidation).every((val: boolean) => val);
    const btnEnabled = !state.isBusy && isFormValid;
    const pageTitle = String.Format(!isInEditMode ? translations.addX : translations.editX, translations.careers);

    const onSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        if (!btnEnabled)
            return;

        await saveChanges();
    };

    return <Container maxWidth="md">
        <PageTitle title={pageTitle} showLoading={state.isBusy} showBackIcon backPath={careersPath} />
        <form onSubmit={onSubmit}>
            <Grid container justify="center" direction="row" spacing={1}>
                <Grid item xs={12} sm={12}>
                    <TextField
                        variant="outlined"
                        margin="normal"
                        fullWidth
                        required
                        size="small"
                        label={translations.name}
                        value={career.name}
                        onChange={handleChange('name')}
                        type="text"
                        error={showValidationError('isNameValid', 'isNameDirty')}
                        helperText={showValidationError('isNameValid', 'isNameDirty') ? translations.invalidName : ''}
                        InputProps={{
                            inputProps: {
                                maxLength: 50,
                            },
                            startAdornment: <InputAdornment position="start">
                                <FontAwesomeIcon icon={faFont} />
                            </InputAdornment>
                        }}
                    />
                </Grid>
                <Grid item xs={12}>
                    <Button
                        type="button"
                        variant="contained"
                        color="primary"
                        onClick={saveChanges}
                        disabled={!btnEnabled}
                        className={classes.submitButton}>
                        {translations.saveChanges}
                    </Button>
                </Grid>
            </Grid>
        </form>
    </Container>;
}



export default Career;
