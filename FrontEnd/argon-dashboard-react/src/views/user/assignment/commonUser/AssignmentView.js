// reactstrap components
import {
    Card,
    CardHeader,
    Container,
    Row,
    Col,
    Button,
    Input,
    CardBody,
    FormGroup,
    Spinner,
    Form
} from "reactstrap";
// core components
import { useState, useEffect } from "react";
import { useLocation } from 'react-router-dom';
import { Slide, ToastContainer, toast } from 'react-toastify';

// component
import Header from "components/Headers/Header.js";
import LoadingOrError from "components/Notifications/LoadingOrError.js";

// custom hooks
import { useGetOneAssignmentByUser, useChangeStatusByUser } from "hooks/UseAssignmentApi.js";

const ViewAssignment = () => {
    // search params
    const location = useLocation();
    const searchParams = new URLSearchParams(location.search);
    const assignmentId = searchParams.get("id");


    const [statusValue, setStatusValue] = useState("");

    // request data
    const {
        data: dataGetAssignment,
        loading: loadingGetAssignment,
        error: errorGetAssignment
    } = useGetOneAssignmentByUser(assignmentId);

    const {
        data: dataChangeStatusResponse,
        loading: loadingChangeStatus,
        error: errorChangeStatus,
        request: requestChangeStatus
    } = useChangeStatusByUser();

    // effect set data 
    useEffect(() => {
        if (Object.keys(dataGetAssignment).length > 0) {
            setStatusValue(dataGetAssignment.results[0].status || "")
        }
    }, [dataGetAssignment]);

    // effect show toast error change status
    useEffect(() => {
        if (errorChangeStatus) {
            toast.error(`Change status fail!, ${errorChangeStatus.response?.data?.messages[1] || "an error occurred, please try again later!"}`, {
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
    }, [errorChangeStatus]);

    // effect show toast success change status
    useEffect(() => {
        if (dataChangeStatusResponse.status === 200) {
            setStatusValue(dataChangeStatusResponse?.messages[1])
            toast.success(`Assignment ${dataChangeStatusResponse?.messages[1]}`, {
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
    }, [dataChangeStatusResponse]);


    // handle click lock/unlock button
    const handleChangeStatus = () => {
        requestChangeStatus(assignmentId, statusValue === "Assigned" ? "In Progress" : "Completed")
    }

    // render loading data assignment
    if (loadingGetAssignment) return (
        <>
            <Header />
            <LoadingOrError status="loading" />
        </>
    );

    // render error load data assignment
    if (errorGetAssignment) return (
        <>
            <Header />
            <LoadingOrError status="error" />
        </>
    );

    // render loaded data assignment
    return (
        <>
            <Header />
            {/* Page content */}
            <Container className="mt--7" fluid>
                <Row>
                    <div className="col">
                        <Card className="bg-secondary shadow">
                            <CardHeader className="bg-white border-0">
                                <Row className="align-items-center">
                                    <Col xs="8">
                                        <h3 className="mb-0">
                                            View Assignment
                                        </h3>
                                    </Col>
                                    <Col className="text-right" xs="4">

                                    </Col>
                                </Row>
                            </CardHeader>
                            <CardBody>
                                {dataGetAssignment.results
                                    ? (<>
                                        <Form>
                                            <div className="pl-lg-4">
                                                <Row>
                                                    <Col lg="2">
                                                        <FormGroup>
                                                            <label
                                                                className="form-control-label"
                                                                htmlFor="input-id"
                                                            >
                                                                Assignment ID
                                                            </label>
                                                            <Input
                                                                className="form-control-alternative"
                                                                id="input-id"
                                                                type="text"
                                                                name="id"
                                                                value={dataGetAssignment.results[0]?.id || ""}
                                                                required
                                                                disabled
                                                            />
                                                        </FormGroup>
                                                    </Col>
                                                </Row>
                                                <Row>
                                                    <Col lg="6">
                                                        <FormGroup>
                                                            <label
                                                                className="form-control-label"
                                                                htmlFor="input-name"
                                                            >
                                                                Name
                                                            </label>
                                                            <Input
                                                                className="form-control-alternative"
                                                                id="input-name"
                                                                type="text"
                                                                name="name"
                                                                value={dataGetAssignment.results[0]?.name || ""}
                                                                onChange={e => e.preventDefault()}
                                                                required
                                                            />
                                                        </FormGroup>
                                                    </Col>
                                                    <Col lg="6">
                                                        <FormGroup>
                                                            <label
                                                                className="form-control-label"
                                                                htmlFor="input-priotity-level"
                                                            >
                                                                Priotity Level
                                                            </label>
                                                            <Input
                                                                className="form-control-alternative"
                                                                id="input-priotity-level"
                                                                type="number"
                                                                name="priotityLevel"
                                                                required
                                                                value={dataGetAssignment.results[0]?.priotityLevel || ""}
                                                                onChange={e => e.preventDefault()}
                                                            />
                                                        </FormGroup>
                                                    </Col>
                                                </Row>
                                            </div>
                                            <div className="pl-lg-4">
                                                <Row>
                                                    <Col md="12">
                                                        <FormGroup>
                                                            <label
                                                                className="form-control-label"
                                                                htmlFor="input-status"
                                                            >
                                                                Status
                                                            </label>
                                                            <Input
                                                                className="form-control-alternative"
                                                                id="input-status"
                                                                type="text"
                                                                value={statusValue || ""}
                                                                onChange={e => { e.preventDefault() }}
                                                                required
                                                            />
                                                        </FormGroup>
                                                    </Col>
                                                </Row>
                                                <Row>
                                                    <Col>
                                                        <FormGroup>
                                                            <label
                                                                className="form-control-label"
                                                                htmlFor="input-tax-salary"
                                                            >
                                                                Detail
                                                            </label>
                                                            <Input
                                                                className="form-control-alternative"
                                                                id="input-tax-salary"
                                                                rows="3"
                                                                type="textarea"
                                                                name="detail"
                                                                required
                                                                value={dataGetAssignment.results[0]?.detail || ""}
                                                                onChange={e => e.preventDefault()}
                                                            />
                                                        </FormGroup>
                                                    </Col>
                                                </Row>
                                            </div>
                                            {statusValue === "Assigned" || statusValue === "In Progress"
                                                ? (
                                                    <>
                                                        <hr />
                                                        <div className="py-0 text-right">
                                                            <Button
                                                                color={statusValue === "Assigned" ? "info" : "success"}
                                                                type="button"
                                                                disabled={loadingChangeStatus}
                                                                onClick={handleChangeStatus}
                                                            >
                                                                {loadingChangeStatus
                                                                    ? (<Spinner color="primary" size="sm"> </Spinner>)
                                                                    : statusValue === "Assigned" ? "Accept Assignment" : "Already Done"
                                                                }
                                                            </Button>
                                                        </div>
                                                    </>
                                                )
                                                : ""
                                            }
                                        </Form>
                                    </>)
                                    : ""}
                            </CardBody>
                        </Card>
                        <div>
                            <ToastContainer />
                        </div>
                    </div>
                </Row>
            </Container>
        </>
    );
};

export default ViewAssignment;