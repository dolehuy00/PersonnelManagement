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
    InputGroup,
    Spinner,
} from "reactstrap";

// core component
import { useState, useRef, useEffect } from "react";

// react toastify component
import { Slide, ToastContainer, toast } from 'react-toastify';

// custom component
import DatePickerWithTooltip from "components/DateTimePickers/DatePickerWithTooltip.js";
import ImagePopup from "components/Popups/ViewImage";

// hooks
import { useAddEmployee } from "hooks/UseEmployeeApi.js";
import SearchWithPopup from "components/Popups/SearchWithPopup";
import { useSearchDepartment } from "hooks/UseDepartmentApi.js";

const EmployeeAdd = ({ onCancel }) => {
    // state constant
    const [dataBody, setDataBody] = useState({});
    const [dateOfBirth, setDateOfBirth] = useState("");
    const [startDate, setStartDate] = useState("");
    const [formValueIsValid, setFormValueIsValid] = useState(false);
    const [imageLink, setImageLink] = useState("");

    // state variable
    let arrSetValueInputDepartment = useState([]);

    // ref constant
    const formRef = useRef(null);
    const dateOfBirthRef = useRef(null);
    const startDateRef = useRef(null);

    // request data
    const { data, loading, error } = useAddEmployee(dataBody);

    // handle change date of birth input
    const handleDateOfBirthChange = (date, isValid) => {
        setDateOfBirth(date);
        setFormValueIsValid(isValid)
    }

    // handle change start date of birth input
    const handleStartDateChange = (date, isValid) => {
        setStartDate(date);
        setFormValueIsValid(isValid)
    }

    // handle select image employee 
    const handleImageChange = (event) => {
        const file = event.target.files[0];
        if (file) {
            const reader = new FileReader();

            // Đọc file dưới dạng Data URL (base64)
            reader.onload = (e) => {
                setImageLink(e.target.result);
            };

            reader.readAsDataURL(file); // Đọc file
        }
    }

    // effect show toast response add employee request
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

    // handle submit form data
    const handleSubmit = (event) => {
        event.preventDefault();
        const formData = new FormData(formRef.current);
        const data = Object.fromEntries(formData.entries());
        setDataBody(data);
    };

    // handle reset form data
    const handleReset = () => {
        dateOfBirthRef.current.state.inputValue = '';
        startDateRef.current.state.inputValue = '';
        setDateOfBirth('');
        setStartDate('');
        formRef.current.reset();
    };

    // render
    return (
        <>
            <Card className="bg-secondary shadow">
                <CardHeader className="bg-white border-0">
                    <Row className="align-items-center">
                        <Col xs="8">
                            <h3 className="mb-0">Add Employee</h3>
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
                                            htmlFor="input-fullname"
                                        >
                                            Fullname
                                        </label>
                                        <Input
                                            className="form-control-alternative"
                                            id="input-fullname"
                                            type="text"
                                            name="fullname"
                                            required
                                        />
                                    </FormGroup>
                                </Col>
                                <Col lg="6">
                                    <FormGroup>
                                        <label
                                            className="form-control-label"
                                            htmlFor="input-date-of-birth"
                                        >
                                            Date of Birth
                                        </label>
                                        <InputGroup className="input-group-alternative">
                                            <DatePickerWithTooltip
                                                ref={dateOfBirthRef}
                                                value={dateOfBirth || ""}
                                                dateFormat="YYYY-MM-DD"
                                                className="form-control-alternative form-control"
                                                name="dateOfBirth"
                                                required="required"
                                                placeholder="YYYY-MM-DD"
                                                id="dateOfBirth"
                                                onChange={(date, isValid) => handleDateOfBirthChange(date, isValid)}
                                            />
                                        </InputGroup>
                                    </FormGroup>
                                </Col>
                            </Row>
                            <Row>
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
                                            type="text"
                                            name="position"
                                            required
                                        />
                                    </FormGroup>
                                </Col>
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
                                            type="number"
                                            step="0.01"
                                            name="basicSalary"
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
                                            htmlFor="input-status"
                                        >
                                            Department
                                        </label>
                                        <SearchWithPopup
                                            titleModal="Search Department (Name)"
                                            nameInput="departmentId"
                                            searchApiFunc={useSearchDepartment}
                                            propertyInDataToViewSearch={
                                                [
                                                    { text: "ID: ", property: "id" },
                                                    { text: " ~ ", property: "name" }
                                                ]
                                            }
                                            propertyInDataToViewDisableInput={["id", "name"]}
                                            propertyInDataToSetRealInput="id"
                                            deboundTimeOut={1500}
                                            arraySetValueInput={arrSetValueInputDepartment}
                                        />
                                    </FormGroup>
                                </Col>
                                <Col lg="6">
                                    <FormGroup>
                                        <label
                                            className="form-control-label"
                                            htmlFor="input-image-employee"
                                        >
                                            Image
                                        </label>
                                        <Row>
                                            <Col>
                                                <Input
                                                    className="form-control-alternative input-file"
                                                    id="input-image-employee"
                                                    type="file"
                                                    name="fileImage"
                                                    accept="image/*"
                                                    onChange={handleImageChange}
                                                />
                                            </Col>
                                            <Col lg={{ size: "auto" }} className="pl-0">
                                                <ImagePopup imageSrc={imageLink} imageAlt="Image Employee" />
                                            </Col>
                                        </Row>
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
                                            htmlFor="input-address"
                                        >
                                            Address
                                        </label>
                                        <Input
                                            className="form-control-alternative"
                                            id="input-address"
                                            type="text"
                                            name="address"
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
                                            htmlFor="input-date-of-birth"
                                        >
                                            Start Date
                                        </label>
                                        <InputGroup className="input-group-alternative">
                                            <DatePickerWithTooltip
                                                ref={startDateRef}
                                                value={startDate || ""}
                                                dateFormat="YYYY-MM-DD"
                                                className="form-control-alternative form-control"
                                                name="startDate"
                                                required="required"
                                                placeholder="YYYY-MM-DD"
                                                id="startDate"
                                                onChange={(date, isValid) => handleStartDateChange(date, isValid)}
                                            />
                                        </InputGroup>
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

                            <Button className="btn-icon btn-3" color="success" type="submit" disabled={loading || !formValueIsValid}>
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

export default EmployeeAdd;