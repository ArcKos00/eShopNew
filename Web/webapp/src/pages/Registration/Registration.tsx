import { ReactElement, FC, useContext, useState } from 'react';
import { Box, TextField, Typography, Button, CircularProgress } from '@mui/material'
import { AppStoreContext } from '../../App';
import RegistrationStore from './RegistrationStore';
import { useNavigate } from 'react-router-dom';
import { observer } from 'mobx-react-lite';

const Registration: FC<any> = (): ReactElement => {
    const app = useContext(AppStoreContext);
    const store = new RegistrationStore(app.authStore);
    const navigate = useNavigate();

    const [user, setUser] = useState({ email: '', password: '', confirm: '' })

    const checkEmail = () => {
        return user.email.length < 6;
    }

    const checkPassword = () => {
        return user.password.length < 1;
    }

    const checkConfirm = () => {
        return user.confirm != user.password;
    }

    return (
        <Box
            sx={{
                flexGrow: 1,
                marginTop: 8,
                display: 'flex',
                flexDirection: 'column',
                justifyContent: 'center',
                alignItems: 'center'
            }}>
            <Typography component='h1' variant='h5'>
                Register
            </Typography>
            <Box component='form'
                onSubmit={async (event) => {
                    event.preventDefault();
                    store.changeData({ email: user.email, password: user.password });
                    alert(await store.register());
                    navigate('/login');
                }}
                sx={{ mt: 1 }}>
                <TextField
                    margin='normal'
                    required
                    fullWidth
                    id='email'
                    label='Email Address'
                    name='email'
                    error={checkEmail()}
                    helperText={!checkEmail() ? '' : 'Email must be at least 6 characters long'}
                    onChange={(event) => setUser({ ...user, email: event.target.value })}
                    autoFocus
                />
                <TextField
                    margin='normal'
                    required
                    fullWidth
                    id='password'
                    label='Password'
                    name='password'
                    error={checkPassword()}
                    helperText={!checkPassword() ? '' : 'Password must be at least 8 characters long'}
                    onChange={(event) => setUser({ ...user, password: event.target.value })}
                    autoFocus
                />
                <TextField
                    margin='normal'
                    required
                    fullWidth
                    id='password'
                    label='Password'
                    name='confirm'
                    error={checkConfirm()}
                    helperText={!checkConfirm() ? '' : 'Passwords do not match'}
                    onChange={(event) => setUser({ ...user, confirm: event.target.value })}
                    autoFocus
                />
                <Button
                    type='submit'
                    fullWidth
                    variant='contained'
                    sx={{ mt: 3, mb: 2 }}
                >
                    {store.isLoading ?
                        (<CircularProgress />) :
                        ('Submit')}
                </Button>
            </Box>
        </Box >
    );
};

export default observer(Registration);