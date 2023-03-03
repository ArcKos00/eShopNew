import { Route } from './interfaces/route';
import Callback from './stores/Callback';
import Artefact from './pages/Artefact';
import Artefacts from './pages/Catalog';
import Basket from './pages/Basket';
import Orders from './pages/Orders/Orders';

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
        place: 'NavBar',
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
    {
        place: 'UserMenu',
        key: 'order',
        title: 'Orders',
        path: '/order',
        enabled: true,
        component: Orders
    },
] 