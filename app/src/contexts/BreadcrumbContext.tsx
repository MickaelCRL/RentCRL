import {
  createContext,
  useContext,
  useEffect,
  useState,
  ReactNode,
} from "react";
import { BreadcrumbItem } from "../model/BreadcrumbItem";

type BreadcrumbContextType = {
  breadcrumbs: BreadcrumbItem[];
  setBreadcrumbs: (items: BreadcrumbItem[]) => void;
};

export const BreadcrumbContext = createContext<BreadcrumbContextType>({
  breadcrumbs: [],
  setBreadcrumbs: () => {},
});

type BreadcrumbProviderProps = {
  children: ReactNode;
};

export const BreadcrumbProvider = ({ children }: BreadcrumbProviderProps) => {
  const [breadcrumbs, setBreadcrumbsState] = useState<BreadcrumbItem[]>([]);

  useEffect(() => {
    const storedBreadcrumb = sessionStorage.getItem("breadcrumbContext");

    if (storedBreadcrumb) {
      setBreadcrumbs(JSON.parse(storedBreadcrumb));
    }
  }, []);

  const setBreadcrumbs = (breadcrumbs: BreadcrumbItem[]) => {
    setBreadcrumbsState(breadcrumbs);
    sessionStorage.setItem("breadcrumbContext", JSON.stringify(breadcrumbs));
  };

  return (
    <BreadcrumbContext.Provider value={{ breadcrumbs, setBreadcrumbs }}>
      {children}
    </BreadcrumbContext.Provider>
  );
};

export const useBreadcrumbContext = () => useContext(BreadcrumbContext);
