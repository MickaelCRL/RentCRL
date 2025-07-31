import axios from "axios";
import { Auth0Client } from "@auth0/auth0-spa-js";

const auth0 = new Auth0Client({
  domain: import.meta.env.VITE_AUTH0_DOMAIN,
  clientId: import.meta.env.VITE_AUTH0_CLIENT_ID,
  authorizationParams: {
    audience: import.meta.env.VITE_AUTH0_AUDIENCE,
    scope: "openid profile email",
  },
  cacheLocation: "localstorage",
});

const api = axios.create({
  baseURL: `${globalConfig.apiBaseUrl}`,
});

api.interceptors.request.use(async (config) => {
  const token = await auth0.getTokenSilently();
  config.headers.set("Authorization", `Bearer ${token}`);
  return config;
});

export default api;
