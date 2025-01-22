import { useAuth0 } from "@auth0/auth0-react";
import Header from "../components/Header";
import Container from "@mui/material/Container";
import Typography from "@mui/material/Typography";
import Profile from "../components/auth/Profile";

function dashboard() {
  const { isAuthenticated, user } = useAuth0();
  console.log("isAuthenticated in dashboard", isAuthenticated);
  return (
    <>
      {isAuthenticated && user && (
        <>
          <Header />
          <Profile />
          <Container
            sx={{
              marginTop: 20,
              alignItems: "center",
              textAlign: "center",
              display: "flex",
              flexDirection: "column",
              justifyContent: "center",
            }}
          >
            <Typography variant="h4" gutterBottom>
              Bonjour, {user.name}!
            </Typography>
            <Typography variant="body1">
              Bienvenue dans votre tableau de bord. Vous pouvez commencer à
              gérer vos quittances et vos données.
            </Typography>
          </Container>
        </>
      )}
    </>
  );
}

export default dashboard;
