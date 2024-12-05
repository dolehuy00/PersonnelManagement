import {
    Button,
    Card,
    CardHeader,
    CardBody,
    FormGroup,
    Input,
    InputGroup,
    Spinner,
    Badge,
    Table,
    Row,
    Col,
    Alert
} from "reactstrap";
import { useState, useRef, useEffect } from "react";
import Datetime from "react-datetime";
import { Slide, ToastContainer, toast } from 'react-toastify';
import { useAddProject } from "hooks/UseProjectApi.js";
import { useAddManyDeptAssignment } from "hooks/UseDeptAssignmentApi.js";
import SearchWithPopup from "components/Popups/SearchWithPopup.js";
import { useSearchDepartment } from "hooks/UseDepartmentApi.js";

// custom component
import DatePickerWithTooltip from "components/DateTimePickers/DatePickerWithTooltip.js";

const ProjectAdd = ({ onCancel }) => {
    const [dataBody, setDataBody] = useState({});    
    const [startDate, setStartDate] = useState("");
    const [duration, setDuration] = useState("");    
    const formRef = useRef(null);
    const formAddDepartmentRef = useRef(null);
    const startDateRef = useRef(null);
    const durationRef = useRef(null);
    let arrSetValueInput = useState([]);
    const [deptAssignments, setDeptAssignments] = useState([]);
    const [dataBodyDeptAssignments, setDataBodyDeptAssignments] = useState([]);
    const [projectId, setProjectId] = useState(1);    
    const { data, loading, error } = useAddProject(dataBody);
    const { dataDeptAssignment, loadingDeptAssignment, errorDeptAssignment }  = useAddManyDeptAssignment(dataBodyDeptAssignments);
    const [isSubmittingAddDeptAssignment, setIsSubmittingAddDeptAssignment] = useState(false);

    const [errorDepartment, setErrorDepartment] = useState("");

    const handleStartDateChange = (date) => setStartDate(date);
    const handleDurationChange = (date) => setDuration(date);

    // Ẩn hiện các form
    const [isFormAddDeptAssignmentVisible, setFormAddDeptAssignmentVisible] = useState(false);
    const toggleForms = () => {
        setFormAddDeptAssignmentVisible(!isFormAddDeptAssignmentVisible);
    };


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
            //Sau khi thêm thành công Project sẽ cho thêm DeptAssignment Ẩn đi Form Project Và Show lên form deptAssignment
            toggleForms()  
            console.log(data.results[0]);
            setProjectId(data.results[0].id);
            toast.success('Add successfully: '+projectId, {
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
            setDeptAssignments([])
            toggleForms();
            setProjectId(0);
            handleReset();
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

    const handleReset = () => {
        formRef.current.reset();
    };

    return (
        <>
            <Card className={`bg-secondary shadow ${isFormAddDeptAssignmentVisible ? 'd-none' : ''}`} id="formAddProject" >
                <CardHeader className="bg-white border-0">
                    <Row className="align-items-center">
                        <Col xs="8">
                            <h3 className="mb-0">Add Project</h3>
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
                                            Project Name
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
                                            htmlFor="input-StartDate"
                                        >
                                            Start Date
                                        </label>
                                        <InputGroup className="input-group-alternative">
                                            <DatePickerWithTooltip
                                                ref={startDateRef}
                                                value={startDate || startDateRef}
                                                dateFormat="YYYY-MM-DD"
                                                className="form-control-alternative form-control"
                                                name="startDate"
                                                required="required"
                                                placeholder="YYYY-MM-DD"
                                                id="StartDate"
                                                onChange={(date, isValid) => handleStartDateChange(date, isValid)}
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
                                                ref={durationRef}
                                                value={duration || ""}
                                                dateFormat="YYYY-MM-DD"
                                                className="form-control-alternative form-control"
                                                name="duration"
                                                required="required"
                                                placeholder="YYYY-MM-DD"
                                                id="duration"
                                                onChange={(date, isValid) => handleDurationChange(date, isValid)}
                                            />
                                        </InputGroup>
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
                                            htmlFor="input-detail"
                                        >
                                            Detail
                                        </label>
                                        <Input
                                            className="form-control-alternative"
                                            id="input-detail"
                                            type="text"
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


            <Card className={`bg-secondary shadow ${!isFormAddDeptAssignmentVisible ? 'd-none' : ''}`} id="formAddDeptAssignment">
                <CardHeader className="bg-white border-0">
                    <Row className="align-items-center">
                        <Col xs="8">
                            <h3 className="mb-0">Add Department For Project</h3>
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
                    <form ref = {formAddDepartmentRef} onSubmit={handleAddDepartment}>
                        <div className="pl-lg-4">
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
                        <div className="pl-lg-4" style={{ marginBottom: "20px" }}>                            
                            <Button className="btn-icon btn-3" color="success" disabled={loading}>
                                <span className="btn-inner--text m-0">
                                    {loading
                                        ? (<><Spinner size="sm">Waiting...</Spinner><span> Waiting...</span></>)
                                        : (<> Add</>)
                                    }
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
                                <th scope="col">Delete</th>
                            </tr>
                            </thead>
                            <tbody>                           
                                {deptAssignments.map((assignment, index) => (
                                    <tr className="text-center" key={index}>
                                        <td>{assignment.projectId}</td>
                                        <td>{assignment.departmentId}</td>                                    
                                        <td>{assignment.PriotityLevel}</td>
                                        <td>{assignment.mainTaskDetail}</td>
                                        <td>
                                            <Button className="btn-icon btn-3" color="" type="button" disabled={loading}  onClick={() => deleteDeptAssignment(assignment.departmentId)}>
                                                <span className="btn-inner--text m-0">
                                                    {loading
                                                        ? (<><Spinner size="sm">Waiting...</Spinner><span> Waiting...</span></>)
                                                        : (<> <i className="fas fa-trash" /></>)
                                                    }
                                                </span>
                                            </Button>
                                        </td>
                                    </tr>
                                ))}
                            </tbody>
                        </Table>
                        <hr />
                        <div className="py-0 text-right">
                            <Button className="btn-icon btn-3" color="success" onClick={handleSubmitAddDeptDepartment} type="button" disabled={loading}>
                                <span className="btn-inner--text m-0">
                                    {loading
                                        ? (<><Spinner size="sm">Waiting...</Spinner><span> Waiting...</span></>)
                                        : (<><i className="fa-solid fa-floppy-disk"></i> Save DeptAssignment</>)
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

export default ProjectAdd;