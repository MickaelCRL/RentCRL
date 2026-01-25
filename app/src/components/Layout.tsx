import { useAuth0 } from "@auth0/auth0-react";
import { useEffect } from "react";
import { Outlet, useNavigate } from "react-router-dom";
import { useUserContext } from "../contexts/UserContext";
import useUser from "../services/users/useUser";

const Layout = () => {
  const { user, isLoading, isAuthenticated } = useAuth0();
  const { userContext, setUserContext } = useUserContext();
  const navigate = useNavigate();
  const email = user?.email || "";
  const { user: userSwr, isLoading: isLoadingSwr } = useUser(email);

  useEffect(() => {
    if (isLoading || isLoadingSwr) return;

    if (!isAuthenticated) {
      navigate("/");
      return;
    }

    if (isAuthenticated && !userContext && userSwr) {
      setUserContext(userSwr);
    }
  }, [isAuthenticated, isLoading, isLoadingSwr]);

  return <>{userContext && <Outlet />} </>;
};

export default Layout;
