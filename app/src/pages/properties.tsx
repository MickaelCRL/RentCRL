import { useAuth0 } from "@auth0/auth0-react";
import { useBreadcrumbContext } from "../contexts/BreadcrumbContext";
import { useEffect } from "react";
import Header from "../components/Header";
import DashboardLayout from "../components/dashboard/DashboardLayout";
import { Box } from "@mui/material";
import BreadcrumbsNav from "../components/ui/Breadcrumbs";

function Properties() {
  const { isAuthenticated, user } = useAuth0();
  const { breadcrumbs, setBreadcrumbs } = useBreadcrumbContext();

  useEffect(() => {
    setBreadcrumbs([{ label: "Tableau de bord" }, { label: "Mes propriétés" }]);
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
          </DashboardLayout>
        </>
      )}
    </>
  );
}

export default Properties;
