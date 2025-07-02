import { useAuth0 } from "@auth0/auth0-react";
import { Box, Typography } from "@mui/material";
import { useEffect, useState } from "react";
import Header from "../components/Header";
import DashboardLayout from "../components/dashboard/DashboardLayout";
import AddPropertyButton from "../components/properties/AddPropertyButton";
import BreadcrumbsNav from "../components/ui/Breadcrumbs";
import { useUserContext } from "../contexts/UserContext";
import BreadcrumbItem from "../model/BreadcrumbItem";
import Property from "../model/Property";
import { getPropertiesByOwnerIdAsync } from "../services/properties/propertyServices";

function Properties() {
  const { userContext } = useUserContext();
  const ownerId = userContext?.id || "";
  const { getAccessTokenSilently } = useAuth0();
  const [properties, setProperties] = useState<Property[]>([]);

  const breadcrumbs: BreadcrumbItem[] = [
    { label: "Tableau de bord" },
    { label: "Mes propriétés" },
  ];

  useEffect(() => {
    const fetchProperties = async () => {
      const token = await getAccessTokenSilently();
      const res = await getPropertiesByOwnerIdAsync(ownerId, token);
      setProperties(res);
    };
    if (ownerId) {
      fetchProperties();
    }
  }, [ownerId, getAccessTokenSilently]);

  return (
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
          <Typography variant="h5">Mes propriétés</Typography>
          <AddPropertyButton />
        </Box>

        <Box sx={{ display: "flex", flexWrap: "wrap", gap: 2 }}>
          {(properties ?? []).map((property) => (
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
              <Typography>
                Adresse : {property.address?.line1}, {property.address?.city},{" "}
                {property.address?.postalCode}, {property.address?.country}
              </Typography>
              <Typography>Surface : {property.surface} m²</Typography>
              <Typography>Statut : {property.status}</Typography>
            </Box>
          ))}
        </Box>
      </DashboardLayout>
    </>
  );
}

export default Properties;
