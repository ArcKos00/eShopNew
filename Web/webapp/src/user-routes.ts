import User from './pages/User';
import Settings from './pages/Settings'


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
    {
        place: 'userMenu',
        key: 'user',
        title: 'User Settings',
        path: 'userSettings',
        enabled: true,
        component: Settings
    }
] 