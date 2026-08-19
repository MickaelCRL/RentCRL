import User from "./User";
import Address from "./Address";

interface Owner extends User {
  entityType?: "Owner";
  address?: Address;
}

export default Owner;
