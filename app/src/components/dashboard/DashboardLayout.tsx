import { useAuth0 } from "@auth0/auth0-react";
import HomeOutlinedIcon from "@mui/icons-material/HomeOutlined";
import HomeWorkIcon from "@mui/icons-material/HomeWork";
import LogoutIcon from "@mui/icons-material/Logout";
import {
  Box,
  List,
  ListItemButton,
  ListItemIcon,
  ListItemText,
  ListSubheader,
} from "@mui/material";
import { useNavigate } from "react-router-dom";

function DashboardLayout({ children }: { children: React.ReactNode }) {
  const { logout } = useAuth0();
  const navigate = useNavigate();
  const iconColor = "#1A237E";

  return (
    <Box sx={{ display: "flex", height: "100vh" }}>
      <Box
        sx={{
          width: 250,
          backgroundColor: "#EEEEEE",
          color: "#000",
          p: 2,
          marginTop: "60px",
        }}
      >
        <List
          subheader={
            <ListSubheader
              component="div"
              disableSticky
              sx={{
                backgroundColor: "#EEEEEE",
                fontWeight: "bold",
                fontSize: 16,
              }}
            >
              Tableau de bord
            </ListSubheader>
          }
        >
          <ListItemButton onClick={() => navigate("/dashboard")}>
            <ListItemIcon sx={{ color: "#fff" }}>
              <HomeOutlinedIcon sx={{ color: iconColor }} />
            </ListItemIcon>
            <ListItemText primary="Accueil" />
          </ListItemButton>

          <ListItemButton onClick={() => navigate("/properties")}>
            <ListItemIcon sx={{ color: "#fff" }}>
              <HomeWorkIcon sx={{ color: iconColor }} />
            </ListItemIcon>
            <ListItemText primary="Mes propriétés" />
          </ListItemButton>

          <ListItemButton
            onClick={() =>
              logout({ logoutParams: { returnTo: window.location.origin } })
            }
          >
            <ListItemIcon sx={{ color: "#fff" }}>
              <LogoutIcon sx={{ color: iconColor }} />
            </ListItemIcon>
            <ListItemText primary="Déconnexion" />
          </ListItemButton>
        </List>
      </Box>

      <Box
        sx={{
          flexGrow: 1,
          backgroundColor: "#f9f9f9",
          p: 4,
          marginTop: "40px",
          overflowY: "auto",
        }}
      >
        {children}
      </Box>
    </Box>
  );
}

export default DashboardLayout;
