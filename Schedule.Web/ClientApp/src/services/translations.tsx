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
    passwordDoesntMatch: string;
    saveChanges: string;
    currentPassword: string;
    newPassword: string;
    newPasswordConfirm: string;
    downloads: string;
    planificationsMsg: string;
    academicPlanification: string;
    clasroomsPlanification: string;
    hoursPlanification: string;
    welcomeX: string;
    currentAcademicPeriod: string;
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
        passwordDoesntMatch: 'Password does not match',
        saveChanges: 'Save changes',
        currentPassword: 'Current password',
        newPassword: 'New password',
        newPasswordConfirm: 'New password confirm',
        downloads: 'Downloads',
        planificationsMsg: 'Here you will find a link a link to each of the planifications for this semester',
        academicPlanification: 'Academic planification',
        clasroomsPlanification: 'Classroom planification',
        hoursPlanification: 'Hours planification',
        welcomeX: 'Welcome {0}',
        currentAcademicPeriod: 'Current academic period',
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
        passwordDoesntMatch: 'La contraseña no coincide',
        saveChanges: 'Guardar cambios',
        currentPassword: 'Contraseña actual',
        newPassword: 'Nueva contraseña',
        newPasswordConfirm: 'Confirmar nueva contraseña',
        downloads: 'Descargas',
        planificationsMsg: 'Aqui encontraras los links a las planificaciones del semestre actual',
        academicPlanification: 'Planificación académica',
        clasroomsPlanification: 'Planificación de aulas',
        hoursPlanification: 'Planificación de horas',
        welcomeX: 'Bienvenido {0}',
        currentAcademicPeriod: 'Periódo académico actual',
    },
});

export default translations;
