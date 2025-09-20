import useSWR from "swr";
import { getPropertiesByOwnerIdAsync } from "./propertyServices";
import Property from "../../model/Property";

function useProperties(ownerId?: string) {
  const { data, error, isLoading, mutate } = useSWR<Property[]>(
    ownerId,
    getPropertiesByOwnerIdAsync,
    {
      onErrorRetry: (error, _key, _config, revalidate, { retryCount }) => {
        if (error.status === 404) return;
        if (retryCount >= 10) return;
        setTimeout(() => revalidate({ retryCount }), 5000);
      },
    }
  );

  let isError = error;
  if (error && error.status === 404) {
    isError = null;
  }

  return {
    properties: data,
    isLoading,
    isError,
    mutate,
  };
}

export default useProperties;
