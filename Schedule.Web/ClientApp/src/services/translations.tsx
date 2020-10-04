import LocalizedStrings, { LocalizedStringsMethods } from "react-localization";

interface IStrings extends LocalizedStringsMethods {
    schedules: string;
    home: string;
    changePassword: string;
    username: string;
    password: string;
    rememberMe: string;
    signIn: string;
    forgotPassword: string;
    invalidEmail: string;
    invalidPassword: string;
}

const translations: IStrings = new LocalizedStrings({
    en: {
        schedules: "Schedules",
        home: "Home",
        changePassword: 'Change Password',
        username: 'Username',
        password: 'Password',
        rememberMe: 'Remember me',
        signIn: 'Sign In',
        forgotPassword: 'Forgot Password',
        invalidEmail: 'Invalid email',
        invalidPassword: 'Invalid password',
    },
    es: {
        schedules: "Schedules",
        home: "Inicio",
        changePassword: 'Cambiar Contraseña',
        username: 'Usuario',
        password: 'Contraseña',
        rememberMe: 'Recuerdame',
        signIn: 'Ingresar',
        forgotPassword: 'Olvidé mi Contraseña',
        invalidEmail: 'El email no es válido',
        invalidPassword: 'La contraseña no es válida',
    },
});

export default translations;
