import {
    Button,
    Card,
    CardHeader,
    CardBody,
    FormGroup,
    Input,
    Row,
    Col,
    InputGroup,
    Spinner,
} from "reactstrap";
import { useState, useRef, useEffect } from "react";
import Datetime from "react-datetime";
import { Slide, ToastContainer, toast } from 'react-toastify';
import { useAddDepartment } from "hooks/UseDepartmentApi.js";

import SearchWithPopup from "components/Popups/SearchWithPopup.js";
import { useSearchEmployee } from "hooks/UseEmployeeApi.js";

const DepartmentAdd = ({ onCancel }) => {
    const [dataBody, setDataBody] = useState({});
    const [dateOfBirth, setDateOfBirth] = useState("");
    const [startDate, setStartDate] = useState("");
    const formRef = useRef(null);
    const dateOfBirthRef = useRef(null);
    const startDateRef = useRef(null);
    let arrSetValueInput = useState([]);

    const { data, loading, error } = useAddDepartment(dataBody);

    const handleDateOfBirthChange = (date) => setDateOfBirth(date);
    const handleStartDateChange = (date) => setStartDate(date);


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
    }, [error, data]);

    const handleSubmit = (event) => {
        event.preventDefault();
        const formData = new FormData(formRef.current);
        const data = Object.fromEntries(formData.entries());
        setDataBody(data);
    };

    const handleReset = () => {
        formRef.current.reset();
    };

    return (
        <>
            <Card className="bg-secondary shadow">
                <CardHeader className="bg-white border-0">
                    <Row className="align-items-center">
                        <Col xs="8">
                            <h3 className="mb-0">Add Department</h3>
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
                                            Department Name
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
                                            htmlFor="input-taskDetail"
                                        >
                                            Main Task
                                        </label>
                                        <Input
                                            className="form-control-alternative"
                                            id="input-taskDetail"
                                            type="text"
                                            name="taskDetail"
                                            required
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
                                            htmlFor="input-leaderId"
                                        >
                                            Leader
                                        </label>
                                        <SearchWithPopup
                                            titleModal="Search Employee (Name or ID)"
                                            nameInput="leaderId"
                                            searchApiFunc={useSearchEmployee}
                                            propertyInDataToViewSearch={
                                                [
                                                    { text: "ID: ", property: "id" },
                                                    { text: " ~ ", property: "fullname" }
                                                ]
                                            }
                                            propertyInDataToViewDisableInput={["id", "fullname"]}
                                            propertyInDataToSetRealInput="id"
                                            required="required"
                                            deboundTimeOut={1500}
                                            arraySetValueInput = {arrSetValueInput}
                                        />
                                    </FormGroup>
                                </Col>
                                <Col lg="6">
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
                                            required
                                        >
                                            <option value="Active">Active</option>
                                            <option value="Lock">Lock</option>
                                        </select>
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

export default DepartmentAdd;