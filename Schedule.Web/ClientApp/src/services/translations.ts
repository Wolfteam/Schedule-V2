import LocalizedStrings, { LocalizedStringsMethods } from "react-localization";

interface ITranslations {
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
    classroomsPlanification: string;
    hoursPlanification: string;
    welcomeX: string;
    currentAcademicPeriod: string;
    teachers: string;
    loadAvailability: string;
    others: string;
    classrooms: string;
    subjects: string;
    priorities: string;
    sections: string;
    semesters: string;
    users: string;
    careers: string;
    careerPeriod: string;
    logout: string;
    hour: string;
    monday: string;
    tuesday: string;
    wednesday: string;
    thursday: string;
    friday: string;
    saturday: string;
    sunday: string;
    lunchHour: string;
}

interface IStrings extends LocalizedStringsMethods, ITranslations {
}

const enTrans: ITranslations = {
    schedules: "Schedules",
    home: "Home",
    changePassword: "Change Password",
    username: "Username",
    password: "Password",
    rememberMe: "Remember me",
    signIn: "Sign In",
    forgotPassword: "Forgot Password",
    invalidEmail: "Invalid email",
    invalidPassword: "Invalid password",
    passwordDoesntMatch: "Password does not match",
    saveChanges: "Save changes",
    currentPassword: "Current password",
    newPassword: "New password",
    newPasswordConfirm: "New password confirm",
    downloads: "Downloads",
    planificationsMsg: "Here you will find a link a link to each of the planifications for this semester",
    academicPlanification: "Academic planification",
    classroomsPlanification: "Classroom planification",
    hoursPlanification: "Hours planification",
    welcomeX: "Welcome {0}",
    currentAcademicPeriod: "Current academic period",
    teachers: "Teachers",
    loadAvailability: "Load availability",
    others: 'Others',
    classrooms: 'Classrooms',
    subjects: 'Subjects',
    priorities: 'Priorities',
    sections: 'Sections',
    semesters: 'Semesters',
    users: 'Users',
    careers: 'Careers',
    careerPeriod: 'Career period',
    logout: 'Logout',
    hour: 'Hour',
    monday: 'Monday',
    tuesday: 'Tuesday',
    wednesday: 'Wednesday',
    thursday: 'Thursday',
    friday: 'Friday',
    saturday: 'Saturday',
    sunday: 'Sunday',
    lunchHour: 'Lunch hour',
};

const esTrans: ITranslations = {
    schedules: "Schedules",
    home: "Inicio",
    changePassword: "Cambiar Contraseña",
    username: "Usuario",
    password: "Contraseña",
    rememberMe: "Recuerdame",
    signIn: "Ingresar",
    forgotPassword: "Olvidé mi Contraseña",
    invalidEmail: "El email no es válido",
    invalidPassword: "La contraseña no es válida",
    passwordDoesntMatch: "La contraseña no coincide",
    saveChanges: "Guardar cambios",
    currentPassword: "Contraseña actual",
    newPassword: "Nueva contraseña",
    newPasswordConfirm: "Confirmar nueva contraseña",
    downloads: "Descargas",
    planificationsMsg: "Aqui encontraras los links a las planificaciones del semestre actual",
    academicPlanification: "Planificación académica",
    classroomsPlanification: "Planificación de aulas",
    hoursPlanification: "Planificación de horas",
    welcomeX: "Bienvenido {0}",
    currentAcademicPeriod: "Periódo académico actual",
    teachers: "Profesores",
    loadAvailability: "Cargar disponibilidad",
    others: 'Otros',
    classrooms: 'Aulas',
    subjects: 'Materias',
    priorities: 'Prioridades',
    sections: 'Secciones',
    semesters: 'Semestres',
    users: 'Usuarios',
    careers: 'Carreras',
    careerPeriod: 'Carreras por período',
    logout: 'Salir',
    hour: 'Hora',
    monday: 'Lunes',
    tuesday: 'Martes',
    wednesday: 'Miercoles',
    thursday: 'Jueves',
    friday: 'Viernes',
    saturday: 'Sábado',
    sunday: 'Domingo',
    lunchHour: 'Hora de almuerzo',
};

const translations: IStrings = new LocalizedStrings({
    en: enTrans,
    es: esTrans
});

export default translations;
