import { Grid, Typography, Divider } from '@material-ui/core';
import React from 'react'

interface Props {
    title: string;
}

function PageTitle(props: Props) {
    return <Grid item xs={12} sm={12} style={{ marginTop: '20px' }}>
        <Typography component="h1" variant="h5">
            {props.title}
        </Typography>
        <Divider style={{ marginTop: '20px' }} />
    </Grid>;
}

export default PageTitle;
