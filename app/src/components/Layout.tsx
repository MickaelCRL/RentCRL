import { useCallback, useEffect } from "react";
import { Outlet, useNavigate } from "react-router-dom";
import { useUserContext } from "../contexts/UserContext";
import { useAuth0 } from "@auth0/auth0-react";
import { getUserByEmailAsync } from "../services/users/userServices";

const Layout = () => {
  const { user, isLoading, isAuthenticated, getAccessTokenSilently } =
    useAuth0();
  const { userContext, setUserContext } = useUserContext();
  const navigate = useNavigate();

  const fetchAndSetUser = useCallback(async () => {
    const token = await getAccessTokenSilently();
    const email = user?.email || "";
    const fetchedUser = await getUserByEmailAsync(email, token);

    if (!fetchedUser) {
      return;
    }

    setUserContext(fetchedUser);
  }, [user?.email, getAccessTokenSilently, navigate, setUserContext]);

  useEffect(() => {
    if (!isLoading) {
      if (!isAuthenticated) {
        navigate("/");
      } else if (!userContext) {
        fetchAndSetUser();
      }
    }
  }, [isAuthenticated, userContext, fetchAndSetUser, navigate]);

  return <>{userContext && <Outlet />} </>;
};

export default Layout;
