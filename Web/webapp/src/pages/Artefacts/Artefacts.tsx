import { ReactElement, FC, useEffect } from 'react';
import {
    Box,
    CircularProgress,
    Container,
    Grid,
    Pagination,
    Typography
} from '@mui/material';
import ArtefactStore from './ArtefactStore';
import { observer } from 'mobx-react-lite';
import ArtefactCard from '../components/ArtefactCard';

const store = new ArtefactStore();

const Artefacts: FC<any> = (): ReactElement => {
    useEffect(() => {
        store.reset();
    }, [])

    return (
        <Box
            sx={{
                flexGrow: 1,
                backgroundColor: 'whitesmoke',
                display: 'flex',
                justifyContent: 'center',
                alignItems: 'center'
            }}
        >
            <Container>
                <Grid container justifyContent="center" my={4}>
                    {store.isLoading ? (
                        <CircularProgress />
                    ) : (
                        <>
                            {store.artefacts?.map((item) => (
                                <Grid key={item.id} justifyContent="center" item my={1} lg={3} md={4} sm={6} xs={12} >
                                    <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center' }}>
                                        <ArtefactCard {...{ artefact: item, isClicable: true }} />
                                    </div>
                                </Grid>
                            ))}
                        </>
                    )}
                </Grid>
                <Box
                    sx={{
                        display: 'flex',
                        justifyContent: 'center'
                    }}>
                    <Pagination
                        count={store.totalPages}
                        page={store.currentPage}
                        onChange={async (event, page) => await store.changePage(page)} />
                </Box>
            </Container>
        </Box >
    );
};

export default observer(Artefacts);
