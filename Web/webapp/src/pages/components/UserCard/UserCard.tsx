import { ReactElement, FC } from 'react';
import {
    Card,
    CardActionArea
} from '@mui/material';
import { IUser } from '../../../interfaces/users';
import CardFilling from './UserFilling';
import { useNavigate } from 'react-router-dom';

interface UserCardProps {
    user: IUser,
    isClicable: boolean
};

const UserCard: FC<UserCardProps> = (card): ReactElement => {
    const navigate = useNavigate();

    return (
        <Card
            sx={{ maxWidth: 250 }}
        >
            {card.isClicable &&
                <CardActionArea
                    onClick={() => navigate(`/user/${card.user?.id}`)}
                >
                    <CardFilling {...card?.user} />
                </CardActionArea>}
            {!card.isClicable &&
                <CardFilling {...card?.user} />}
        </Card>
    );
};

export default UserCard;