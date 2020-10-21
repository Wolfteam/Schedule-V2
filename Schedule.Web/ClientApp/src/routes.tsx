import React, { Fragment, useContext } from "react";
import { Switch, Route, useRouteMatch, Redirect } from "react-router-dom";

import { AuthContext } from "./contexts/auth-context";

const Availability = React.lazy(() => import("./pages/availability/availability"));
const ChangePassword = React.lazy(() => import("./pages/change-password/change-password"));
const Careers = React.lazy(() => import("./pages/others/careers"));
const CareersPeriod = React.lazy(() => import("./pages/others/careers-period"));
const Classrooms = React.lazy(() => import("./pages/others/clasrooms"));
const Priorities = React.lazy(() => import("./pages/others/priorities"));
const Sections = React.lazy(() => import("./pages/others/sections"));
const Semesters = React.lazy(() => import("./pages/others/semesters"));
const SubjectClassroomTypes = React.lazy(() => import("./pages/others/subject-classroom-types"));
const Subjects = React.lazy(() => import("./pages/others/subjects"));
const Teachers = React.lazy(() => import("./pages/others/teachers"));
const TeacherSubject = React.lazy(() => import("./pages/others/teachers-subjects"));
const Users = React.lazy(() => import("./pages/others/users"));
const Home = React.lazy(() => import("./pages/home/home"));
const Login = React.lazy(() => import("./pages/login/login"));
const NotFound = React.lazy(() => import("./pages/not-found/not-found"));

export const LoginPath = '/login';
export const HomePath = "/";
export const ChangePasswordPath = "/change-password";
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

export const AppRoutes: React.FC = () => {
    const [authContext] = useContext(AuthContext);

    console.log("Rendering admin routes", authContext && authContext.isAuthenticated);
    const routes = authContext && authContext.isAuthenticated
        ? <AdminAppRoutes />
        : <UnauthenticatedAppRoutes />;

    return routes;
};

const UnauthenticatedAppRoutes: React.FC = () => {
    const match = useRouteMatch(LoginPath);
    const route = match?.isExact
        ? <Route exact path={LoginPath} component={Login} />
        : <Redirect to={LoginPath} />
    return <Switch>
        {route}
        <Route path="*" component={NotFound} />
    </Switch>;
};

const AdminAppRoutes: React.FC = () => {
    return <Switch>
        <Route exact path={ChangePasswordPath} component={ChangePassword} />
        <Route exact path={availabilityPath} component={Availability} />
        <Route exact path={careersPeriodPath} component={CareersPeriod} />
        <Route exact path={careersPath} component={Careers} />
        <Route exact path={classRoomsPath} component={Classrooms} />
        <Route exact path={prioritiesPath} component={Priorities} />
        <Route exact path={sectionsPath} component={Sections} />
        <Route exact path={semestersPath} component={Semesters} />
        <Route exact path={sbujectClassroomTypePath} component={SubjectClassroomTypes} />
        <Route exact path={subjectsPath} component={Subjects} />
        <Route exact path={teachersPerSubjectsPath} component={TeacherSubject} />
        <Route exact path={teachersPath} component={Teachers} />
        <Route exact path={usersPath} component={Users} />
        <Route exact path={HomePath} component={Home} />
        <Route path="*" component={NotFound} />
    </Switch>;
};

const UserAppRoutes: React.FC = () => {
    return <Switch>
        <Route exact path={ChangePasswordPath} component={ChangePassword} />
        <Route exact path={availabilityPath} component={Availability} />
        <Route exact path={HomePath} component={Home} />
        <Route path="*" component={NotFound} />
    </Switch>;
};