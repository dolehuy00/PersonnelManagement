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
    useGetOneAccount,
    useEditAccount,
    useChangeStatusAccount
} from "hooks/UseAccountApi.js";
import { useGetAllRole } from "hooks/UseRoleApi.js";
import { useSearchEmployee } from "hooks/UseEmployeeApi.js";

const ViewAccount = () => {
    // search params
    const location = useLocation();
    const searchParams = new URLSearchParams(location.search);
    const accountId = searchParams.get("id");
    const mode = searchParams.get("mode")

    // state constant
    const [formValues, setFormValues] = useState({});
    const [formValuesDefault, setFormValuesDefault] = useState({});
    const [viewMode, setViewMode] = useState(mode || "");
    const [formValueIsValid, setFormValueIsValid] = useState(false);
    const [dataEdit, setDataEdit] = useState({});
    const [statusValue, setStatusValue] = useState("");

    // state variable
    let arrSetValueInput = useState([]);

    // request data
    const { data: dataRole, loading: loadingRole, requestGetAllRole } = useGetAllRole();
    const { data: dataGetAcc, loading: loadingGetAcc, error: errorGetAcc } = useGetOneAccount(accountId);
    const { data: dataEditResponse, loading: loadingEdit, error: errorEdit } = useEditAccount(dataEdit);
    const { data: dataLockResponse, loading: loadingLock, error: errorLock, request: requestLock } = useChangeStatusAccount();

    // effect for get all role for dropdown choose role
    useEffect(() => {
        requestGetAllRole()
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [])

    // effect for get object data
    useEffect(() => {
        if (Object.keys(dataGetAcc).length > 0) {
            setDataForm(dataGetAcc);
            setFormValuesDefault(dataGetAcc)
            setStatusValue(dataGetAcc.results[0].status || "")
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [dataGetAcc]);

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

    // effect error lock object to show error toast
    useEffect(() => {
        if (errorLock) {
            toast.error(`Change status fail, ${errorLock.response?.data?.messages[1]}`, {
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
    }, [errorLock]);

    // effect success lock object to show success message
    useEffect(() => {
        if (dataLockResponse.status === 200) {
            setStatusValue(dataLockResponse?.messages[1])
            toast.success('Change status successfully', {
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
    }, [dataLockResponse]);

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

    // handle select dropdown and choose employee
    const handleSelectChange = (e) => {
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

    // handle click button lock/unlock status
    const handleLock = () => {
        requestLock(accountId, statusValue)
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
                email: result.email || "",
                roleId: result.roleId || -1,
                employeeId: result.employeeId || -1
            });
            arrSetValueInput[0]({ fullname: result.employeeName, id: result.employeeId })
        }
    }

    // render loading data
    if (loadingGetAcc) return (
        <>
            <Header />
            <LoadingOrError status="loading" />
        </>
    );

    // render error load data
    if (errorGetAcc) return (
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
                                            {isEditMode() ? "Edit Account" : "View Account"}
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
                                {dataGetAcc.results
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
                                                                Account ID
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
                                                                htmlFor="input-email"
                                                            >
                                                                Email
                                                            </label>
                                                            <Input
                                                                className="form-control-alternative"
                                                                id="input-email"
                                                                type="text"
                                                                name="email"
                                                                value={formValues.email || ""}
                                                                required
                                                                disabled
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
                                                            <div className="position-relative">
                                                                {loadingRole && (
                                                                    <div className="overlay-spinner-table-loading">
                                                                        <Spinner size="sm" color="info"></Spinner>
                                                                    </div>
                                                                )}
                                                                <select
                                                                    id="roleId"
                                                                    name="roleId"
                                                                    className="form-control"
                                                                    value={formValues.roleId || ""}
                                                                    required
                                                                    disabled={!isEditMode()}
                                                                    onChange={handleSelectChange}
                                                                >
                                                                    <option value="">Select Role</option>
                                                                    {dataRole
                                                                        ? dataRole.results?.map(item => (
                                                                            <option
                                                                                key={`option-role-${item.id}`}
                                                                                value={item.id}
                                                                            >
                                                                                {item.name}
                                                                            </option>
                                                                        ))
                                                                        : ""
                                                                    }
                                                                </select>
                                                            </div>
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
                                                                Status
                                                            </label>
                                                            <Row>
                                                                <Col>
                                                                    <Input
                                                                        className="form-control-alternative"
                                                                        id="input-status"
                                                                        type="text"
                                                                        value={statusValue}
                                                                        required
                                                                        disabled="disabled"
                                                                    />
                                                                </Col>
                                                                {viewMode === "edit"
                                                                    ? (<>
                                                                        <Col lg={{ size: "auto" }} className="pl-0">
                                                                            <Button
                                                                                color={statusValue === "Active" ? "warning" : "info"}
                                                                                type="button"
                                                                                disabled={loadingLock}
                                                                                onClick={handleLock}
                                                                            >
                                                                                {loadingLock
                                                                                    ? (<Spinner color="primary" size="sm"> </Spinner>)
                                                                                    : statusValue === "Active" ? "Lock" : "Unlock"
                                                                                }
                                                                            </Button>
                                                                        </Col>
                                                                    </>)
                                                                    : ""
                                                                }
                                                            </Row>
                                                        </FormGroup>
                                                    </Col>
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
                                                                onChange={handleSelectChange}
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

export default ViewAccount;