import React, { useContext } from "react";
import { Redirect, Route, Switch, useRouteMatch } from "react-router-dom";
import { AuthContext } from "./contexts/auth-context";


const Availability = React.lazy(() => import("./pages/availability/availability"));
const ChangePassword = React.lazy(() => import("./pages/change-password/change-password"));
const Careers = React.lazy(() => import("./pages/others/careers/careers"));
const CareersPeriod = React.lazy(() => import("./pages/others/careers-period"));
const Classrooms = React.lazy(() => import("./pages/others/classrooms/clasrooms"));
const Priorities = React.lazy(() => import("./pages/others/teachers/priorities"));
const Sections = React.lazy(() => import("./pages/others/sections"));
const Semesters = React.lazy(() => import("./pages/others/semesters"));
const SubjectClassroomTypes = React.lazy(() => import("./pages/others/subject-classroom-types"));
const Subjects = React.lazy(() => import("./pages/others/subjects/subjects"));
const Teachers = React.lazy(() => import("./pages/others/teachers/teachers"));
const TeacherSubject = React.lazy(() => import("./pages/others/teachers-subjects"));
const Users = React.lazy(() => import("./pages/others/users/users"));
const Home = React.lazy(() => import("./pages/home/home"));
const Login = React.lazy(() => import("./pages/login/login"));
const NotFound = React.lazy(() => import("./pages/not-found/not-found"));
const Subject = React.lazy(() => import("./pages/others/subjects/subject"));
const Teacher = React.lazy(() => import("./pages/others/teachers/teacher"));
const Career = React.lazy(() => import("./pages/others/careers/career"));
const Classroom = React.lazy(() => import("./pages/others/classrooms/classroom"));

export const loginPath = '/login';
export const homePath = "/";
export const changePasswordPath = "/change-password";
export const availabilityPath = '/availability';
export const careersPeriodPath = '/careers-period';
export const careersPath = '/careers';
export const classRoomsPath = '/classrooms';
export const prioritiesPath = '/priorities';
export const sectionsPath = '/sections';
export const semestersPath = '/semesters';
export const sbujectClassroomTypePath = '/subject-classroom-types';
export const subjectsPath = '/subjects';
export const teachersPerSubjectsPath = '/teachers-subjects';
export const teachersPath = '/teachers';
export const usersPath = '/users';
export const subjectPath = `${subjectsPath}/:id`;
export const teacherPath = `${teachersPath}/:id`;
export const careerPath = `${careersPath}/:id`;
export const classroomPath = `${classRoomsPath}/:id`;

export const AppRoutes: React.FC = () => {
    const [authContext] = useContext(AuthContext);

    console.log("Rendering admin routes", authContext && authContext.isAuthenticated);
    const routes = authContext && authContext.isAuthenticated
        ? <AdminAppRoutes />
        : <UnauthenticatedAppRoutes />;

    return routes;
};

const UnauthenticatedAppRoutes: React.FC = () => {
    const match = useRouteMatch(loginPath);
    const route = match?.isExact
        ? <Route exact path={loginPath} component={Login} />
        : <Redirect to={loginPath} />
    return <Switch>
        {route}
        <Route path="*" component={NotFound} />
    </Switch>;
};

const AdminAppRoutes: React.FC = () => {
    return <Switch>
        <Route exact path={changePasswordPath} component={ChangePassword} />
        <Route exact path={availabilityPath} component={Availability} />
        <Route exact path={careersPeriodPath} component={CareersPeriod} />
        <Route exact path={careerPath} component={Career} />
        <Route exact path={careersPath} component={Careers} />
        <Route exact path={classroomPath} component={Classroom} />
        <Route exact path={classRoomsPath} component={Classrooms} />
        <Route exact path={prioritiesPath} component={Priorities} />
        <Route exact path={sectionsPath} component={Sections} />
        <Route exact path={semestersPath} component={Semesters} />
        <Route exact path={sbujectClassroomTypePath} component={SubjectClassroomTypes} />
        <Route exact path={subjectPath} component={Subject} />
        <Route exact path={subjectsPath} component={Subjects} />
        <Route exact path={teachersPerSubjectsPath} component={TeacherSubject} />
        <Route exact path={teachersPath} component={Teachers} />
        <Route exact path={teacherPath} component={Teacher} />
        <Route exact path={usersPath} component={Users} />
        <Route exact path={homePath} component={Home} />
        <Route path="*" component={NotFound} />
    </Switch>;
};

const UserAppRoutes: React.FC = () => {
    return <Switch>
        <Route exact path={changePasswordPath} component={ChangePassword} />
        <Route exact path={availabilityPath} component={Availability} />
        <Route exact path={homePath} component={Home} />
        <Route path="*" component={NotFound} />
    </Switch>;
};