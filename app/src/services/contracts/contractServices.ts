import Contract from "../../model/Contract";
import api from "../../api";

export async function createContractAsync(ownerId: string, contract: Contract) {
  const response = await api.post(`/owners/${ownerId}/contracts`, contract);
  return response.data;
}

export async function getContractsByOwnerIdAsync(ownerId?: string) {
  const response = await api.get(`/owners/${ownerId}/contracts`);
  return response.data;
}

export async function deleteContractByIdAsync(
  ownerId: string,
  contractId: string,
) {
  const response = await api.delete(
    `/owners/${ownerId}/contracts/${contractId}`,
  );
  return response.data;
}

export async function getContractByIdAsync(
  ownerId: string,
  contractId: string,
) {
  const response = await api.get(`/owners/${ownerId}/contracts/${contractId}`);
  return response.data;
}
