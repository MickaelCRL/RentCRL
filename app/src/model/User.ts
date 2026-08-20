interface User {
  id?: string;
  auth0Id?: string;
  lastName?: string;
  firstName?: string;
  email?: string;
  phoneNumber?: string;
  entityType?: "Owner" | "Tenant";
}

export default User;
