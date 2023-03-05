import { ReactElement, FC, useEffect } from 'react';
import {
    Box,
    CircularProgress,
    Container,
    FormControl,
    Grid,
    Select,
    InputLabel,
    Pagination,
    Typography,
    MenuItem,
    Button
} from '@mui/material';
import ArtefactStore from './CatalogStore';
import { observer } from 'mobx-react-lite';
import ArtefactCard from '../components/ArtefactCard';
import Filter from './Filter';

const store = new ArtefactStore();
const filter = new Filter();

const Artefacts: FC<any> = (): ReactElement => {
    useEffect(() => {
        store.reset();
    }, [])

    const filterSearch = () => {
        store.setFilter(filter.filter);
    }
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
            <Grid container justifyContent="space-between" direction="row">
                <Grid item direction="column" justifyContent="center" sx={{ height: "100%", maxWidth: "15%" }}>
                    <FormControl fullWidth>
                        <InputLabel id="anomaly">Anomaly</InputLabel>
                        <Select
                            labelId='anomaly'
                            id="anomalyselect"
                            value={filter.filter["Anomaly"]}
                            onChange={event => {
                                filter.setAnomaly(event.target.value as number);
                                filterSearch();
                            }}
                        >
                            {filter.Anomalies?.map((item) => (
                                < MenuItem value={item.id} > {item.name}</MenuItem>
                            ))}
                        </Select>
                    </FormControl>
                    <FormControl fullWidth>
                        <InputLabel id="type">Type</InputLabel>
                        <Select
                            labelId='type'
                            id="typeselect"
                            value={filter.filter["Type"]}
                            onChange={event => {
                                filter.setType(event.target.value as number);
                                filterSearch();
                            }}
                        >
                            {filter.Types?.map((item) => (
                                < MenuItem value={item.id} > {item.name}</MenuItem>
                            ))}
                        </Select>
                    </FormControl>
                    <FormControl fullWidth>
                        <InputLabel id="meet">Meet</InputLabel>
                        <Select
                            labelId='meet'
                            id="meetselect"
                            value={filter.filter["Meets"]}
                            onChange={event => {
                                filter.setMeet(event.target.value as number);
                                filterSearch();
                            }}
                        >
                            {filter.Meets?.map((item) => (
                                < MenuItem value={item.id} > {item.meets}</MenuItem>
                            ))}
                        </Select>
                    </FormControl>
                    <Button onClick={() => {
                        filter.clearFilter();
                        filterSearch();
                    }}>
                        Clear
                    </Button>
                </Grid>
                <Grid item sx={{ height: "100%", maxWidth: "85%" }}>
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
                </Grid>
            </Grid>
        </Box >
    );
};

export default observer(Artefacts);
