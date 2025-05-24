import { Box, Button, TextField, Typography, Paper } from "@mui/material";
import { useState } from "react";
import Property from "../../model/Property";
import { useNavigate } from "react-router-dom";
import SuccessNotification from "../ui/SuccessNotification";

function NewPropertyForm() {
  const [property, setProperty] = useState<Property>();
  const [snackbarOpen, setSnackbarOpen] = useState(false);
  const navigate = useNavigate();

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setProperty({ ...property, [e.target.name]: e.target.value });
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    setTimeout(() => {
      console.log("Property saved:", property);
      setSnackbarOpen(true);
      setTimeout(() => {
        navigate("/properties");
      }, 1500);
    }, 500);
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
            value={property?.name}
            onChange={handleChange}
            required
          />
          <TextField
            label="Adresse"
            name="address"
            value={property?.address}
            onChange={handleChange}
            required
          />
          <TextField
            label="Ville"
            name="city"
            value={property?.city}
            onChange={handleChange}
            required
          />
          <TextField
            label="Code postal"
            name="postalCode"
            value={property?.postalCode}
            onChange={handleChange}
            required
          />
          <TextField
            label="Surface (m²)"
            name="surface"
            type="number"
            value={property?.surface}
            onChange={handleChange}
            required
          />
          <TextField
            label="Prix du loyer (€)"
            name="rentPrice"
            type="number"
            value={property?.rentPrice}
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
        open={snackbarOpen}
        message="Propriété ajoutée avec succès !"
        onClose={() => setSnackbarOpen(false)}
      />
    </>
  );
}

export default NewPropertyForm;
