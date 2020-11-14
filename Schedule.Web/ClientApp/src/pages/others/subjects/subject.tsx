import { faCode, faHourglassEnd, faHourglassStart, faIdCard } from '@fortawesome/free-solid-svg-icons';
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
import CareersAutocomplete from '../../../components/others/careers-autocomplete';
import ClassroomtypeAutocomplete from '../../../components/others/classroomtype-autocomplete';
import SemestersAutocomplete from '../../../components/others/semesters-autocomplete';
import PageTitle from '../../../components/page-title/page-title';
import {
    IGetAllCareersResponseDto,
    IGetAllClassroomTypesResponseDto,
    IGetAllSemestersResponseDto,
    ISaveSubjectRequestDto
} from '../../../models';
import { subjectsPath } from '../../../routes';
import { createSubject, getSubject, updateSubject } from '../../../services/subject.service';
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

interface SubjectState {
    code: number;
    name: string;
    totalAcademicHours: number;
    academicHoursPerWeek: number;
    careerId: number;
    semesterId: number;
    classroomSubjectId: number;
}

interface SubjectValidationState {
    isCodeValid: boolean;
    isCodeDirty: boolean;
    isNameValid: boolean;
    isNameDirty: boolean;
    areTotalAcademicHoursValid: boolean;
    areTotalAcademicHoursDirty: boolean;
    areAcademicHoursPerWeekHoursValid: boolean;
    areAcademicHoursPerWeekHoursDirty: boolean;
    isCareerIdValid: boolean;
    isSemesterIdValid: boolean;
    isClassroomSubjectIdValid: boolean;
}

interface ParamTypes {
    id: string;
}

