import { Outlet } from "react-router-dom";

const Layout = () => {
  return (
    <>
      <section
        id="section"
        style={{
          width: "100%",
          height: "70vh",
          alignItems: "center",
          margin: "100px 0",
        }}
      >
        <Outlet />
      </section>
    </>
  );
};

export default Layout;
