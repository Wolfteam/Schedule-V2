import { faBold, faFont, faIdCard } from '@fortawesome/free-solid-svg-icons';
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
import React, { useEffect, useState } from 'react';
import { useHistory, useParams } from 'react-router-dom';
import { String } from 'typescript-string-operations';
import validator from 'validator';
import PrioritiesAutocomplete from '../../../components/others/priorities-autocomplete';
import PageTitle from '../../../components/page-title/page-title';
import { IGetAllPrioritiesResponseDto, ISaveTeacherRequestDto } from '../../../models';
import { teachersPath } from '../../../routes';
import { createTeacher, getTeacher, updateTeacher } from '../../../services/teacher.service';
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

interface TeacherState {
    identifierNumber: number;
    firstName: string;
    firstLastName: string;
    secondName: string;
    secondLastName: string;
    priorityId: number;
}

interface TeacherValidationState {
    isIdentifierNumberValid: boolean;
    isFirstNameValid: boolean;
    isFirstLastNameValid: boolean;
    isSecondNameValid: boolean;
    isSecondLastNameValid: boolean;
    isPriorityIdValid: boolean;
    isIdentifierNumberDirty: boolean;
    isFirstNameDirty: boolean;
    isFirstLastNameDirty: boolean;
    isSecondNameDirty: boolean;
    isSecondLastNameDirty: boolean;
}

interface ParamTypes {
    id: string;
}

const initialState: TeacherState = {
    identifierNumber: 0,
    firstName: '',
    firstLastName: '',
    secondName: '',
    secondLastName: '',
    priorityId: 0
};

const initialValidationState: TeacherValidationState = {
    isIdentifierNumberValid: false,
    isFirstNameValid: false,
    isFirstLastNameValid: false,
    isSecondNameValid: true,
    isSecondLastNameValid: true,
    isPriorityIdValid: false,
    isIdentifierNumberDirty: false,
    isFirstNameDirty: false,
    isFirstLastNameDirty: false,
    isSecondNameDirty: true,
    isSecondLastNameDirty: true,
};

