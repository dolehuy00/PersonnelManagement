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
import SearchWithPopup from "components/Popups/SearchWithPopup.js";

// custom hooks
import {
    useGetOneAssignmentByLeader,
    useEditAssignmentByLeader
} from "hooks/UseAssignmentApi.js";
import { useSearchEmployeeByLeader } from "hooks/UseEmployeeApi.js";

const ViewAssignment = () => {
    // search params
    const location = useLocation();
    const searchParams = new URLSearchParams(location.search);
    const assignmentId = searchParams.get("id");
    const departmentId = searchParams.get("department");
    const mode = searchParams.get("mode")

    // state constant
    const [formValues, setFormValues] = useState({});
    const [formValuesDefault, setFormValuesDefault] = useState({});
    const [viewMode, setViewMode] = useState(mode || "");
    const [formValueIsValid, setFormValueIsValid] = useState(false);
    const [dataEdit, setDataEdit] = useState({});

    // state variable
    let arrSetValueInputEmployee = useState([]);

    // request data
    const { data: dataGetAssignment, loading: loadingGetAssignment, error: errorGetAssignment } = useGetOneAssignmentByLeader(departmentId, assignmentId);
    const { data: dataEditResponse, loading: loadingEdit, error: errorEdit } = useEditAssignmentByLeader(departmentId, dataEdit);

    // effect set data to form
    useEffect(() => {
        if (Object.keys(dataGetAssignment).length > 0) {
            setDataForm(dataGetAssignment);
            setFormValuesDefault(dataGetAssignment)
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [dataGetAssignment]);

    // effect show toast error respone save change
    useEffect(() => {
        if (errorEdit) {
            toast.error(`Save failed, ${errorEdit.response?.data?.messages[0] || "an error occurred, please try again later!"}`, {
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
    }, [errorEdit]);

    // effect show toast success respone save change
    useEffect(() => {
        if (dataEditResponse.status === 200) {
            setFormValuesDefault(dataEditResponse)
            setDataEdit({});
            toast.success('Edit successfully', {
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
    }, [dataEditResponse]);

    // handle submit form data
    const handleSubmit = (event) => {
        event.preventDefault();
        setDataEdit(formValues);
        setFormValueIsValid(false);
    };

    // handle cancel edit mode
    const handleCancelEdit = (event) => {
        event.preventDefault();
        setViewMode("view");
        setDataForm(formValuesDefault);
        setFormValueIsValid(false);
    }

    // handle view edit mode
    const handleClickEdit = (event) => {
        event.preventDefault();
        setViewMode("edit");
    }

    // handle common input change
    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setFormValues((prevValues) => ({
            ...prevValues,
            [name]: value,
        }));
        setFormValueIsValid(true);
    };

    // check edit mode function
    const isEditMode = () => {
        return viewMode === "edit";
    };

    // set data to form fuction
    const setDataForm = (data) => {
        if (data.results) {
            const result = data.results[0];
            setFormValues({
                id: result.id || -1,
                detail: result.detail || "",
                name: result.name || "",
                priotityLevel: result.priotityLevel || "",
                status: result.status || "",
                responsiblePesonId: result.responsiblePesonId || "",
                deptAssignmentId: result.deptAssignmentId || "",
            });
            arrSetValueInputEmployee[0]({ fullname: result.responsiblePesonName || "", id: result.responsiblePesonId || "" })
        }
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
                                            {isEditMode() ? "Edit Assignment" : "View Assignment"}
                                        </h3>
                                    </Col>
                                    <Col className="text-right" xs="4">
                                        {isEditMode()
                                            ? (
                                                <Button
                                                    color="primary"
                                                    href="#pablo"
                                                    onClick={handleCancelEdit}
                                                    size="sm"
                                                >
                                                    Cancel
                                                </Button>
                                            )
                                            : (
                                                <Button
                                                    color="primary"
                                                    href="#pablo"
                                                    onClick={handleClickEdit}
                                                    size="sm"
                                                >
                                                    Edit
                                                </Button>
                                            )
                                        }
                                    </Col>
                                </Row>
                            </CardHeader>
                            <CardBody>
                                {dataGetAssignment.results
                                    ? (<>
                                        <Form onSubmit={handleSubmit}>
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
                                                                value={formValues.id || ""}
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
                                                                value={formValues.name || ""}
                                                                onChange={handleInputChange}
                                                                required
                                                                disabled={!isEditMode()}
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
                                                                value={formValues.priotityLevel || ""}
                                                                disabled={!isEditMode()}
                                                                onChange={handleInputChange}
                                                            />
                                                        </FormGroup>
                                                    </Col>
                                                </Row>
                                                <Row>
                                                    <Col lg="6">
                                                        <FormGroup>
                                                            <label
                                                                className="form-control-label"
                                                                htmlFor="input-dept"
                                                            >
                                                                Department Assignment
                                                            </label>
                                                            <Input
                                                                className="form-control-alternative"
                                                                id="input-priotity-level"
                                                                type="number"
                                                                value={formValues.deptAssignmentId || ""}
                                                                readOnly
                                                                name="deptAssignmentId"
                                                                required
                                                            />
                                                        </FormGroup>
                                                    </Col>
                                                    <Col lg="6">
                                                        <FormGroup>
                                                            <label
                                                                className="form-control-label"
                                                                htmlFor="input-employee"
                                                            >
                                                                Responsible Person
                                                            </label>
                                                            <SearchWithPopup
                                                                titleModal="Search Employee (Name or ID)"
                                                                nameInput="responsiblePesonId"
                                                                searchApiFunc={useSearchEmployeeByLeader}
                                                                propertyInDataToViewSearch={
                                                                    [
                                                                        { text: "ID: ", property: "id" },
                                                                        { text: " ~ ", property: "fullname" },
                                                                        { text: " ~ ", property: "dateOfBirth" },
                                                                    ]
                                                                }
                                                                propertyInDataToViewDisableInput={["id", "fullname"]}
                                                                propertyInDataToSetRealInput="id"
                                                                deboundTimeOut={1500}
                                                                disabled={!isEditMode()}
                                                                arraySetValueInput={arrSetValueInputEmployee}
                                                                onChange={handleInputChange}
                                                                departmentId = {departmentId}
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
                                                            <select
                                                                id="status"
                                                                name="status"
                                                                className="form-control"
                                                                value={formValues.status || ""}
                                                                disabled={!isEditMode()}
                                                                onChange={handleInputChange}
                                                                required
                                                            >
                                                                <option value="Pending">Pending</option>
                                                                <option value="Assigned">Assigned</option>
                                                                <option value="In Progress">In Progress</option>
                                                                <option value="On Hold">On Hold</option>
                                                                <option value="Completed">Completed</option>
                                                                <option value="Verified">Verified</option>
                                                                <option value="Rejected">Rejected</option>
                                                                <option value="Cancelled">Cancelled</option>
                                                                <option value="Failed">Failed</option>
                                                            </select>
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
                                                                value={formValues.detail || ""}
                                                                disabled={!isEditMode()}
                                                                onChange={handleInputChange}
                                                            />
                                                        </FormGroup>
                                                    </Col>
                                                </Row>
                                            </div>
                                            {isEditMode()
                                                ? (
                                                    <>
                                                        <hr />
                                                        <div className="py-0 text-right">
                                                            <Button className="btn-icon btn-3" color="success" type="submit" disabled={!formValueIsValid}>
                                                                <span className="btn-inner--text m-0">
                                                                    {loadingEdit
                                                                        ? (<><Spinner size="sm">Waiting...</Spinner><span> Waiting...</span></>)
                                                                        : (<><i className="fa-solid fa-floppy-disk"></i> Save</>)
                                                                    }
                                                                </span>
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