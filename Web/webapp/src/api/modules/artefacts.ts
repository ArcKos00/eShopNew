import apiClient from '../client';

export const getArtefactById = ( Id: string ) => apiClient({
    path: `/catalogbff/item/`,
    method: 'POST',
    data: {Id}
});

export const getArtefactsByPage = (paginated: { PageIndex: number, PageSize: number, Filters: [string, number] | null}) => apiClient({
    path: `/catalogbff/items/`,
    method: 'POST',
    data: paginated
});

export const getAnomalies = () => apiClient({
    path: `/catalogbff/getanomalies`,
    method: 'POST'
});

export const getMeets = () => apiClient({
    path: `/catalogbff/getmeets`,
    method: 'POST'
});

export const getTypes = () => apiClient({
    path: `/catalogbff/gettypes`,
    method: 'POST'
});