import { ReactElement, FC, useEffect, useState, useContext } from 'react';
import {
    Box,
    Button,
    Container,
    CircularProgress,
    Grid,
    Card,
    CardMedia,
    CardContent,
    Typography
} from '@mui/material';
import { IArtefact } from '../../interfaces/artefact';
import { useParams } from 'react-router-dom';
import { getItem } from '../../api/modules/catalogApi';
import { AppStoreContext } from '../../App';
import { observer } from 'mobx-react-lite';
import BasketStore from '../Basket/BasketStore';

const Artefact: FC<any> = (): ReactElement => {
    const app = useContext(AppStoreContext);
    const basketStore = new BasketStore();
    const [artefact, setArtefact] = useState<IArtefact | null>(null);
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const { id } = useParams();

    useEffect(() => {
        if (id) {
            const getArtefact = async () => {
                try {
                    setIsLoading(true);
                    const res = await getItem(Number(id));
                    setArtefact(res)
                }
                catch (e) {
                    if (e instanceof Error) {
                        console.error(e.message);
                    }
                }
                setIsLoading(false);
            };
            getArtefact();
        };
    }, [id])

    const addToBasket = async () => {
        await basketStore.add(app.authStore.user?.profile.sub!, artefact?.id!, artefact?.name!, artefact?.cost!);
    }

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
                <Grid item container justifyContent='center'>
                    {isLoading ? (
                        <CircularProgress />
                    ) : (
                        <>
                            {!!artefact &&
                                <Card
                                    sx={{ width: "100vw", height: "100vh" }}
                                >
                                    <Grid container>
                                        <Grid item>
                                            <CardMedia
                                                sx={{ width: 200, height: 200 }}
                                                component='img'
                                                image={artefact.imageUrl}
                                            />
                                        </Grid>
                                        <Grid item>
                                            <CardContent>
                                                <Typography>
                                                    Id: {artefact.id}
                                                </Typography>
                                                <Typography>
                                                    Nature: {artefact.nature}
                                                </Typography>
                                                <Typography>
                                                    Name: {artefact.name}
                                                </Typography>
                                                <Typography>
                                                    Cost: {artefact.cost.toLocaleString('en-US', { style: 'currency', currency: 'USD' })}
                                                </Typography>
                                            </CardContent>
                                        </Grid>
                                    </Grid>
                                    <Button variant='contained' color='inherit' onClick={addToBasket}>
                                        Add To Basket
                                    </Button>
                                </Card>
                            }
                        </>
                    )}
                </Grid>
            </Container>
        </Box>
    );
};

export default observer(Artefact);