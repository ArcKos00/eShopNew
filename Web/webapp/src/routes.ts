import { Route } from './interfaces/route';
import Callback from './stores/Callback';
import Artefact from './pages/Artefact';

export const routes: Array<Route> = [
    {
        place: 'NavBar',
        key: 'home-route',
        title: 'Home',
        path: '/',
        enabled: true,
        component: Callback
    },
    {
        place: 'NavBar',
        key: 'artefact-route',
        title: 'Artefact',
        path: '/artefact/:id',
        enabled: false,
        component: Artefact
    },
    {
        place: 'UserMenu',
        key: 'info-user',
        title: 'User Info',
        path: '/user/:id',
        enabled: false,
        component: Callback
    },
    {
        place: 'void',
        key: 'callback',
        title: 'CallBack',
        path: '/callback',
        enabled: false,
        component: Callback
    },
] 