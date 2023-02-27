import { Route } from './interfaces/route';
import Callback from './stores/Callback';
import Artefact from './pages/Artefact';
import Artefacts from './pages/Artefacts';
import Basket from './pages/Basket';

export const routes: Array<Route> = [
    {
        place: 'NavBar',
        key: 'home-route',
        title: 'Catalog',
        path: '/',
        enabled: true,
        component: Artefacts
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
        title: 'Basket',
        path: '/basket',
        enabled: true,
        component: Basket
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