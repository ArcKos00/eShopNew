import { ReactElement, FC, useState, useEffect } from 'react';
import {
    Box,
    Container,
    CircularProgress,
    Grid
} from '@mui/material';
import ResourceCard from '../components/ResourceCard';
import { IResources } from '../../interfaces/resources';
import * as resourceApi from '../../api/modules/resources';
import { useParams } from 'react-router-dom';

const Resource: FC<any> = (): ReactElement => {
    const [resource, setResource] = useState<IResources | null>(null)
    const [isLoading, setIsLoading] = useState(false);
    const { id } = useParams();

    useEffect(() => {
        if (id) {
            const getResult = async () => {
                try {
                    setIsLoading(true);
                    const res = await resourceApi.getResourceById(id);
                    setResource(res?.data)
                }
                catch (e) {
                    if (e instanceof Error) {
                        console.log(e.message);
                    }
                };
                setIsLoading(false);
            };
            getResult();
        }
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
                <Grid container spacing={4} justifyContent='center' m={4}>
                    {isLoading ? (
                        <CircularProgress />
                    ) : (
                        <>
                            {!!resource &&
                                <ResourceCard{...{ resource: resource, isClicable: false }} />
                            }
                        </>
                    )}
                </Grid>
            </Container>
        </Box>
    );
};

export default Resource;