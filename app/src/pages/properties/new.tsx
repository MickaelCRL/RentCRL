import { Box } from "@mui/material";
import DashboardLayout from "../../components/dashboard/DashboardLayout";
import Header from "../../components/Header";
import NewPropertyForm from "../../components/properties/NewPropertyForm";
import BreadcrumbsNav from "../../components/ui/Breadcrumbs";
import BreadcrumbItem from "../../model/BreadcrumbItem";

function NewPropertyPage() {
  const breadcrumbs: BreadcrumbItem[] = [
    { label: "Tableau de bord" },
    { label: "Mes propriétés", href: "/properties" },
    { label: "Ajouter une propriété" },
  ];

  return (
    <>
      <Header />
      <DashboardLayout>
        <Box>
          <BreadcrumbsNav breadcrumbs={breadcrumbs} />
        </Box>
        <NewPropertyForm />
      </DashboardLayout>
    </>
  );
}

export default NewPropertyPage;
