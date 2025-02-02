import { useEffect, useState } from "react";
import { useAuth0 } from "@auth0/auth0-react";
import { useNavigate } from "react-router-dom";
import {
  Container,
  Button,
  Typography,
  Box,
  TextField,
  CircularProgress,
  Card,
  CardContent,
} from "@mui/material";
import Header from "../components/Header";
import Owner from "../model/Owner";
import Regexes from "../model/Regexes";

const Registration = () => {
  const { user, getAccessTokenSilently } = useAuth0();
  console.log("user", user);
  const navigate = useNavigate();
  const [loading, setLoading] = useState(false);
  const [phone, setPhone] = useState("");
  const [phoneError, setPhoneError] = useState("");
  const [owner, setOwner] = useState<Owner>();

  const showIdToken = async () => {};

  useEffect(() => {
    if (user) {
      setOwner({
        auth0Id: user.sub,
        lastname: user.family_name,
        firstname: user.given_name,
        email: user.email,
        phoneNumber: "",
      });
    }
    showIdToken();
    console.log(user);
  }, [user]);

  const handlePhoneChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setPhone(e.target.value);

    if (!phone.match(Regexes.phoneNumber)) {
      setPhoneError("Numéro de téléphone invalide");
    } else {
      setPhoneError("");
      setOwner({ ...owner, phoneNumber: e.target.value });
    }
  };

  const handleSubmit = async () => {
    console.log("owner", owner);
    const token = await getAccessTokenSilently();

    setLoading(true);
    const res = await fetch(`http://localhost:5047/owners`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify({ ...owner }),
    });

    const data = await res.json();
    console.log("data", data);

    setLoading(false);
    // navigate("/dashboard");
  };

  return (
    <>
      <Header />
      {user && (
        <Container maxWidth="sm" sx={{ mt: 5 }}>
          <Card sx={{ p: 3, boxShadow: 3, marginTop: 15 }}>
            <CardContent>
              <Typography
                variant="h5"
                align="center"
                gutterBottom
                marginBottom={3}
              >
                Complétez votre profil
              </Typography>

              <Box sx={{ display: "flex", flexDirection: "column", gap: 2 }}>
                <TextField
                  fullWidth
                  variant="outlined"
                  label="Nom"
                  name="lastname"
                  value={user.family_name}
                  disabled
                  required
                />
                <TextField
                  fullWidth
                  variant="outlined"
                  label="Prénom"
                  name="firstname"
                  value={user.given_name}
                  disabled
                  required
                />
                <TextField
                  fullWidth
                  variant="outlined"
                  label="Adresse mail"
                  name="email"
                  value={user.email}
                  disabled
                  required
                />
                <TextField
                  fullWidth
                  variant="outlined"
                  label="Téléphone"
                  name="phone"
                  defaultValue={owner?.phoneNumber}
                  onChange={handlePhoneChange}
                  required
                  error={phoneError ? true : false}
                  helperText={phoneError}
                />
              </Box>

              <Box sx={{ display: "flex", justifyContent: "center", mt: 3 }}>
                <Button
                  variant="contained"
                  color="primary"
                  onClick={handleSubmit}
                  disabled={loading}
                >
                  {loading ? (
                    <CircularProgress size={24} color="inherit" />
                  ) : (
                    "Terminer"
                  )}
                </Button>
              </Box>
            </CardContent>
          </Card>
        </Container>
      )}
    </>
  );
};

export default Registration;
