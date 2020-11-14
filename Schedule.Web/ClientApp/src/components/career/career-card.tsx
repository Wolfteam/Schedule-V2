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
import { careersPath } from '../../routes';
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
    isBusy: boolean;
    onDelete: (id: number) => void;
}

function CareerCard(props: Props) {
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
        const path = `${careersPath}/${props.id}`;
        history.push(path);
    };

    const deleteTitle = String.Format(translations.deleteX, translations.career);
    const deleteContent = String.Format(translations.deleteItemsConfirm, "1");

    return <Card className={classes.card}>
        <CardContent>
            <Typography variant="h5" component="h2">
                {props.name}
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

export default CareerCard;
