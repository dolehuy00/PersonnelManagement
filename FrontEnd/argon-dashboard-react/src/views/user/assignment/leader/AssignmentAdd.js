// reactstrap component
import {
    Button,
    Card,
    CardHeader,
    CardBody,
    FormGroup,
    Input,
    Row,
    Col,
    Spinner,
} from "reactstrap";

// core component
import { useState, useRef, useEffect } from "react";
import { useLocation } from "react-router-dom";

// react toastify component
import { Slide, ToastContainer, toast } from 'react-toastify';

// custom component
import SearchWithPopup from "components/Popups/SearchWithPopup.js";

// hooks
import { useAddAssignmentByLeader } from "hooks/UseAssignmentApi.js";
import { useSearchEmployeeByLeader } from "hooks/UseEmployeeApi.js";

const AssignmentAdd = ({ onCancel }) => {
    // search params
    const location = useLocation();
    const searchParams = new URLSearchParams(location.search);
    const departmentId = searchParams.get("department");
    const deptAssignmentId = searchParams.get("dept_assignment");

    // state constant
    const [dataBody, setDataBody] = useState({});
    let arrSetValueInputEmployee = useState([]);
    const [status, setStatus] = useState("Pending");

    // ref constant
    const formRef = useRef(null);

    // request data
    const { data, loading, error } = useAddAssignmentByLeader(departmentId, dataBody);

    // effect show toast response add assignment request
    useEffect(() => {
        if (error) {
            toast.error("Save failed, an error occurred, please try again later!", {
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
        if (data.status === 200) {
            handleReset();
            toast.success('Add successfully', {
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

    // handle submit form data
    const handleSubmit = (event) => {
        event.preventDefault();
        const formData = new FormData(formRef.current);
        const data = Object.fromEntries(formData.entries());
        setDataBody(data);
    };

    // handle reset form data
    const handleReset = () => {
        formRef.current.reset();
        arrSetValueInputEmployee[0]({ fullname: "", id: "" })
        setStatus("Pending");
    };

    // handle select dropdown and choose employee
    const handleSelectResponsiblePersonChange = () => {
        setStatus("Assigned");
    };

    // render
    return (
        <>
            <Card className="bg-secondary shadow">
                <CardHeader className="bg-white border-0">
                    <Row className="align-items-center">
                        <Col xs="8">
                            <h3 className="mb-0">Add Assignment</h3>
                        </Col>
                        <Col className="text-right" xs="4">
                            <Button
                                color="primary"
                                href="#pablo"
                                onClick={onCancel}
                                size="sm"
                            >
                                Cancel
                            </Button>
                        </Col>
                    </Row>
                </CardHeader>
                <CardBody>
                    <form ref={formRef} onSubmit={handleSubmit}>
                        <div className="pl-lg-4">
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
                                            Dept Assignment
                                        </label>
                                        <Input
                                            className="form-control-alternative"
                                            id="input-priotity-level"
                                            type="number"
                                            value={deptAssignmentId}
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
                                            arraySetValueInput={arrSetValueInputEmployee}
                                            onChange={handleSelectResponsiblePersonChange}
                                            departmentId = {departmentId}
                                        />
                                    </FormGroup>
                                </Col>
                            </Row>
                        </div>
                        <div className="pl-lg-4">
                            <Row>
                                <Col lg="6">
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
                                            name="status"
                                            value={status}
                                            readOnly
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
                                        />
                                    </FormGroup>
                                </Col>
                            </Row>
                        </div>
                        <hr />
                        <div className="py-0 text-right">
                            <Button
                                className="btn-icon btn-3"
                                color="secondary"
                                type="button"
                                disabled={loading}
                                onClick={handleReset}
                            >
                                <i className="fa-regular fa-trash-can"></i>
                                <span className="btn-inner--text m-0">Clear</span>
                            </Button>

                            <Button className="btn-icon btn-3" color="success" type="submit" disabled={loading}>
                                <span className="btn-inner--text m-0">
                                    {loading
                                        ? (<><Spinner size="sm">Waiting...</Spinner><span> Waiting...</span></>)
                                        : (<><i className="fa-solid fa-floppy-disk"></i> Save</>)
                                    }
                                </span>
                            </Button>
                        </div>
                    </form>
                </CardBody>
            </Card>
            <div>
                <ToastContainer />
            </div>
        </>
    )
}

export default AssignmentAdd;