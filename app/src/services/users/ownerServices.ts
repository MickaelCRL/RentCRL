import Owner from "../../model/Owner";
import { fetcherWithToken } from "../../utils/fetcher";

const ownerUrl = `${globalConfig.apiBaseUrl}/owners`;

export async function createOwnerAsync(owner: Owner, token: string) {
  const response = await fetcherWithToken(ownerUrl, token, "POST", owner);
  return response;
}
