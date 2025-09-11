import { useCallback, useEffect } from "react";
import { Outlet, useNavigate } from "react-router-dom";
import { useUserContext } from "../contexts/UserContext";
import { useAuth0 } from "@auth0/auth0-react";
import { getUserByEmailAsync } from "../services/users/userServices";

const Layout = () => {
  const { user, isLoading, isAuthenticated } = useAuth0();
  const { userContext, setUserContext } = useUserContext();
  const navigate = useNavigate();

  const fetchAndSetUser = useCallback(async () => {
    const email = user?.email;
    const fetchedUser = await getUserByEmailAsync(email);
    setUserContext(fetchedUser);
  }, [user?.email]);

  useEffect(() => {
    if (isLoading) return;

    if (!isAuthenticated) {
      navigate("/");
      return;
    }

    if (isAuthenticated && !userContext) {
      fetchAndSetUser();
    }
  }, [isAuthenticated, isLoading, fetchAndSetUser]);

  return (
    <>
      <div>
        {isAuthenticated} {userContext?.email} toto
      </div>
      {userContext && <Outlet />}{" "}
    </>
  );
};

export default Layout;
