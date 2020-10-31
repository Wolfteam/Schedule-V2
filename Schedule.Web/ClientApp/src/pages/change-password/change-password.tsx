import React, { useState } from 'react'
import {
    Button,
    Card,
    CardContent,
    Container,
    createStyles,
    CssBaseline,
    Grid,
    IconButton,
    InputAdornment,
    makeStyles,
    TextField,
    Theme
} from '@material-ui/core';
import { faLock } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Visibility, VisibilityOff } from '@material-ui/icons';
import validator from 'validator';
import PageTitle from '../../components/page-title/page-title';
import translations from '../../services/translations';

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        card: {
            marginTop: '20px',
            padding: '20px 20px 0px 20px'
        },
    }),
);

interface State {
    showCurrentPassword: boolean;
    isCurrentPasswordValid: boolean;
    isCurrentPasswordDirty: boolean;
    currentPassword: string;

    showNewPassword: boolean;
    isNewPasswordValid: boolean;
    isNewPasswordDirty: boolean;
    newPassword: string;

    showNewPasswordConfirm: boolean;
    isNewPasswordConfirmValid: boolean;
    isNewPasswordConfirmDirty: boolean;
    newPasswordConfirm: string;
}

function ChangePassword() {
    const classes = useStyles();
    const [state, setState] = useState<State>({
        showCurrentPassword: false,
        isCurrentPasswordValid: false,
        isCurrentPasswordDirty: false,
        currentPassword: '',

        showNewPassword: false,
        isNewPasswordValid: false,
        isNewPasswordDirty: false,
        newPassword: '',

        showNewPasswordConfirm: false,
        isNewPasswordConfirmValid: false,
        isNewPasswordConfirmDirty: false,
        newPasswordConfirm: '',
    });

    const handleClickShowPassword = (prop: keyof State) => {
        setState({ ...state, [prop]: !state[prop] });
    };

    const handleChange = (prop: keyof State) => (event: React.ChangeEvent<HTMLInputElement>) => {
        const newState = { ...state, [prop]: event.target.value };
        const passValidator = { max: 10, min: 6 };
        switch (prop) {
            case 'currentPassword':
                newState.isCurrentPasswordValid = validator.isLength(newState.currentPassword, passValidator);
                newState.isCurrentPasswordDirty = true;
                break;
            case 'newPassword':
                newState.isNewPasswordValid = validator.isLength(newState.newPassword, passValidator);
                newState.isNewPasswordDirty = true;
                break;
            case 'newPasswordConfirm':
                newState.isNewPasswordConfirmValid = validator.isLength(newState.newPasswordConfirm, passValidator) && newState.newPassword === newState.newPasswordConfirm;
                newState.isNewPasswordConfirmDirty = true;
                break;
            default:
                break;
        }
        setState(newState);
    };

    const showCurrentPasswordError = !state.isCurrentPasswordValid && state.isCurrentPasswordDirty;
    const showNewPasswordError = !state.isNewPasswordValid && state.isNewPasswordDirty;
    const showNewPasswordConfirmError = !state.isNewPasswordConfirmValid && state.isNewPasswordConfirmDirty;
    const enableSubmitBtn = state.isCurrentPasswordValid && state.isNewPasswordValid && state.isNewPasswordConfirmValid;

    const showPasswordBtn = (prop: keyof State) => {
        const show = state[prop];
        return <InputAdornment position="end" >
            <IconButton
                aria-label="toggle password visibility"
                onClick={() => handleClickShowPassword(prop)}>
                {show ? <Visibility /> : <VisibilityOff />}
            </IconButton>
        </InputAdornment>;
    };

    const lockIcon = <InputAdornment position="start" >
        <FontAwesomeIcon icon={faLock} />
    </InputAdornment>;

    return <Container >
        <CssBaseline />
        <PageTitle title="Cambio de contraseña" showLoading={false} />
        <Grid container justify="center" alignItems="center">
            <Grid item sm={6}>
                <Card className={classes.card}>
                    <CardContent>
                        <form noValidate>
                            <TextField variant="outlined"
                                margin="normal"
                                required
                                fullWidth
                                label={translations.currentPassword}
                                type={state.showCurrentPassword ? 'text' : 'password'}
                                error={showCurrentPasswordError}
                                helperText={showCurrentPasswordError ? translations.invalidPassword : ''}
                                value={state.currentPassword}
                                onChange={handleChange('currentPassword')}
                                InputProps={{
                                    startAdornment: (lockIcon),
                                    endAdornment: (showPasswordBtn('showCurrentPassword'))
                                }} />

                            <TextField variant="outlined"
                                margin="normal"
                                required
                                fullWidth
                                label={translations.newPassword}
                                type={state.showNewPassword ? 'text' : 'password'}
                                error={showNewPasswordError}
                                helperText={showNewPasswordError ? translations.invalidPassword : ''}
                                value={state.newPassword}
                                onChange={handleChange('newPassword')}
                                InputProps={{
                                    startAdornment: (lockIcon),
                                    endAdornment: (showPasswordBtn('showNewPassword'))
                                }} />

                            <TextField variant="outlined"
                                margin="normal"
                                required
                                fullWidth
                                label={translations.newPasswordConfirm}
                                type={state.showNewPasswordConfirm ? 'text' : 'password'}
                                error={showNewPasswordConfirmError}
                                helperText={showNewPasswordConfirmError ? translations.passwordDoesntMatch : ''}
                                value={state.newPasswordConfirm}
                                onChange={handleChange('newPasswordConfirm')}
                                InputProps={{
                                    startAdornment: (lockIcon),
                                    endAdornment: (showPasswordBtn('showNewPasswordConfirm'))
                                }} />

                            <Button type="submit"
                                fullWidth
                                variant="contained"
                                color="primary"
                                disabled={!enableSubmitBtn}
                                style={{ marginTop: '10px' }}>{translations.saveChanges}</Button>
                        </form>
                    </CardContent>
                </Card>
            </Grid>
        </Grid>
    </Container>;
}

export default ChangePassword;