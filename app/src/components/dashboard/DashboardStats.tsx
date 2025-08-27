import { Box, Typography, Paper, Stack } from "@mui/material";
import EuroIcon from "@mui/icons-material/Euro";
import HomeIcon from "@mui/icons-material/Home";
import MailIcon from "@mui/icons-material/Mail";
import AccessTimeIcon from "@mui/icons-material/AccessTime";

const stats = [
  { label: "Biens loués", value: 3, icon: <HomeIcon fontSize="large" /> },
  {
    label: "Revenus du mois",
    value: "3 200 €",
    icon: <EuroIcon fontSize="large" />,
  },
  {
    label: "Retards",
    value: 1,
    icon: <AccessTimeIcon fontSize="large" color="warning" />,
  },
  {
    label: "Quittances envoyées",
    value: 4,
    icon: <MailIcon fontSize="large" />,
  },
];

export default function DashboardStats() {
  return (
    <Stack
      direction="row"
      spacing={2}
      useFlexGap
      flexWrap="wrap"
      justifyContent="space-between"
    >
      {stats.map((stat, index) => (
        <Paper
          key={index}
          elevation={3}
          sx={{
            p: 2,
            minWidth: 200,
            flex: 1,
            display: "flex",
            alignItems: "center",
            gap: 2,
          }}
        >
          {stat.icon}
          <Box>
            <Typography variant="h6">{stat.value}</Typography>
            <Typography color="text.secondary">{stat.label}</Typography>
          </Box>
        </Paper>
      ))}
    </Stack>
  );
}
