import { ReactElement, FC, useEffect, useState } from 'react';
import {
    Box,
    Button,
    Container,
    CircularProgress,
    Grid
} from '@mui/material';
import * as artefactApi from '../../api/modules/artefacts';
import { IArtefact } from '../../interfaces/artefacts';
import { useParams } from 'react-router-dom';
import Card from '@mui/material/Card';
import CardFilling from '../components/ArtefactCard/ArtefactFilling';

const User: FC<any> = (): ReactElement => {
    const [artefact, setArtefact] = useState<IArtefact | null>(null);
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const { id } = useParams();

    useEffect(() => {
        if (id) {
            const getUser = async () => {
                try {
                    setIsLoading(true);
                    const res = await artefactApi.getArtefactById(id);
                    setArtefact(res.data)
                }
                catch (e) {
                    if (e instanceof Error) {
                        console.error(e.message);
                    }
                }
                setIsLoading(false);
            };
            getUser();
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

export default User;