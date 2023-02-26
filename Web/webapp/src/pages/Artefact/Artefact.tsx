import { ReactElement, FC, useEffect, useState } from 'react';
import {
    Box,
    Button,
    Container,
    CircularProgress,
    Grid
} from '@mui/material';
import { IArtefact } from '../../interfaces/artefact';
import { useParams } from 'react-router-dom';
import Card from '@mui/material/Card';
import CardFilling from '../components/ArtefactCard/CardFilling';
import { getitemById } from '../../api/modules/catalogApi';

const Artefact: FC<any> = (): ReactElement => {
    const [artefact, setArtefact] = useState<IArtefact | null>(null);
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const { id } = useParams();

    useEffect(() => {
        if (id) {
            const getArtefact = async () => {
                try {
                    setIsLoading(true);
                    const res = await getitemById(Number(id));
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
                <Grid item container justifyContent='center' m={4}>
                    {isLoading ? (
                        <CircularProgress />
                    ) : (
                        <>
                            {!!artefact &&
                                <Card sx={{ display: 'flex' }}>
                                    <Box
                                        sx={{
                                            display: 'flex',
                                            flexDirection: 'column'
                                        }}>
                                        <CardFilling {...artefact} />
                                    </Box>
                                </Card>
                            }
                        </>
                    )}
                </Grid>
            </Container>
        </Box>
    );
};

export default Artefact;