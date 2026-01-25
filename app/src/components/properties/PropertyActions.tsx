import DeleteOutlineIcon from "@mui/icons-material/DeleteOutline";
import EditOutlinedIcon from "@mui/icons-material/EditOutlined";
import InfoOutlinedIcon from "@mui/icons-material/InfoOutlined";
import { Box, IconButton } from "@mui/material";
import { deletePropertyByIdAsync } from "../../services/properties/propertyServices";
import { useState } from "react";
import { DeleteConfirmationDialog } from "../ui/DeleteConfirmationDialog";
import { useUserContext } from "../../contexts/UserContext";

interface PropertyActionsProps {
  propertyId?: string;
  onDeleted: () => void;
}

export default function PropertyActions({
  propertyId,
  onDeleted,
}: PropertyActionsProps) {
  const [openDialog, setOpenDialog] = useState(false);
  const { userContext } = useUserContext();

  const handleDelete = async () => {
    const ownerId = userContext?.id || "";
    await deletePropertyByIdAsync(ownerId, propertyId);
    onDeleted();
  };

  return (
    <Box
      sx={{
        display: "flex",
        justifyContent: "flex-end",
        mt: 2,
        mr: "auto",
        ml: "auto",
      }}
    >
      <IconButton aria-label="Détails">
        <InfoOutlinedIcon />
      </IconButton>
      <IconButton aria-label="Modifier">
        <EditOutlinedIcon />
      </IconButton>
      <IconButton aria-label="Supprimer" onClick={() => setOpenDialog(true)}>
        <DeleteOutlineIcon />
      </IconButton>
      <DeleteConfirmationDialog
        open={openDialog}
        onClose={() => setOpenDialog(false)}
        onConfirm={handleDelete}
      />
    </Box>
  );
}
