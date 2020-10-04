import React, { Fragment } from 'react';
import { BrowserRouter } from 'react-router-dom';
import Footer from '../components/footer/footer';
import Header from '../components/header/header';
import { AuthContextProvider } from '../contexts/auth-context';
import { TranslationContextProvider } from '../contexts/translations-context';
import { AppRoutes } from '../routes';

function App() {
  return <Fragment>
    <AuthContextProvider>
      <TranslationContextProvider>
        <BrowserRouter>
          <Header />
          <AppRoutes />
          <Footer />
        </BrowserRouter>
      </TranslationContextProvider>
    </AuthContextProvider>
  </Fragment>;
}

export default App;
