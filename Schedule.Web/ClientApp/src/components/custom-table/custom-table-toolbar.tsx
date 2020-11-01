import React, { useState } from 'react';
import {
    Toolbar,
    Typography,
    Tooltip,
    IconButton,
    createStyles,
    darken,
    makeStyles,
    Theme,
    Button,
    Dialog,
    DialogActions,
    DialogContent,
    DialogContentText,
    DialogTitle
} from '@material-ui/core';
import { Delete, Edit } from '@material-ui/icons';
import translations from '../../services/translations';
import { String } from 'typescript-string-operations';
import validator from 'validator';

const useStyles = makeStyles((theme: Theme) => createStyles({
    selectedItemsText: {
        flex: '1 1 100%'
    },
    toolbar: {
        marginTop: '10px',
        marginBottom: '10px',
        color: 'white',
        backgroundColor: darken(theme.palette.primary.light, 0.05),
    }
}));

interface Props {
    deleteDialogTitle: string;
    deleteDialogContent?: string;
    selectedNumberOfItems: number;
    onEditClick: () => void;
    onDeleteClick: () => void;
}

interface State {
    showDeleteDialog: boolean;
}

function CustomTableToolbar(props: Props) {
    const classes = useStyles();
    const [state, setState] = useState<State>({
        showDeleteDialog: false
    });

    if (props.selectedNumberOfItems === 0)
        return null;

    const msg = String.Format(translations.xItemsSelected, props.selectedNumberOfItems);

    const onDeleteClick = () => {
        setState({ ...state, showDeleteDialog: true });
    };

    const onDeleteDialogBtnClick = (deleteItems: boolean) => {
        setState({ ...state, showDeleteDialog: false });
        if (deleteItems)
            props.onDeleteClick();
    };

    const content = validator.isEmpty(props.deleteDialogContent ?? "")
        ? String.Format(translations.deleteItemsConfirm, props.selectedNumberOfItems)
        : props.deleteDialogContent;

    const editButton = props.selectedNumberOfItems === 1
        ? <Tooltip title="Edit">
            <IconButton aria-label="delete" onClick={props.onEditClick}>
                <Edit htmlColor='yellow' />
            </IconButton>
        </Tooltip>
        : null;

    return <Toolbar className={classes.toolbar}>
        <Typography className={classes.selectedItemsText} color="inherit" variant="subtitle1" component="div">
            {msg}
        </Typography>
        {editButton}
        <Tooltip title="Delete">
            <IconButton aria-label="delete" onClick={onDeleteClick}>
                <Delete htmlColor='red' />
            </IconButton>
        </Tooltip>
        <Dialog
            open={state.showDeleteDialog}
            onClose={() => onDeleteDialogBtnClick(false)}>
            <DialogTitle>{props.deleteDialogTitle}</DialogTitle>
            <DialogContent>
                <DialogContentText>
                    {content}
                </DialogContentText>
            </DialogContent>
            <DialogActions>
                <Button onClick={() => onDeleteDialogBtnClick(false)} color="primary">
                    {translations.cancel}
                </Button>
                <Button onClick={() => onDeleteDialogBtnClick(true)} color="primary" autoFocus>
                    {translations.yes}
                </Button>
            </DialogActions>
        </Dialog>
    </Toolbar>;
}

export default React.memo(CustomTableToolbar);
