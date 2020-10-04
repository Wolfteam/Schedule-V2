import React, { Fragment, useContext } from "react";
import { Switch, Route, useRouteMatch, Redirect } from "react-router-dom";

import { AuthContext } from "./contexts/auth-context";

import Availability from "./pages/availability/availability";
import ChangePassword from "./pages/change-password/change-password";
import Careers from "./pages/db/careers";
import CareersPeriod from "./pages/db/careers-period";
import Classrooms from "./pages/db/clasrooms";
import Priorities from "./pages/db/priorities";
import Sections from "./pages/db/sections";
import Semesters from "./pages/db/semesters";
import SubjectClassroomTypes from "./pages/db/subject-classroom-types";
import Subjects from "./pages/db/subjects";
import Teachers from "./pages/db/teachers";
import TeacherSubject from "./pages/db/teachers-subjects";
import Users from "./pages/db/users";
import Home from "./pages/home/home";
import Login from "./pages/login/login";
import NotFound from "./pages/not-found/not-found";

export const LoginPath = '/login';
export const HomePath = "/";
export const ChangePasswordPath = "/change-password";
export const availabilityPath = '/availability';
export const careersPeriodPath = '/db/careers-period';
export const careersPath = '/db/careers';
export const classRoomsPath = '/db/classrooms';
export const prioritiesPath = '/db/priorities';
export const sectionsPath = '/db/sections';
export const semestersPath = '/db/semesters';
export const sbujectClassroomTypePath = '/db/subject-classroom-types';
export const subjectsPath = '/db/subjects';
export const teachersPerSubjectsPath = '/db/teachers-subjects';
export const teachersPath = '/db/teachers';
export const usersPath = '/db/users';

export const AppRoutes: React.FC = () => {
    const [authContext, _] = useContext(AuthContext);

    const routes = authContext && authContext.isAuthenticated
        ? <AdminAppRoutes />
        : <UnauthenticatedAppRoutes />;
    return (
        <Switch>
            {routes}
            <Route path="*" component={NotFound} />
        </Switch>
    );
};

const UnauthenticatedAppRoutes: React.FC = () => {
    const match = useRouteMatch(LoginPath);
    const route = match?.isExact
        ? <Route exact path={LoginPath} component={Login} />
        : <Redirect to={LoginPath} />
    return <React.Fragment>
        {route}
    </React.Fragment>;
};

const AdminAppRoutes: React.FC = () => {
    return <Fragment>
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
    </Fragment>;
};

const UserAppRoutes: React.FC = () => {
    return <Fragment>
        <Route exact path={ChangePasswordPath} component={ChangePassword} />
        <Route exact path={availabilityPath} component={Availability} />
        <Route exact path={HomePath} component={Home} />
    </Fragment>;
};