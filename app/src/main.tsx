import { createRoot } from "react-dom/client";
import { RouterProvider } from "react-router";
import { Auth0Provider } from "@auth0/auth0-react";
import router from "./router";

createRoot(document.getElementById("root")!).render(
  <Auth0Provider
    domain={import.meta.env.VITE_AUTH0_DOMAIN}
    clientId={import.meta.env.VITE_AUTH0_CLIENT_ID}
    authorizationParams={{
      audience: {import.meta.env.VITE_AUTH0_AUDIENCE},
      scope: "openid profile read:owners write:owners delete:owners",
      redirect_uri: window.location.origin,
    }}
    cacheLocation="localstorage"
  >
    <RouterProvider router={router} />
  </Auth0Provider>
);
