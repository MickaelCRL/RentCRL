import { Box, Button, TextField, Typography, Paper } from "@mui/material";
import { useEffect, useState } from "react";
import Property from "../../model/Property";
import Address from "../../model/Address";
import { useNavigate, useParams } from "react-router-dom";
import { updatePropertyAsync } from "../../services/properties/propertyServices";
import { useUserContext } from "../../contexts/UserContext";
import useProperty from "../../services/properties/useProperty";

function EditPropertyForm() {
  const [property, setProperty] = useState<Property>();
  const [address, setAddress] = useState<Address>();
  const { userContext } = useUserContext();
  const { id } = useParams();
  const { property: propertySwr } = useProperty(userContext?.id, id);

  const navigate = useNavigate();

  useEffect(() => {
    setProperty(propertySwr);
  }, [propertySwr]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setProperty({ ...property!, [e.target.name]: e.target.value });
  };

  const handleAddressChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    const newAddress: Address = { ...(property?.address ?? {}), [name]: value };
    setProperty((prev) => ({
      ...prev!,
      address: newAddress,
    }));
    setAddress(newAddress);
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!id) return;
    const updatedProperty: Property = {
      ...property!,
      address: address ?? property!.address,
    };
    const ownerId = userContext?.id || "";
    const propertyId = property?.id || "";
    await updatePropertyAsync(ownerId, propertyId, updatedProperty);
    navigate("/properties");
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
        Modifier la propriété
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
          value={property?.address?.line1 || ""}
          onChange={handleAddressChange}
          required
        />
        <TextField
          label="Adresse 2"
          name="line2"
          value={property?.address?.line2 || ""}
          onChange={handleAddressChange}
        />
        <TextField
          label="Ville"
          name="city"
          value={property?.address?.city || ""}
          onChange={handleAddressChange}
          required
        />
        <TextField
          label="Code postal"
          name="postalCode"
          value={property?.address?.postalCode || ""}
          onChange={handleAddressChange}
          required
        />
        <TextField
          label="Pays"
          name="country"
          value={property?.address?.country || ""}
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
            Enregistrer les modifications
          </Button>
        </Box>
      </Box>
    </Paper>
  );
}

export default EditPropertyForm;
