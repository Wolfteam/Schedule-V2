import { faFill, faFont } from '@fortawesome/free-solid-svg-icons';
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
import ClassroomtypeAutocomplete from '../../../components/others/classroomtype-autocomplete';
import PageTitle from '../../../components/page-title/page-title';
import {
    IGetAllClassroomTypesResponseDto,
    ISaveClassroomRequestDto
} from '../../../models';
import { classRoomsPath } from '../../../routes';
import { createClassroom, getClassroom, updateClassroom } from '../../../services/classroom.service';
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

interface ClassroomState {
    name: string;
    capacity: number;
    classroomSubjectId: number;
}

interface ClassroomValidationState {
    isNameValid: boolean;
    isNameDirty: boolean;
    isCapacityValid: boolean;
    isCapacityDirty: boolean;
    isClassroomSubjectIdValid: boolean;
}

interface ParamTypes {
    id: string;
}

function Classroom() {
    const classes = useStyles();
    const [state, setState] = useState<State>({
        isBusy: false,
    });

    const [classroomTypesLoaded, setClassroomTypesLoaded] = useState<boolean>(false);

    const [classroom, setClassroom] = useState<ClassroomState>({
        name: '',
        capacity: 40,
        classroomSubjectId: 0
    });

    const [classroomValidation, setClassroomValidation] = useState<ClassroomValidationState>({
        isNameValid: false,
        isNameDirty: false,
        isCapacityValid: true,
        isCapacityDirty: true,
        isClassroomSubjectIdValid: false,
    });

    const { enqueueSnackbar } = useSnackbar();
    const history = useHistory();
    const params = useParams<ParamTypes>();
    const isInEditMode = validator.isInt(params.id) && +params.id > 0;

    const loadClassroom = async () => {
        if (!isInEditMode) {
            return;
        }

        setState({ ...state, isBusy: true });
        const response = await getClassroom(+params.id);
        if (!response.succeed) {
            enqueueSnackbar(getErrorCodeTranslation(response.errorMessageId), { variant: 'error' });
            setState({ ...state, isBusy: false });
            return;
        }
        const classroom = response.result;
        setClassroom({
            name: classroom.name,
            capacity: classroom.capacity,
            classroomSubjectId: classroom.classroomSubjectId
        });

        setClassroomValidation({
            isCapacityDirty: true,
            isCapacityValid: true,
            isClassroomSubjectIdValid: true,
            isNameDirty: true,
            isNameValid: true
        });
        setState({ ...state, isBusy: false });
    };

    useEffect(() => {
        loadClassroom();
    }, []);

    const handleClassroomTypesLoaded = () => {
        setClassroomTypesLoaded(true);
    };

    const handleClassroomTypeChange = (item: IGetAllClassroomTypesResponseDto | null) => {
        const id = item?.id ?? 0;
        setClassroom({ ...classroom, classroomSubjectId: id });
        setClassroomValidation({ ...classroomValidation, isClassroomSubjectIdValid: id > 0 });
    };

    const handleChange = (prop: keyof ClassroomState) => (event: React.ChangeEvent<HTMLInputElement>) => {
        let newVal = event.target.value;
        const newState = { ...classroom };
        const newValidationState = { ...classroomValidation };
        switch (prop) {
            case 'name':
                newState[prop] = newVal;
                const isValid = !validator.isEmpty(newVal) && validator.isLength(newVal, { min: 3, max: 50 });
                newValidationState.isNameValid = isValid;
                newValidationState.isNameDirty = true;
                break;
            case 'capacity':
                newState.capacity = +newVal;
                newValidationState.isCapacityValid = validator.isInt(newVal, { min: 8, max: 100 }) && validator.isLength(newVal, { min: 1, max: 2 });
                newValidationState.isCapacityDirty = true;
                break;
            default:
                break;
        }
        setClassroom(newState);
        setClassroomValidation(newValidationState);
    };

    const showValidationError = (isValid: keyof ClassroomValidationState, isDirty: keyof ClassroomValidationState) => {
        return !classroomValidation[isValid] && classroomValidation[isDirty];
    };

    const saveChanges = async () => {
        setState({ ...state, isBusy: true });
        const request: ISaveClassroomRequestDto = {
            name: classroom.name,
            capacity: classroom.capacity,
            classroomSubjectId: classroom.classroomSubjectId
        };

        const response = isInEditMode
            ? await updateClassroom(+params.id, request)
            : await createClassroom(request);

        setState({ ...state, isBusy: false });
        if (!response.succeed) {
            enqueueSnackbar(getErrorCodeTranslation(response.errorMessageId), { variant: 'error' });
            return;
        }

        enqueueSnackbar(translations.classroomWasSaved, { variant: 'success' });
        history.replace(classRoomsPath);
    };

    const isFormValid = Object.values(classroomValidation).every((val: boolean) => val);
    const btnEnabled = !state.isBusy && isFormValid && classroomTypesLoaded;
    const pageTitle = String.Format(!isInEditMode ? translations.addX : translations.editX, translations.classrooms);

    const onSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        if (!btnEnabled)
            return;

        await saveChanges();
    };

    return <Container maxWidth="md">
        <PageTitle title={pageTitle} showLoading={state.isBusy} showBackIcon backPath={classRoomsPath} />
        <form onSubmit={onSubmit}>
            <Grid container justify="center" direction="row" spacing={1}>
                <Grid item xs={12} sm={6}>
                    <TextField
                        variant="outlined"
                        margin="normal"
                        fullWidth
                        required
                        size="small"
                        label={translations.name}
                        value={classroom.name}
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
                <Grid item xs={12} sm={6}>
                    <TextField
                        variant="outlined"
                        margin="normal"
                        fullWidth
                        required
                        size="small"
                        label={translations.capacity}
                        value={classroom.capacity}
                        onChange={handleChange('capacity')}
                        type="number"
                        error={showValidationError('isCapacityValid', 'isCapacityDirty')}
                        helperText={showValidationError('isCapacityValid', 'isCapacityDirty') ? translations.invalidCapacity : ''}
                        InputProps={{
                            inputProps: {
                                maxLength: 2,
                            },
                            startAdornment: <InputAdornment position="start">
                                <FontAwesomeIcon icon={faFill} />
                            </InputAdornment>
                        }}
                    />
                </Grid>
                <Grid item xs={12}>
                    <ClassroomtypeAutocomplete
                        isInEditMode={isInEditMode}
                        canSearch={!isInEditMode || classroomTypesLoaded}
                        selectedValue={classroom.classroomSubjectId}
                        onClassroomTypeSelected={handleClassroomTypeChange}
                        onClassroomTypesLoaded={handleClassroomTypesLoaded} />
                </Grid>
                <Grid item xs={12}>
                    <Button
                        type="submit"
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

export default Classroom;
