import { Breadcrumbs, Typography, Link as MuiLink } from "@mui/material";
import { BreadcrumbItem } from "../../model/BreadcrumbItem";
import { Link } from "react-router-dom";

interface BreadcrumbsNavProps {
  breadcrumbs: BreadcrumbItem[];
}
function BreadcrumbsNav({ breadcrumbs }: BreadcrumbsNavProps) {
  return (
    <Breadcrumbs aria-label="breadcrumb">
      {breadcrumbs.map((breadcrumb, idx) => {
        const isLast = idx === breadcrumbs.length - 1;

        if (isLast) {
          return (
            <Typography key={breadcrumb.label} color="#1A237E">
              {breadcrumb.label}
            </Typography>
          );
        }

        return breadcrumb.href ? (
          <MuiLink
            key={breadcrumb.label}
            component={Link}
            to={breadcrumb.href}
            underline="hover"
            color="inherit"
          >
            {breadcrumb.label}
          </MuiLink>
        ) : (
          <Typography key={breadcrumb.label} color="inherit">
            {breadcrumb.label}
          </Typography>
        );
      })}
    </Breadcrumbs>
  );
}

export default BreadcrumbsNav;
