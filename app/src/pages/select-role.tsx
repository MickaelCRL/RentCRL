import { useEffect, useState } from "react";
import {
  Button,
  Container,
  FormControl,
  FormControlLabel,
  Radio,
  RadioGroup,
  Typography,
  Alert,
  Paper,
} from "@mui/material";
import Header from "../components/Header";
import { useNavigate } from "react-router-dom";
import { useAuth0 } from "@auth0/auth0-react";
import SpinnerLoading from "../components/ui/SpinnerLoading";
import { useUserContext } from "../contexts/UserContext";

function SelectRolePage() {
  const [role, setRole] = useState("");
  const [message, setMessage] = useState("");
  const { isAuthenticated, isLoading } = useAuth0();
  const { setUserContext } = useUserContext();
  const navigate = useNavigate();

  useEffect(() => {
    if (!isLoading && !isAuthenticated) {
      navigate("/");
    }
  }, [isAuthenticated, isLoading, navigate]);

  const handleSelect = async () => {
    if (!role) return;

    if (role === "Tenant") {
      setMessage("Veuillez attendre l'invitation de votre propriétaire.");
    }

    if (role === "Owner") {
      setUserContext({
        entityType: "Owner",
      });
      navigate("/registration");
    }
  };

  if (isLoading) return <SpinnerLoading />;

  return (
    <>
      {isAuthenticated && (
        <>
          <Header />
          <Container maxWidth="sm" sx={{ mt: 20 }}>
            <Paper elevation={3} sx={{ p: 4, borderRadius: 3 }}>
              <Typography variant="h5" gutterBottom>
                Choisissez votre rôle
              </Typography>

              <FormControl component="fieldset" sx={{ my: 2 }}>
                <RadioGroup
                  value={role}
                  onChange={(e) => setRole(e.target.value)}
                >
                  <FormControlLabel
                    value="Owner"
                    control={<Radio />}
                    label="Je suis propriétaire"
                  />
                  <FormControlLabel
                    value="Tenant"
                    control={<Radio />}
                    label="Je suis locataire"
                  />
                </RadioGroup>
              </FormControl>

              {message && (
                <Alert severity="info" sx={{ mb: 2 }}>
                  {message}
                </Alert>
              )}

              <Button
                variant="contained"
                color="primary"
                onClick={handleSelect}
                disabled={!role}
                fullWidth
              >
                Valider
              </Button>
            </Paper>
          </Container>
        </>
      )}
    </>
  );
}

export default SelectRolePage;
