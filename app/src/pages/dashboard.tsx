import Header from "../components/Header";
import DashboardLayout from "../components/dashboard/DashboardLayout";
import { Box } from "@mui/material";
import { useBreadcrumbContext } from "../contexts/BreadcrumbContext";
import { useEffect } from "react";
import BreadcrumbsNav from "../components/ui/Breadcrumbs";

function Dashboard() {
  const { breadcrumbs, setBreadcrumbs } = useBreadcrumbContext();

  useEffect(() => {
    setBreadcrumbs([{ label: "Tableau de bord" }, { label: "Accueil" }]);
  }, []);

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

export default Dashboard;
