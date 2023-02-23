import { ReactElement, FC } from 'react';
import {
    Box,
    Container,
    CircularProgress,
    Grid,
    Pagination
} from '@mui/material';
import { observer } from 'mobx-react-lite';
import ResourceCard from '../components/ResourceCard';
import ResourcesStore from './ResourcesStore';

const store = new ResourcesStore();

const Resources: FC<any> = (): ReactElement => {
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
                            {store.resources?.map((item) => (
                                <Grid key={item.id} item lg={3} md={4} sm={6} xs={12}>
                                    <ResourceCard {...{ resource: item, isClicable: true }} />
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
            </Container>
        </Box>
    );
};

export default observer(Resources);