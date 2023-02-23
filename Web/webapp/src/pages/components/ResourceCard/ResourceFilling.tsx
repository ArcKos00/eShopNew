import { FC } from 'react';
import {
    CardContent,
    Box,
    Typography
} from '@mui/material';
import { IResources } from '../../../interfaces/resources';

const ResourcesFilling: FC<IResources> = (resource) => {
    return (
        <>
            <CardContent>
                <Typography noWrap gutterBottom variant='h6' component='div'>
                    Name: {resource?.name}
                </Typography>
                <Typography noWrap gutterBottom variant='h6' component='div'>
                    Year: {resource?.year}
                </Typography>
                <Box sx={{ display: 'flex', justifyContent: 'space-between', flexDirection: 'row' }}>
                    <Typography noWrap gutterBottom variant='h6' component='div'>
                        Color: {resource?.color}
                    </Typography>
                    <Box bgcolor={resource?.color} sx={{ width: 25, height: 25 }} />
                </Box>
                <Typography noWrap gutterBottom variant='h6' component='div'>
                    Pantone Value: {resource?.pantone_value}
                </Typography>
            </CardContent>
        </>
    );
};

export default ResourcesFilling;