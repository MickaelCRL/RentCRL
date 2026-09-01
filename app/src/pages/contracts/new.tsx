import { Box, CircularProgress, Typography } from "@mui/material";
import Header from "../../components/Header";
import NewContractForm from "../../components/contracts/NewContractForm";
import DashboardLayout from "../../components/dashboard/DashboardLayout";
import BreadcrumbsNav from "../../components/ui/Breadcrumbs";
import BreadcrumbItem from "../../model/BreadcrumbItem";
import useProperties from "../../services/properties/useProperties";
import { useUserContext } from "../../contexts/UserContext";

function NewContractPage() {
  const { userContext } = useUserContext();
  const { properties, isLoading } = useProperties(userContext?.id);

  const breadcrumbs: BreadcrumbItem[] = [
    { label: "Tableau de bord" },
    { label: "Mes contrats", href: "/contracts" },
    { label: "Créer un contrat" },
  ];

  return (
    <>
      <Header />
      <DashboardLayout>
        <Box>
          <BreadcrumbsNav breadcrumbs={breadcrumbs} />
        </Box>
        {isLoading ? (
          <CircularProgress sx={{ display: "block", mx: "auto", mt: 4 }} />
        ) : (properties?.length ?? 0) === 0 ? (
          <Typography variant="body1" textAlign="center" mt={4}>
            Vous devez d'abord ajouter une propriété avant de créer un contrat.
          </Typography>
        ) : (
          <NewContractForm properties={properties || []} />
        )}
      </DashboardLayout>
    </>
  );
}

export default NewContractPage;
