import {
    AppBar,
    Toolbar,
    IconButton,
    makeStyles,
    createStyles,
    Theme, Grid
} from '@material-ui/core';
import React, { useContext, useState } from 'react'
import { Menu, List } from '@material-ui/icons';
import grey from '@material-ui/core/colors/grey';
import Drawer from '../drawer/drawer'
import { AuthContext } from '../../contexts/auth-context';
import translations from '../../services/translations';

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        root: {
            flexGrow: 1,
        },
        appBar: {
            backgroundColor: grey[900]
        },
        menuButton: {
            marginRight: theme.spacing(2),
        },
        title: {
            display: 'flex',
            justifyContent: 'center',
            alignItems: 'center',
        }
    }),
);

interface State {
    isDrawerOpened: boolean,
    isDrawerVisible: boolean
}

function Header() {
    const classes = useStyles();

    const [state, setState] = useState<State>({ isDrawerOpened: false, isDrawerVisible: true });

    const [authState, _] = useContext(AuthContext);

    const handleMenuClick = () => {
        handleDrawerChange(true);
    };

    const handleDrawerChange = (isOpen: boolean) => {
        setState({ ...state, isDrawerOpened: isOpen });
    };

    const menuButton = authState?.isAuthenticated
        ? <IconButton
            onClick={handleMenuClick}
            edge="start"
            className={classes.menuButton}
            color="inherit"
            aria-label="menu">
            <Menu />
        </IconButton>
        : null;

    return <div className={classes.root}>
        <AppBar className={classes.appBar} position="static">
            <Toolbar variant="dense">
                {menuButton}
                <Grid container justify="center" alignItems="center">
                    <Grid item>
                        <h1 className={classes.title}>
                            {translations.schedules}
                        </h1>
                    </Grid>
                </Grid>
            </Toolbar>
        </AppBar>
        <Drawer isDrawerOpen={state.isDrawerOpened} onDrawerStateChanged={handleDrawerChange} />
    </div>;
}

export default Header;