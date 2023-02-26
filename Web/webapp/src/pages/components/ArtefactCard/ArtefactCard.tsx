import { ReactElement, FC } from 'react';
import {
    Card,
    CardActionArea
} from '@mui/material';
import { IArtefact } from '../../../interfaces/artefact';
import CardFilling from './CardFilling';
import { useNavigate } from 'react-router-dom';

interface ArtefactCardProps {
    artefact: IArtefact,
    isClicable: boolean
};

const UserCard: FC<ArtefactCardProps> = (card): ReactElement => {
    const navigate = useNavigate();

    return (
        <Card
            sx={{ maxWidth: 250 }}
        >
            {card.isClicable &&
                <CardActionArea
                    onClick={() => navigate(`/user/${card.artefact?.id}`)}
                >
                    <CardFilling {...card?.artefact} />
                </CardActionArea>}
            {!card.isClicable &&
                <CardFilling {...card?.artefact} />}
        </Card>
    );
};

export default UserCard;