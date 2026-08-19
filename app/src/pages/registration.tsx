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
import { useUserContext } from "../contexts/UserContext";
import { createOwnerAsync } from "../services/users/ownerServices";

const Registration = () => {
  const { user, isAuthenticated } = useAuth0();
  const [loading, setLoading] = useState(false);

  const [phoneNumber, setPhoneNumber] = useState("");
  const [phoneNumberError, setPhoneNumberError] = useState("");

  const [address, setAddress] = useState({
    line1: "",
    line2: "",
    postalCode: "",
    city: "",
    country: "France",
  });

  const { userContext, setUserContext } = useUserContext();
  const navigate = useNavigate();

  useEffect(() => {
    if (!isAuthenticated) {
      navigate("/");
    }

    if (isAuthenticated && !userContext?.entityType) {
      navigate("/select-role");
    }
  }, [user, isAuthenticated, navigate, userContext]);

  const handlePhoneChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const value = e.target.value;
    setPhoneNumber(value);
    if (!value.match(Regexes.phoneNumber)) {
      setPhoneNumberError("Numéro de téléphone invalide");
    } else {
      setPhoneNumberError("");
    }
  };

  const handleAddressChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setAddress((prev) => ({ ...prev, [name]: value }));
  };

  const handleSubmit = async () => {
    const owner: Owner = {
      auth0Id: user?.sub,
      lastname: user?.family_name,
      firstname: user?.given_name,
      email: user?.email,
      phoneNumber,
      entityType: "Owner",
      address: {
        line1: address.line1,
        line2: address.line2,
        postalCode: address.postalCode,
        city: address.city,
        country: address.country,
      },
    };

    setLoading(true);

    try {
      const response = await createOwnerAsync(owner);
      setUserContext(response);
      navigate("/dashboard");
    } catch (error) {
      console.error("Erreur lors de la création du profil :", error);
    } finally {
      setLoading(false);
    }
  };

  const isFormValid =
    phoneNumber !== "" &&
    phoneNumberError === "" &&
    address.line1.trim() !== "" &&
    address.postalCode.trim() !== "" &&
    address.city.trim() !== "" &&
    address.country.trim() !== "";

  return (
    <>
      <Header />
      {user && (
        <Container maxWidth="sm" sx={{ mt: 5, mb: 5 }}>
          <Card sx={{ p: 3, boxShadow: 3, marginTop: 10 }}>
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
                <Typography
                  variant="subtitle1"
                  color="text.secondary"
                  sx={{ mt: 1 }}
                >
                  Informations personnelles
                </Typography>

                <TextField
                  fullWidth
                  variant="outlined"
                  label="Nom"
                  name="lastname"
                  value={user.family_name || ""}
                  disabled
                  required
                />
                <TextField
                  fullWidth
                  variant="outlined"
                  label="Prénom"
                  name="firstname"
                  value={user.given_name || ""}
                  disabled
                  required
                />
                <TextField
                  fullWidth
                  variant="outlined"
                  label="Adresse mail"
                  name="email"
                  value={user.email || ""}
                  disabled
                  required
                />
                <TextField
                  fullWidth
                  variant="outlined"
                  label="Téléphone"
                  name="phone"
                  value={phoneNumber}
                  onChange={handlePhoneChange}
                  required
                  error={!!phoneNumberError}
                  helperText={phoneNumberError}
                />

                <Typography
                  variant="subtitle1"
                  color="text.secondary"
                  sx={{ mt: 2 }}
                >
                  Adresse légale (figurera sur les quittances)
                </Typography>

                <TextField
                  fullWidth
                  variant="outlined"
                  label="Adresse"
                  name="line1"
                  value={address.line1}
                  onChange={handleAddressChange}
                  required
                />
                <TextField
                  fullWidth
                  variant="outlined"
                  label="Complément d'adresse"
                  name="line2"
                  value={address.line2}
                  onChange={handleAddressChange}
                />
                <Box sx={{ display: "flex", gap: 2 }}>
                  <TextField
                    sx={{ flex: 1 }}
                    variant="outlined"
                    label="Code postal"
                    name="postalCode"
                    value={address.postalCode}
                    onChange={handleAddressChange}
                    required
                  />
                  <TextField
                    sx={{ flex: 2 }}
                    variant="outlined"
                    label="Ville"
                    name="city"
                    value={address.city}
                    onChange={handleAddressChange}
                    required
                  />
                </Box>
                <TextField
                  fullWidth
                  variant="outlined"
                  label="Pays"
                  name="country"
                  value={address.country}
                  onChange={handleAddressChange}
                  required
                />
              </Box>

              <Box sx={{ display: "flex", justifyContent: "center", mt: 4 }}>
                <Button
                  variant="contained"
                  color="primary"
                  size="large"
                  onClick={handleSubmit}
                  disabled={loading || !isFormValid}
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
