import { createContext, useContext, useState, ReactNode } from "react";
import User from "../model/User";

interface UserContextType {
  userContext?: User;
  setUserContext: (userContext: User) => void;
}

export const UserContext = createContext<UserContextType>({
  userContext: undefined,
  setUserContext: () => {},
});

interface UserProviderProps {
  children: ReactNode;
}

export const UserProvider = ({ children }: UserProviderProps) => {
  const [userContext, setUserContext] = useState<User>();

  return (
    <UserContext.Provider
      value={{
        userContext,
        setUserContext,
      }}
    >
      {children}
    </UserContext.Provider>
  );
};

export const useUserContext = () => useContext(UserContext);
