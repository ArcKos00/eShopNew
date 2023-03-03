import apiClient from '../client';

const url = "http://www.alevelwebsite.com:5003/api/v1/basketbff"

export const add = (id: number, name: string, cost: number) => apiClient({
    path: `${url}/addtobasket`,
    method: 'POST',
    data: { id, name, cost }
});

export const remove = ( id: number) => apiClient({
    path: `${url}/removefrombasket`,
    method: 'POST',
    data: { id }
});

export const makeAnOrder = () => apiClient({
    path: `${url}/makeanorder`,
    method: 'POST',
});

export const getBasket = () => apiClient({
    path: `${url}/getbasket`,
    method: 'POST'
});

export const clear = () => apiClient({
    path: `${url}/clearbasket`,
    method: `POST`
});