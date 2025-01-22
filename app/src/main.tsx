import { createRoot } from "react-dom/client";
import { RouterProvider } from "react-router";
import { Auth0Provider } from "@auth0/auth0-react";
import router from "./router";

createRoot(document.getElementById("root")!).render(
  <Auth0Provider
    domain="dev-bko6t72qud7vo3ue.us.auth0.com"
    clientId="UGNkulDhA0DnTQWwQ0E42J5XMf1aDlAo"
    authorizationParams={{
      redirect_uri: window.location.origin,
    }}
    cacheLocation="localstorage"
  >
    <RouterProvider router={router} />
  </Auth0Provider>
);
