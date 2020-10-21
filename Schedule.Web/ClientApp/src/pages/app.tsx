import React, { Fragment } from 'react';
import { BrowserRouter } from 'react-router-dom';
import { SnackbarProvider } from 'notistack';

import Header from '../components/header/header';
import AutoLogin from '../components/auto-login/auto-login';
import Footer from '../components/footer/footer';
import { AuthContextProvider } from '../contexts/auth-context';
import { TranslationContextProvider } from '../contexts/translations-context';


function App() {
  return <Fragment>
    <AuthContextProvider>
      <SnackbarProvider autoHideDuration={3000} anchorOrigin={{
        vertical: 'bottom',
        horizontal: 'right',
      }}>
        <TranslationContextProvider>
          <BrowserRouter>
            <Header />
            <AutoLogin />
            <Footer />
          </BrowserRouter>
        </TranslationContextProvider>
      </SnackbarProvider>
    </AuthContextProvider>
  </Fragment>;
}

export default App;
