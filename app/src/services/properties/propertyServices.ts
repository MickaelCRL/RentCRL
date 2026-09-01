import Property from "../../model/Property";
import api from "../../api";

export async function createPropertyAsync(ownerId: string, property: Property) {
  const response = await api.post(`/owners/${ownerId}/properties`, property);
  return response.data;
}

export async function getPropertiesByOwnerIdAsync(ownerId?: string) {
  const response = await api.get(`/owners/${ownerId}/properties`);
  return response.data;
}

export async function deletePropertyByIdAsync(
  ownerId: string,
  propertyId: string,
) {
  const response = await api.delete(
    `/owners/${ownerId}/properties/${propertyId}`,
  );
  return response.data;
}

export async function getPropertyByIdAsync(
  ownerId: string,
  propertyId: string,
) {
  const response = await api.get(`/owners/${ownerId}/properties/${propertyId}`);
  return response.data;
}

export async function updatePropertyAsync(ownerId: string, property: Property) {
  const response = await api.patch(
    `/owners/${ownerId}/properties/${property.id}`,
    property,
  );
  return response.data;
}
