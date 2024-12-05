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
    Form,
    InputGroup,
} from "reactstrap";

// core components
import { useState, useEffect } from "react";
import { useLocation } from 'react-router-dom';
import { Slide, ToastContainer, toast } from 'react-toastify';

// component
import Header from "components/Headers/Header.js";
import LoadingOrError from "components/Notifications/LoadingOrError.js";
import SearchWithPopup from "components/Popups/SearchWithPopup.js";
import DatePickerWithTooltip from "components/DateTimePickers/DatePickerWithTooltip.js";

// custom hooks
import {
    useGetOneSalaryHistory,
    useEditSalaryHistory
} from "hooks/UseSalaryHistoryApi.js";
import { useSearchEmployee } from "hooks/UseEmployeeApi.js";

const ViewSalaryHistory = () => {
    // search params
    const location = useLocation();
    const searchParams = new URLSearchParams(location.search);
    const salaryHistoryId = searchParams.get("id");
    const mode = searchParams.get("mode")

    // state constant
    const [formValues, setFormValues] = useState({});
    const [formValuesDefault, setFormValuesDefault] = useState({});
    const [viewMode, setViewMode] = useState(mode || "");
    const [formValueIsValid, setFormValueIsValid] = useState(false);
    const [dataEdit, setDataEdit] = useState({});
    const [totalSalary, setTotalSalary] = useState(0);

    // state variable
    let arrSetValueInput = useState([]);

    // request data
    const { data: dataGetSalary, loading: loadingGetSalary, error: errorGetSalary } = useGetOneSalaryHistory(salaryHistoryId);
    const { data: dataEditResponse, loading: loadingEdit, error: errorEdit } = useEditSalaryHistory(dataEdit);

    // effect for get object data
    useEffect(() => {
        if (Object.keys(dataGetSalary).length > 0) {
            setDataForm(dataGetSalary);
            setFormValuesDefault(dataGetSalary)
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [dataGetSalary]);

    useEffect(() => {
        try {
            let total = parseFloat(formValues.basicSalary) + parseFloat(formValues.bonusSalary) - parseFloat(formValues.penalty) - parseFloat(formValues.tax)
            setTotalSalary(total.toFixed(3));
        } catch {
            setTotalSalary(0);
        }
    }, [formValues.basicSalary, formValues.bonusSalary, formValues.penalty, formValues.tax]);

    // effect error edit to show toast error
    useEffect(() => {
        if (errorEdit) {
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
    }, [errorEdit]);

    // effect success edit to show toast edit success
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

    // handle submit form
    const handleSubmit = (event) => {
        event.preventDefault();
        setDataEdit(formValues);
        setFormValueIsValid(false);
    };

    // handle click cancel edit mode
    const handleCancelEdit = (event) => {
        event.preventDefault();
        setViewMode("view");
        setDataForm(formValuesDefault);
        setFormValueIsValid(false);
    }

    // handle date input change
    const handleDateChange = (date, isValid, name) => {
        if (isValid) {
            setFormValues((prevValues) => ({
                ...prevValues,
                [name]: date.format("YYYY-MM-DD"),
            }));
            setFormValueIsValid(true);
        } else {
            setFormValues((prevValues) => ({
                ...prevValues,
                [name]: "",
            }));
            setFormValueIsValid(false);
        }
    }

    // handle select dropdown and choose employee
    const handleSelectEmployeeChange = (e) => {
        const { name, value } = e.target;
        const otherData = e.otherData;
        setFormValues((prevValues) => ({
            ...prevValues,
            [name]: value,
            basicSalary: otherData.basicSalary,
        }));
        setFormValueIsValid(true);
    };

    // handle common input change
    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setFormValues((prevValues) => ({
            ...prevValues,
            [name]: value,
        }));
        setFormValueIsValid(true);
    };


    // handle click edit button
    const handleClickEdit = (event) => {
        event.preventDefault();
        setViewMode("edit");
    }

    // check edit mode
    const isEditMode = () => {
        return viewMode === "edit";
    };

    // set data to dataForm constant
    const setDataForm = (data) => {
        if (data.results) {
            const result = data.results[0];
            setFormValues({
                id: result.id || -1,
                basicSalary: result.basicSalary || "",
                bonusSalary: result.bonusSalary || "",
                employeeId: result.employeeId || "",
                detail: result.detail || "",
                penalty: result.penalty || "",
                tax: result.tax || "",
                date: result.date || "",
            });
            arrSetValueInput[0]({ fullname: result.employeeName, id: result.employeeId })
        }
    }

    // render loading data
    if (loadingGetSalary) return (
        <>
            <Header />
            <LoadingOrError status="loading" />
        </>
    );

    // render error load data
    if (errorGetSalary) return (
        <>
            <Header />
            <LoadingOrError status="error" />
        </>
    );

    // render data
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
                                            {isEditMode() ? "Edit SalaryHistory" : "View SalaryHistory"}
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
                                {dataGetSalary.results
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
                                                                Salary ID
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
                                                                disabled={!isEditMode()}
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
                                                                disabled={!isEditMode()}
                                                                value={formValues.basicSalary || ""}
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
                                                                disabled={!isEditMode()}
                                                                value={formValues.bonusSalary || ""}
                                                                name="bonusSalary"
                                                                onChange={handleInputChange}
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
                                                                disabled={!isEditMode()}
                                                                value={formValues.penalty || ""}
                                                                onChange={handleInputChange}
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
                                                                disabled={!isEditMode()}
                                                                value={formValues.tax || ""}
                                                                onChange={handleInputChange}
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
                                                                    value={formValues.date || ""}
                                                                    dateFormat="YYYY-MM-DD"
                                                                    className="form-control-alternative form-control"
                                                                    name="date"
                                                                    required="required"
                                                                    placeholder="YYYY-MM-DD"
                                                                    disabled={!isEditMode()}
                                                                    id="date"
                                                                    onChange={(date, isValid) => handleDateChange(date, isValid, "date")}
                                                                />
                                                            </InputGroup>
                                                        </FormGroup>
                                                    </Col>
                                                </Row>
                                            </div>
                                            <div className="mx-4 py-0 text-right">
                                                <h2><b>Total:</b> {totalSalary}</h2>
                                            </div>
                                            <hr className="my-3" />
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
                                                                value={formValues.detail || ""}
                                                                disabled={!isEditMode()}
                                                                onChange={handleInputChange}
                                                                required
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

export default ViewSalaryHistory;