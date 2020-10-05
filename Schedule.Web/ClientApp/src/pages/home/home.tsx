import { Container, CssBaseline, Divider, Grid, Typography } from '@material-ui/core';
import React, { useContext } from 'react'
import Downloads from '../../components/home/downloads';
import CurrentAcademicPeriod from '../../components/home/current-academic-period';
import { AuthContext } from '../../contexts/auth-context';
import PageTitle from '../../components/page-title/page-title';


function Home() {
    const [authContext, setAuthContext] = useContext(AuthContext);

    return <Container style={{ padding: '0px 20px' }}>
        <CssBaseline />
        <Grid container direction="row" justify="center" alignItems="center" spacing={5}>
            <PageTitle title="Bienvenido Unexpolcm" />
            <Grid item xs={12} sm={6}>
                <Downloads />
            </Grid>
            <Grid item xs={12} sm={6}>
                <CurrentAcademicPeriod />
            </Grid>
        </Grid>
    </Container>;
}

export default Home;