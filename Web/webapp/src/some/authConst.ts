import { login } from "../api/modules/auth";

export const IdentityConfig = {
    authority: process.env.AuthAppUrl,
    client_id: process.env.ClientId,
    redirect_uri: process.env.RedirectUrl,
    login: process.env.AuthAppUrl + "/login",
    automaticSilentRenew: false,
    loadUserInfo: false,
    silent_redirect_uri: process.env.SilentRedirectUrl,
    post_logout_redirect_uri: process.env.PostLogoutRedirectUrl,
    audience: "alevelwebsite.com",
    response_type: "token id_token",
    grantType: "implicit",
    scope: "openid profile client catalog.api.catalogbff basket.basketCache.api order.orderbff.api",
    webAuthResponseType: "id_token token"
}

export const Metadata_Oidc = {
    jwks_uri: process.env.AuthAppUrl + "/.well-know/openid-configuration/jwks",
    authorization_endpoint: process.env.AuthAppUrl + "/connect/authorize",
    token_endpoint: process.env.AuthAppUrl + "/connect/token",
    userinfo_endpoint: process.env.AuthAppUrl + "/connect/userinfo",
    end_session_endpont: process.env.AuthAppUrl + "/connect/endsession",
    check_session_iframe: process.env.AuthAppUrl = "/connect/checksession",
    revocation_endpoint: process.env.AuthAppUrl + "/connect/revocation",
    introspection_endpoint: process.env.AuthAppUrl + "/connect/introspect"
}