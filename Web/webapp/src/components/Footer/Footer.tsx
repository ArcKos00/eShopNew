import { ReactElement, FC, useContext } from 'react';
import { Box, Container, Grid, Typography } from '@mui/material';
import { AppStoreContext } from '../../App';
import { observer } from 'mobx-react-lite';

export const Footer: FC = (): ReactElement => {
    const app = useContext(AppStoreContext);

    return (
        <Box
            sx={{
                width: '100%',
                height: 'auto',
                backgroundColor: 'secondary.main',
                padding: '1rem',
                paddingBottom: '1rem'
            }}
        >
            <Container maxWidth='lg'>
                <Grid container direction='column' alignItems='center'>
                    <Grid item xs={12}>
                        <Typography color='black' variant='h5'>
                            {!!app.authStore.token ? (
                                <span
                                    style={{ color: 'green' }}>{`Token is: ${app.authStore.token}`}</span>
                            ) : 'ArcKos00'}
                        </Typography>
                    </Grid>
                    <Grid item xs={12}>
                        <Typography color='textSecondary' variant='subtitle1'>
                            {`${new Date().getFullYear()} | React | Material UI | React Router`}
                        </Typography>
                    </Grid>
                </Grid>
            </Container>
        </Box>
    );
};

export default observer(Footer);