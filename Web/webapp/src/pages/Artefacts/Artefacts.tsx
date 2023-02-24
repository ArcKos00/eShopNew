import { ReactElement, FC } from 'react';
import {
    Box,
    Button,
    Container,
    CircularProgress,
    Grid,
    Pagination
} from '@mui/material';
import UserCard from '../components/ArtefactCard';
import Form from '../components/UpdateForm/Form';
import * as artefactApi from '../../api/modules/artefacts';
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
                <Grid container spacing={4} justifyContent='center' my={4}>
                    {store.isLoading ? (
                        <CircularProgress />
                    ) : (
                        <>
                            {store.users?.map((item) => (
                                <Grid key={item.Id} item lg={2} md={3} xs={6}>
                                    <UserCard {...{ artefact: item, isClicable: true }} />
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