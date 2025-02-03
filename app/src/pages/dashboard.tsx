import { useAuth0 } from "@auth0/auth0-react";
import { Button, Container, Typography } from "@mui/material";
import Header from "../components/Header";

function Dashboard() {
  const { isAuthenticated, user } = useAuth0();

  return (
    <>
      {isAuthenticated && user && (
        <>
          <Header />
          <Container
            sx={{
              width: "100%",
              marginTop: 20,
              alignItems: "center",
              textAlign: "center",
              display: "flex",
              flexDirection: "column",
              justifyContent: "center",
            }}
          >
            <Typography variant="h4" gutterBottom sx={{ width: "100%" }}>
              Bonjour, {user.name}!
            </Typography>
            <Typography variant="body1" sx={{ width: "100%" }}>
              Bienvenue dans votre tableau de bord. Vous pouvez commencer à
              gérer vos quittances et vos données.
            </Typography>
          </Container>
        </>
      )}
    </>
  );
}

export default Dashboard;
