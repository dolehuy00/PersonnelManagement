
import { Link, useNavigate } from "react-router-dom";
import { logout } from "services/auth/AuthApi";

// reactstrap components
import {
  DropdownMenu,
  DropdownItem,
  UncontrolledDropdown,
  DropdownToggle,
  Navbar,
  Nav,
  Container,
  Media,
} from "reactstrap";
import ImageWithSkeleton from "components/Images/ImageWithSkeleton";
import { useEffect, useState } from "react";

const AdminNavbar = (props) => {
  const navigate = useNavigate();
  const role = localStorage.getItem("role");
  const leaderOfDepartment = JSON.parse(localStorage.getItem("leaderOfDepartments"));
  const [imageUser, setImageUser] = useState("");

  useEffect(() => {
    let counter = 0;
    const interval = setInterval(() => {
      counter++;
      let img = localStorage.getItem('image');
      setImageUser(img);

      if (img || counter === 10) {
        clearInterval(interval); // Dừng sau 10 lần
      }
    }, 1000);
  }, [])

  return (
    <>
      <Navbar className="navbar-top navbar-dark" expand="md" id="navbar-main">
        <Container fluid>
          <Link
            className="h4 mb-0 text-white text-uppercase d-none d-lg-inline-block"
            to={props.linkText}
          >
            {props.brandText}
          </Link>
          {/* <Form className="navbar-search navbar-search-dark form-inline mr-3 d-none d-md-flex ml-lg-auto">
            <FormGroup className="mb-0">
              <InputGroup className="input-group-alternative">
                <InputGroupAddon addonType="prepend">
                  <InputGroupText>
                    <i className="fas fa-search" />
                  </InputGroupText>
                </InputGroupAddon>
                <Input placeholder="Search" type="text" />
              </InputGroup>
            </FormGroup>
          </Form> */}
          <Nav className="align-items-center d-none d-md-flex" navbar>
            <UncontrolledDropdown nav>
              <DropdownToggle className="pr-0" nav>
                <Media className="align-items-center">
                  <span className="avatar avatar-sm rounded-circle">
                    <ImageWithSkeleton
                      alt="avatar"
                      placeholder={require("../../assets/img/theme/img-gray.png")}
                      className="rounded-circle"
                      src={imageUser}
                    />
                  </span>
                  <Media className="ml-2 d-none d-lg-block">
                    <span className="mb-0 text-sm font-weight-bold">
                      Jessica Jones
                    </span>
                  </Media>
                </Media>
              </DropdownToggle>
              <DropdownMenu className="dropdown-menu-arrow" right>
                <DropdownItem className="noti-title" header tag="div">
                  <h6 className="text-overflow m-0">Welcome!</h6>
                </DropdownItem>
                {/* <DropdownItem to="/dashboard" tag={Link}>
                  <i className="fa-solid fa-chart-line"></i>
                  <span>Dashboard</span>
                </DropdownItem> */}
                <DropdownItem to="/user-profile" tag={Link}>
                  <i className="ni ni-single-02" />
                  <span>My profile</span>
                </DropdownItem>
                <DropdownItem to="/change-password" tag={Link}>
                  <i className="ni ni-single-02" />
                  <span>Change Password</span>
                </DropdownItem>
                <DropdownItem to="/assignment" tag={Link}>
                  <i className="ni ni-single-02" />
                  <span>Assignment</span>
                </DropdownItem>
                {leaderOfDepartment?.length > 0
                  ? (
                    <DropdownItem to="/leader/department-management" tag={Link}>
                      <i className="fa-solid fa-screwdriver-wrench" />
                      <span>Department Management</span>
                    </DropdownItem>
                  )
                  : ""}
                {role.toLowerCase() === "admin"
                  ? (
                    <DropdownItem to="/admin" tag={Link}>
                      <i className="fa-solid fa-screwdriver-wrench" />
                      <span>Go to admin page</span>
                    </DropdownItem>
                  )
                  : ""
                }
                <DropdownItem divider />
                <DropdownItem href="#pablo" onClick={(e) => { e.preventDefault(); logout(navigate) }}>
                  <i className="ni ni-user-run" />
                  <span>Logout</span>
                </DropdownItem>
              </DropdownMenu>
            </UncontrolledDropdown>
          </Nav>
        </Container>
      </Navbar>
    </>
  );
};

export default AdminNavbar;
