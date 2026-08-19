import { useAuth0 } from "@auth0/auth0-react";
import { Avatar, Popover, Typography } from "@mui/material";
import { useState } from "react";
import LogoutButton from "./LogoutButton";
import { useUserContext } from "../../contexts/UserContext";

function Profile() {
  const { user } = useAuth0();
  const { userContext } = useUserContext();
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);

  const open = Boolean(anchorEl);
  const id = open ? "simple-popover" : undefined;

  const handleClick = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

  const displayName =
    userContext?.firstName && userContext?.lastName
      ? `${userContext.firstName} ${userContext.lastName}`
      : user?.name;

  return (
    <>
      {user && (
        <>
          <Avatar
            src={user.picture}
            alt="user picture"
            sx={{ width: 56, height: 56, cursor: "pointer" }}
            onClick={handleClick}
          />
          <Popover
            id={id}
            open={open}
            anchorEl={anchorEl}
            onClose={handleClose}
            anchorOrigin={{
              vertical: "bottom",
              horizontal: "center",
            }}
            transformOrigin={{
              vertical: "top",
              horizontal: "center",
            }}
            sx={{ mt: 1 }}
          >
            <Typography component="div" sx={{ p: 2, textAlign: "center" }}>
              <Avatar
                src={user.picture}
                alt="user picture"
                sx={{ width: 100, height: 100, margin: "auto" }}
              />
              <Typography variant="h6">{displayName}</Typography>
              <Typography variant="body1">{user.email}</Typography>
              <LogoutButton />
            </Typography>
          </Popover>
        </>
      )}
    </>
  );
}

export default Profile;
