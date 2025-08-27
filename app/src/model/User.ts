interface User {
  id?: string;
  auth0Id?: string;
  lastname?: string;
  firstname?: string;
  email?: string;
  phoneNumber?: string;
  entityType?: "Owner" | "Tenant";
}

export default User;
