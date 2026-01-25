import useSWR from "swr";
import { getUserByEmailAsync } from "./userServices";
import User from "../../model/User";

function useUser(email: string) {
  const { data, error, isLoading } = useSWR<User>(email, getUserByEmailAsync);

  return {
    user: data,
    isLoading,
    isError: error,
  };
}

export default useUser;
