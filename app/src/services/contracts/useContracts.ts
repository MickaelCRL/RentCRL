import useSWR from "swr";
import { getContractsByOwnerIdAsync } from "./contractServices";
import Contract from "../../model/Contract";

function useContracts(ownerId?: string) {
  const { data, error, isLoading, mutate } = useSWR<Contract[]>(
    ownerId ? ["contracts", ownerId] : null,
    ([, id]: [string, string]) => getContractsByOwnerIdAsync(id),
    {
      onErrorRetry: (error, _key, _config, revalidate, { retryCount }) => {
        if (error.status === 404) return;
        if (retryCount >= 10) return;
        setTimeout(() => revalidate({ retryCount }), 5000);
      },
    },
  );

  let isError = error;
  if (error && error.status === 404) {
    isError = null;
  }

  return {
    contracts: data,
    isLoading,
    isError,
    mutate,
  };
}

export default useContracts;
