import { UserManager } from "oidc-client";

const settings ={
    client_id: "client_pkce",
    authority: "http://www.alevelwebsite.com:5002",
    response_type: "token id_token",
    redirect_uri: "http://www.alevelwebsite.com:3001/signin-oidc",
    post_logout_redirect_uri: "http://www.alevelwebsite.com:3001",
    scope: "openid profile client catalog.api.catalogbff basket.basketCache.api order.orderbff.api",
    loadUserInfo: true,
}

export default class AuthService {
    userManager;

    constructor()
    {
        this.userManager = new UserManager(settings);
    }
}