import { ReactElement, FC } from 'react';
import {
    Box,
    Button,
    Container,
    CircularProgress,
    Grid,
    Pagination
} from '@mui/material';
import UserCard from '../components/UserCard';
import Form from '../components/UpdateForm/Form';
import { createUser } from '../../api/modules/users';
import UserStore from './UsersStore';
import { observer } from 'mobx-react-lite';

const store = new UserStore();

const Users: FC<any> = (): ReactElement => {
    return (
        <Box
            sx={{
                flexGrow: 1,
                backgroundColor: 'whitesmoke',
                display: 'flex',
                justifyContent: 'center',
                alignItems: 'center',
            }}
        >
            <Container>
                <Box
                    sx={{
                        display: 'flex',
                        justifyContent: 'center'
                    }}>
                    <Button
                        fullWidth
                        variant='contained'
                        sx={{ mt: 3, mb: 2 }}
                        onClick={store.handleOpenForm}>
                        Create New User
                    </Button>
                    {store.openForm &&
                        <Box>
                            <Form
                                formName='Create User'
                                apiMethod={createUser}
                                data={{ name: '', job: '' }}
                                callBackClose={store.handleOpenForm}
                            />
                        </Box>}
                </Box>
                <Grid container spacing={4} justifyContent='center' my={4}>
                    {store.isLoading ? (
                        <CircularProgress />
                    ) : (
                        <>
                            {store.users?.map((item) => (
                                <Grid key={item.id} item lg={2} md={3} xs={6}>
                                    <UserCard {...{ user: item, isClicable: true }} />
                                </Grid>
                            ))}
                        </>
                    )}
                </Grid>
                <Box
                    sx={{
                        display: 'flex',
                        justifyContent: 'center'
                    }}
                >
                    <Pagination
                        count={store.totalPages}
                        page={store.currentPage}
                        onChange={async (event, page) => await store.changePage(page)} />
                </Box>
            </Container >
        </Box >
    );
};

export default observer(Users);