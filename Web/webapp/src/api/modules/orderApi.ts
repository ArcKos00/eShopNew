import apiClient from '../client';

const url = "http://www.alevelwebsite.com:5004/api/v1/orderbff"

export const getItem = (userId: string, pageIndex: number, pageSize: number) => apiClient({
    path: `${url}/getuserorders`,
    method: 'POST',
    data: { userId, pageIndex, pageSize }
});