import { AppBar, Box, Toolbar } from "@mui/material";
import logoInline from "../static/img/logo-inline.png";
import Profile from "./auth/Profile";

function Header() {
  return (
    <>
      <AppBar
        elevation={0}
        sx={{ backgroundColor: "#ffffff", color: "#000000", width: "100%" }}
      >
        <Toolbar sx={{ justifyContent: "space-between", padding: "0 20px" }}>
          <Box sx={{ display: "flex", alignItems: "center" }}>
            <img
              src={logoInline}
              alt="RentCRL Logo"
              width={200}
              style={{ marginRight: 10 }}
            />
          </Box>
          <Profile />
        </Toolbar>
      </AppBar>
    </>
  );
}

export default Header;
