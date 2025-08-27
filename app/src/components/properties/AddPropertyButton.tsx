import { Add } from "@mui/icons-material";
import { Button } from "@mui/material";
import { useNavigate } from "react-router-dom";

function AddProperty() {
  const navigate = useNavigate();
  return (
    <Button
      variant="contained"
      startIcon={<Add />}
      onClick={() => navigate("/properties/new")}
      sx={{ backgroundColor: "#1A237E" }}
    >
      Ajouter une propriété
    </Button>
  );
}

export default AddProperty;
