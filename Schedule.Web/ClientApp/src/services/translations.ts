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
    invalidUsername: string;
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
    unknownError: string;
    availabilityWasSaved: string;
    availabilitiesAreNotValid: string;
    identifierNumber: string;
    name: string;
    priority: string;
    code: string;
    totalAcademicHours: string;
    academicHoursPerWeeek: string;
    subjectType: string;
    semester: string;
    career: string;
    pageXofX: string;
    totalResults: string;
    xItemsSelected: string;
    xItemsWereDeleted: string;
    notAllSelectedItemsWereDeleted: string;
    cancel: string;
    yes: string;
    ok: string;
    deleteSubjects: string;
    deleteItemsConfirm: string;
    search: string;
    itemsPerPage: string;
    invalidName: string;
    invalidCode: string;
    invalidHours: string;
    addSubject: string;
    editSubject: string;
    subjectWasSaved: string;
    classroomType: string;
    createdAt: string;
    firstName: string;
    firstLastName: string;
    secondName: string;
    secondLastName: string;
    invalidIdentifierNumber: string;
    invalidLastName: string;
    addX: string;
    editX: string;
    deleteCareers: string;
    careerWasSaved: string;
    capacity: string;
    deleteX: string;
    classroomWasSaved: string;
    invalidCapacity: string;
    createdBy: string;
    errorCodes: IAppMessageTranslations;
}

interface IAppMessageTranslations {
    SCH_100: string;
    SCH_101: string;
    SCH_102: string;
    SCH_103: string;
    SCH_104: string;

    IDS_1000: string;
    IDS_1001: string;
    IDS_1002: string;

