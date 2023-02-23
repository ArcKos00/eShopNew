import { ReactElement, FC, useEffect, useState } from 'react';
import {
    Box,
    Button,
    Container,
    CircularProgress,
    Grid
} from '@mui/material';
import * as userApi from '../../api/modules/users';
import { IUser } from '../../interfaces/users';
import { useParams } from 'react-router-dom';
import Form from '../components/UpdateForm/Form';
import Card from '@mui/material/Card';
import CardFilling from '../components/UserCard/UserFilling';
import * as apiClient from '../../api/modules/users';

const User: FC<any> = (): ReactElement => {
    const [user, setUser] = useState<IUser | null>(null);
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const { id } = useParams();
    const [openUpdate, setOpenUpdate] = useState(false);

    useEffect(() => {
        if (id) {
            const getUser = async () => {
                try {
                    setIsLoading(true);
                    const res = await userApi.getUserById(id);
                    setUser(res.data)
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

    const handlerUpdateClick = () => {
        setOpenUpdate(!openUpdate);
    };

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
                            {!!user &&
                                <Card sx={{ display: 'flex' }}>
                                    <Box
                                        sx={{
                                            display: 'flex',
                                            flexDirection: 'column'
                                        }}>
                                        <CardFilling {...user} />
                                        <Button
                                            onClick={handlerUpdateClick}
                                        >
                                            Update This User
                                        </Button>
                                    </Box>
                                    {openUpdate &&
                                        <Box>
                                            <Form
                                            formName = 'Update User'
                                                apiMethod={apiClient.updateUser}
                                                data={{ id: id, name: '', job: '' }}
                                                callBackClose={handlerUpdateClick}
                                            />
                                        </Box>}
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