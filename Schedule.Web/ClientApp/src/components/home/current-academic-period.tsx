import {
    Card,
    CardActions,
    CardContent,
    CircularProgress,
    createStyles,
    Grid,
    makeStyles,
    Theme,
    Typography
} from '@material-ui/core';
import { grey } from '@material-ui/core/colors';
import React, { useEffect, useState } from 'react'
import translations from '../../services/translations';
import { getCurrentPeriod } from '../../services/period.service';

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        root: {
            flexGrow: 1,
            color: 'white',
            backgroundColor: grey[900],
            padding: '10px'
        },
        period: {
            marginTop: '0px',
            marginBottom: '0px'
        }
    }),
);

interface State {
    period: string;
    isBusy: boolean;
}

function CurrentAcademicPeriod() {
    const classes = useStyles();
    const [state, setState] = useState<State>({
        period: '',
        isBusy: true
    });

    useEffect(() => {
        getCurrentPeriod().then(r => {
            const period = r.result?.name ?? "N/A";
            setState({ ...state, period: period, isBusy: false });
        });
    }, []);

    console.log("rendering academic period");

    const periodElement = state.isBusy
        ? <CircularProgress />
        : <h2 className={classes.period}>{state.period}</h2>;

    return <Card className={classes.root}>
        <CardContent>
            <Typography variant="h5">
                {translations.academicPlanification}
            </Typography>
            <Typography>
                {translations.currentAcademicPeriod + ':'}
            </Typography>
        </CardContent>
        <CardActions>
            <Grid container justify="center" alignItems="center">
                {periodElement}
            </Grid>
        </CardActions>
    </Card>;
}

export default CurrentAcademicPeriod
