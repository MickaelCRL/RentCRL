import { Box } from "@mui/material";
import { useEffect } from "react";
import DashboardLayout from "../../components/dashboard/DashboardLayout";
import Header from "../../components/Header";
import NewPropertyForm from "../../components/properties/NewPropertyForm";
import BreadcrumbsNav from "../../components/ui/Breadcrumbs";
import { useBreadcrumbContext } from "../../contexts/BreadcrumbContext";

function NewPropertyPage() {
  const { breadcrumbs, setBreadcrumbs } = useBreadcrumbContext();

  useEffect(() => {
    setBreadcrumbs([
      { label: "Tableau de bord" },
      { label: "Mes propriétés", href: "/properties" },
      { label: "Ajouter une propriété" },
    ]);
  }, []);

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
