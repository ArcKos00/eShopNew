import { FC } from 'react';
import {
    CardContent,
    CardMedia,
    Typography,
} from '@mui/material';
import { IArtefact } from '../../../interfaces/artefact';

const CardFilling: FC<IArtefact> = (artefact) => {
    return (
        <>
            <CardMedia
                component='img'
                height='250'
                image={artefact?.imageUrl}
                alt={artefact?.nature}
            />
            <CardContent>
                <Typography noWrap gutterBottom variant='h6' component='div'>
                    {artefact?.name}
                </Typography>
                <Typography variant='body2' textAlign='end' color='text.secondary'>
                    {artefact?.cost.toLocaleString('en-US', { style: 'currency', currency: 'USD' })}
                </Typography>
            </CardContent>
        </>
    );
};

export default CardFilling;