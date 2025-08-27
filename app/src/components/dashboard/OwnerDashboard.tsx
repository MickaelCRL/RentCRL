import Header from "../Header";
import DashboardLayout from "./DashboardLayout";
import { Box } from "@mui/material";
import BreadcrumbsNav from "../ui/Breadcrumbs";
import BreadcrumbItem from "../../model/BreadcrumbItem";

function OwnerDashboard() {
  const breadcrumbs: BreadcrumbItem[] = [
    { label: "Tableau de bord" },
    { label: "Accueil" },
  ];

  return (
    <>
      <Header />
      <DashboardLayout>
        <Box>
          <BreadcrumbsNav breadcrumbs={breadcrumbs} />
        </Box>
      </DashboardLayout>
    </>
  );
}

export default OwnerDashboard;
