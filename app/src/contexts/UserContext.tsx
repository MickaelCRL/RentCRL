import {
  createContext,
  useContext,
  useState,
  ReactNode,
  useEffect,
} from "react";
import User from "../model/User";
import { useAuth0 } from "@auth0/auth0-react";
import { getUserByEmailAsync } from "../services/users/userServices";

interface UserContextType {
  userContext?: User;
  setUserContext: (userContext: User) => void;
  isUserContextLoading: boolean;
}

export const UserContext = createContext<UserContextType>({
  userContext: undefined,
  setUserContext: () => {},
  isUserContextLoading: true,
});

interface UserProviderProps {
  children: ReactNode;
}

export const UserProvider = ({ children }: UserProviderProps) => {
  const [userContext, setUserContext] = useState<User>();
  const [isUserContextLoading, setIsUserContextLoading] = useState(true);

  const {
    user: auth0User,
    isAuthenticated,
    isLoading: isAuth0Loading,
  } = useAuth0();

  useEffect(() => {
    if (isAuth0Loading) return;

    const fetchDatabaseUser = async () => {
      if (isAuthenticated && auth0User?.email) {
        try {
          setIsUserContextLoading(true);
          const dbUser = await getUserByEmailAsync(auth0User.email);

          if (dbUser) {
            setUserContext(dbUser);
          }
        } catch (error) {
          console.error("New tenant or owner detected");
        } finally {
          setIsUserContextLoading(false);
        }
      } else {
        setIsUserContextLoading(false);
      }
    };

    fetchDatabaseUser();
  }, [isAuthenticated, isAuth0Loading, auth0User]);

  return (
    <UserContext.Provider
      value={{
        userContext,
        setUserContext,
        isUserContextLoading,
      }}
    >
      {children}
    </UserContext.Provider>
  );
};

export const useUserContext = () => useContext(UserContext);
