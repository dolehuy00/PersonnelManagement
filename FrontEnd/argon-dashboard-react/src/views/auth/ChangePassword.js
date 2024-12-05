
// reactstrap components
import {
  Button,
  Card,
  CardHeader,
  CardBody,
  FormGroup,
  Form,
  Input,
  InputGroupAddon,
  InputGroupText,
  InputGroup,
  Row,
  Col,
  Alert,
  Spinner,
  Container
} from "reactstrap";
// react toastify component
import { Slide, ToastContainer, toast } from 'react-toastify';
import React, { useEffect, useRef, useState } from "react";
import { useChangePassword } from "hooks/UseAccountApi";

const Login = () => {
  const [dataBody, setDataBody] = useState({});
  const formRef = useRef(null);
  const { data, loading, error } = useChangePassword(dataBody);

  useEffect(() => {
    if (data.status === 200) {
      toast.success('Change successfully', {
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
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [error, data]);


  const onSubmit = async (e) => {
    e.preventDefault();
    const formData = new FormData(formRef.current);
    const data = Object.fromEntries(formData.entries());
    setDataBody(data);
  };

  return (
    <>
      <div className="main-content" >
        <div className="header bg-gradient-info py-7 py-lg-8">
          <div className="separator separator-bottom separator-skew zindex-100">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              preserveAspectRatio="none"
              version="1.1"
              viewBox="0 0 2560 100"
              x="0"
              y="0"
            >
              <polygon
                className="fill-default"
                points="2560 0 2560 100 0 100"
              />
            </svg>
          </div>
        </div>
        {/* Page content */}
        <Container className="mt--8 pb-5">
          <Row className="justify-content-center">      <Col lg="5" md="7">
            <Card className="bg-secondary shadow border-0">
              <CardHeader className="bg-transparent">
                <div className="text-muted text-center mt-2 mb-3">
                  <h4>Change Password</h4>
                </div>
              </CardHeader>
              <CardBody className="px-lg-5 py-lg-5">
                <form ref={formRef} onSubmit={onSubmit}>
                  <FormGroup className="mb-3">
                    <InputGroup className="input-group-alternative">
                      <InputGroupAddon addonType="prepend">
                        <InputGroupText>
                          <i className="ni ni-lock-circle-open" />
                        </InputGroupText>
                      </InputGroupAddon>
                      <Input
                        placeholder="Current Password"
                        type="password"
                        autoComplete="new-email"
                        name="currentPassword"
                      />
                    </InputGroup>
                  </FormGroup>
                  <FormGroup className="mb-2">
                    <InputGroup className="input-group-alternative">
                      <InputGroupAddon addonType="prepend">
                        <InputGroupText>
                          <i className="ni ni-lock-circle-open" />
                        </InputGroupText>
                      </InputGroupAddon>
                      <Input
                        placeholder="New Password"
                        type="password"
                        autoComplete="new-password"
                        name="newPassword"
                      />
                    </InputGroup>
                  </FormGroup>
                  <FormGroup className="mb-2">
                    <InputGroup className="input-group-alternative">
                      <InputGroupAddon addonType="prepend">
                        <InputGroupText>
                          <i className="ni ni-lock-circle-open" />
                        </InputGroupText>
                      </InputGroupAddon>
                      <Input
                        placeholder="Confirm Password"
                        type="password"
                        autoComplete="new-password"
                        name="passwordConfirm"
                      />
                    </InputGroup>
                  </FormGroup>
                  {error && (
                    <Alert color="danger" className="py-1 mb-0">
                      {error.response?.data?.messages[0]}
                    </Alert>
                  )}
                  <div className="text-center">
                    {loading ? (
                      <Button className="my-4" color="primary" type="button" disabled>
                        <Spinner size="sm">
                          Wating...
                        </Spinner>
                        <span>
                          {' '}Wating...
                        </span>
                      </Button>
                    ) : (
                      <Button className="my-4" color="primary" type="submit">
                        Confirm
                      </Button>
                    )}

                  </div>
                </form>
              </CardBody>
            </Card>
          </Col>
            <div>
              <ToastContainer />
            </div>
          </Row>
        </Container>
      </div>
    </>
  );
};

export default Login;
