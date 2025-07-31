import Property from "../../model/Property";
import api from "../../api";

export async function createPropertyAsync(ownerId: string, property: Property) {
  const response = await api.post(`/owners/${ownerId}/properties`, property);
  return response.data;
}

export async function getPropertiesByOwnerIdAsync(ownerId: string) {
  const response = await api.get(`/owners/${ownerId}/properties`);
  return response.data;
}
