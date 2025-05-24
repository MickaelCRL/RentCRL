import DashboardLayout from "../../components/dashboard/DashboardLayout";
import Header from "../../components/Header";
import NewPropertyForm from "../../components/properties/NewPropertyForm";
import { useAuth0 } from "@auth0/auth0-react";
import { useBreadcrumbContext } from "../../contexts/BreadcrumbContext";
import { useEffect } from "react";
import { Box } from "@mui/material";
import BreadcrumbsNav from "../../components/ui/Breadcrumbs";

function NewPropertyPage() {
  const { isAuthenticated, user } = useAuth0();
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
      {isAuthenticated && user && (
        <>
          <Header />
          <DashboardLayout>
            <Box>
              <BreadcrumbsNav breadcrumbs={breadcrumbs} />
            </Box>
            <NewPropertyForm />
          </DashboardLayout>
        </>
      )}
    </>
  );
}

export default NewPropertyPage;
