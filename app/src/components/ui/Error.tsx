import { Alert, Box } from "@mui/material";

function Error({
  message = "Une erreur est survenue lors du chargement des données.",
}) {
  return (
    <Box sx={{ width: "100%", mt: 2 }}>
      <Alert severity="error">{message}</Alert>
    </Box>
  );
}

export default Error;
