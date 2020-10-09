import React, { Fragment, Suspense } from 'react';
import { CircularProgress, Container, Grid } from '@material-ui/core';
import { BrowserRouter } from 'react-router-dom';
import { SnackbarProvider } from 'notistack';

import Footer from '../components/footer/footer';
import Header from '../components/header/header';
import { AuthContextProvider } from '../contexts/auth-context';
import { TranslationContextProvider } from '../contexts/translations-context';
import { AppRoutes } from '../routes';

function App() {
  const loading = <Container>
    <Grid container justify="center" alignItems="center" direction="column" style={{ minHeight: '60vh' }}>
      <Grid item xs={12}>
        <CircularProgress />
      </Grid>
    </Grid>
  </Container>;

  return <Fragment>
    <AuthContextProvider>
      <SnackbarProvider>
        <TranslationContextProvider>
          <BrowserRouter>
            <Header />
            <Suspense fallback={loading}>
              <div style={{ marginTop: '20px', marginBottom: '120px' }}>
                <AppRoutes />
              </div>
            </Suspense>
            <Footer />
          </BrowserRouter>
        </TranslationContextProvider>
      </SnackbarProvider>
    </AuthContextProvider>
  </Fragment>;
}

export default App;
