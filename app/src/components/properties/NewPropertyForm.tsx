import { Box, Button, TextField, Typography, Paper } from "@mui/material";
import { useState } from "react";
import Property from "../../model/Property";
import { useNavigate } from "react-router-dom";
import SuccessNotification from "../ui/SuccessNotification";
import Address from "../../model/Address";
import { createPropertyAsync } from "../../services/properties/propertyServices";
import { useUserContext } from "../../contexts/UserContext";

function NewPropertyForm() {
  const [property, setProperty] = useState<Property>();
  const [address, setAddress] = useState<Address>();
  const [successNotificationDisplay, setSnackbarDisplay] = useState(false);
  const { userContext } = useUserContext();
  const navigate = useNavigate();

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setProperty({ ...property, [e.target.name]: e.target.value });
  };

  const handleAddressChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setAddress({ ...address, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    const newProperty = { ...property, address: address };
    const ownerId = userContext?.id || "";
    await createPropertyAsync(ownerId, newProperty);
    navigate("/properties");
  };

  return (
    <>
      <Paper
        elevation={1}
        sx={{
          maxWidth: 600,
          mx: "auto",
          p: 4,
          backgroundColor: "#fff",
          borderRadius: 2,
          boxShadow: "0 0 8px rgba(0,0,0,0.05)",
          mt: 4,
        }}
      >
        <Typography variant="h5" color="#1A237E" mb={3}>
          Ajouter une propriété
        </Typography>
        <Box
          component="form"
          onSubmit={handleSubmit}
          sx={{ display: "flex", flexDirection: "column", gap: 2 }}
        >
          <TextField
            label="Nom"
            name="name"
            value={property?.name || ""}
            onChange={handleChange}
            required
          />
          <TextField
            label="Adresse"
            name="line1"
            value={address?.line1 || ""}
            onChange={handleAddressChange}
            required
          />
          <TextField
            label="Adresse 2"
            name="line2"
            value={address?.line2 || ""}
            onChange={handleAddressChange}
          />
          <TextField
            label="Ville"
            name="city"
            value={address?.city || ""}
            onChange={handleAddressChange}
            required
          />
          <TextField
            label="Code postal"
            name="postalCode"
            value={address?.postalCode || ""}
            onChange={handleAddressChange}
            required
          />
          <TextField
            label="Pays"
            name="country"
            value={address?.country || ""}
            onChange={handleAddressChange}
            required
          />
          <TextField
            label="Surface (m²)"
            name="surface"
            type="number"
            value={property?.surface || ""}
            onChange={handleChange}
            required
          />
          <Box mt={3} textAlign="right">
            <Button
              type="submit"
              variant="contained"
              sx={{ backgroundColor: "#1A237E", textTransform: "none" }}
            >
              Ajouter
            </Button>
          </Box>
        </Box>
      </Paper>
      <SuccessNotification
        open={successNotificationDisplay}
        message="Propriété ajoutée avec succès !"
        onClose={() => setSnackbarDisplay(false)}
      />
    </>
  );
}

export default NewPropertyForm;
