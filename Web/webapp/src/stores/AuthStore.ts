import { makeAutoObservable } from 'mobx';
import * as authApi from '../api/modules/auth';
import AuthService from './AuthService';

class AuthStore {
    token = '';
    AuthService;
    constructor() {
        makeAutoObservable(this);
        this.AuthService = new AuthService();
    };

    async login(email: string, password: string) {
        
    };

    logout() {
        this.token = '';
    };
};

export default AuthStore;