import apiClient from '../client';

const url = "http://www.alevelwebsite.com:5004/api/v1/orderbff"

export const getItems = (pageIndex: number, pageSize: number, accessToken?: string) => apiClient({
    path: `${url}/getuserorders`,
    method: 'POST',
    data: { pageIndex, pageSize }
});