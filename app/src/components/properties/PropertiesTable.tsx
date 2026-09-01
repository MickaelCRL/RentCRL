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
import Property from "../../model/Property";
import PropertyActions from "./PropertyActions";

interface PropertiesTableProps {
  properties: Property[];
  onMutate: () => void;
}

export default function PropertiesTable({
  properties,
  onMutate,
}: PropertiesTableProps) {
  if (!properties || properties.length === 0) return null;

  return (
    <TableContainer component={Paper} sx={{ borderRadius: 2, boxShadow: 1 }}>
      <Table sx={{ minWidth: 650 }}>
        <TableHead sx={{ backgroundColor: "#f5f5f5" }}>
          <TableRow>
            <TableCell sx={{ fontWeight: "bold" }}>Nom du bien</TableCell>
            <TableCell sx={{ fontWeight: "bold" }}>Emplacement</TableCell>
            <TableCell sx={{ fontWeight: "bold" }}>Surface</TableCell>
            <TableCell sx={{ fontWeight: "bold" }}>Statut</TableCell>
            <TableCell align="right" sx={{ fontWeight: "bold" }}>
              Actions
            </TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {properties.map((property, idx) => (
            <TableRow
              key={property.id || idx}
              sx={{ "&:hover": { backgroundColor: "#fafafa" } }}
            >
              <TableCell>
                <Typography variant="subtitle2">
                  {property.name || "-"}
                </Typography>
              </TableCell>
              <TableCell>
                {property.address?.city || "-"}{" "}
                {property.address?.postalCode
                  ? `(${property.address.postalCode})`
                  : ""}
              </TableCell>
              <TableCell>
                {property.surface ? `${property.surface} m²` : "-"}
              </TableCell>
              <TableCell>
                <Chip
                  label={property.status || "Vacant"}
                  color={property.status === "Loué" ? "success" : "default"}
                  size="small"
                />
              </TableCell>
              <TableCell align="right">
                <PropertyActions
                  propertyId={property.id!}
                  onDeleted={onMutate}
                />
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
}
