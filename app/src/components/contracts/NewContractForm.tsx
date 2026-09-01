import {
  Box,
  Button,
  MenuItem,
  Paper,
  TextField,
  Typography,
  CircularProgress,
} from "@mui/material";
import { useState } from "react";
import Contract from "../../model/Contract";
import Property from "../../model/Property";
import { useNavigate } from "react-router-dom";
import SuccessNotification from "../ui/SuccessNotification";
import { createContractAsync } from "../../services/contracts/contractServices";
import { useUserContext } from "../../contexts/UserContext";

interface NewContractFormProps {
  properties: Property[];
}

export default function NewContractForm({ properties }: NewContractFormProps) {
  const [contract, setContract] = useState<Partial<Contract>>({});
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [successNotificationDisplay, setSnackbarDisplay] = useState(false);
  const navigate = useNavigate();
  const { userContext } = useUserContext();

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setContract((prev) => ({ ...prev, [name]: value }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setIsSubmitting(true);

    try {
      const newContract: Contract = {
        propertyId: contract.propertyId,
        tenantEmail: contract.tenantEmail,
        rent: Number(contract.rent),
        deposit: Number(contract.deposit || 0),
        familyAllowanceFundAmount: Number(
          contract.familyAllowanceFundAmount || 0,
        ),
        startDate: contract.startDate,
        endDate: contract.endDate || undefined,
      };

      const ownerId = userContext?.id || "";
      await createContractAsync(ownerId, newContract);

      setSnackbarDisplay(true);
      setTimeout(() => navigate("/contracts"), 1500);
    } catch (error) {
      console.error(error);
    } finally {
      setIsSubmitting(false);
    }
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
          Ajouter un contrat
        </Typography>
        <Box
          component="form"
          onSubmit={handleSubmit}
          sx={{ display: "flex", flexDirection: "column", gap: 2 }}
        >
          <TextField
            select
            label="Propriété"
            name="propertyId"
            value={contract.propertyId || ""}
            onChange={handleChange}
            required
          >
            {properties?.map((p) => (
              <MenuItem key={p.id} value={p.id}>
                {p.name}
              </MenuItem>
            ))}
          </TextField>

          <TextField
            label="Email du locataire"
            name="tenantEmail"
            type="email"
            value={contract.tenantEmail || ""}
            onChange={handleChange}
            required
          />

          <Typography variant="subtitle2" color="text.secondary" sx={{ mt: 1 }}>
            Détails financiers
          </Typography>

          <Box sx={{ display: "flex", gap: 2 }}>
            <TextField
              sx={{ flex: 1 }}
              label="Montant du loyer (€)"
              name="rent"
              type="number"
              value={contract.rent || ""}
              onChange={handleChange}
              required
            />
            <TextField
              sx={{ flex: 1 }}
              label="Dépôt de garantie (€)"
              name="deposit"
              type="number"
              value={contract.deposit || ""}
              onChange={handleChange}
            />
          </Box>

          <TextField
            label="Montant versé par la CAF (€)"
            name="familyAllowanceFundAmount"
            type="number"
            value={contract.familyAllowanceFundAmount || ""}
            onChange={handleChange}
          />

          <Typography variant="subtitle2" color="text.secondary" sx={{ mt: 1 }}>
            Durée du bail
          </Typography>

          <Box sx={{ display: "flex", gap: 2 }}>
            <TextField
              sx={{ flex: 1 }}
              label="Début du bail"
              name="startDate"
              type="date"
              value={contract.startDate || ""}
              onChange={handleChange}
              required
              slotProps={{ inputLabel: { shrink: true } }}
            />
            <TextField
              sx={{ flex: 1 }}
              label="Fin du bail (Optionnel)"
              name="endDate"
              type="date"
              value={contract.endDate || ""}
              onChange={handleChange}
              slotProps={{
                inputLabel: { shrink: true },
                htmlInput: { min: contract.startDate || undefined },
              }}
            />
          </Box>

          <Box mt={3} textAlign="right">
            <Button
              type="submit"
              variant="contained"
              disabled={isSubmitting}
              sx={{ backgroundColor: "#1A237E", textTransform: "none" }}
            >
              {isSubmitting ? (
                <CircularProgress size={24} color="inherit" />
              ) : (
                "Ajouter le contrat"
              )}
            </Button>
          </Box>
        </Box>
      </Paper>
      <SuccessNotification
        open={successNotificationDisplay}
        message="Contrat créé avec succès !"
        onClose={() => setSnackbarDisplay(false)}
      />
    </>
  );
}
