import { Snackbar, Alert } from "@mui/material";

interface SuccessNotificationProps {
  open: boolean;
  message: string;
  onClose?: () => void;
}

function SuccessNotification({
  open,
  message,
  onClose,
}: SuccessNotificationProps) {
  return (
    <Snackbar
      open={open}
      anchorOrigin={{ vertical: "top", horizontal: "center" }}
      autoHideDuration={3000}
      onClose={onClose}
    >
      <Alert severity="success" sx={{ width: "100%" }} onClose={onClose}>
        {message}
      </Alert>
    </Snackbar>
  );
}

export default SuccessNotification;
