import React, { Suspense, useContext, useEffect, useState } from 'react'
import { CircularProgress, Container, Grid } from '@material-ui/core';

import { AuthContext } from '../../contexts/auth-context';
import { isUserLogged } from '../../services/account.service';
import { AppRoutes, homePath, loginPath } from '../../routes';
import { useHistory } from 'react-router-dom';

interface State {
    isCheckingAuth: boolean;
}

function AutoLogin() {
    const [authContext, setAuthContext] = useContext(AuthContext);
    const [state, setState] = useState<State>({
        isCheckingAuth: true,
    });
    const history = useHistory();

    const checkCurrentUser = async () => {
        const isInLoginPath = history.location.pathname !== loginPath;
        const pathToUse = isInLoginPath ? history.location.pathname : homePath;
        const response = await isUserLogged();
        const user = response.result;

        if (!user) {
            console.log("There is no user, session might have expired...");
            if (!isInLoginPath) {
                history.replace(loginPath);
                setAuthContext({
                    isAuthenticated: false,
                    username: ''
                });
            }
            setState({ isCheckingAuth: false });
            return;
        }

        console.log("User is logged in", user);
        if (setAuthContext) {
            setAuthContext({ username: user.username, isAuthenticated: true });
        }
        setState({ isCheckingAuth: false });
        history.replace(pathToUse);
    };

    useEffect(() => {
        checkCurrentUser();
    }, []);

    useEffect(() => {
        if (!authContext.isAuthenticated)
            return;

        const timeout = setInterval(async () => {
            console.log("Checking if im logged");
            await checkCurrentUser();
        }, 1000 * 30);

        return () => clearInterval(timeout);
    }, [authContext.isAuthenticated]);

    const loading = <Container>
        <Grid container justify="center" alignItems="center" direction="column" style={{ minHeight: '60vh' }}>
            <Grid item xs={12}>
                <CircularProgress />
            </Grid>
        </Grid>
    </Container>;

    const body = state.isCheckingAuth
        ? loading
        : <Suspense fallback={loading}>
            <div style={{ marginTop: '20px', marginBottom: '120px' }}>
                <AppRoutes />
            </div>
        </Suspense>;

    return body;
}

export default React.memo(AutoLogin);
