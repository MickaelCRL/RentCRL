import { createBrowserRouter, Outlet } from "react-router";
import App from "./App";
import Dashbord from "./pages/dashbord";

const Layout = () => {
  return (
    <>
      <Outlet />
    </>
  );
};

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
        path: "/dashbord",
        element: <Dashbord />,
      },
    ],
  },
]);

export default router;
