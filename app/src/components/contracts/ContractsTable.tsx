import {
  TableContainer,
  Paper,
  Table,
  TableHead,
  TableRow,
  TableCell,
  TableBody,
  Chip,
  Typography,
} from "@mui/material";
import Contract from "../../model/Contract";
import Property from "../../model/Property";

interface ContractsTableProps {
  contracts: Contract[];
  properties: Property[];
}

export default function ContractsTable({
  contracts,
  properties,
}: ContractsTableProps) {
  if (!contracts || contracts.length === 0) return null;

  const getPropertyName = (propertyId?: string) => {
    if (!propertyId) return "-";
    const prop = properties.find((p) => p.id === propertyId);
    return prop?.name || "Propriété supprimée";
  };

  return (
    <TableContainer component={Paper} sx={{ borderRadius: 2, boxShadow: 1 }}>
      <Table sx={{ minWidth: 650 }}>
        <TableHead sx={{ backgroundColor: "#f5f5f5" }}>
          <TableRow>
            <TableCell sx={{ fontWeight: "bold" }}>Bien</TableCell>
            <TableCell sx={{ fontWeight: "bold" }}>Locataire</TableCell>
            <TableCell sx={{ fontWeight: "bold" }}>Loyer</TableCell>
            <TableCell sx={{ fontWeight: "bold" }}>Dates du bail</TableCell>
            <TableCell sx={{ fontWeight: "bold" }}>Statut</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {contracts.map((contract) => (
            <TableRow
              key={contract.id}
              sx={{ "&:hover": { backgroundColor: "#fafafa" } }}
            >
              <TableCell>
                <Typography variant="subtitle2">
                  {getPropertyName(contract.propertyId)}
                </Typography>
              </TableCell>
              <TableCell>{contract.tenantEmail || "-"}</TableCell>
              <TableCell>
                {contract.rent ? `${contract.rent} €` : "-"}
              </TableCell>
              <TableCell>
                {contract.startDate
                  ? new Date(contract.startDate).toLocaleDateString()
                  : "-"}
                {contract.endDate
                  ? ` au ${new Date(contract.endDate).toLocaleDateString()}`
                  : " (En cours)"}
              </TableCell>
              <TableCell>
                {contract.tenantId ? (
                  <Chip label="Actif" color="success" size="small" />
                ) : (
                  <Chip label="En attente" color="warning" size="small" />
                )}
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
}
