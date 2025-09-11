import { useAuth0 } from "@auth0/auth0-react";
import { Box, Container, Typography } from "@mui/material";
import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import LoginButton from "./components/auth/LoginButton";
import logo from "./static/img/logo.svg";
import SpinnerLoading from "./components/ui/SpinnerLoading";
import { useUserContext } from "./contexts/UserContext";
import { getUserByEmailAsync } from "./services/users/userServices";

function App() {
  const { isAuthenticated, isLoading, user } = useAuth0();
  const { setUserContext } = useUserContext();
  const navigate = useNavigate();

  useEffect(() => {
    const checkUser = async () => {
      if (!isLoading && isAuthenticated) {
        const email = user?.email;
        const response = await getUserByEmailAsync(email);
        if (response) {
          setUserContext(response);
          navigate("/dashboard");
        } else {
          navigate("/select-role");
        }
      }
    };
    checkUser();
  }, [isAuthenticated, isLoading, navigate]);

  if (isLoading || isAuthenticated) {
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
