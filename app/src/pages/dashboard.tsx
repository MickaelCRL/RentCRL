import LogoutButton from "../components/auth/LogoutButton";
import { useAuth0 } from "@auth0/auth0-react";

function dashboard() {
  const { isAuthenticated, user } = useAuth0();
  return (
    <>
      {isAuthenticated && (
        <>
          <h1>Bienvenue, {user?.name}!</h1>
          <p>Email: {user?.email}</p>
        </>
      )}

      <LogoutButton />
    </>
  );
}

export default dashboard;
