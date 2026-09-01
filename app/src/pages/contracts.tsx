import { Box, Typography, Paper } from "@mui/material";
import Header from "../components/Header";
import ContractsTable from "../components/contracts/ContractsTable";
import DashboardLayout from "../components/dashboard/DashboardLayout";
import BreadcrumbsNav from "../components/ui/Breadcrumbs";
import CreateContractButton from "../components/contracts/CreateContractButton";
import BreadcrumbItem from "../model/BreadcrumbItem";
import { useUserContext } from "../contexts/UserContext";
import useContracts from "../services/contracts/useContracts";
import useProperties from "../services/properties/useProperties";
import SpinnerLoading from "../components/ui/SpinnerLoading";
import Error from "../components/ui/Error";

function Contracts() {
  const { userContext } = useUserContext();

  const {
    contracts,
    isLoading: isContractsLoading,
    isError: isContractsError,
  } = useContracts(userContext?.id);
  const { properties, isLoading: isPropertiesLoading } = useProperties(
    userContext?.id,
  );

  const breadcrumbs: BreadcrumbItem[] = [
    { label: "Tableau de bord" },
    { label: "Mes contrats" },
  ];

  const isLoading = isContractsLoading || isPropertiesLoading;
  const isError = isContractsError;

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
            alignItems: "center",
            mb: 3,
            mt: 2,
          }}
        >
          <Typography variant="h5">Mes contrats</Typography>
          <CreateContractButton />
        </Box>

        <Box mt={4}>
          {isLoading ? (
            <SpinnerLoading />
          ) : isError ? (
            <Error />
          ) : !contracts || contracts.length === 0 ? (
            // L'état vide professionnel
            <Paper
              sx={{
                p: 6,
                textAlign: "center",
                borderRadius: 2,
                backgroundColor: "#fafafa",
                border: "1px dashed #ccc",
              }}
            >
              <Typography variant="h6" color="text.secondary" gutterBottom>
                Aucun contrat
              </Typography>
              <Typography variant="body2" color="text.secondary">
                Vous n'avez pas encore créé de bail. Cliquez sur "Créer un
                contrat" pour inviter un locataire.
              </Typography>
            </Paper>
          ) : (
            <ContractsTable
              contracts={contracts}
              properties={properties || []}
            />
          )}
        </Box>
      </DashboardLayout>
    </>
  );
}

export default Contracts;
