/*!

=========================================================
* Argon Dashboard React - v1.2.4
=========================================================

* Product Page: https://www.creative-tim.com/product/argon-dashboard-react
* Copyright 2024 Creative Tim (https://www.creative-tim.com)
* Licensed under MIT (https://github.com/creativetimofficial/argon-dashboard-react/blob/master/LICENSE.md)

* Coded by Creative Tim

=========================================================

* The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

*/
import React from "react";
import { useLocation, Route, Routes, Navigate } from "react-router-dom";
// reactstrap components
import { Container } from "reactstrap";
// core components
import UserNavbar from "components/Navbars/UserNavbar.js";
import AdminFooter from "components/Footers/AdminFooter.js";

import { userRoutes } from "routes.js";

const User = (props) => {
  const mainContent = React.useRef(null);
  const location = useLocation();

  React.useEffect(() => {
    document.documentElement.scrollTop = 0;
    document.scrollingElement.scrollTop = 0;
    mainContent.current.scrollTop = 0;
  }, [location]);

  const getRoutes = (routes) => {
    return routes.map((prop, key) => {
      if (prop.layout === "") {
        return (
          <Route path={prop.path} element={prop.component} key={key} exact />
        );
      } else {
        return null;
      }
    });
  };

  const getBrandText = (path) => {
    for (let i = 0; i < userRoutes.length; i++) {
      if (
        path.indexOf(userRoutes[i].layout + userRoutes[i].path) !== -1
      ) {
        return userRoutes[i].name;
      }
    }
    return "Brand";
  };
  return (
    <>
      {/* <Sidebar
        {...props}
        routes={userRoutes}
        logo={{
          innerLink: "/dashboard",
          imgSrc: require("../assets/img/brand/argon-react.png"),
          imgAlt: "...",
        }}
      /> */}
      <div className="main-content" ref={mainContent}>
        <UserNavbar
          {...props}
          brandText={getBrandText(location?.pathname)}
          linkText={location?.pathname.split("/")[1]}
        />
        <Routes>
          {getRoutes(userRoutes)}
          <Route path="*" element={<Navigate to="/user-profile" replace />} />
        </Routes>
        <Container fluid className="my-6">
          {/* <AdminFooter /> */}
        </Container>
      </div>
    </>
  );
};

export default User;
