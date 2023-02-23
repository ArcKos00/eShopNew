import React, { FC, ReactElement, useContext } from 'react';
import {
    Box,
    Button,
    CircularProgress,
    TextField,
    Typography
} from '@mui/material';
import LoginStore from './LoginStore';
import { AppStoreContext } from '../../App';
import { observer } from 'mobx-react-lite';
import { useNavigate } from 'react-router-dom';

const Login = () => {
    const appStore = useContext(AppStoreContext);
    const store = new LoginStore(appStore.authStore);
    const navigate = useNavigate();

    return (
        <Box sx={{
            flexGrow: 1,
            justifyContent: 'center',
            display: 'flex',
            flexDirection: 'column',
            alignItems: 'center'
        }}>
            <Typography component='h1' variant='h5'>
                Sign in
            </Typography>
            <Box component='form'
                onSubmit={async (event) => {
                    event.preventDefault();
                    await store.login()
                    if (!!appStore.authStore.token) {
                        navigate('/')
                    }
                }}
                noValidate sx={{ mt: 1 }}>
                <TextField
                    margin='normal'
                    required
                    fullWidth
                    name='email'
                    label='Email'
                    autoComplete='email'
                    onChange={(event) => store.changeEmail(event.target.value)}
                    autoFocus
                />
                <TextField
                    margin='normal'
                    required
                    fullWidth
                    name='password'
                    label='Password'
                    type='password'
                    onChange={(event) => store.changePassword(event.target.value)}
                    autoComplete='current-password'
                />
                {!!store.error && (
                    <p style={{ color: 'red', fontSize: 14 }}>
                        {store.error}
                    </p>
                )}
                <Button
                    type='submit'
                    fullWidth
                    variant='contained'
                    sx={{ mt: 3, mb: 2 }}>
                    {!!store.isLoading ? (<CircularProgress />) : ('Submit')}
                </Button>
            </Box>
        </Box>
    );
};

export default observer(Login);