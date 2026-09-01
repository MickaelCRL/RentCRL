import useSWR from "swr";
import Property from "../../model/Property";
import { getPropertyByIdAsync } from "./propertyServices";

function useProperty(ownerId?: string, propertyId?: string) {
  const { data, error, isLoading, mutate } = useSWR<Property>(
    ownerId && propertyId ? ["property", ownerId, propertyId] : null,

    ([, oId, pId]: [string, string, string]) => getPropertyByIdAsync(oId, pId),

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
    property: data,
    isLoading,
    isError,
    mutate,
  };
}

export default useProperty;
