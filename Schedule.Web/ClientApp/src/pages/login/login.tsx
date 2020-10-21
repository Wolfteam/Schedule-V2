import React, { useContext, useState } from 'react'
import {
    Avatar,
    Button,
    Card,
    CardContent,
    Checkbox,
    CssBaseline,
    Container,
    FormControlLabel,
    Grid,
    Link,
    makeStyles,
    TextField,
    Typography,
    InputAdornment,
    IconButton,
    LinearProgress
} from '@material-ui/core';
import { AccountCircle, LockOutlined, Visibility, VisibilityOff } from '@material-ui/icons';
import { useHistory } from 'react-router-dom';
import validator from 'validator';

import { AuthContext } from '../../contexts/auth-context';
import { TranslationContext } from '../../contexts/translations-context';
import * as routes from '../../routes';
import translations, { getErrorCodeTranslation } from '../../services/translations';
import { login } from '../../services/account.service'
import { useSnackbar } from 'notistack';

interface State {
    username: string;
    password: string;
    rememberMe: boolean;
    showPassword: boolean,
    isUsernameDirty: boolean,
    isUsernameValid: boolean,
    isPasswordDirty: boolean,
    isPasswordValid: boolean,
    isBusy: boolean,
    isAuthenticated: boolean
}

const useStyles = makeStyles((theme) => ({
    paper: {
        // marginTop: theme.spacing(2),
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
    },
    avatar: {
        margin: theme.spacing(1),
        backgroundColor: theme.palette.secondary.main,
    },
    form: {
        width: '100%', // Fix IE 11 issue.
        marginTop: theme.spacing(1),
    },
    submit: {
        margin: theme.spacing(3, 0, 2),
    },
}));

function Login() {
    const classes = useStyles();

    const [transContext, setTransContext] = useContext(TranslationContext);

    const [authContext, setAuthContext] = useContext(AuthContext);

    const [formState, setFormState] = useState<State>({
        username: '',
        password: '',
        rememberMe: false,
        showPassword: false,
        isUsernameDirty: false,
        isPasswordDirty: false,
        isUsernameValid: false,
        isPasswordValid: false,
        isBusy: false,
        isAuthenticated: false,
    });

    const history = useHistory();

    const { enqueueSnackbar } = useSnackbar();

    const handleChange = (prop: keyof State) => (event: React.ChangeEvent<HTMLInputElement>) => {
        const newState = { ...formState, [prop]: event.target.value };
        switch (prop) {
            case 'username':
                newState.isUsernameValid = validator.isLength(newState.username, { min: 4 });
                newState.isUsernameDirty = true;
                break;
            case 'password':
                newState.isPasswordValid = validator.isLength(newState.password, { max: 10, min: 6 });
                newState.isPasswordDirty = true;
                break;
            default:
                break;
        }
        setFormState(newState);
    };

    const handleClickShowPassword = () => {
        setFormState({ ...formState, showPassword: !formState.showPassword });
    };

    const onRememberMeChanged = (e: React.ChangeEvent<HTMLInputElement>, isChecked: boolean) => {
        setFormState({
            ...formState,
            rememberMe: isChecked
        });
    };

    const handleLogin = async (e: React.MouseEvent<HTMLButtonElement, MouseEvent>) => {
        e.preventDefault();
        console.log(formState);
        setFormState({
            ...formState,
            isBusy: true,
        });

        const response = await login(formState.username, formState.password, formState.rememberMe);
        if (!response.succeed) {
            console.log(response);
            enqueueSnackbar(getErrorCodeTranslation(response.errorMessageId), {
                variant: 'error',
                autoHideDuration: 3000,
            });
            setFormState({ ...formState, isBusy: false });
            return;
        }

        if (setAuthContext) {
            setAuthContext({
                username: formState.username,
                isAuthenticated: true
            });
            history.replace(routes.HomePath);
        }
    };

    const showEmailError = !formState.isUsernameValid && formState.isUsernameDirty;
    const showPasswordError = !formState.isPasswordValid && formState.isPasswordDirty;
    const enableSubmitButton = formState.isUsernameValid && formState.isPasswordValid && !formState.isBusy;
    // if (formState.isAuthenticated) {

    //     return <Redirect to={HomePath} />;
    // }

    const handleChangeLang = () => {
        let lang = 'es';
        if (transContext?.currentLanguage === 'es') {
            lang = 'en';
        }
        setTransContext!({ currentLanguage: lang });
    };

    const testBtn = <Button onClick={handleChangeLang}>Change lang</Button>

    const emailInput = <TextField
        variant="outlined"
        margin="normal"
        required
        fullWidth
        id="username"
        label={translations.username}
        name="username"
        error={showEmailError}
        helperText={showEmailError ? translations.invalidUsername : ''}
        InputProps={{
            endAdornment: (
                <InputAdornment position="start">
                    <AccountCircle />
                </InputAdornment>
            ),
        }}
        value={formState.username}
        onChange={handleChange('username')}
        autoFocus />;

    const passwordInput = <TextField
        variant="outlined"
        margin="normal"
        required
        fullWidth
        name="password"
        label={translations.password}
        type={formState.showPassword ? 'text' : 'password'}
        id="password"
        error={showPasswordError}
        helperText={showPasswordError ? translations.invalidPassword : ''}
        value={formState.password}
        onChange={handleChange('password')}
        autoComplete="current-password"
        InputProps={{
            endAdornment: (
                <InputAdornment position="end" >
                    <IconButton
                        aria-label="toggle password visibility"
                        onClick={handleClickShowPassword}>
                        {formState.showPassword ? <Visibility /> : <VisibilityOff />}
                    </IconButton>
                </InputAdornment>
            )
        }} />;

    const rememberMeControl = <Checkbox
        value="remember"
        color="primary"
        checked={formState.rememberMe}
        onChange={onRememberMeChanged} />;

    return <Container>
        <CssBaseline />
        <Grid className={classes.paper} container direction="column" justify="center" alignItems="center">
            <Card variant="elevation" elevation={5}>
                <CardContent>
                    <Grid container direction="column" justify="center" alignItems="center">
                        <Grid item sm={12}>
                            <Avatar className={classes.avatar}>
                                <LockOutlined />
                            </Avatar>
                        </Grid>
                        <Grid item sm={12}>
                            <Typography component="h1" variant="h5">
                                {translations.signIn}
                            </Typography>
                        </Grid>
                        <Grid item sm={12}>
                            <form className={classes.form} noValidate >
                                <fieldset disabled={formState.isBusy} style={{ borderColor: 'transparent' }}>
                                    {emailInput}
                                    {passwordInput}
                                    <FormControlLabel
                                        control={rememberMeControl}
                                        label={translations.rememberMe} />
                                    <LinearProgress hidden={!formState.isBusy} />
                                    <Button
                                        className={classes.submit}
                                        type="submit"
                                        fullWidth
                                        variant="contained"
                                        color="primary"
                                        onClick={handleLogin}
                                        disabled={!enableSubmitButton}>
                                        {translations.signIn}
                                    </Button>
                                </fieldset>
                            </form>
                        </Grid>
                        <Grid item sm={12}>
                            <Link href="#" variant="body2">
                                {translations.forgotPassword}
                            </Link>
                        </Grid>
                    </Grid>
                </CardContent>
            </Card>
        </Grid>
    </Container>;
}

export default Login;