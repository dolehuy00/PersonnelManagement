
// reactstrap components
import {
    Card,
    CardHeader,
    CardFooter,
    Table,
    Container,
    Row,
    Col,
} from "reactstrap";

// core components
import React, { useState } from "react";


// hooks
import { useFilterDeptAssignmentByLeader } from "hooks/UseDeptAssignmentApi.js";

//components
import CustomPagination from "components/Pagination/Pagination.js";
import DropdownButtonSmall from "components/Dropdowns/DropdownButtonSmall.js";
import FilterPopup from "components/Popups/FilterPopup.js";
import Header from "components/Headers/Header.js";
import LoadingOrError from "components/Notifications/LoadingOrError.js";
import { useLocation, useNavigate } from "react-router-dom";

const Tables = () => {
     // search params
     const location = useLocation();
     const searchParams = new URLSearchParams(location.search);
     const departmentId = searchParams.get("department");
     const deparmentName = JSON.parse(localStorage.getItem("leaderOfDepartments")).find(item => item.id.toString() === departmentId)?.name;

     // navigate
     const navigate = useNavigate();

    // data component
    const arrayItemPerPage = [
        { text: "Item per page: ", value: 5 },
        { text: "Item per page: ", value: 10 },
        { text: "Item per page: ", value: 30 },
        { text: "Item per page: ", value: 50 },
        { text: "Item per page: ", value: 100 }
    ];
    const arrayItemSortBy = [
        { text: "Sort by id ascending", value: "id:asc" },
        { text: "Sort by id decreasing", value: "id:dec" },
    ];
    const itemSingleFilters = [
        { labelName: "Id", nameInput: "id", type: "number" },
        { labelName: "Project Id", nameInput: "projectId", type: "number" }
    ];

    //states
    const [pageIndex, setPageIndex] = useState(1);
    const [pageSize, setPageSize] = useState(5);
    const [dataFilter, setDataFilter] = useState({});
    const [sortBy, setSortBy] = useState("id:asc");
    const buttonPerPageState = useState(arrayItemPerPage[0]);
    const buttonSortByState = useState(arrayItemSortBy[0])

    // handle function
    const handlePageChange = (newPageIndex) => {
        setPageIndex(newPageIndex);
    };

    const handleSelectPerPageChange = (newItemPerPage) => {
        setPageIndex(1);
        setPageSize(newItemPerPage);
    };

    const onConfirmFilter = (dataFilter) => {
        setPageIndex(1);
        setDataFilter(dataFilter);
    }

    const handleSelectSortByChange = (newItem) => {
        setSortBy(newItem);
    }

    // request data
    const { data, loading, error } = useFilterDeptAssignmentByLeader(departmentId, dataFilter, sortBy, pageIndex, pageSize);
    const totalPage = data.totalPage;

    // render
    if (loading) return (
        <>
            <Header />
            <LoadingOrError status="loading" />
        </>
    );

    if (error) return (
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
                {/* Table */}
                <Row>
                    <div className="col">
                        <Card className="shadow">
                            <CardHeader className="border-0">
                                <Container className="mw-100">
                                    <Row>
                                        <Col className="text-left">
                                        <h3>Dept Assignment ({deparmentName})</h3>
                                        </Col>
                                        <Col className="text-right">
                                            <DropdownButtonSmall
                                                selectedItemState={buttonSortByState}
                                                viewValue={false}
                                                arrayItems={arrayItemSortBy}
                                                onSelectChange={handleSelectSortByChange}
                                            />
                                            <FilterPopup
                                                itemSingleFilters={itemSingleFilters}
                                                onConfirmFilter={onConfirmFilter}
                                                dataFilterUseState={dataFilter}
                                            />
                                        </Col>
                                    </Row>
                                </Container>
                            </CardHeader>
                            <Table className="align-items-center table-flush" responsive>
                                <thead className="thead-light">
                                    <tr className="text-center">
                                        <th scope="col">Id</th>
                                        <th scope="col">Priotity Level</th>
                                        <th scope="col">Main Task</th>
                                        <th scope="col">Project Id</th>
                                        <th scope="col">Department Id</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {data
                                        ? data.results.map((item, index) => (
                                            <tr 
                                            key={index} 
                                            className="text-center table-row-salary-user" 
                                            onClick={e => {navigate(`/leader/assignment?dept_assignment=${item.id}&department=${departmentId}`)}}
                                            >
                                                <th scope="row">{item.id}</th>
                                                <td>{item.priotityLevel}</td>
                                                <td>{item.mainTaskDetail}</td>
                                                <td>{`# ${item.projectId}`}</td>
                                                <td>{`# ${item.departmentId}`}</td>
                                            </tr>
                                        ))
                                        : ""}
                                </tbody>
                            </Table>
                            <CardFooter className="py-4">
                                <Container className="mw-100">
                                    <Row>
                                        <Col>
                                            <DropdownButtonSmall selectedItemState={buttonPerPageState} viewValue={true} arrayItems={arrayItemPerPage} onSelectChange={handleSelectPerPageChange} />
                                        </Col>
                                        <Col>
                                            <CustomPagination pageIndex={pageIndex} totalPage={totalPage} maxPageView={5} onPageChange={handlePageChange} />
                                        </Col>
                                    </Row>
                                </Container>
                            </CardFooter>
                        </Card>
                    </div>
                </Row>
            </Container>
        </>
    );
};

export default Tables;
