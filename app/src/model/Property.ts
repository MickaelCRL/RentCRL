import Address from "./Address";

interface Property {
  id?: string;
  name?: string;
  address?: Address;
  surface?: number;
  status?: string;
}

export default Property;
