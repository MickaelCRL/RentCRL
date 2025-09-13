import { Box, Typography } from "@mui/material";
import Header from "../components/Header";
import DashboardLayout from "../components/dashboard/DashboardLayout";
import AddPropertyButton from "../components/properties/AddPropertyButton";
import PropertyActions from "../components/properties/PropertyActions";
import BreadcrumbsNav from "../components/ui/Breadcrumbs";
import { useUserContext } from "../contexts/UserContext";
import BreadcrumbItem from "../model/BreadcrumbItem";
import useProperty from "../services/properties/useProperty";
import SpinnerLoading from "../components/ui/SpinnerLoading";
import Error from "../components/ui/Error";

function Properties() {
  const { userContext } = useUserContext();
  const { properties, isLoading, isError, mutate } = useProperty(
    userContext?.id
  );

  const breadcrumbs: BreadcrumbItem[] = [
    { label: "Tableau de bord" },
    { label: "Mes propriétés" },
  ];

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
          {isLoading ? (
            <SpinnerLoading />
          ) : isError ? (
            <Error />
          ) : (
            (properties ?? []).map((property) => (
              <Box
                key={property.id}
                sx={{
                  width: "300px",
                  backgroundColor: "#fff",
                  p: 2,
                  borderRadius: 2,
                  boxShadow: 1,
                  display: "flex",
                  flexDirection: "column",
                  justifyContent: "space-between",
                }}
              >
                <Box>
                  <Typography variant="h6">{property.name}</Typography>
                  <Typography>
                    Adresse : {property.address?.line1},{" "}
                    {property.address?.city}, {property.address?.postalCode},{" "}
                    {property.address?.country}
                  </Typography>
                  <Typography>Surface : {property.surface} m²</Typography>
                  <Typography>Statut : {property.status}</Typography>
                </Box>
                <PropertyActions propertyId={property.id} onDeleted={mutate} />
              </Box>
            ))
          )}
        </Box>
      </DashboardLayout>
    </>
  );
}

export default Properties;
