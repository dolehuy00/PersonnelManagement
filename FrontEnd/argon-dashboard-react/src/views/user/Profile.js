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

// reactstrap components
import {
  Card,
  CardHeader,
  CardBody,
  FormGroup,
  Form,
  Input,
  Container,
  Row,
  Col,
  Spinner,
  Alert,
  Table,
  Button
} from "reactstrap";
// core components
import UserHeader from "components/Headers/UserHeader.js";
import ImageWithSkeleton from "components/Images/ImageWithSkeleton.js";
import { useGetInfoByEmployee, useChangeImageEmployeeByUser } from "hooks/UseEmployeeApi.js";
import { useGetPageSalaryHistoryByUser } from "hooks/UseSalaryHistoryApi.js";
import { useNavigate } from "react-router-dom";
import { useEffect, useRef, useState } from "react";
import { Slide, ToastContainer, toast } from 'react-toastify';
import { saveImageToLocalStorage } from "services/Image.js";

const Profile = () => {

  const navigate = useNavigate();

  const fileInputRef = useRef(null);

  const [imageValue, setImageValue] = useState(null);
  const [isImgChange, setIsImgChange] = useState(false);
  const [imageLink, setImageLink] = useState("");

  const { data, loading, error } = useGetInfoByEmployee();
  const { data: dataSalary, loading: loadingSalary, error: errorSalary } = useGetPageSalaryHistoryByUser(1, 6);
  const {
    data: dataChangeImageEmployeeResponse,
    loading: loadingChangeImageEmployee,
    error: errorChangeImageEmployee,
    request: requestChangeImageEmployee
  } = useChangeImageEmployeeByUser();

  // effect set data to form
  useEffect(() => {
    if (Object.keys(data).length > 0) {
      setImageLink(data.results ? data.results[0].image : "")
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [data]);


  // effect show toast error change img
  useEffect(() => {
    if (errorChangeImageEmployee) {
      toast.error(`Change image fail, ${errorChangeImageEmployee.response?.data?.messages[1]}`, {
        position: "bottom-right",
        autoClose: 10000,
        hideProgressBar: false,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
        progress: undefined,
        theme: "colored",
        transition: Slide,
      });
    }
  }, [errorChangeImageEmployee]);

  // effect show toast success change img
  useEffect(() => {
    if (dataChangeImageEmployeeResponse.status === 200) {
      setIsImgChange(false);
      saveImageToLocalStorage(dataChangeImageEmployeeResponse?.results[0]?.fileUrl, "image")
      toast.success('Change image successfully', {
        position: "bottom-right",
        autoClose: 5000,
        hideProgressBar: false,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
        progress: undefined,
        theme: "colored",
        transition: Slide,
      });
    }
  }, [dataChangeImageEmployeeResponse]);

  // handle save change image
  const handleChangeImageEmployee = () => {
    requestChangeImageEmployee(imageValue);
  }

  // handle select image employee 
  const handleImageChange = (event) => {
    const file = event.target.files[0];
    setImageValue(file);
    setIsImgChange(true);
    if (file) {
      const reader = new FileReader();

      // Đọc file dưới dạng Data URL (base64)
      reader.onload = (e) => {
        setImageLink(e.target.result);
      };

      reader.readAsDataURL(file); // Đọc file
    }
  }

  const handleImageClick = () => {
    // Kích hoạt sự kiện click trên input file
    if (fileInputRef.current) {
      fileInputRef.current.click();
    }
    console.log("click")
  };

  return (
    <>
      <UserHeader />
      {/* Page content */}
      <Container className="mt--7" fluid>
        <Row>
          <Col className="order-xl-2 mb-5 mb-xl-0" xl="4">
            <Card className="card-profile shadow">
              <Row className="justify-content-center">
                <Col className="order-lg-2" lg="3">
                  <div className="card-profile-image">
                    <ImageWithSkeleton
                      alt="avatar"
                      placeholder={require("../../assets/img/theme/img-gray.png")}
                      className="rounded-circle"
                      transform="translate(-50%, 70%)"
                      showSpiner="true"
                      src={imageLink}
                      onClick={handleImageClick}
                    />
                  </div>
                </Col>
              </Row>
              <CardBody className="pt-5 pt-md-4">
                <Form>
                  <input
                    ref={fileInputRef}
                    className="form-control-alternative input-file"
                    id="input-image-employee"
                    type="file"
                    hidden
                    accept="image/*"
                    onChange={handleImageChange}
                  />
                </Form>
                <Row className="pt-5">
                  <div className="col">
                    <div className="card-profile-stats d-flex justify-content-center mt-md-5">
                      {isImgChange
                        ? (<>
                          <Col lg={{ size: "auto" }} className="pl-0">
                            <Button
                              color="info"
                              type="button"
                              disabled={loadingChangeImageEmployee}
                              onClick={handleChangeImageEmployee}
                            >
                              {loadingChangeImageEmployee
                                ? (<Spinner color="primary" size="sm"> </Spinner>)
                                : "Save Image"
                              }
                            </Button>
                          </Col>
                        </>)
                        : ""
                      }
                    </div>
                  </div>
                </Row>

                {/* <Row className="pt-5">
                  <div className="col">
                    <div className="card-profile-stats d-flex justify-content-center mt-md-5">
                      <div>
                        <span className="heading">22/22</span>
                        <span className="description">Assignments</span>
                      </div>
                      <div>
                        <span className="heading">10</span>
                        <span className="description">Projects</span>
                      </div>
                    </div>
                  </div>
                </Row>
                <div className="text-center">
                  <h3>
                    Jessica Jones
                    <span className="font-weight-light">, 27</span>
                  </h3>
                  <div className="h5 font-weight-300">
                    <i className="ni location_pin mr-2" />
                    Bucharest, Romania
                  </div>
                  <div className="h5 mt-4">
                    <i className="ni business_briefcase-24 mr-2" />
                    Solution Manager - Creative Tim Officer
                  </div>
                  <div>
                    <i className="ni education_hat mr-2" />
                    University of Computer Science
                  </div>
                  <p className="my-3"></p>
                </div> */}
              </CardBody>
            </Card>
            <Card className="card-profile shadow mt-4">
              <Row className="justify-content-center mt-3">
                <h3>My salary history</h3>
              </Row>
              {loadingSalary && (
                <div className="overlay-spinner-table-loading">
                  <Spinner color="info"></Spinner>
                </div>
              )}
              <CardBody className="py-md-1">
                <Table className="table-salary-user">
                  <thead>
                    <tr className="text-center">
                      <th>Basic</th>
                      <th>Total</th>
                      <th>Date</th>
                    </tr>
                  </thead>
                  <tbody>
                    {dataSalary?.results?.length
                      ? dataSalary.results.map((item, index) => (
                        <tr
                          key={`salary-${index}`}
                          className="text-center"
                          onClick={(e) => { e.preventDefault(); navigate(`/salary-history/view?id=${item.id}`); }}
                        >
                          <td>{item.basicSalary.toFixed(2)}</td>
                          <td>{item.total.toFixed(2)}</td>
                          <td>{item.date.split("T")[0]}</td>
                        </tr>
                      ))
                      : !loading
                        ? errorSalary
                          ? (
                            <tr>
                              <td className="text-center" colSpan="3" style={{ color: "red" }}>
                                <i className="fa-solid fa-circle-exclamation mr-1"></i><strong>Error load data!</strong>
                              </td>
                            </tr>)
                          : (
                            <tr>
                              <td className="text-center" colSpan="3">Salary history is empty !</td>
                            </tr>
                          )
                        : (<tr></tr>)
                    }
                  </tbody>
                </Table>
              </CardBody>
              {dataSalary?.results?.length
                ? (
                  <Row className="justify-content-center my-3">
                    <a href="/salary-history">
                      View All <i className="fa-solid fa-arrow-right-long"></i>
                    </a>
                  </Row>
                )
                : ""
              }
            </Card>
          </Col>
          <Col className="order-xl-1" xl="8">
            <Card className="bg-secondary shadow">
              {loading && (
                <div className="overlay-spinner-table-loading">
                  <Spinner color="info"></Spinner>
                </div>
              )}
              <CardHeader className="bg-white border-0">
                <Row className="align-items-center">
                  <Col xs="8">
                    <h3 className="mb-0">My account</h3>
                  </Col>
                  <Col className="text-right" xs="4"></Col>
                </Row>
              </CardHeader>
              <CardBody>
                <Form>
                  <h6 className="heading-small text-muted mb-4">
                    User information
                  </h6>
                  {error && (
                    <Alert className="w-100 text-center mb-0" color="danger">
                      <i className="fa-solid fa-circle-exclamation mr-1"></i><strong>Error load data!</strong>
                    </Alert>
                  )}
                  {data?.results
                    ? (<>
                      <div>
                        <div className="pl-lg-4">
                          <Row>
                            <Col lg="6">
                              <FormGroup>
                                <label
                                  className="form-control-label"
                                  htmlFor="input-fullname"
                                >
                                  Fullname
                                </label>
                                <Input
                                  className="form-control-alternative"
                                  id="input-fullname"
                                  value={data.results[0].fullname || "No data"}
                                  readOnly
                                  placeholder="Fullname"
                                  type="text"
                                />
                              </FormGroup>
                            </Col>
                            <Col lg="6">
                              <FormGroup>
                                <label
                                  className="form-control-label"
                                  htmlFor="input-date-of-birth"
                                >
                                  Date Of Birth
                                </label>
                                <Input
                                  className="form-control-alternative"
                                  id="input-date-of-birth"
                                  placeholder="Date Of Birth"
                                  type="text"
                                  value={data.results[0].dateOfBirth || "No data"}
                                  readOnly
                                />
                              </FormGroup>
                            </Col>
                          </Row>
                          <Row>
                            <Col lg="6">
                              <FormGroup>
                                <label
                                  className="form-control-label"
                                  htmlFor="input-basic-salary"
                                >
                                  Basic Salary
                                </label>
                                <Input
                                  className="form-control-alternative"
                                  id="input-basic-salary"
                                  placeholder="Basic Salary"
                                  type="text"
                                  value={data.results[0].basicSalary || "No data"}
                                  readOnly
                                />
                              </FormGroup>
                            </Col>
                            <Col lg="6">
                              <FormGroup>
                                <label
                                  className="form-control-label"
                                  htmlFor="input-position"
                                >
                                  Position
                                </label>
                                <Input
                                  className="form-control-alternative"
                                  id="input-position"
                                  placeholder="Position"
                                  type="text"
                                  value={data.results[0].position || "No data"}
                                  readOnly
                                />
                              </FormGroup>
                            </Col>
                          </Row>
                          <Row>
                            <Col lg="6">
                              <FormGroup>
                                <label
                                  className="form-control-label"
                                  htmlFor="input-department"
                                >
                                  Department
                                </label>
                                <Input
                                  className="form-control-alternative"
                                  id="input-department"
                                  placeholder="Department"
                                  type="text"
                                  value={data.results[0].departmentName || "No data"}
                                  readOnly
                                />
                              </FormGroup>
                            </Col>
                            <Col lg="6">
                              <FormGroup>
                                <label
                                  className="form-control-label"
                                  htmlFor="input-start-date"
                                >
                                  Start Date
                                </label>
                                <Input
                                  className="form-control-alternative"
                                  id="input-start-date"
                                  placeholder="Start Date"
                                  type="text"
                                  value={data.results[0].startDate || "No data"}
                                  readOnly
                                />
                              </FormGroup>
                            </Col>
                          </Row>
                        </div>
                        <hr className="my-4" />
                        {/* Address */}
                        <h6 className="heading-small text-muted mb-4">
                          Contact information
                        </h6>
                        <div className="pl-lg-4">
                          <Row>
                            <Col md="12">
                              <FormGroup>
                                <label
                                  className="form-control-label"
                                  htmlFor="input-address"
                                >
                                  Address
                                </label>
                                <Input
                                  className="form-control-alternative"
                                  id="input-address"
                                  placeholder="Home Address"
                                  type="text"
                                  value={data.results[0].address || "No data"}
                                  readOnly
                                />
                              </FormGroup>
                            </Col>
                          </Row>
                        </div>
                      </div>
                    </>)
                    : ""
                  }
                  <hr className="my-4" />
                  {/* Account */}
                  <h6 className="heading-small text-muted mb-4">
                    Account information
                  </h6>
                  <div className="pl-lg-4">
                    <Row>
                      <Col lg="6">
                        <FormGroup>
                          <label
                            className="form-control-label"
                            htmlFor="input-email"
                          >
                            Email address
                          </label>
                          <Input
                            className="form-control-alternative"
                            id="input-email"
                            placeholder="jesse@example.com"
                            type="email"
                            value={localStorage.getItem('email') || "No data"}
                            readOnly
                          />
                        </FormGroup>
                      </Col>
                      <Col lg="6">
                        <FormGroup>
                          <label
                            className="form-control-label"
                            htmlFor="input-role"
                          >
                            Role
                          </label>
                          <Input
                            className="form-control-alternative"
                            id="input-role"
                            placeholder="Role Name"
                            type="text"
                            value={localStorage.getItem('role') || "No data"}
                            readOnly
                          />
                        </FormGroup>
                      </Col>
                    </Row>
                  </div>
                </Form>
              </CardBody>
            </Card>
            <div>
              <ToastContainer />
            </div>
          </Col>
        </Row>
      </Container>
    </>
  );
};

export default Profile;
