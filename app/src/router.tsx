import { createBrowserRouter } from "react-router";
import App from "./App";
import Layout from "./components/Layout";
import Dashboard from "./pages/dashboard";
import Registration from "./pages/registration";

const router = createBrowserRouter([
  {
    path: "/",
    element: <Layout />,
    children: [
      {
        path: "/",
        element: <App />,
      },
      {
        path: "/dashboard",
        element: <Dashboard />,
      },
      {
        path: "/registration",
        element: <Registration />,
      },
    ],
  },
]);

export default router;
