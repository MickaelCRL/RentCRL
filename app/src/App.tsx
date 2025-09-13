import { useAuth0 } from "@auth0/auth0-react";
import { Box, Container, Typography } from "@mui/material";
import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import LoginButton from "./components/auth/LoginButton";
import SpinnerLoading from "./components/ui/SpinnerLoading";
import { useUserContext } from "./contexts/UserContext";
import useUser from "./services/users/useUser";
import logo from "./static/img/logo.svg";

function App() {
  const { isAuthenticated, isLoading, user } = useAuth0();
  const { setUserContext } = useUserContext();
  const email = user?.email || "";
  const { user: userSwr, isLoading: isLoadingSwr } = useUser(email);
  const navigate = useNavigate();

  useEffect(() => {
    if (!isLoading && isAuthenticated && email) {
      if (!isLoadingSwr && userSwr) {
        setUserContext(userSwr);
        navigate("/dashboard");
      } else if (!isLoadingSwr && !userSwr) {
        navigate("/select-role");
      }
    }
  }, [isAuthenticated, isLoading, isLoadingSwr, userSwr, email]);

  if (isLoading || (isAuthenticated && (!email || isLoadingSwr))) {
    return <SpinnerLoading />;
  }
  return (
    <>
      <Container
        sx={{
          width: "100%",
          textAlign: "center",
          display: "flex",
          flexDirection: "column",
          alignItems: "center",
          justifyContent: "center",
        }}
      >
        <Box sx={{ textAlign: "center" }}>
          <img
            src={logo}
            alt="logo"
            style={{ width: "100%", maxWidth: "600px", maxHeight: "600px" }}
          />
        </Box>
        <Typography variant="h4" sx={{ marginBottom: 1, width: "100%" }}>
          Bienvenue sur RentCRL
        </Typography>
        <Typography variant="body1" sx={{ marginBottom: 4, width: "100%" }}>
          Connectez-vous pour accéder à votre tableau de bord et gérer vos
          quittances.
        </Typography>
        <LoginButton />
      </Container>
    </>
  );
}

export default App;
