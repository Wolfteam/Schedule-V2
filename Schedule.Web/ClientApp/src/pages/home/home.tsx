import React, { Fragment } from 'react'
import { Link } from 'react-router-dom';
import { ChangePasswordPath } from '../../routes';

function Home() {
    return <Fragment>
        <h1>Home</h1>
        {/* <Link to={ChangePasswordPath} title="Change your password">Change your password</Link> */}
    </Fragment>;
}

export default Home;