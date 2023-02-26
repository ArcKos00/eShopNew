import User from './pages/Artefact';


import { Route } from './interfaces/route'

export const userRoutes: Array<Route> = [
    {
        place: 'userMenu',
        key: 'info-user',
        title: 'User Info',
        path: 'user/:id',
        enabled: false,
        component: User
    },
] 