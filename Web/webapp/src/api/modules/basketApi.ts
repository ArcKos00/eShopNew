import apiClient from '../client';

const url = "http://www.alevelwebsite.com:5003/api/v1/basketbff"

export const add = (userId: string, id: number, name: string, cost: number) => apiClient({
    path: `${url}/addtobasket`,
    method: 'POST',
    data: { userId, id, name, cost }
});

export const remove = (userId: string, id: number) => apiClient({
    path: `${url}/removefrombasket`,
    method: 'POST',
    data: { userId, id }
});

export const makeAnOrder = (userId: string) => apiClient({
    path: `${url}/makeanorder`,
    method: 'POST',
    data: { userId }
});

export const getBasket = (userId: string) => apiClient({
    path: `${url}/getbasket`,
    method: 'POST',
    data: { userId }
});

export const clear = (userId: string) => apiClient({
    path: `${url}/clearbasket`,
    method: `POST`,
    data: { userId }
});