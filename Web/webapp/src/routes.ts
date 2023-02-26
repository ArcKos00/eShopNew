import About from './pages/About';
import Home from './pages/Home';
import User from './pages/User';
import Users from './pages/Users';
import { Route } from './interfaces/route';
import Callback from './stores/Callback';
export const routes: Array<Route> = [
    {
        place: 'NavBar',
        key: 'home-route',
        title: 'Home',
        path: '/',
        enabled: true,
        component: Home
    },
    {
        place: 'NavBar',
        key: 'about-route',
        title: 'About',
        path: '/about',
        enabled: true,
        component: About
    },
    {
        place: 'NavBar',
        key: 'users-route',
        title: 'Users',
        path: '/users',
        enabled: true,
        component: Users
    },
    {
        place: 'NavBar',
        key: 'user-route',
        title: 'User',
        path: '/user/:id',
        enabled: false,
        component: User
    },
    {
        place: 'UserMenu',
        key: 'info-user',
        title: 'User Info',
        path: '/user/:id',
        enabled: false,
        component: User
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