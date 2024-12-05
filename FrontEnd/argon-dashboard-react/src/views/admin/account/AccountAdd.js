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
} from "reactstrap";
// core component
import { useState, useRef, useEffect } from "react";
// react toastify component
import { Slide, ToastContainer, toast } from 'react-toastify';
// custom component
import SearchWithPopup from "components/Popups/SearchWithPopup.js";
// hooks
import { useAddAccount } from "hooks/UseAccountApi.js";
import { useGetAllRole } from "hooks/UseRoleApi.js";
import { useSearchEmployee } from "hooks/UseEmployeeApi.js";

const AccountAdd = ({ onCancel }) => {
    const [dataBody, setDataBody] = useState({});
    const formRef = useRef(null);
    let arrSetValueInput = useState([]);

    const { data, loading, error } = useAddAccount(dataBody);
    const {
        data: dataRole,
        loading: loadingRole,
        requestGetAllRole
    } = useGetAllRole();

    useEffect(() => {
        requestGetAllRole()
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [])

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

    const handleSubmit = (event) => {
        event.preventDefault();
        const formData = new FormData(formRef.current);
        const data = Object.fromEntries(formData.entries());
        setDataBody(data);
    };

    const handleReset = () => {
        formRef.current.reset();
        arrSetValueInput[0]({fullname: "", id: ""})     
    };

    return (
        <>
            <Card className="bg-secondary shadow">
                <CardHeader className="bg-white border-0">
                    <Row className="align-items-center">
                        <Col xs="8">
                            <h3 className="mb-0">Create Account</h3>
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
                                            htmlFor="input-email"
                                        >
                                            Email
                                        </label>
                                        <Input
                                            className="form-control-alternative"
                                            id="input-email"
                                            type="text"
                                            name="email"
                                            required
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
                                                required
                                            >
                                                <option value="">Select Role</option>
                                                {dataRole
                                                    ? dataRole.results?.map(item => (
                                                        <option key={`option-role-${item.id}`} value={item.id}>{item.name}</option>
                                                    ))
                                                    : ""
                                                }
                                            </select>

                                        </div>
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
                                        <select
                                            id="status"
                                            name="status"
                                            className="form-control"
                                            required
                                        >
                                            <option value="">Select Status</option>
                                            <option value="Active">Active</option>
                                            <option value="Lock">Lock</option>
                                        </select>
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
                                            arraySetValueInput = {arrSetValueInput}
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

export default AccountAdd;