import Owner from "../../model/Owner";
import api from "../../api";

export async function createOwnerAsync(owner: Owner) {
  const response = await api.post("/owners", owner);
  return response.data;
}
