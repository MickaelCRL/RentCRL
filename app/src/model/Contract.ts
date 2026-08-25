interface Contract {
  id?: string;
  propertyId?: string;
  tenantId?: string;
  tenantEmail?: string;
  rent?: number;
  deposit?: number;
  familyAllowanceFundAmount?: number;
  startDate?: string;
  endDate?: string;
  note?: string;
}

export default Contract;
