import { useAuth0 } from "@auth0/auth0-react";
import { Button, Container, Typography } from "@mui/material";
import Header from "../components/Header";

function Dashboard() {
  const { isAuthenticated, user, getAccessTokenSilently } = useAuth0();

  const validateToken = async () => {
    const token = await getAccessTokenSilently();
    console.log("token", token);
    const res = await fetch("http://localhost:5047/users", {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    const data = await res.json();
    console.log("data", data);
  };
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

            <Button variant="contained" color="primary" onClick={validateToken}>
              Valider le token
            </Button>
          </Container>
        </>
      )}
    </>
  );
}

export default Dashboard;
