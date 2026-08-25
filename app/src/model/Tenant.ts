import User from "./User";

interface Tenant extends User {
  entityType?: "Tenant";
}

export default Tenant;
