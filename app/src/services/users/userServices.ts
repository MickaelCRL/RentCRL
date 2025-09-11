import api from "../../api";

export async function getUserByEmailAsync(email?: string) {
  const emailEncode = encodeURIComponent(email!);
  const response = await api.get(`/users?email=${emailEncode}`);
  return response.data;
}
