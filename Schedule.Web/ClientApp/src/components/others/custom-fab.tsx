import React from 'react'
import {
    createStyles,
    Fab,
    makeStyles,
    Theme,
} from '@material-ui/core';
import { Add } from '@material-ui/icons';

const useStyles = makeStyles((theme: Theme) => createStyles({
    fab: {
        zIndex: 100,
        position: 'fixed',
        bottom: 70,
        right: 20
    }
}));

interface Props {
    onClick: () => void;
}

function CustomFab(props: Props) {
    const classes = useStyles();

    return <Fab color="primary" aria-label="add" className={classes.fab} onClick={props.onClick}>
        <Add />
    </Fab>;
}

export default CustomFab;
