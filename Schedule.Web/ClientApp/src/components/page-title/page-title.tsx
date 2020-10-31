import React from 'react'
import { Grid, Typography, Divider, IconButton, LinearProgress } from '@material-ui/core';
import { faArrowLeft } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { useHistory } from 'react-router-dom';

interface Props {
    title: string;
    showLoading?: boolean;
    showBackIcon?: boolean;
    backPath?: string;
}

function PageTitle(props: Props) {
    const history = useHistory();

    const handleBackClick = () => {
        if (props.backPath)
            history.replace(props.backPath);
    };

    const back = props.showBackIcon
        ? <IconButton onClick={handleBackClick} color="secondary" children={<FontAwesomeIcon icon={faArrowLeft} />}></IconButton>
        : null;

    const loading = props.showLoading ? <LinearProgress /> : null;

    return <Grid item xs={12} sm={12}>
        <Typography component="h1" variant="h5">
            {back}
            {props.title}
        </Typography>
        <Divider style={{ marginTop: '10px', marginBottom: '5px' }} />
        {loading}
    </Grid>;
}

export default React.memo(PageTitle);
