import apiClient from '../client';

const url = "http://www.alevelwebsite.com:5000/api/v1/catalogbff"

export const getItem = (id: number) => apiClient({
    path: `${url}/item`,
    method: 'POST',
    data: { id: id }
});

export const getItems = (pageIndex: number, pageSize: number, filter: object) => apiClient({
    path: `${url}/items`,
    method: 'POST',
    data: { pageIndex, pageSize, filter }
});

export const getAnomalies = () => apiClient({
    path: `${url}/getanomalies`,
    method: 'POST'
});

export const getMeets = () => apiClient({
    path: `${url}/getmeets`,
    method: 'POST'
});

export const getTypes = () => apiClient({
    path: `${url}/gettypes`,
    method: 'POST'
});