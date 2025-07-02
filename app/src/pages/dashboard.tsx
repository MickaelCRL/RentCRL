import OwnerDashboard from "../components/dashboard/OwnerDashboard";
import TenantDashboard from "../components/dashboard/TenantDashboard";
import { useUserContext } from "../contexts/UserContext";

function Dashboard() {
  const { userContext } = useUserContext();
  return (
    <>
      {userContext?.entityType === "Owner" ? (
        <OwnerDashboard />
      ) : userContext?.entityType === "Tenant" ? (
        <TenantDashboard />
      ) : null}
    </>
  );
}

export default Dashboard;
