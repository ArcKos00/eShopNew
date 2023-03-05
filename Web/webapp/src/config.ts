export const config = {
    authority: 'http://www.alevelwebsite.com:5002',
    client_id: 'client_pkce',
    client_secret: "secret",
    redirect_uri: `http://http://www.alevelwebsite.com:5001/callback`,
    response_type: 'code',
    scope: 'openid profile endUser',
    post_logout_redirect_uri: `http://http://www.alevelwebsite.com:5001`,
    automaticSilentRenew: true,
    monitorSession: true,
    checkSessionInterval: 2000,
    silent_redirect_uri: `http://http://www.alevelwebsite.com:5001`,
    filterProtocolClaims: true,
    revokeAccessTokenOnSignout: true,
    revokeRefreshTokenOnSignout: true,
    usePkce: true,
};

export default config;