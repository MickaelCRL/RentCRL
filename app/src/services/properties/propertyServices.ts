import Property from "../../model/Property";
import { fetcherWithToken } from "../../utils/fetcher";

const ownerUrl = `${globalConfig.apiBaseUrl}/owners`;

export async function createPropertyAsync(
  ownerId: string,
  property: Property,
  token: string
) {
  const response = await fetcherWithToken(
    `${ownerUrl}/${ownerId}/properties`,
    token,
    "POST",
    property
  );
  return response;
}

export async function getPropertiesByOwnerIdAsync(
  ownerId: string,
  token: string
) {
  const response = await fetcherWithToken(
    `${ownerUrl}/${ownerId}/properties`,
    token,
    "GET"
  );
  return response;
}
