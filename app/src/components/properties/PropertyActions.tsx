import DeleteOutlineIcon from "@mui/icons-material/DeleteOutline";
import EditOutlinedIcon from "@mui/icons-material/EditOutlined";
import InfoOutlinedIcon from "@mui/icons-material/InfoOutlined";
import { Box, IconButton, Tooltip } from "@mui/material";
import { deletePropertyByIdAsync } from "../../services/properties/propertyServices";
import { useState } from "react";
import { DeleteConfirmationDialog } from "../ui/DeleteConfirmationDialog";
import { useUserContext } from "../../contexts/UserContext";
import { useNavigate } from "react-router";

interface PropertyActionsProps {
  propertyId: string;
  onDeleted: () => void;
}

export default function PropertyActions({
  propertyId,
  onDeleted,
}: PropertyActionsProps) {
  const [openDialog, setOpenDialog] = useState(false);
  const { userContext } = useUserContext();
  const navigate = useNavigate();

  const handleDelete = async () => {
    const ownerId = userContext?.id || "";
    await deletePropertyByIdAsync(ownerId, propertyId);
    onDeleted();
  };

  return (
    <Box sx={{ display: "flex", justifyContent: "flex-end", gap: 0.5 }}>
      <Tooltip title="Détails">
        <IconButton
          size="small"
          onClick={() => navigate(`/properties/${propertyId}`)}
        >
          <InfoOutlinedIcon fontSize="small" />
        </IconButton>
      </Tooltip>
      <Tooltip title="Modifier">
        <IconButton
          size="small"
          onClick={() => navigate(`/properties/${propertyId}/edit`)}
        >
          <EditOutlinedIcon fontSize="small" />
        </IconButton>
      </Tooltip>
      <Tooltip title="Supprimer">
        <IconButton
          size="small"
          onClick={() => setOpenDialog(true)}
          color="error"
        >
          <DeleteOutlineIcon fontSize="small" />
        </IconButton>
      </Tooltip>

      <DeleteConfirmationDialog
        open={openDialog}
        onClose={() => setOpenDialog(false)}
        onConfirm={handleDelete}
      />
    </Box>
  );
}
