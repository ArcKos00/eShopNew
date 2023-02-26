import { ReactElement, FC } from 'react';
import {
    Box,
    Typography
} from '@mui/material';

const Home: FC<any> = (): ReactElement => {
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
            
        </Box>
    );
};

export default Home;
