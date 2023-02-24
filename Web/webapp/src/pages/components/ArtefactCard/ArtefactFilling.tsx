import { FC } from 'react';
import {
    CardContent,
    CardMedia,
    Typography,
} from '@mui/material';
import { IArtefact } from '../../../interfaces/artefacts';

const CardFilling: FC<IArtefact> = (user) => {
    return (
        <>
            <CardMedia
                component='img'
                height='250'
                image={user?.ImageUrl}
                alt={user?.Nature}
            />
            <CardContent>
                <Typography noWrap gutterBottom variant='h6' component='div'>
                    {user?.Name}
                </Typography>
                <Typography variant='body2' color='text.secondary'>
                    {user?.Cost}
                </Typography>
            </CardContent>
        </>
    );
};

export default CardFilling;