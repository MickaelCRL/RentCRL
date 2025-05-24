import { useAuth0 } from "@auth0/auth0-react";
import { useBreadcrumbContext } from "../contexts/BreadcrumbContext";
import { useEffect } from "react";
import Header from "../components/Header";
import DashboardLayout from "../components/dashboard/DashboardLayout";
import { Box, Typography } from "@mui/material";
import BreadcrumbsNav from "../components/ui/Breadcrumbs";
import AddPropertyButton from "../components/properties/AddPropertyButton";

function Properties() {
  const { isAuthenticated, user } = useAuth0();
  const { breadcrumbs, setBreadcrumbs } = useBreadcrumbContext();

  useEffect(() => {
    setBreadcrumbs([{ label: "Tableau de bord" }, { label: "Mes propriétés" }]);
  }, []);

  const properties = [
    {
      id: 1,
      name: "Maison à Lyon",
      rent: "850€",
      status: "Louée",
    },
    {
      id: 2,
      name: "Studio Paris 15e",
      rent: "1200€",
      status: "Disponible",
    },
  ];

  return (
    <>
      {isAuthenticated && user && (
        <>
          <Header />

          <DashboardLayout>
            <Box>
              <BreadcrumbsNav breadcrumbs={breadcrumbs} />
            </Box>
            <Box
              sx={{
                display: "flex",
                justifyContent: "space-between",
                mb: 3,
                mt: 2,
              }}
            >
              <Typography variant="h5" fontWeight="bold">
                Mes propriétés
              </Typography>
              <AddPropertyButton />
            </Box>

            <Box sx={{ display: "flex", flexWrap: "wrap", gap: 2 }}>
              {properties.map((property) => (
                <Box
                  key={property.id}
                  sx={{
                    width: "300px",
                    backgroundColor: "#fff",
                    p: 2,
                    borderRadius: 2,
                    boxShadow: 1,
                  }}
                >
                  <Typography variant="h6">{property.name}</Typography>
                  <Typography>Loyer : {property.rent}</Typography>
                  <Typography>Statut : {property.status}</Typography>
                </Box>
              ))}
            </Box>
          </DashboardLayout>
        </>
      )}
    </>
  );
}

export default Properties;
