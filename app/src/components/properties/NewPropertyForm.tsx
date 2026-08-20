import {
  Box,
  Button,
  TextField,
  Typography,
  Paper,
  CircularProgress,
} from "@mui/material";
import { useState } from "react";
import Property from "../../model/Property";
import { useNavigate } from "react-router-dom";
import Address from "../../model/Address";
import { createPropertyAsync } from "../../services/properties/propertyServices";
import { useUserContext } from "../../contexts/UserContext";

function NewPropertyForm() {
  const [property, setProperty] = useState<Partial<Property>>({
    name: "",
    surface: 0,
  });
  const [address, setAddress] = useState<Address>({
    line1: "",
    line2: "",
    postalCode: "",
    city: "",
    country: "France",
  });
  const [isSubmitting, setIsSubmitting] = useState(false);

  const { userContext } = useUserContext();
  const navigate = useNavigate();

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setProperty((prev) => ({ ...prev, [name]: value }));
  };

  const handleAddressChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setAddress((prev) => ({ ...prev, [name]: value }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setIsSubmitting(true);

    try {
      const newProperty = {
        ...property,
        surface: Number(property.surface),
        address: address,
      } as Property;

      const ownerId = userContext?.id || "";
      await createPropertyAsync(ownerId, newProperty);
      navigate("/properties");
    } catch (error) {
      console.error("Erreur lors de l'ajout", error);
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
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
          label="Nom de la propriété (ex: Studio 15e)"
          name="name"
          value={property.name}
          onChange={handleChange}
          required
        />

        <Typography variant="subtitle2" color="text.secondary" sx={{ mt: 1 }}>
          Localisation
        </Typography>
        <TextField
          label="Adresse"
          name="line1"
          value={address.line1}
          onChange={handleAddressChange}
          required
        />
        <TextField
          label="Complément d'adresse"
          name="line2"
          value={address.line2}
          onChange={handleAddressChange}
        />
        <Box sx={{ display: "flex", gap: 2 }}>
          <TextField
            sx={{ flex: 1 }}
            label="Code postal"
            name="postalCode"
            value={address.postalCode}
            onChange={handleAddressChange}
            required
          />
          <TextField
            sx={{ flex: 2 }}
            label="Ville"
            name="city"
            value={address.city}
            onChange={handleAddressChange}
            required
          />
        </Box>
        <TextField
          label="Pays"
          name="country"
          value={address.country}
          onChange={handleAddressChange}
          required
        />

        <Typography variant="subtitle2" color="text.secondary" sx={{ mt: 1 }}>
          Détails
        </Typography>
        <TextField
          label="Surface (m²)"
          name="surface"
          type="number"
          value={property.surface || ""}
          onChange={handleChange}
          required
        />

        <Box mt={3} textAlign="right">
          <Button
            type="submit"
            variant="contained"
            disabled={isSubmitting}
            sx={{ backgroundColor: "#1A237E", textTransform: "none" }}
          >
            {isSubmitting ? (
              <CircularProgress size={24} />
            ) : (
              "Ajouter la propriété"
            )}
          </Button>
        </Box>
      </Box>
    </Paper>
  );
}

export default NewPropertyForm;
