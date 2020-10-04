import React from 'react'
import { Container, Grid, Link, makeStyles, Typography } from '@material-ui/core';
import grey from '@material-ui/core/colors/grey';
import { GitHub } from '@material-ui/icons';

const useStyle = makeStyles((theme) => ({
    footer: {
        position: 'absolute',
        bottom: '0',
        width: '100% !important',
        height: '100px !important',
        color: 'white',
        background: grey[900]
    },
    githubLink: {
        textDecoration: 'none',
        color: 'white',
        display: 'flex',
        flexDirection: 'row',
        alignItems: 'center'
    }
}));


function Footer() {
    const classes = useStyle();

    return <footer className={classes.footer}>
        <Container style={{ height: '100%' }}>
            <Grid container direction="row" justify="space-evenly" alignItems="center" style={{ height: 'inherit' }}>
                <Grid item>
                    <Typography variant="h5" color="inherit">
                        © Copyright 2017
                    </Typography>
                    <Typography variant="h6" color="inherit" >
                        Wolfteam20. All rights reserved.
                    </Typography>
                </Grid>
                <Grid item>
                    <Typography variant="h6" color="inherit" >
                        <Link className={classes.githubLink} href="https://github.com/Wolfteam">GitHub
                            <GitHub style={{ marginLeft: '10px' }} />
                        </Link>
                    </Typography>
                </Grid>
            </Grid>
        </Container>
    </footer>;
}

export default Footer;