function Subject() {
    const classes = useStyles();
    const [state, setState] = useState<State>({
        isBusy: false,
    });

    const [careersLoaded, setCareersLoaded] = useState<boolean>(false);
    const [semestersLoaded, setSemestersLoaded] = useState<boolean>(false);
    const [classroomTypesLoaded, setClassroomTypesLoaded] = useState<boolean>(false);

    const [subject, setSubject] = useState<SubjectState>({
        code: 0,
        name: '',
        academicHoursPerWeek: 4,
        totalAcademicHours: 72,
        careerId: 0,
        classroomSubjectId: 0,
        semesterId: 0,
    });

    const [subjectValidation, setSubjectValidation] = useState<SubjectValidationState>({
        isCodeValid: false,
        isCodeDirty: false,
        isNameValid: false,
        isNameDirty: false,
        areTotalAcademicHoursValid: true,
        areTotalAcademicHoursDirty: true,
        areAcademicHoursPerWeekHoursValid: true,
        areAcademicHoursPerWeekHoursDirty: true,
        isCareerIdValid: false,
        isSemesterIdValid: false,
        isClassroomSubjectIdValid: false,
    });

    const { enqueueSnackbar } = useSnackbar();
    const history = useHistory();
    const params = useParams<ParamTypes>();
    const isInEditMode = validator.isInt(params.id) && +params.id > 0;

    const loadSubject = useCallback(async () => {
        if (!isInEditMode) {
            return;
        }

        setState(s => ({ ...s, isBusy: true }));
        const response = await getSubject(+params.id);
        if (!response.succeed) {
            enqueueSnackbar(getErrorCodeTranslation(response.errorMessageId), { variant: 'error' });
            setState(s => ({ ...s, isBusy: false }));
            return;
        }
        const subject = response.result;
        setSubject({
            code: subject.code,
            name: subject.name,
            academicHoursPerWeek: subject.academicHoursPerWeek,
            totalAcademicHours: subject.totalAcademicHours,
            careerId: subject.careerId,
            classroomSubjectId: subject.classroomTypePerSubjectId,
            semesterId: subject.semesterId
        });

        setSubjectValidation(sv => ({
            ...sv,
            isCodeValid: true,
            isCodeDirty: true,
            isNameValid: true,
            isNameDirty: true,
            isCareerIdValid: true,
            isSemesterIdValid: true,
            isClassroomSubjectIdValid: true,
        }));
        setState(s => ({ ...s, isBusy: false }));
    }, [isInEditMode, params.id, enqueueSnackbar]);

    useEffect(() => {
        loadSubject();
    }, [loadSubject]);

    const handleSemestersLoaded = useCallback(() => {
        setSemestersLoaded(true);
    }, []);

    const handleSemesterChange = (item: IGetAllSemestersResponseDto | null) => {
        const id = item?.id ?? 0;
        setSubject({ ...subject, semesterId: id });
        setSubjectValidation({ ...subjectValidation, isSemesterIdValid: id > 0 });
    };

    const handleClassroomTypesLoaded = useCallback(() => {
        setClassroomTypesLoaded(true);
    }, []);

    const handleClassroomTypeChange = (item: IGetAllClassroomTypesResponseDto | null) => {
        const id = item?.id ?? 0;
        setSubject({ ...subject, classroomSubjectId: id });
        setSubjectValidation({ ...subjectValidation, isClassroomSubjectIdValid: id > 0 });
    };

    const handleCareersLoaded = useCallback(() => {
        setCareersLoaded(true);
    }, []);

    const handleCareerChange = (item: IGetAllCareersResponseDto | null) => {
        const id = item?.id ?? 0;
        setSubject({ ...subject, careerId: id });
        setSubjectValidation({ ...subjectValidation, isCareerIdValid: id > 0 });
    };

    const handleSubjectChange = (prop: keyof SubjectState) => (event: React.ChangeEvent<HTMLInputElement>) => {
        let newVal = event.target.value;
        const newSubjectState = { ...subject };
        const newSubjectValidationState = { ...subjectValidation };
        switch (prop) {
            case 'name':
                newSubjectState.name = newVal;
                newSubjectValidationState.isNameDirty = true;
                newSubjectValidationState.isNameValid = validator.isLength(newVal, { min: 4, max: 100 });
                break;
            case 'code':
                newSubjectState.code = +newVal;
                newSubjectValidationState.isCodeDirty = true;
                newSubjectValidationState.isCodeValid = validator.isLength(newVal, { min: 4, max: 6 }) && validator.isInt(newVal);
                break;
            case 'academicHoursPerWeek':
                if (newVal.length > 2) {
                    newVal = newVal.slice(0, 2);
                }
                newSubjectState.academicHoursPerWeek = +newVal;
                newSubjectValidationState.areAcademicHoursPerWeekHoursDirty = true;
                newSubjectValidationState.areAcademicHoursPerWeekHoursValid =
                    validator.isLength(newVal, { min: 1, max: 2 }) &&
                    validator.isInt(newVal, { min: 1, max: 12 }) &&
                    +newVal < newSubjectState.totalAcademicHours;
                break;
            case 'totalAcademicHours':
                if (newVal.length > 2) {
                    newVal = newVal.slice(0, 2);
                }
                newSubjectState.totalAcademicHours = +newVal;
                newSubjectValidationState.areTotalAcademicHoursDirty = true;
                newSubjectValidationState.areTotalAcademicHoursValid =
                    validator.isLength(newVal, { min: 1, max: 2 }) &&
                    validator.isInt(newVal, { min: 1, max: 99 }) &&
                    +newVal > newSubjectState.academicHoursPerWeek;
                break;
            default:
                break;
        }

        setSubject(newSubjectState);
        setSubjectValidation(newSubjectValidationState);
    };

    const showValidationError = (isValid: keyof SubjectValidationState, isDirty: keyof SubjectValidationState) => {
        return !subjectValidation[isValid] && subjectValidation[isDirty];
    };

    const saveChanges = async () => {
        setState({ ...state, isBusy: true });
        const request: ISaveSubjectRequestDto = {
            code: subject.code,
            name: subject.name,
            academicHoursPerWeek: subject.academicHoursPerWeek,
            totalAcademicHours: subject.totalAcademicHours,
            careerId: subject.careerId,
            classroomTypePerSubjectId: subject.classroomSubjectId,
            semesterId: subject.semesterId
        };

        const response = isInEditMode
            ? await updateSubject(+params.id, request)
            : await createSubject(request);

        setState({ ...state, isBusy: false });
        if (!response.succeed) {
            enqueueSnackbar(getErrorCodeTranslation(response.errorMessageId), { variant: 'error' });
            return;
        }

        enqueueSnackbar(translations.subjectWasSaved, { variant: 'success' });
        history.replace(subjectsPath);
    };
    const isFormValid = Object.values(subjectValidation).every((val: boolean) => val);
    const btnEnabled = !state.isBusy && isFormValid && careersLoaded && semestersLoaded && classroomTypesLoaded;
    const pageTitle = String.Format(!isInEditMode ? translations.addX : translations.editX, translations.subjects);

    const onSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        if (!btnEnabled)
            return;

        await saveChanges();
    };

    return <Container maxWidth="md">
        <PageTitle title={pageTitle} showLoading={state.isBusy} showBackIcon backPath={subjectsPath} />
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
                        value={subject.name}
                        onChange={handleSubjectChange('name')}
                        type="text"
                        error={showValidationError('isNameValid', 'isNameDirty')}
                        helperText={showValidationError('isNameValid', 'isNameDirty') ? translations.invalidName : ''}
                        InputProps={{
                            inputProps: {
                                maxLength: 100,
                            },
                            startAdornment: <InputAdornment position="start">
                                <FontAwesomeIcon icon={faIdCard} />
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
                        label={translations.code}
                        value={subject.code}
                        onChange={handleSubjectChange('code')}
                        type="text"
                        error={showValidationError('isCodeValid', 'isCodeDirty')}
                        helperText={showValidationError('isCodeValid', 'isCodeDirty') ? translations.invalidCode : ''}
                        InputProps={{
                            inputProps: {
                                maxLength: 6,
                            },
                            startAdornment: <InputAdornment position="start">
                                <FontAwesomeIcon icon={faCode} />
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
                        value={subject.academicHoursPerWeek}
                        label={translations.academicHoursPerWeeek}
                        onChange={handleSubjectChange('academicHoursPerWeek')}
                        type="number"
                        error={showValidationError('areAcademicHoursPerWeekHoursValid', 'areAcademicHoursPerWeekHoursDirty')}
                        helperText={showValidationError('areAcademicHoursPerWeekHoursValid', 'areAcademicHoursPerWeekHoursDirty') ? translations.invalidHours : ''}
                        InputProps={{
                            startAdornment: <InputAdornment position="start">
                                <FontAwesomeIcon icon={faHourglassStart} />
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
                        value={subject.totalAcademicHours}
                        label={translations.totalAcademicHours}
                        onChange={handleSubjectChange('totalAcademicHours')}
                        type="number"
                        error={showValidationError('areTotalAcademicHoursValid', 'areTotalAcademicHoursDirty')}
                        helperText={showValidationError('areTotalAcademicHoursValid', 'areTotalAcademicHoursDirty') ? translations.invalidHours : ''}
                        InputProps={{
                            startAdornment: <InputAdornment position="start">
                                <FontAwesomeIcon icon={faHourglassEnd} />
                            </InputAdornment>
                        }}
                    />
                </Grid>
                <Grid item xs={12} sm={4}>
                    <SemestersAutocomplete
                        selectedValue={subject.semesterId}
                        onSemesterSelected={handleSemesterChange}
                        onSemesterLoaded={handleSemestersLoaded} />
                </Grid>
                <Grid item xs={12} sm={4}>
                    <ClassroomtypeAutocomplete
                        isInEditMode={isInEditMode}
                        canSearch={!isInEditMode || classroomTypesLoaded}
                        selectedValue={subject.classroomSubjectId}
                        onClassroomTypeSelected={handleClassroomTypeChange}
                        onClassroomTypesLoaded={handleClassroomTypesLoaded} />
                </Grid>
                <Grid item xs={12} sm={4}>
                    <CareersAutocomplete
                        selectedValue={subject.careerId}
                        onCareerLoaded={handleCareersLoaded}
                        onCareerSelected={handleCareerChange} />
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

export default Subject
