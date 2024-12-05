// reactstap component
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
    InputGroup
} from "reactstrap";

// core component
import { useState, useRef, useEffect } from "react";

// react toastify component
import { Slide, ToastContainer, toast } from 'react-toastify';

// custom component
import SearchWithPopup from "components/Popups/SearchWithPopup.js";
import DatePickerWithTooltip from "components/DateTimePickers/DatePickerWithTooltip.js";

// hooks
import { useAddSalaryHistory } from "hooks/UseSalaryHistoryApi.js";
import { useSearchEmployee } from "hooks/UseEmployeeApi.js";

const SalaryHistoryAdd = ({ onCancel }) => {
    // state constant
    const [dataBody, setDataBody] = useState({});
    const [date, setDate] = useState("");
    const [formValueIsValid, setFormValueIsValid] = useState(false);
    const [basicSalary, setBasicSalary] = useState("");

    // ref constant
    const formRef = useRef(null);
    const dateRef = useRef(null);

    // state variable
    let arrSetValueInput = useState([]);

    // request data
    const { data, loading, error } = useAddSalaryHistory(dataBody);

    // effect show toast response save object
    useEffect(() => {
        if (error) {
            toast.error(error.response.data.messages[0] || "Save failed, an error occurred, please try again later!", {
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

    // handle date input change
    const handleDateChange = (date, isValid) => {
        setDate(date);
        setFormValueIsValid(isValid);
    }

    // handle submit form
    const handleSubmit = (event) => {
        event.preventDefault();
        const formData = new FormData(formRef.current);
        const data = Object.fromEntries(formData.entries());
        setDataBody(data);
    };

    // handle reset form
    const handleReset = () => {
        // clear date input
        setDate("");
        dateRef.current.state.inputValue = "";
        // clear common input
        formRef.current.reset();
        // clear choose employee input
        arrSetValueInput[0]({ fullname: "", id: "" })
        // clear basic salary input
        setBasicSalary("");
    };

    // handle select choose employee
    const handleSelectEmployeeChange = (e) => {
        const otherData = e.otherData;
        setBasicSalary(otherData.basicSalary)
    };

    // render
    return (
        <>
            <Card className="bg-secondary shadow">
                <CardHeader className="bg-white border-0">
                    <Row className="align-items-center">
                        <Col xs="8">
                            <h3 className="mb-0">Create Salary</h3>
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
                                            htmlFor="input-status"
                                        >
                                            Employee
                                        </label>
                                        <SearchWithPopup
                                            titleModal="Search Employee (Name or ID)"
                                            nameInput="employeeId"
                                            searchApiFunc={useSearchEmployee}
                                            propertyInDataToViewSearch={
                                                [
                                                    { text: "ID: ", property: "id" },
                                                    { text: " ~ ", property: "fullname" },
                                                    { text: " ~ ", property: "dateOfBirth" },
                                                ]
                                            }
                                            propertyInDataToViewDisableInput={["id", "fullname"]}
                                            propertyInDataToSetRealInput="id"
                                            required="required"
                                            deboundTimeOut={1500}
                                            arraySetValueInput={arrSetValueInput}
                                            propertyPassedToOtherDataEventOnChange={["basicSalary"]}
                                            onChange={handleSelectEmployeeChange}
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
                                            value={basicSalary || ""}
                                            onChange={(e) => e.preventDefault()}
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
                                            htmlFor="input-bonus-salary"
                                        >
                                            Bonus Salary
                                        </label>
                                        <Input
                                            className="form-control-alternative"
                                            id="input-bonus-salary"
                                            type="number"
                                            step="0.01"
                                            name="bonusSalary"
                                            required
                                        />
                                    </FormGroup>
                                </Col>
                                <Col lg="6">
                                    <FormGroup>
                                        <label
                                            className="form-control-label"
                                            htmlFor="input-penalty-salary"
                                        >
                                            Penalty
                                        </label>
                                        <Input
                                            className="form-control-alternative"
                                            id="input-penalty-salary"
                                            type="number"
                                            step="0.01"
                                            name="penalty"
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
                                            htmlFor="input-tax-salary"
                                        >
                                            Tax
                                        </label>
                                        <Input
                                            className="form-control-alternative"
                                            id="input-tax-salary"
                                            type="number"
                                            step="0.01"
                                            name="tax"
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
                                            Date
                                        </label>
                                        <InputGroup className="input-group-alternative">
                                            <DatePickerWithTooltip
                                                ref={dateRef}
                                                value={date || ""}
                                                dateFormat="YYYY-MM-DD"
                                                className="form-control-alternative form-control"
                                                name="date"
                                                required="required"
                                                placeholder="YYYY-MM-DD"
                                                id="date"
                                                onChange={(date, isValid) => handleDateChange(date, isValid)}
                                            />
                                        </InputGroup>
                                    </FormGroup>
                                </Col>
                            </Row>
                        </div>
                        <div className="pl-lg-4">
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

export default SalaryHistoryAdd;