import { Box, Grid, Typography } from "@mui/material";
import { observer } from "mobx-react-lite";
import { FC, ReactElement, useContext } from "react";
import { AppStoreContext } from "../../App";
import BasketStore from "./BasketStore";

const Basket: FC<any> = (): ReactElement => {
    const app = useContext(AppStoreContext);
    const store = new BasketStore();
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
            <Grid
                container
                direction="row"
                justifyContent="space-evenly"
                alignItems="stretch"
            >
                {store.basket?.map(item => (
                    <Grid key={item.id} lg={3} md={4} sm={6} xs={12}>
                        <Typography>
                            {item.name}  {item.cost}
                        </Typography>
                    </Grid>
                ))}
            </Grid>
        </Box>
    );
}

export default observer(Basket);