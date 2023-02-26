import { ReactElement, FC } from 'react';
import {
    Card,
    CardActionArea
} from '@mui/material';
import { IResources } from '../../../interfaces/resources';
import { useNavigate } from 'react-router-dom';
import ResourceFilling from './ResourceFilling';

interface ResourceCardProps {
    resource: IResources,
    isClicable: boolean
};

const ResourceCard: FC<ResourceCardProps> = (card): ReactElement => {
    const navigate = useNavigate();

    return (
        <Card
            sx={{
                maxWidth: 350,
                minWidth: 250
            }}
        >
            {
                card.isClicable &&
                <CardActionArea
                    onClick={() => navigate(`/resource/${card.resource?.id}`)}
                >
                    <ResourceFilling {...card?.resource} />
                </CardActionArea>
            }
            {
                !card.isClicable &&
                <ResourceFilling {...card?.resource} />
            }
        </Card >
    );
};

export default ResourceCard;