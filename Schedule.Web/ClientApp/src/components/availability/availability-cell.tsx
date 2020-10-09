import React, { useState } from 'react'
import {
    createStyles,
    makeStyles,
    TableCell,
    Theme,
} from '@material-ui/core';
import * as responses from '../../models';
import * as enums from '../../enums';

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        selectableCell: {
            cursor: 'pointer'
        },
        selectedCell: {
            backgroundColor: '#5cb85c',
        }
    }),
);

interface Props {
    hour: number;
    day: enums.Day;
    availability: responses.ITeacherAvailabilityResponseDto[];
    onCellClick(hour: number, day: enums.Day): void;
}

interface State {
    isCellHovered: boolean;
}

function AvailabilityCell(props: Props) {
    const classes = useStyles();
    const [state, setState] = useState<State>({
        isCellHovered: false
    });

    const getCellClassToApply = (hour: number, day: enums.Day): string => {
        const defaultClass = classes.selectableCell;

        if (props.availability.length === 0)
            return '';

        const match = props.availability.some(a => a.day === day);
        if (!match)
            return state.isCellHovered ? `${defaultClass} ${classes.selectedCell}` : defaultClass;

        const found = props.availability.some(a => a.day === day && hour >= a.startHourId && hour <= a.endHourId);
        return found || state.isCellHovered ? `${classes.selectedCell} ${defaultClass}` : defaultClass;
    };

    const handleCellMouseOverOut = () => {
        setState({ ...state, isCellHovered: !state.isCellHovered });
    };

    return <TableCell
        className={getCellClassToApply(props.hour, props.day)}
        onMouseOver={handleCellMouseOverOut}
        onMouseOut={handleCellMouseOverOut}
        onClick={() => props.onCellClick(props.hour, props.day)} />;
}

export default React.memo(AvailabilityCell);
