import { makeAutoObservable } from 'mobx';
import { User, UserManager, Log, WebStorageStateStore } from 'oidc-client';

const config = {
    authority: 'http://localhost:5002',
    client_id: 'client_pkce',
    client_secret: "secret",
    redirect_uri: `http://localhost:3000/callback`,
    response_type: 'code',
    scope: 'openid profile catalog.api.catalogbff basket.basketCache.api order.orderbff.api',
    post_logout_redirect_uri: `http://localhost:3000`,
    useStore: new WebStorageStateStore({ store: window.localStorage }),
    automaticSilentRenew: true,
    monitorSession: true,
    checkSessionInterval: 2000,
    silent_redirect_uri: `http://localhost:3000`,
    filterProtocolClaims: true,
    revokeAccessTokenOnSignout: true,
    revokeRefreshTokenOnSignout: true,
    usePkce: true,
};

Log.level = Log.INFO;
Log.logger = console;

class AuthStore {
    user: User | null = null;
    oidc_client: UserManager;

    constructor() {
        this.oidc_client = new UserManager(config);
        makeAutoObservable(this);
        this.oidc_client.events.addUserLoaded((user) => {
            if (user) {
                this.user = user;
            }
        });
    };

    async login() {
        this.oidc_client.signinRedirect({ state: { some: "some" } });
    };

    logout() {
        this.oidc_client.signoutRedirect();
        this.user = null;
    };

    async handleCallback() {
        const user = await this.oidc_client.signinRedirectCallback();
        this.user = user;
    }
};

export default AuthStore;