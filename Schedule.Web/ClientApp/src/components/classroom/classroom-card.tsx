import {
    Card,
    CardActions,
    CardContent,
    createStyles,
    Fab,
    makeStyles,
    Theme,
    Typography
} from '@material-ui/core';
import { Delete, Edit } from '@material-ui/icons';
import React, { useState } from 'react';
import { useHistory } from 'react-router-dom';
import { String } from 'typescript-string-operations';
import { classRoomsPath } from '../../routes';
import translations from '../../services/translations';
import DeleteDialog from '../dialogs/delete-dialog';

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        card: {
            width: '100%'
        },
        buttons: {
            justifyContent: 'flex-end'
        },
    }),
);

interface State {
    showDeleteDialog: boolean;
}

interface Props {
    id: number;
    name: string;
    capacity: number;
    classroomSubjectId: number;
    classroomSubject: number;
    createdBy: string;
    createdAt: Date
    isBusy: boolean;
    onDelete: (id: number) => void;
}

function ClassroomCard(props: Props) {
    const classes = useStyles();
    const [state, setState] = useState<State>({
        showDeleteDialog: false,
    });
    const history = useHistory();

    const onDeleteClick = async (deleteItem: boolean) => {
        setState({ ...state, showDeleteDialog: false });
        if (!deleteItem)
            return;
        props.onDelete(props.id);
    };

    const onDeleteBtnClick = () => {
        setState({ ...state, showDeleteDialog: true });
    };

    const onEditClick = () => {
        const path = `${classRoomsPath}/${props.id}`;
        history.push(path);
    };

    const deleteTitle = String.Format(translations.deleteX, translations.classrooms.toLowerCase());
    const deleteContent = String.Format(translations.deleteItemsConfirm, "1");
    const date = new Date(props.createdAt).toLocaleDateString('es-us', {
        year: 'numeric',
        month: '2-digit',
        day: 'numeric',
        hour: '2-digit',
        minute: '2-digit',
        second: '2-digit',
        timeZone: 'UTC'
    });

    return <Card className={classes.card}>
        <CardContent>
            <Typography variant="h5" component="h2">
                {`${props.name} - ${props.classroomSubject}`}
            </Typography>
            <Typography color="textSecondary">
                {`${translations.capacity}: ${props.capacity}`}
            </Typography>
            <Typography color="textSecondary">
                {`${translations.createdBy}: ${props.createdBy}`}
            </Typography>
            <Typography color="textSecondary">
                {date}
            </Typography>
        </CardContent>
        <CardActions className={classes.buttons}>
            <Fab color="primary" size="small" onClick={onEditClick} disabled={props.isBusy}>
                <Edit />
            </Fab>
            <Fab color="secondary" size="small" onClick={onDeleteBtnClick} disabled={props.isBusy}>
                <Delete />
            </Fab>
        </CardActions>
        <DeleteDialog
            showDeleteDialog={state.showDeleteDialog}
            deleteDialogTitle={deleteTitle}
            deleteDialogContent={deleteContent}
            onDeleteClick={onDeleteClick} />
    </Card>;
}

export default ClassroomCard;
