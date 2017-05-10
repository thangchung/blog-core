import { createUserManager } from "redux-oidc";

const userManagerConfig = {
  client_id: "blogcore_client",
  redirect_uri: `${window.location.protocol}//${window.location.hostname}${window.location.port ? `:${window.location.port}` : ""}/callback`,
  response_type: "token id_token",
  scope: "openid profile blogcore_api_blogs",
  authority: `${window.location.protocol}//${window.location.hostname}:8483`,
  // silent_redirect_uri: `${window.location.protocol}//${window.location.hostname}${window.location.port ? `:${window.location.port}` : ""}/silent_renew.html`,
  automaticSilentRenew: true,
  filterProtocolClaims: true,
  loadUserInfo: true
};

const userManager = createUserManager(userManagerConfig);

export default userManager;
