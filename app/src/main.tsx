import { createRoot } from "react-dom/client";
import { RouterProvider } from "react-router";
import { Auth0Provider } from "@auth0/auth0-react";
import router from "./router";
import "./style.css";
import { BreadcrumbProvider } from "./contexts/BreadcrumbContext";

console.log("From main");
console.log(globalConfig);

createRoot(document.getElementById("root")!).render(
  <Auth0Provider
    domain={import.meta.env.VITE_AUTH0_DOMAIN}
    clientId={import.meta.env.VITE_AUTH0_CLIENT_ID}
    authorizationParams={{
      audience: import.meta.env.VITE_AUTH0_AUDIENCE,
      scope: "openid profile email",
      redirect_uri: window.location.origin,
    }}
    cacheLocation="localstorage"
  >
    <BreadcrumbProvider>
      <RouterProvider router={router} />
    </BreadcrumbProvider>
  </Auth0Provider>
);
