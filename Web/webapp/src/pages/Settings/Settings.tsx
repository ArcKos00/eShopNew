import { ReactElement, FC } from 'react';
import { Box } from '@mui/material';
import { useParams } from 'react-router-dom';

const Settings: FC = (): ReactElement => {

    const {id} = useParams();
    return (
        <Box sx={{
            flexGrow: 1,
            justifyContent: 'center',
            display: 'flex',
            flexDirection: 'column',
            alignItems: 'center'
        }}>
            Some User Settings
        </Box>
    );
};

export default Settings;