    SCH_2000: string;
    SCH_2001: string;
    SCH_2002: string;
    SCH_2003: string;
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
    invalidUsername: "Invalid username",
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
    unknownError: 'Unknown error',
    availabilityWasSaved: 'Availability was successfully saved',
    availabilitiesAreNotValid: 'One or more availabilities are not valid, please verify',
    identifierNumber: 'Identification number',
    name: 'Name',
    priority: 'Priority',
    code: 'Code',
    totalAcademicHours: 'Total academic hours',
    academicHoursPerWeeek: 'Academic hours per week',
    subjectType: 'Subject type',
    semester: 'Semester',
    career: 'Career',
    pageXofX: 'Page {0} of {1}',
    totalResults: 'Total results',
    xItemsSelected: '{0} item(s) selected',
    xItemsWereDeleted: '{0} {1} were successfully deleted',
    notAllSelectedItemsWereDeleted: 'Not all selected items were deleted',
    cancel: 'Cancel',
    yes: 'Yes',
    ok: 'Ok',
    deleteSubjects: 'Delete subjects',
    deleteItemsConfirm: 'Are you sure you wanna delete the {0} selected item(s) ?',
    search: 'Search',
    itemsPerPage: 'Items per page',
    invalidName: 'Invalid name',
    invalidCode: 'Invalid code',
    invalidHours: 'Invalid hours',
    addSubject: 'Add subject',
    editSubject: 'Edit subject',
    subjectWasSaved: 'Subject was successfully saved',
    classroomType: 'Classrooom type',
    createdAt: 'Created at',
    firstName: 'First name',
    firstLastName: 'First last name',
    secondName: 'Second name',
    secondLastName: 'Second last name',
    invalidIdentifierNumber: 'Invalid identifier number',
    invalidLastName: 'Invalid last name',
    addX: 'Add {0}',
    editX: 'Edit {0}',
    deleteCareers: 'Delete careers',
    careerWasSaved: 'Career was successfully saved',
    capacity: 'Capacity',
    deleteX: 'Delete {0}',
    classroomWasSaved: 'Classroom was successfully saved',
    invalidCapacity: 'Invalid capacity',
    createdBy: 'Created by',
    errorCodes: {
        SCH_100: 'Invalid api request',
        SCH_101: 'Unknown error occurred in api',
        SCH_102: 'Resource was not found in api',
        SCH_103: 'The resource already exists',
        SCH_104: 'Resource cannot be deleted because it is being used',

        IDS_1000: 'Invalid request sent to identity server',
        IDS_1001: 'Unknown error occurred in the identity server',
        IDS_1002: 'Resource was not found in the identity server',

        SCH_2000: 'Invalid request',
        SCH_2001: 'Unknown error',
        SCH_2002: 'Resource was not found',
        SCH_2003: 'Invalid username or password',
    }
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
    invalidUsername: "El nombre de usuario no es válido",
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
    unknownError: 'Unknown error',
    availabilityWasSaved: 'La disponibilidad fue guardada exitosamente',
    availabilitiesAreNotValid: 'Una o mas disponibilidades no son válidas por favor verifique',
    identifierNumber: 'Número de identificación',
    name: 'Nombre',
    priority: 'Prioridad',
    code: 'Código',
    totalAcademicHours: 'Total horas académicas',
    academicHoursPerWeeek: 'Horas académicas semanales',
    subjectType: 'Tipo materia',
    semester: 'Semestre',
    career: 'Carrera',
    pageXofX: 'Página {0} de {1}',
    totalResults: 'Total de resultados',
    xItemsSelected: '{0} fila(s) seleccionadas',
    xItemsWereDeleted: '{0} {1} fueron borrados exitosamente',
    notAllSelectedItemsWereDeleted: 'No todos los items seleccionados fueron borrados',
    cancel: 'Cancelar',
    yes: 'Si',
    ok: 'Ok',
    deleteSubjects: 'Borrar materias',
    deleteItemsConfirm: '¿ Estás seguro que deseas borrar los {0} item(s) seleccionados ?',
    search: 'Buscar',
    itemsPerPage: 'Items por página',
    invalidName: 'El nombre no es válido',
    invalidCode: 'El código no es válido',
    invalidHours: 'Las horas no son válidas',
    addSubject: 'Agregar materia',
    editSubject: 'Editar materia',
    subjectWasSaved: 'La materia fue guardada exitosamente',
    classroomType: 'Tipos de aula',
    createdAt: 'Created at',
    firstName: 'Primer nombre',
    firstLastName: 'Primer apellido',
    secondName: 'Segundo nombre',
    secondLastName: 'Segundo apellido',
    invalidIdentifierNumber: 'El número de identificación no es válido',
    invalidLastName: 'El apellido no es válido',
    addX: 'Agregar {0}',
    editX: 'Editar {0}',
    deleteCareers: 'Borrar carreras',
    careerWasSaved: 'La carrera fue guardada exitosamente',
    capacity: 'Capacidad',
    deleteX: 'Borrar {0}',
    classroomWasSaved: 'La aula fue guardada exitosamente',
    invalidCapacity: 'La capacidad no es válida',
    createdBy: 'Creado por',
    errorCodes: {
        SCH_100: 'La solicitud hecha a la api no es válida',
        SCH_101: 'Un error desconocido ocurrió en la api',
        SCH_102: 'El recurso no fue encontrado en la api',
        SCH_103: 'El recurso ya existe',
        SCH_104: 'El recurso no puede ser borrado debido a que esta siendo usado',

        IDS_1000: 'La solicitud hecha al servidor de identidad no es válida',
        IDS_1001: 'Un error desconocido ocurrió en el servidor de identidad',
        IDS_1002: 'El recurso no fue encontrado en el servidor de identidad',

        SCH_2000: 'La solicitud no es válida',
        SCH_2001: 'Error desconocido',
        SCH_2002: 'El recurso no fue encontrado',
        SCH_2003: 'Invalid username or password',
    }
};

const translations: IStrings = new LocalizedStrings({
    en: enTrans,
    es: esTrans
});

export const getErrorCodeTranslation = (errorMsgId: string): string => {
    if (errorMsgId in translations.errorCodes) {
        const val = translations.errorCodes[errorMsgId as keyof IAppMessageTranslations];
        return val;
    }
    console.warn(`Key = ${errorMsgId} is not being handled`);
    return translations.unknownError;
};


export default translations;
