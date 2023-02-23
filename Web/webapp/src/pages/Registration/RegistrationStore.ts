import { makeAutoObservable } from 'mobx';
import AuthStore from '../../stores/AuthStore';

class RegistrationStore {
    private authStore: AuthStore;

    email = '';
    password = '';
    confirm = '';
    error = '';
    isLoading = false;

    constructor(authStore: AuthStore) {
        this.authStore = authStore;
        makeAutoObservable(this);
    };

    changeData({ email, password }: { email: string, password: string }) {
        this.email = email;
        this.password = password;
        if (!!this.error) {
            this.error = ''
        }
    };

    changeEmail(email: string) {
        this.email = email;
    };

    changePassword(password: string) {
        this.password = password;
    };

    changeConfirm(confirm: string) {
        this.confirm = confirm;
    };

    async register() {
        let response = { id: 0, token: '' };
        try {
            this.isLoading = true;
            response = await this.authStore.register(this.email, this.password);
        }
        catch (e) {
            if (e instanceof Error) {
                this.error = e.message;
            };
        }
        this.isLoading = false;
        return `User with id: ${response.id} and token: ${response.token}`;
    };
};

export default RegistrationStore;