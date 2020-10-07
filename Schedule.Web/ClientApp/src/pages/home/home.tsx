import { Container, CssBaseline, Grid } from '@material-ui/core';
import React, { useContext } from 'react'
import { String } from 'typescript-string-operations';

import Downloads from '../../components/home/downloads';
import CurrentAcademicPeriod from '../../components/home/current-academic-period';
import { AuthContext } from '../../contexts/auth-context';
import PageTitle from '../../components/page-title/page-title';
import translations from '../../services/translations';


function Home() {
    const [authContext, setAuthContext] = useContext(AuthContext);

    const welcomeMsg = String.Format(translations.welcomeX, 'unexpolcm');

    return <Container style={{ padding: '0px 20px' }}>
        <CssBaseline />
        <Grid container direction="row" justify="center" alignItems="center" spacing={5}>
            <PageTitle title={welcomeMsg} />
            <Grid item xs={12} sm={12} md={6}>
                <Downloads />
            </Grid>
            <Grid item xs={12} sm={12} md={6}>
                <CurrentAcademicPeriod />
            </Grid>
        </Grid>
    </Container>;
}

export default Home;