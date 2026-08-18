import { Box } from "@mui/material";
import DashboardLayout from "../../components/dashboard/DashboardLayout";
import Header from "../../components/Header";
import BreadcrumbsNav from "../../components/ui/Breadcrumbs";
import BreadcrumbItem from "../../model/BreadcrumbItem";
import EditPropertyForm from "../../components/properties/EditPropertyForm";

function EditPropertyPage() {
  const breadcrumbs: BreadcrumbItem[] = [
    { label: "Tableau de bord" },
    { label: "Mes propriétés", href: "/properties" },
    { label: "Edit" },
  ];

  return (
    <>
      <Header />
      <DashboardLayout>
        <Box>
          <BreadcrumbsNav breadcrumbs={breadcrumbs} />
        </Box>
        <EditPropertyForm />
      </DashboardLayout>
    </>
  );
}

export default EditPropertyPage;
