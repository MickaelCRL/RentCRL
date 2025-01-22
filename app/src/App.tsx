import { useAuth0 } from "@auth0/auth0-react";
import LoginButton from "./components/auth/LoginButton";
import { useEffect } from "react";
import { useNavigate } from "react-router-dom";

function App() {
  const { isAuthenticated } = useAuth0();
  const navigate = useNavigate();

  useEffect(() => {
    if (isAuthenticated) {
      navigate("/dashboard");
    } else {
      console.log("Not authenticated");
    }
  }, [isAuthenticated, navigate]);

  return (
    <>
      <h1>Bienvenue sur RentCRL</h1>
      <LoginButton />
    </>
  );
}

export default App;