function Teacher() {
    const classes = useStyles();
    const [state, setState] = useState<State>({
        isBusy: false,
    });

    const [teacher, setTeacher] = useState<TeacherState>(initialState);

    const [teacherValidation, setTeacherValidation] = useState<TeacherValidationState>(initialValidationState);

    const [prioritiesLoaded, setPrioritiesLoaded] = useState<boolean>(false);

    const { enqueueSnackbar } = useSnackbar();
    const history = useHistory();
    const params = useParams<ParamTypes>();
    const isInEditMode = validator.isInt(params.id) && +params.id > 0;

    const loadTeacher = async () => {
        if (!isInEditMode) {
            return;
        }

        setState({ ...state, isBusy: true });
        const response = await getTeacher(+params.id);
        if (!response.succeed) {
            enqueueSnackbar(getErrorCodeTranslation(response.errorMessageId), { variant: 'error' });
            setState({ ...state, isBusy: false });
            return;
        }
        const teacher = response.result;
        setTeacher({
            identifierNumber: teacher.identifierNumber,
            firstName: teacher.firstName,
            firstLastName: teacher.firstLastName,
            secondName: teacher.secondName,
            secondLastName: teacher.secondLastName,
            priorityId: teacher.priorityId
        });

        setTeacherValidation({
            ...teacherValidation,
            isIdentifierNumberValid: true,
            isFirstNameValid: true,
            isFirstLastNameValid: true,
            isPriorityIdValid: true,
            isIdentifierNumberDirty: true,
            isFirstNameDirty: true,
            isFirstLastNameDirty: true,
        });
        setState({ ...state, isBusy: false });
    };

    useEffect(() => {
        loadTeacher();
    }, []);

    const handleChange = (prop: keyof TeacherState) => (event: React.ChangeEvent<HTMLInputElement>) => {
        let newVal = event.target.value;
        const newState = { ...teacher };
        const newValidationState = { ...teacherValidation };
        switch (prop) {
            case 'identifierNumber':
                newState.identifierNumber = +newVal;
                newValidationState.isIdentifierNumberValid = validator.isInt(newVal) && validator.isLength(newVal, { min: 6, max: 10 });
                newValidationState.isIdentifierNumberDirty = true;
                break;
            case 'firstName':
            case 'firstLastName':
            case 'secondName':
            case 'secondLastName':
                newState[prop] = newVal;
                const isValid = !validator.isEmpty(newVal) && validator.isLength(newVal, { min: 3, max: 50 }) && validator.isAlpha(newVal);
                if (prop === 'firstName') {
                    newValidationState.isFirstNameValid = isValid;
                    newValidationState.isFirstNameDirty = true;
                } else if (prop === 'firstLastName') {
                    newValidationState.isFirstLastNameValid = isValid;
                    newValidationState.isFirstLastNameDirty = true;
                } else if (prop === 'secondName') {
                    newValidationState.isSecondNameValid = validator.isEmpty(newVal) || isValid;
                    newValidationState.isSecondNameDirty = true;
                } else {
                    newValidationState.isSecondLastNameValid = validator.isEmpty(newVal) || isValid;
                    newValidationState.isSecondLastNameDirty = true;
                }
                break;
            default:
                break;
        }
        setTeacher(newState);
        setTeacherValidation(newValidationState);
    };

    const showValidationError = (isValid: keyof TeacherValidationState, isDirty: keyof TeacherValidationState) => {
        return !teacherValidation[isValid] && teacherValidation[isDirty];
    };

    const onPrioritiesLoaded = () => setPrioritiesLoaded(true);

    const onPrioritySelected = (item: IGetAllPrioritiesResponseDto | null) => {
        const id = item?.id ?? 0;
        setTeacher({ ...teacher, priorityId: id });
        setTeacherValidation({ ...teacherValidation, isPriorityIdValid: id > 0 });
    };

    const saveChanges = async () => {
        setState({ ...state, isBusy: true });
        const request: ISaveTeacherRequestDto = {
            identifierNumber: teacher.identifierNumber,
            firstName: teacher.firstName,
            firstLastName: teacher.firstLastName,
            secondName: teacher.secondName,
            secondLastName: teacher.secondLastName,
            priorityId: teacher.priorityId
        };

        const response = isInEditMode
            ? await updateTeacher(+params.id, request)
            : await createTeacher(request);

        setState({ ...state, isBusy: false });
        if (!response.succeed) {
            enqueueSnackbar(getErrorCodeTranslation(response.errorMessageId), { variant: 'error' });
            return;
        }

        enqueueSnackbar(translations.subjectWasSaved, { variant: 'success' });
        history.replace(teachersPath);
    };

    const isFormValid = Object.values(teacherValidation).every((val: boolean) => val);
    const btnEnabled = !state.isBusy && isFormValid && prioritiesLoaded;
    const pageTitle = String.Format(!isInEditMode ? translations.addX : translations.editX, translations.teachers);
    
    return <Container maxWidth="md">
        <PageTitle title={pageTitle} showLoading={state.isBusy} showBackIcon backPath={teachersPath} />
        <form>
            <Grid container justify="center" direction="row" spacing={1}>
                <Grid item xs={12} sm={6}>
                    <TextField
                        variant="outlined"
                        margin="normal"
                        fullWidth
                        required
                        size="small"
                        label={translations.identifierNumber}
                        value={teacher.identifierNumber}
                        onChange={handleChange('identifierNumber')}
                        type="number"
                        error={showValidationError('isIdentifierNumberValid', 'isIdentifierNumberDirty')}
                        helperText={showValidationError('isIdentifierNumberValid', 'isIdentifierNumberDirty') ? translations.invalidIdentifierNumber : ''}
                        InputProps={{
                            inputProps: {
                                maxLength: 10,
                            },
                            startAdornment: <InputAdornment position="start">
                                <FontAwesomeIcon icon={faIdCard} />
                            </InputAdornment>
                        }}
                    />
                </Grid>
                <Grid item xs={12} sm={6}>
                    <PrioritiesAutocomplete
                        selectedValue={teacher.priorityId}
                        onPrioritiesLoaded={onPrioritiesLoaded}
                        onPrioritySelected={onPrioritySelected} />
                </Grid>
                <Grid item xs={12} sm={6}>
                    <TextField
                        variant="outlined"
                        margin="normal"
                        fullWidth
                        required
                        size="small"
                        label={translations.firstName}
                        value={teacher.firstName}
                        onChange={handleChange('firstName')}
                        type="text"
                        error={showValidationError('isFirstNameValid', 'isFirstNameDirty')}
                        helperText={showValidationError('isFirstNameValid', 'isFirstNameDirty') ? translations.invalidName : ''}
                        InputProps={{
                            inputProps: {
                                maxLength: 6,
                            },
                            startAdornment: <InputAdornment position="start">
                                <FontAwesomeIcon icon={faFont} />
                            </InputAdornment>
                        }}
                    />
                </Grid>
                <Grid item xs={12} sm={6}>
                    <TextField
                        variant="outlined"
                        margin="normal"
                        fullWidth
                        required
                        size="small"
                        value={teacher.firstLastName}
                        label={translations.firstLastName}
                        onChange={handleChange('firstLastName')}
                        type="text"
                        error={showValidationError('isFirstLastNameValid', 'isFirstLastNameDirty')}
                        helperText={showValidationError('isFirstLastNameValid', 'isFirstLastNameDirty') ? translations.invalidLastName : ''}
                        InputProps={{
                            startAdornment: <InputAdornment position="start">
                                <FontAwesomeIcon icon={faBold} />
                            </InputAdornment>
                        }}
                    />
                </Grid>
                <Grid item xs={12} sm={6}>
                    <TextField
                        variant="outlined"
                        margin="normal"
                        fullWidth
                        size="small"
                        value={teacher.secondName}
                        label={translations.secondName}
                        onChange={handleChange('secondName')}
                        type="text"
                        error={showValidationError('isSecondNameValid', 'isSecondNameDirty')}
                        helperText={showValidationError('isSecondNameValid', 'isSecondNameDirty') ? translations.invalidName : ''}
                        InputProps={{
                            startAdornment: <InputAdornment position="start">
                                <FontAwesomeIcon icon={faFont} />
                            </InputAdornment>
                        }}
                    />
                </Grid>
                <Grid item xs={12} sm={6}>
                    <TextField
                        variant="outlined"
                        margin="normal"
                        fullWidth
                        size="small"
                        value={teacher.secondLastName}
                        label={translations.secondLastName}
                        onChange={handleChange('secondLastName')}
                        type="text"
                        error={showValidationError('isSecondLastNameValid', 'isSecondLastNameDirty')}
                        helperText={showValidationError('isSecondLastNameValid', 'isSecondLastNameDirty') ? translations.invalidLastName : ''}
                        InputProps={{
                            startAdornment: <InputAdornment position="start">
                                <FontAwesomeIcon icon={faBold} />
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

export default Teacher;
