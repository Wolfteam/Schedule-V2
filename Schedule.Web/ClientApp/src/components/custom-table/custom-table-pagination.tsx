import { createStyles, Grid, makeStyles, Theme } from '@material-ui/core';
import { Pagination } from '@material-ui/lab';
import React, { useEffect } from 'react';
import { String } from 'typescript-string-operations';
import translations from '../../services/translations';

const useStyles = makeStyles((theme: Theme) => createStyles({
    paginationContainer: {
        marginTop: '10px'
    },
    pagination: {
        display: 'flex',
        justifyContent: 'flex-end'
    }
}));

interface Props {
    isBusy: boolean;
    currentPage: number;
    totalPages: number;
    totalRecords: number;
    onPageChanged: (page: number) => void;
}

function CustomTablePagination(props: Props) {
    const classes = useStyles();
    const exceedsPage = props.totalPages < props.currentPage;
    const notFound = props.totalPages === 0;
    const currentPage = exceedsPage ? 1 : props.currentPage;

    const { onPageChanged } = props;

    useEffect(() => {
        if (exceedsPage && !notFound) {
            console.log("pagination exceeds")
            onPageChanged(currentPage);
        }
    }, [onPageChanged, exceedsPage, notFound, currentPage]);

    const handlePageChange = (newVal: number) => {
        if (props.currentPage !== newVal)
            props.onPageChanged(newVal);
    };

    const pageMsg = String.Format(translations.pageXofX, props.currentPage, props.totalPages);
    const totalMsg = `${translations.totalResults}: ${props.totalRecords}`;

    return <Grid
        container
        justify="space-between"
        alignItems="center"
        spacing={2}
        className={classes.paginationContainer}>
        <Grid item xs>
            <p>{pageMsg}. {totalMsg}</p>
        </Grid>
        <Grid item xs>
            <Pagination
                page={currentPage}
                disabled={props.isBusy}
                onChange={(e, p) => handlePageChange(p)}
                className={classes.pagination}
                count={props.totalPages}
                color="primary" />
        </Grid>
    </Grid>;
}

export default React.memo(CustomTablePagination);
