import { FC } from 'react';
import {
    CardContent,
    CardMedia,
    Typography,
} from '@mui/material';
import { IUser } from '../../../interfaces/users';

const CardFilling: FC<IUser> = (user) => {
    return (
        <>
            <CardMedia
                component='img'
                height='250'
                image={user?.avatar}
                alt={user?.email}
            />
            <CardContent>
                <Typography noWrap gutterBottom variant='h6' component='div'>
                    {user?.email}
                </Typography>
                <Typography variant='body2' color='text.secondary'>
                    {user?.first_name} {user?.last_name}
                </Typography>
            </CardContent>
        </>
    );
};

export default CardFilling;