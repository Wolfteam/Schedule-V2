import React, { Suspense, useContext, useEffect, useState } from 'react'
import { CircularProgress, Container, Grid } from '@material-ui/core';

import { AuthContext } from '../../contexts/auth-context';
import { isUserLogged } from '../../services/account.service';
import { AppRoutes, HomePath, LoginPath } from '../../routes';
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

    useEffect(() => {
        isUserLogged().then(response => {
            if (!response.result) {
                setState({ isCheckingAuth: false });
                return;
            }
            const user = response.result;
            const pathToUse = history.location.pathname !== LoginPath ? history.location.pathname : HomePath;

            console.log("Auto login result", user);
            if (setAuthContext) {
                setAuthContext({ username: user.username, isAuthenticated: true });
            }
            setState({ isCheckingAuth: false });
            history.replace(pathToUse);
        });
    }, []);

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

export default AutoLogin
