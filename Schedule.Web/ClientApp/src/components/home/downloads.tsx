import {
    Button,
    Card,
    CardActions,
    CardContent,
    createStyles,
    Divider,
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
        buttonContainer: {
            justifyContent: 'center',
        },
        button: {
            color: 'orange !important'
        }
    }),
);

function Downloads() {
    const classes = useStyles();

    return <Card className={classes.root}>
        <CardContent>
            <Typography variant="h5">
                {translations.downloads}
            </Typography>
            <Typography>
                {translations.planificationsMsg}
            </Typography>
        </CardContent>
        <Divider />
        <CardActions className={classes.buttonContainer}>
            <Button className={classes.button} size="small">{translations.academicPlanification}</Button>
            <Button className={classes.button} size="small">{translations.clasroomsPlanification}</Button>
            <Button className={classes.button} size="small">{translations.hoursPlanification}</Button>
        </CardActions>
    </Card>;
}

export default Downloads
