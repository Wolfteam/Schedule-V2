import React from 'react'
import {
    createStyles,
    makeStyles,
    Table,
    TableContainer,
    TableHead,
    TableCell,
    TableRow,
    Theme,
    Paper,
    TableBody
} from '@material-ui/core';
import { academicHours, getHourText, isAcademicHourBetweenRange, isLunchHour } from '../../utils/academic-hours';
import * as responses from '../../models';
import * as enums from '../../enums';
import { grey } from '@material-ui/core/colors';
import AvailabilityCell from './availability-cell';
import translations from '../../services/translations';

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        cards: {
            margin: '20px 10px'
        },
        lunchHour: {
            backgroundColor: grey[900],
            color: 'white',
        },
    }),
);

interface Props {
    availability: responses.ITeacherAvailabilityResponseDto[];
    onAvailabilityChange(newValue: responses.ITeacherAvailabilityResponseDto[]): void;
}

function AvailabilityTable(props: Props) {
    const classes = useStyles();

    const handleCellClick = (hour: number, day: enums.Day) => {
        const newId = Math.max.apply(Math, props.availability.map(a => a.id)) + 1;
        const match: responses.ITeacherAvailabilityResponseDto | undefined = props.availability
            .find(a => a.day === day && isAcademicHourBetweenRange(a.startHourId, a.endHourId, hour));
        //If match, that means that the user clicked a selected cell
        //otherwise the cell was not selected, and we clicked in an unselected cell
        if (!match) {
            // const endHour = isLastAcademicHour(hour) ? hour : hour + 1;
            const closestTop = props.availability.find(a => a.day === day && a.endHourId === hour - 1);
            const closestBottom = props.availability.find(a => a.day === day && a.startHourId === hour + 1);

            let newAvailability: responses.ITeacherAvailabilityResponseDto[] = [];
            if (!closestTop && !closestBottom) {
                const newValue: responses.ITeacherAvailabilityResponseDto = {
                    day: day,
                    startHourId: hour,
                    endHourId: hour,
                    id: newId,
                    periodId: 2
                }
                newAvailability = props.availability.concat(newValue);
            }
            else if (closestTop && closestBottom) {
                const newValue: responses.ITeacherAvailabilityResponseDto = {
                    day: day,
                    startHourId: closestTop.startHourId,
                    endHourId: closestBottom.endHourId,
                    id: newId,
                    periodId: 2
                };
                newAvailability = props.availability.filter(a => a.id !== closestTop.id && a.id !== closestBottom.id).concat(newValue);
            } else {
                if (closestTop) {
                    const newValue: responses.ITeacherAvailabilityResponseDto = {
                        ...closestTop,
                        endHourId: hour
                    };
                    newAvailability = props.availability.filter(a => a.id !== closestTop.id).concat(newValue);
                } else {
                    const newValue: responses.ITeacherAvailabilityResponseDto = {
                        ...closestBottom!,
                        startHourId: hour
                    };
                    newAvailability = props.availability.filter(a => a.id !== closestBottom!.id).concat(newValue);
                }
            }
            props.onAvailabilityChange(newAvailability);
            return;
        }

        let newAvailability: responses.ITeacherAvailabilityResponseDto[] = [];
        const availabilityWithoutMatch = props.availability.filter(a => a.id !== match.id);
        const sequenceExist = match.endHourId - match.startHourId > 0;
        const clickHappenedInThePoles = hour === match.startHourId || hour === match.endHourId;

        if (!clickHappenedInThePoles && sequenceExist) {
            const top: responses.ITeacherAvailabilityResponseDto = {
                ...match,
                id: newId,
                endHourId: hour - 1
            };
            const bottom: responses.ITeacherAvailabilityResponseDto = {
                ...match,
                id: newId + 1,
                startHourId: hour + 1
            };
            newAvailability = availabilityWithoutMatch.concat(top, bottom);
        } else if (clickHappenedInThePoles && sequenceExist) {
            const newValue: responses.ITeacherAvailabilityResponseDto = {
                ...match
            };
            if (hour === match.startHourId)
                newValue.startHourId++;
            else
                newValue.endHourId--;

            newAvailability = availabilityWithoutMatch.concat(newValue);
        } else {
            newAvailability = availabilityWithoutMatch;
        }

        props.onAvailabilityChange(newAvailability);
    };

    const tableHead = <TableHead>
        <TableRow>
            <TableCell align="center">{translations.hour}</TableCell>
            <TableCell align="center">{translations.monday}</TableCell>
            <TableCell align="center">{translations.tuesday}</TableCell>
            <TableCell align="center">{translations.wednesday}</TableCell>
            <TableCell align="center">{translations.thursday}</TableCell>
            <TableCell align="center">{translations.friday}</TableCell>
            <TableCell align="center">{translations.saturday}</TableCell>
        </TableRow>
    </TableHead>;

    const tableBody = academicHours.map(hour => {
        const text = getHourText(hour);
        if (isLunchHour(hour)) {
            return <TableRow key={hour}>
                <TableCell align="center">{text}</TableCell>
                <TableCell align="center"
                    className={classes.lunchHour}
                    colSpan={6}>{translations.lunchHour}</TableCell>
            </TableRow>;
        }

        return <TableRow key={hour}>
            <TableCell align="center" className="hour">{text}</TableCell>
            <AvailabilityCell
                availability={props.availability}
                day={enums.Day.monday}
                hour={hour}
                onCellClick={handleCellClick} />
            <AvailabilityCell
                availability={props.availability}
                day={enums.Day.tuesday}
                hour={hour}
                onCellClick={handleCellClick} />
            <AvailabilityCell
                availability={props.availability}
                day={enums.Day.wednesday}
                hour={hour}
                onCellClick={handleCellClick} />
            <AvailabilityCell
                availability={props.availability}
                day={enums.Day.thursday}
                hour={hour}
                onCellClick={handleCellClick} />
            <AvailabilityCell
                availability={props.availability}
                day={enums.Day.friday}
                hour={hour}
                onCellClick={handleCellClick} />
            <AvailabilityCell
                availability={props.availability}
                day={enums.Day.saturday}
                hour={hour}
                onCellClick={handleCellClick} />
        </TableRow>
    });

    return <TableContainer className={classes.cards} component={Paper}>
        <Table size="small">
            {tableHead}
            <TableBody>
                {tableBody}
            </TableBody>
        </Table>
    </TableContainer>;
}

export default AvailabilityTable
