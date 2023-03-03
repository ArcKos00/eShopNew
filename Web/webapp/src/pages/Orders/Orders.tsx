import { observer } from "mobx-react-lite";
import {
    Box,
    CircularProgress,
    Container,
    Grid,
    List,
    ListItemText,
    Pagination,
    Divider,
    Accordion,
    AccordionSummary,
    AccordionDetails,
} from '@mui/material';
import ArrowDropDownIcon from '@mui/icons-material/ArrowDropDown';
import { FC, ReactElement, useEffect } from "react";
import OrdersStore from "./OrdersStore";


const store = new OrdersStore();

const Orders: FC<any> = (): ReactElement => {
    useEffect(() => {
        store.getOrders()
    }, [])

    return (
        <Box
            sx={{
                flexGrow: 1,
                backgroundColor: 'whitesmoke',
                display: 'flex',
                justifyContent: 'center',
            }}
        >
            <Container>
                {store.isLoading ? (
                    <CircularProgress />
                ) : (
                    <>
                        <Pagination
                            count={store.countPage}
                            page={store.currentPage}
                            onChange={async (event, page) => await store.changePage(page)} />
                        <List sx={{ width: '100%', height: '80%', overflow: 'auto' }}>
                            {store.orders?.map((item) => (
                                <Accordion>
                                    <AccordionSummary expandIcon={<ArrowDropDownIcon />}>
                                        <Grid container justifyContent='space-between' direction="row">
                                            <Grid item>
                                                <ListItemText primary={item.id} />
                                                <ListItemText primary={item.date.getDate()} />
                                            </Grid>
                                            <Grid item>
                                                <ListItemText primary={item.status} />
                                            </Grid>
                                            <Grid item>
                                                <ListItemText primary={item.totalCost} />
                                            </Grid>
                                        </Grid>
                                    </AccordionSummary>
                                    <AccordionDetails>
                                        <ListItemText primary="Details for item 1" />
                                    </AccordionDetails>
                                    <Divider />
                                </Accordion>
                            ))}
                        </List>
                    </>
                )}
            </Container>
        </Box >
    );
}

export default observer(Orders);