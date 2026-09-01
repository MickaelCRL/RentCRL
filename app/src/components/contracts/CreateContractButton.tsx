import { Add } from "@mui/icons-material";
import { Button } from "@mui/material";
import { useNavigate } from "react-router-dom";

function CreateContractButton() {
  const navigate = useNavigate();
  return (
    <Button
      variant="contained"
      startIcon={<Add />}
      onClick={() => navigate("/contracts/new")}
      sx={{ backgroundColor: "#1A237E" }}
    >
      Créer un contrat
    </Button>
  );
}

export default CreateContractButton;
