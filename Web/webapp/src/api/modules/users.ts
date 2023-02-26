import apiClient from '../client';

export const getUserById = (id: string) => apiClient({
    path: `users/${id}`,
    method: 'GET'
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