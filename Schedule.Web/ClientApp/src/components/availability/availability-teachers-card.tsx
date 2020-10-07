import React from 'react';
import {
    createStyles,
    Grid,
    InputAdornment,
    makeStyles,
    TextField,
    Theme,
    Button,
    CardContent,
    Card
} from '@material-ui/core';
import { faHourglass, faHourglassEnd } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Autocomplete } from '@material-ui/lab';
import translations from '../../services/translations';

import * as responses from '../../models/responses';

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        cards: {
            margin: '20px 10px'
        },
        submitButton: {
            margin: 'auto',
            display: 'flex'
        },
    }),
);

interface Props {
    teachers: responses.ITeacherResponseDto[];
    hoursToComplete: number;
    remainingHours: number;
    isSaveButtonEnabled: boolean;
    onTeacherChange(newValue: responses.ITeacherResponseDto): void;
    onSaveChanges(): void;
}

function AvailabilityTeachersCard(props: Props) {
    const classes = useStyles();

    const handleTeacherChange = (event: React.ChangeEvent<{}>, newValue: responses.ITeacherResponseDto | null) => {
        if (!newValue)
            return;

        props.onTeacherChange(newValue);
    };

    return <Card className={classes.cards}>
        <CardContent>
            <Grid container direction="column" justify="space-between" spacing={2}>
                <Grid item>
                    <Autocomplete
                        fullWidth
                        size="small"
                        onChange={handleTeacherChange}
                        options={props.teachers}
                        getOptionLabel={(teacher: responses.ITeacherResponseDto) => `${teacher.name} ${teacher.lastName}`}
                        style={{ marginTop: '7px' }}
                        renderInput={(params: any) => <TextField {...params} label={translations.profesors} variant="outlined" />} />
                </Grid>

                <Grid item>
                    <TextField
                        variant="outlined"
                        margin="normal"
                        fullWidth
                        size="small"
                        label="Horas a cumplir"
                        type="text"
                        value={props.hoursToComplete}
                        disabled={true}
                        InputProps={{
                            startAdornment: (<InputAdornment position="start" >
                                <FontAwesomeIcon icon={faHourglassEnd} />
                            </InputAdornment>),
                        }} />
                </Grid>

                <Grid item>
                    <TextField
                        variant="outlined"
                        margin="normal"
                        fullWidth
                        size="small"
                        label="Horas por asignar"
                        value={props.remainingHours}
                        type="text"
                        disabled={true}
                        InputProps={{
                            startAdornment: (<InputAdornment position="start" >
                                <FontAwesomeIcon icon={faHourglass} />
                            </InputAdornment>),
                        }} />
                </Grid>

                <Grid item>
                    <Button
                        type="button"
                        variant="contained"
                        color="primary"
                        onClick={props.onSaveChanges}
                        className={classes.submitButton}
                        disabled={!props.isSaveButtonEnabled}>
                        {translations.saveChanges}
                    </Button>
                </Grid>
            </Grid>
        </CardContent>
    </Card>;
}

export default React.memo(AvailabilityTeachersCard);
