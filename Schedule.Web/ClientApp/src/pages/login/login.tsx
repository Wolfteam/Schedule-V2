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
import React, { useContext, useState } from 'react'
import { Redirect, useHistory } from 'react-router-dom';
import validator from 'validator';
import { AuthContext } from '../../contexts/auth-context';
import { TranslationContext } from '../../contexts/translations-context';
import * as routes from '../../routes';
import translations from '../../services/translations';

interface State {
    email: string;
    password: string;
    rememberMe: boolean;
    showPassword: boolean,
    isEmailDirty: boolean,
    isEmailValid: boolean,
    isPasswordDirty: boolean,
    isPasswordValid: boolean,
    isBusy: boolean,
    isAuthenticated: boolean
}

const useStyles = makeStyles((theme) => ({
    paper: {
        marginTop: theme.spacing(8),
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
        email: '',
        password: '',
        rememberMe: false,
        showPassword: false,
        isEmailDirty: false,
        isPasswordDirty: false,
        isEmailValid: false,
        isPasswordValid: false,
        isBusy: false,
        isAuthenticated: false,
    });

    const history = useHistory();

    const handleChange = (prop: keyof State) => (event: React.ChangeEvent<HTMLInputElement>) => {
        const newState = { ...formState, [prop]: event.target.value };
        switch (prop) {
            case 'email':
                newState.isEmailValid = validator.isEmail(newState.email);
                newState.isEmailDirty = true;
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

    const handleLogin = (e: React.MouseEvent<HTMLButtonElement, MouseEvent>) => {
        e.preventDefault();
        console.log(formState);
        setFormState({
            ...formState,
            isBusy: true,
        });

        setTimeout(() => {
            // setFormState({
            //     ...formState,
            //     isBusy: false,
            //     isAuthenticated: true
            // });
            if (setAuthContext) {
                setAuthContext({
                    email: formState.email,
                    isAuthenticated: true
                });
                history.replace(routes.HomePath);
            }
        }, 2000);
    };

    const showEmailError = !formState.isEmailValid && formState.isEmailDirty;
    const showPasswordError = !formState.isPasswordValid && formState.isPasswordDirty;
    const enableSubmitButton = formState.isEmailValid && formState.isPasswordValid && !formState.isBusy;
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
        helperText={showEmailError ? translations.invalidEmail : ''}
        InputProps={{
            endAdornment: (
                <InputAdornment position="start">
                    <AccountCircle />
                </InputAdornment>
            ),
        }}
        value={formState.email}
        onChange={handleChange('email')}
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
                {testBtn}
                <CardContent>
                    <Grid container direction="column" justify="center" alignItems="center">
                        <Grid item md={12}>
                            <Avatar className={classes.avatar}>
                                <LockOutlined />
                            </Avatar>
                        </Grid>
                        <Grid item md={12}>
                            <Typography component="h1" variant="h5">
                                {translations.signIn}
                            </Typography>
                        </Grid>
                        <Grid item md={12}>
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
                        <Grid item md={12}>
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