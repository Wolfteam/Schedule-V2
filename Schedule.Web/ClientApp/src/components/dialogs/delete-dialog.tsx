import {
    Button,
    Dialog,
    DialogActions,
    DialogContent,
    DialogContentText,
    DialogTitle
} from '@material-ui/core';
import React from 'react';
import translations from '../../services/translations';

interface Props {
    showDeleteDialog: boolean;
    deleteDialogTitle: string;
    deleteDialogContent?: string;
    onDeleteClick: (deleteItem: boolean) => void;
}

function DeleteDialog(props: Props) {
    return <Dialog
        open={props.showDeleteDialog}
        onClose={() => props.onDeleteClick(false)}>
        <DialogTitle>{props.deleteDialogTitle}</DialogTitle>
        <DialogContent>
            <DialogContentText>
                {props.deleteDialogContent}
            </DialogContentText>
        </DialogContent>
        <DialogActions>
            <Button onClick={() => props.onDeleteClick(false)} color="primary">
                {translations.cancel}
            </Button>
            <Button onClick={() => props.onDeleteClick(true)} color="primary" autoFocus>
                {translations.yes}
            </Button>
        </DialogActions>
    </Dialog>;
}

export default React.memo(DeleteDialog);
