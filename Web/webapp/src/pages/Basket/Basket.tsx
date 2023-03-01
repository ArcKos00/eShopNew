import {
    Box, Button, Container, CircularProgress, Grid, Typography, List,
    ListItem,
    ListItemText,
    IconButton,
    Divider,
} from "@mui/material";
import DeleteIcon from '@mui/icons-material/Delete'
import { observer } from "mobx-react-lite";
import { FC, ReactElement, useContext, useEffect } from "react";
import { AppStoreContext } from "../../App";
import BasketStore from "./BasketStore";

const store = new BasketStore();

const Basket: FC<any> = (): ReactElement => {
    const app = useContext(AppStoreContext);
    useEffect(() => {
        store.get(app.authStore.user?.profile.sub!);
    }, [])

    return (
        <>
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
                        <List sx={{ width: '100%', height: '80%', overflow: 'auto' }}>
                            {store.basket?.map((item) => (
                                <>
                                    <ListItem
                                        secondaryAction={
                                            <IconButton onClick={() =>
                                                store.remove(app.authStore.user?.profile.sub!, item.id)
                                            } edge="end" aria-label="delete">
                                                <DeleteIcon />
                                            </IconButton>
                                        }
                                    >
                                        <ListItemText>
                                            <Grid container justifyContent="space-between">
                                                <Grid item>
                                                    <Typography variant="h6" >
                                                        {item.name}
                                                    </Typography>
                                                </Grid>
                                                <Grid item>
                                                    <Typography variant="h6">
                                                        {item.cost.toLocaleString('en-US', { style: 'currency', currency: 'USD' })}
                                                    </Typography>
                                                </Grid>
                                            </Grid>
                                        </ListItemText>
                                    </ListItem>
                                    <Divider />
                                </>
                            ))}
                        </List>
                    )}
                    <Container>
                        <Typography textAlign="end">
                            Total Cost: {store.totalCost.toLocaleString('en-US', { style: 'currency', currency: 'USD' })}
                        </Typography>
                        <Grid container justifyContent="end">
                            <Grid item>
                                <Button variant="outlined" onClick={() => store.clear(app.authStore.user?.profile.sub!)}>
                                    Clear Basket
                                </Button>
                            </Grid>
                            <Grid item>
                                <Button variant="contained" onClick={() => store.makeAnOrder(app.authStore.user?.profile.sub!)}>
                                    Create Order
                                </Button>
                            </Grid>
                        </Grid>
                    </Container>
                </Container>
            </Box >
        </>
    );
}

export default observer(Basket);