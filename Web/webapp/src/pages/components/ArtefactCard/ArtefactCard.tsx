import { ReactElement, FC } from 'react';
import {
    Card,
    CardActionArea
} from '@mui/material';
import { IArtefact } from '../../../interfaces/artefacts';
import CardFilling from './ArtefactFilling';
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
                    onClick={() => navigate(`/user/${card.artefact?.Id}`)}
                >
                    <CardFilling {...card?.artefact} />
                </CardActionArea>}
            {!card.isClicable &&
                <CardFilling {...card?.artefact} />}
        </Card>
    );
};

export default UserCard;