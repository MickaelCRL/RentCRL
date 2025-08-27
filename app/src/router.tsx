import { createBrowserRouter } from "react-router";
import App from "./App";
import Layout from "./components/Layout";
import Registration from "./pages/registration";
import Properties from "./pages/properties";
import NewPropertyPage from "./pages/properties/new";
import Contracts from "./pages/contracts";
import NewContractPage from "./pages/contracts/new";
import SelectRolePage from "./pages/select-role";
import Dashboard from "./pages/dashboard";

const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
  },
  {
    path: "/select-role",
    element: <SelectRolePage />,
  },
  {
    path: "/registration",
    element: <Registration />,
  },
  {
    path: "/",
    element: <Layout />,
    children: [
      {
        path: "/dashboard",
        element: <Dashboard />,
      },

      {
        path: "/properties",
        element: <Properties />,
      },
      {
        path: "/properties/new",
        element: <NewPropertyPage />,
      },
      {
        path: "/contracts",
        element: <Contracts />,
      },
      {
        path: "/contracts/new",
        element: <NewContractPage />,
      },
    ],
  },
]);

export default router;
