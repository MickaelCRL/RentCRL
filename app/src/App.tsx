import { useAuth0 } from "@auth0/auth0-react";
import { Box, Container, Typography } from "@mui/material";
import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import LogoutButton from "./components/auth/LoginButton";
import logo from "./static/img/logo.svg";

function App() {
  const { isAuthenticated } = useAuth0();
  const navigate = useNavigate();

  useEffect(() => {
    if (isAuthenticated) {
      navigate("/registration");
    } else {
      console.log("Not authenticated");
    }
  }, [isAuthenticated, navigate]);

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
        <LogoutButton />
      </Container>
    </>
  );
}

export default App;
