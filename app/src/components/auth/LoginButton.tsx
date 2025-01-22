import { useAuth0 } from "@auth0/auth0-react";
import Button from "@mui/material/Button";

const LoginButton = () => {
  const { loginWithRedirect } = useAuth0();

  return (
    <Button
      variant="contained"
      color="primary"
      onClick={() => loginWithRedirect()}
      sx={{ padding: "12px 24px", fontSize: "18px", minWidth: "150px" }}
    >
      Log In
    </Button>
  );
};

export default LoginButton;
