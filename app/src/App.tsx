import { useAuth0 } from "@auth0/auth0-react";
import { Box, Container, Typography } from "@mui/material";
import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import LogoutButton from "./components/auth/LoginButton";

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
      <Box sx={{ marginBottom: 2, textAlign: "center" }}>
        <img
          src="./src/static/img/logo.png"
          alt="logo"
          style={{ maxWidth: "600px", maxHeight: "600px" }}
        />
      </Box>
      <Container
        maxWidth="sm"
        sx={{
          textAlign: "center",
          display: "flex",
          flexDirection: "column",
          alignItems: "center",
          justifyContent: "center",
        }}
      >
        <Typography variant="h4" sx={{ marginBottom: 2 }}>
          Bienvenue sur RentCRL
        </Typography>
        <Typography variant="body1" sx={{ marginBottom: 4 }}>
          Connectez-vous pour accéder à votre tableau de bord et gérer vos
          quittances.
        </Typography>
        <LogoutButton />
      </Container>
    </>
  );
}

export default App;
