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
    Table,
    FormGroup,
    InputGroup,
    Spinner,
    Form
} from "reactstrap";
// core components
import { useState, useRef, useEffect } from "react";
import { useLocation } from 'react-router-dom';
import { Slide, ToastContainer, toast } from 'react-toastify';
import SearchWithPopup from "components/Popups/SearchWithPopup.js";
// component
import Header from "components/Headers/Header.js";
import LoadingOrError from "components/Notifications/LoadingOrError.js";
import DatePickerWithTooltip from "components/DateTimePickers/DatePickerWithTooltip.js";
import { useEditManyDeptAssignment } from "hooks/UseDeptAssignmentApi.js";
import { useSearchDepartment } from "hooks/UseDepartmentApi.js";
import { useGetDeptsAssignmentsByProject } from "hooks/UseDeptAssignmentApi.js";
// custom hooks
import {
    useGetOneProject,
    useEditProject,
    useChangeStatusProject
} from "hooks/UseProjectApi.js";

const ViewProject = () => {
    // search params
    const location = useLocation();
    const searchParams = new URLSearchParams(location.search);
    const projectId = searchParams.get("id");
    const mode = searchParams.get("mode")
    let arrSetValueInput = useState([]);
    const formAddDepartmentRef = useRef(null);


    // state
    const [formValues, setFormValues] = useState({});
    const [formValuesDefault, setFormValuesDefault] = useState({});
    const [viewMode, setViewMode] = useState(mode || "");
    const [formValueIsValid, setFormValueIsValid] = useState(false);
    const [dataEdit, setDataEdit] = useState({});
    const [statusValue, setStatusValue] = useState("");
    const [dataBodyDeptAssignments, setDataBodyDeptAssignments] = useState([]);
    const { dataDeptAssignment, loadingDeptAssignment, errorDeptAssignment }  = useEditManyDeptAssignment(projectId, dataBodyDeptAssignments);
    const [deptAssignments, setDeptAssignments] = useState([]);
    const [errorDepartment, setErrorDepartment] = useState("");
    const [firstSetDeptAssignment, setFirstSetDeptAssignment] = useState(true);
    // request data    
    const { data: dataGetProj, loading: loadingGetProj, error: errorGetProj } = useGetOneProject(projectId);
    const { data: dataEditResponse, loading: loadingEdit, error: errorEdit } = useEditProject(dataEdit);
    const { data: dataGetDeptAssignment, loading: loadingGetDeptAssignment, error: errorGetDeptAssignment }  = useGetDeptsAssignmentsByProject(projectId);
    

    const addDeptAssignment = (projectId, departmentId, priotityLevel, mainTaskDetail) => {
        // Tạo một đối tượng deptAssignment mới với các tham số truyền vào
        const newDeptAssignment = {
          PriotityLevel: priotityLevel,
          mainTaskDetail: mainTaskDetail,
          projectId: projectId,
          departmentId: departmentId,
        };
      
        // Cập nhật mảng deptAssignments nếu departmentId chưa tồn tại
        setDeptAssignments((prevDeptAssignments) => {
          const isExisting = prevDeptAssignments.some(
            (assignment) => assignment.departmentId === departmentId
          );
          if (isExisting) {
            // Nếu đã tồn tại, không thêm và trả về danh sách hiện tại
            setErrorDepartment("Department Is Existing!")
            return prevDeptAssignments;
          }
          setErrorDepartment("")
          // Nếu chưa tồn tại, thêm mới vào danh sách
          return [...prevDeptAssignments, newDeptAssignment];
        });
      };

    const deleteDeptAssignment = (departmentId) => {
        setDeptAssignments((prevAssignments) =>
          prevAssignments.filter((assignment) => assignment.departmentId !== departmentId)
        );
    };


    const handleAddDepartment = (event) => {
        event.preventDefault();
        const formAddDepartmentData = new FormData(formAddDepartmentRef.current);
        const data = Object.fromEntries(formAddDepartmentData.entries());
        addDeptAssignment(projectId, data.departmentId, data.priotityLevel, data.mainTaskDetail);
    };

    const handleSubmitAddDeptDepartment = (event) => {
        event.preventDefault();
        try {
            setDataBodyDeptAssignments(deptAssignments);
            toast.success('Add successfully:', {
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
        } catch (error){
            console.log(error)
            toast.error("Save Assignment failed, an error occurred, please try again later!", {
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
    };


    // effect
    useEffect(() => {
        if (Object.keys(dataGetProj).length > 0) {
            setDataForm(dataGetProj);
            setFormValuesDefault(dataGetProj)
            console.log(formValues)
            setStatusValue(dataGetProj.results[0].status || "")            
        }
    }, [dataGetProj]);

    useEffect(() => {
        if (dataGetDeptAssignment.status === 200 && firstSetDeptAssignment) {
            setDeptAssignments(dataGetDeptAssignment.results)
            setFirstSetDeptAssignment(false);
        }
    })

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


    // handle function
    const handleSubmit = (event) => {
        event.preventDefault();
        setDataEdit(formValues);
        setFormValueIsValid(false);
    };

    const handleCancelEdit = (event) => {
        event.preventDefault();
        setViewMode("view");
        setDataForm(formValuesDefault);
        setFormValueIsValid(false);
    }

    const handleClickEdit = (event) => {
        event.preventDefault();
    }

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setFormValues((prevValues) => ({
            ...prevValues,
            [name]: value,
        }));
        setFormValueIsValid(true);
    };

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
    };

    // other function
    const isEditMode = () => {
        return viewMode === "edit";
    };

    const setDataForm = (data) => {
        if (data.results) {
            const result = data.results[0];
            setFormValues({
                id: result.id || -1,
                name: result.name || "",
                startDate: result.startDate || "",
                duration: result.duration || "",
                detail: result.detail || ""
            });
        }
    }


    // render
    if (loadingGetProj) return (
        <>
            <Header />
            <LoadingOrError status="loading" />
        </>
    );
    if (errorGetProj) return (
        <>
            <Header />
            <LoadingOrError status="error" />
        </>
    );
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
                                            {isEditMode() ? "Edit Project" : "View Project"}
                                        </h3>
                                    </Col>
                                    <Col className="text-right" xs="4">
                                        {isEditMode()
                                            ? (
                                                <Button
                                                    color="primary"
                                                    href={`?id=${projectId}`}
                                                    size="sm"
                                                >
                                                    Cancel
                                                </Button>
                                            )
                                            : (
                                                    <Button
                                                        color="primary"
                                                        size="sm"
                                                        href={`?id=${projectId}&mode=edit`}
                                                    >
                                                        Edit
                                                    </Button>
                                                
                                            )
                                        }
                                    </Col>
                                </Row>
                            </CardHeader>
                            <CardBody>
                                {dataGetProj.results
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
                                                                Project ID
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
                                                                Project Name
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
                                                                htmlFor="input-status"
                                                            >
                                                                Status
                                                            </label>
                                                            <select
                                                                id="status"
                                                                name="status"
                                                                className="form-control"
                                                                required
                                                                value={formValues.status || "Not Started"}
                                                                onChange={handleInputChange}
                                                                disabled={!isEditMode()}
                                                            >
                                                                <option value="Not Started">Not Started</option>
                                                                <option value="In Progress">In Progress</option>
                                                                <option value="On Hold">On Hold</option>
                                                                <option value="Completed">Completed</option>
                                                                <option value="Cancelled">Cancelled</option>
                                                            </select>
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
                                                                Start Date
                                                            </label>
                                                            <InputGroup className="input-group-alternative">
                                                                <DatePickerWithTooltip
                                                                    value={formValues.startDate || ""}
                                                                    dateFormat="YYYY-MM-DD"
                                                                    className="form-control-alternative form-control"
                                                                    name="startDate"
                                                                    required="required"
                                                                    placeholder="YYYY-MM-DD"
                                                                    disabled={!isEditMode()}
                                                                    id="startDate"
                                                                    onChange={(date, isValid) => handleDateChange(date, isValid, "startDate")}
                                                                />
                                                            </InputGroup>
                                                        </FormGroup>
                                                    </Col>
                                                    <Col lg="6">
                                                        <FormGroup>
                                                            <label
                                                                className="form-control-label"
                                                                htmlFor="input-duration"
                                                            >
                                                                Duration
                                                            </label>
                                                            <InputGroup className="input-group-alternative">
                                                                <DatePickerWithTooltip
                                                                    value={formValues.duration || ""}
                                                                    dateFormat="YYYY-MM-DD"
                                                                    className="form-control-alternative form-control"
                                                                    name="duration"
                                                                    required="required"
                                                                    placeholder="YYYY-MM-DD"
                                                                    disabled={!isEditMode()}
                                                                    id="duration"
                                                                    onChange={(date, isValid) => handleDateChange(date, isValid, "duration")}
                                                                />
                                                            </InputGroup>
                                                        </FormGroup>
                                                    </Col>                                                    
                                                </Row>
                                                <Row>
                                                    <Col md="12">
                                                        <FormGroup>
                                                            <label
                                                                className="form-control-label"
                                                                htmlFor="input-detail"
                                                            >
                                                                Project detail
                                                            </label>
                                                            <Input
                                                                className="form-control-alternative"
                                                                id="input-detail"
                                                                type="text"
                                                                name="detail"
                                                                value={formValues.detail || ""}
                                                                onChange={handleInputChange}
                                                                required
                                                                disabled={!isEditMode()}
                                                            />
                                                        </FormGroup>
                                                    </Col>
                                                </Row>                                             
                                            </div>
                                            
                                            <hr />
                                            {isEditMode()
                                                ? (
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
                                                )
                                                : ""
                                            }
                                        </Form>
                                    </>)
                                    : ""}
                            </CardBody>
                        </Card>

                        <Card className={`bg-secondary shadow`} id="formAddDeptAssignment">
                            <CardHeader className="bg-white border-0">
                                <Row className="align-items-center">
                                    <Col xs="8">
                                        <h3 className={`mb-0 ${mode=="edit" ? 'd-none' : ''}`}>List Department Assignment</h3>
                                        <h3 className={`mb-0 ${mode!="edit" ? 'd-none' : ''}`}>Edit Department For Project</h3>
                                    </Col>                                    
                                </Row>
                            </CardHeader>
                            <CardBody>
                                <form ref = {formAddDepartmentRef} onSubmit={handleAddDepartment}>
                                    <div className={`pl-lg-4 ${mode!="edit" ? 'd-none' : ''}` }>
                                        <Row>
                                            <Col lg="6">
                                                <FormGroup>
                                                    <label
                                                        className="form-control-label"
                                                        htmlFor="input-departmentId"
                                                    >
                                                        Department
                                                    </label>
                                                    <SearchWithPopup
                                                        titleModal="Search Department (Name or ID)"
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
                                                        required="required"
                                                        deboundTimeOut={1500}
                                                        arraySetValueInput = {arrSetValueInput}
                                                    />
                                                    {errorDepartment && (
                                                        <div className="text-danger mt-2">{errorDepartment}</div> // Hiển thị lỗi chỉ khi Submit
                                                    )}
                                                </FormGroup>
                                            </Col>
                                            <Col lg="6">
                                                <FormGroup>
                                                    <label
                                                        className="form-control-label"
                                                        htmlFor="input-status"
                                                    >
                                                        Priotity Level
                                                    </label>
                                                    <select
                                                        id="priotityLevel"
                                                        name="priotityLevel"
                                                        className="form-control"
                                                        required
                                                    >
                                                        <option value="1">1</option>
                                                        <option value="2">2</option>
                                                        <option value="3">3</option>
                                                        <option value="4">4</option>
                                                        <option value="5">5</option>
                                                    </select>
                                                </FormGroup>
                                            </Col>                                
                                        </Row>     
                                        <Row>
                                            <Col md="12">
                                                <FormGroup>
                                                    <label
                                                        className="form-control-label"
                                                        htmlFor="input-detail"
                                                    >
                                                        Main Task Detail
                                                    </label>
                                                    <Input
                                                        className="form-control-alternative"
                                                        id="input-mainTaskDetail"
                                                        type="text"
                                                        name="mainTaskDetail"
                                                        required
                                                    />
                                                </FormGroup>
                                            </Col>
                                        </Row>                                  
                                    </div>
                                    <div className={`pl-lg-4 ${mode!="edit" ? 'd-none' : ''}` } style={{ marginBottom: "20px" }}>                            
                                        <Button className="btn-icon btn-3" color="success">
                                            <span className="btn-inner--text m-0">
                                                Add
                                            </span>
                                        </Button>
                                    </div>
                                    <Table className="align-items-center table-flush" responsive>
                                        <thead className="thead-light">
                                        <tr className="text-center">
                                            <th scope="col">Project Id</th>
                                            <th scope="col">Department Id</th>                                            
                                            <th scope="col">Priotity Level</th>
                                            <th scope="col">Main Task Detail</th>
                                            <th className={`${mode!="edit" ? 'd-none' : ''}`} scope="col">Delete</th>
                                        </tr>
                                        </thead>
                                        <tbody>                           
                                            {deptAssignments.map((assignment, index) => (
                                                <tr className="text-center" key={index}>
                                                    <td>{assignment.projectId}</td>
                                                    <td> <a href={`../department/view?id=${assignment.departmentId}`}>{assignment.departmentId}</a></td>                                                    
                                                    <td>{assignment.priotityLevel}</td>
                                                    <td>{assignment.mainTaskDetail}</td>
                                                    <td className={`${mode!="edit" ? 'd-none' : ''}`}> 
                                                        <Button className="btn-icon btn-3" color="" type="button"  onClick={() => deleteDeptAssignment(assignment.departmentId)}>
                                                            <span className="btn-inner--text m-0">
                                                                <i className="fas fa-trash" />
                                                            </span>
                                                        </Button>
                                                    </td>
                                                </tr>
                                            ))}
                                        </tbody>
                                    </Table>
                                    <hr />
                                    <div className={`py-0 text-right ${mode!="edit" ? 'd-none' : ''}`}>
                                        <Button className="btn-icon btn-3" color="success" onClick={handleSubmitAddDeptDepartment} type="button">
                                            <span className="btn-inner--text m-0">
                                                <><i className="fa-solid fa-floppy-disk"></i> Save DeptAssignment</>
                                            </span>
                                        </Button>
                                    </div>
                                </form>
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

export default ViewProject;