import { UserManager, WebStorageStateStore } from "oidc-client";
import { Metadata_Oidc, IdentityConfig } from "./authConst";

export default class AuthServiceNew {
    UserManager;

    constructor() {
        this.UserManager = new UserManager(...IdentityConfig,
            userStore: new WebStorageStateStore({state: window.sessionStorage}),
            
            )
    }
}
