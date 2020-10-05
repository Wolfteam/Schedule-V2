import {
    Card,
    CardActions,
    CardContent,
    createStyles,
    Grid,
    makeStyles,
    Theme,
    Typography
} from '@material-ui/core';
import { grey } from '@material-ui/core/colors';
import React from 'react'
import translations from '../../services/translations';

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

function CurrentAcademicPeriod() {
    const classes = useStyles();

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
                <h2 className={classes.period}>2017-II</h2>
            </Grid>
        </CardActions>
    </Card>;
}

export default CurrentAcademicPeriod
