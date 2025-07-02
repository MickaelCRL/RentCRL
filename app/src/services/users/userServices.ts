import { fetcherWithToken } from "../../utils/fetcher";

const userUrl = `${globalConfig.apiBaseUrl}/users`;

export async function getUserByEmailAsync(email: string, token: string) {
  const emailEncode = encodeURIComponent(email);
  const response = await fetcherWithToken(
    `${userUrl}?email=${emailEncode}`,
    token,
    "GET"
  );
  return response;
}
