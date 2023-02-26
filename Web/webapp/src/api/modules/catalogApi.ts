import apiClient from '../client';

const url = "http://www.alevelwebsite.com:5000/api/v1/catalogbff"

export const getitemById = (id: number) => apiClient({
    path: `${url}/item`,
    method: 'POST',
    data: { id: id }
});

export const getUserByPage = (page: number) => apiClient({
    path: `users?page=${page}`,
    method: 'GET'
});

export const updateUser = ({ id, name, job }: { id: number, name: string, job: string }) => apiClient({
    path: `users/${id}`,
    method: 'PUT',
    data: { id, name, job }
});

export const createUser = ({ name, job }: { name: string, job: string }) => apiClient({
    path: `users`,
    method: 'POST',
    data: { name, job }
});