import React from 'react'
import { createStyles, makeStyles, TableCell, TableHead, TableRow, TableSortLabel, Theme } from '@material-ui/core';
import validator from 'validator';

const useStyles = makeStyles((theme: Theme) => createStyles({
    visuallyHidden: {
        border: 0,
        clip: 'rect(0 0 0 0)',
        height: 1,
        margin: -1,
        overflow: 'hidden',
        padding: 0,
        position: 'absolute',
        top: 20,
        width: 1,
    }
}));

export interface Header {
    orderByKey?: string;
    text: string;
    isOrderable: boolean;
}

interface Props {
    orderBy: string;
    orderByAsc: boolean;
    cells: Header[];
    onOrderByChanged: (orderBy: string, orderByAsc: boolean) => void;
}

function CustomTableHeader(props: Props) {
    const classes = useStyles();

    const sortDirectionChanged = (orderBy: string, orderByAsc: boolean) => {
        if (orderBy !== props.orderBy || orderByAsc !== props.orderByAsc)
            props.onOrderByChanged(orderBy, orderByAsc);
    };

    const sortDirection = props.orderByAsc ? 'asc' : 'desc';
    const header = props.cells.map((el, index) => {
        if (validator.isEmpty(el.text)) {
            return <TableCell key="selected-items" align='center' padding='none' />
        }

        const isBeingUsed = props.orderBy === el.orderByKey;
        const visualSortIndicator = isBeingUsed && el.isOrderable ? (
            <span className={classes.visuallyHidden}>
                {!props.orderByAsc ? 'sorted descending' : 'sorted ascending'}
            </span>
        ) : null;

        return <TableCell
            key={el.orderByKey ?? `GeneratedKey_${index}`}
            align='center'
            padding='none'
            sortDirection={isBeingUsed ? sortDirection : false}>
            <TableSortLabel
                active={isBeingUsed}
                direction={isBeingUsed ? sortDirection : 'asc'}
                onClick={() => sortDirectionChanged(el.orderByKey!, !props.orderByAsc)}>
                {el.text}
                {visualSortIndicator}
            </TableSortLabel>
        </TableCell>;
    });
    return <TableHead>
        <TableRow>
            {header}
        </TableRow>
    </TableHead>;
}

export default React.memo(CustomTableHeader);
