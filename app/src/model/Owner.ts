import User from "./User";

interface Owner extends User {
  entityType?: "Owner";
}

export default Owner;
