import {
  Box,
  Typography,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  Chip,
} from "@mui/material";
import Header from "../components/Header";
import DashboardLayout from "../components/dashboard/DashboardLayout";
import AddPropertyButton from "../components/properties/AddPropertyButton";
import PropertyActions from "../components/properties/PropertyActions";
import BreadcrumbsNav from "../components/ui/Breadcrumbs";
import { useUserContext } from "../contexts/UserContext";
import BreadcrumbItem from "../model/BreadcrumbItem";
import useProperties from "../services/properties/useProperties";
import SpinnerLoading from "../components/ui/SpinnerLoading";
import Error from "../components/ui/Error";

function Properties() {
  const { userContext } = useUserContext();
  const { properties, isLoading, isError, mutate } = useProperties(
    userContext?.id,
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
            alignItems: "center", // Aligné verticalement
            mb: 3,
            mt: 2,
          }}
        >
          <Typography variant="h5">Mes propriétés</Typography>
          <AddPropertyButton />
        </Box>

        {isLoading ? (
          <SpinnerLoading />
        ) : isError ? (
          <Error />
        ) : (properties?.length ?? 0) === 0 ? (
          <Paper sx={{ p: 4, textAlign: "center", borderRadius: 2 }}>
            <Typography variant="body1" color="text.secondary">
              Vous n'avez pas encore ajouté de propriété.
            </Typography>
          </Paper>
        ) : (
          <TableContainer
            component={Paper}
            sx={{ borderRadius: 2, boxShadow: 1 }}
          >
            <Table sx={{ minWidth: 650 }} aria-label="tableau des propriétés">
              <TableHead sx={{ backgroundColor: "#f5f5f5" }}>
                <TableRow>
                  <TableCell sx={{ fontWeight: "bold" }}>Nom du bien</TableCell>
                  <TableCell sx={{ fontWeight: "bold" }}>Emplacement</TableCell>
                  <TableCell sx={{ fontWeight: "bold" }}>Surface</TableCell>
                  <TableCell sx={{ fontWeight: "bold" }}>Statut</TableCell>
                  <TableCell align="right" sx={{ fontWeight: "bold" }}>
                    Actions
                  </TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {properties!.map((property) => (
                  <TableRow
                    key={property.id}
                    sx={{
                      "&:last-child td, &:last-child th": { border: 0 },
                      "&:hover": { backgroundColor: "#fafafa" },
                    }}
                  >
                    <TableCell component="th" scope="row">
                      <Typography variant="subtitle2">
                        {property.name}
                      </Typography>
                    </TableCell>
                    <TableCell>
                      {property.address?.city} ({property.address?.postalCode})
                    </TableCell>
                    <TableCell>{property.surface} m²</TableCell>
                    <TableCell>
                      {/* Affichage d'un Chip pour le statut (ex: Loué / Vacant) */}
                      <Chip
                        label={property.status || "Vacant"}
                        color={
                          property.status === "Loué" ? "success" : "default"
                        }
                        size="small"
                      />
                    </TableCell>
                    <TableCell align="right">
                      <PropertyActions
                        propertyId={property.id || ""}
                        onDeleted={mutate}
                      />
                    </TableCell>
                  </TableRow>
                ))}
              </TableBody>
            </Table>
          </TableContainer>
        )}
      </DashboardLayout>
    </>
  );
}

export default Properties;
