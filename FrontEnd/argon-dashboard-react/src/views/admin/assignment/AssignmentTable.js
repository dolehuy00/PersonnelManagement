
// reactstrap components
import {
  Badge,
  Card,
  CardHeader,
  CardFooter,
  DropdownMenu,
  DropdownItem,
  UncontrolledDropdown,
  DropdownToggle,
  Table,
  Container,
  Row,
  Col,
  Button,
} from "reactstrap";

// core components
import React, { useState } from "react";

// hooks
import { useFilterAssignment } from "hooks/UseAssignmentApi.js";

//components
import CustomPagination from "components/Pagination/Pagination.js";
import DropdownButtonSmall from "components/Dropdowns/DropdownButtonSmall.js";
import FilterPopup from "components/Popups/FilterPopup.js";
import Header from "components/Headers/Header.js";
import AssignmentAdd from "./AssignmentAdd.js";
import LoadingOrError from "components/Notifications/LoadingOrError.js";

const Tables = () => {
  // data component
  const arrayItemPerPage = [
    { text: "Item per page: ", value: 5 },
    { text: "Item per page: ", value: 10 },
    { text: "Item per page: ", value: 30 },
    { text: "Item per page: ", value: 50 },
    { text: "Item per page: ", value: 100 }
  ];
  const arrayItemSortBy = [
    { text: "No sort", value: "" },
    { text: "Sort by fullname ascending", value: "fullname:asc" },
    { text: "Sort by fullname decreasing", value: "fullname:dec" },
    { text: "Sort by priotity level ascending", value: "priotityLevel:asc" },
    { text: "Sort by priotity level decreasing", value: "priotityLevel:dec" },
  ];
  const itemSingleFilters = [
    { labelName: "Responsible Peson Id", nameInput: "responsiblePesonId", type: "number" },
    { labelName: "Project Id", nameInput: "projectId", type: "number" },
    { labelName: "Department Id", nameInput: "departmentId", type: "number" },
  ];

  const itemSelectOptions = [
    {
        labelName: "Status",
        nameSelect: "status",
        Option: [
            { labelName: "Pending", value: "Pending" },
            { labelName: "Assigned", value: "Assigned" },
            { labelName: "In Progress", value: "In Progress" },
            { labelName: "On Hold", value: "On Hold" },
            { labelName: "Completed", value: "Completed" },
            { labelName: "Verified", value: "Verified" },
            { labelName: "Rejected", value: "Rejected" },
            { labelName: "Cancelled", value: "Cancelled" },
            { labelName: "Failed", value: "Failed" },
        ],
        type: "select_option"
    }
  ];

  //states
  const [pageIndex, setPageIndex] = useState(1);
  const [pageSize, setPageSize] = useState(5);
  const [dataFilter, setDataFilter] = useState({});
  const [sortBy, setSortBy] = useState("");
  const [bodyView, setBodyView] = useState("");
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

  const handleClickCreate = () => {
    setBodyView("create");
  }

  const handleCancelCreate = (event) => {
    event.preventDefault();
    setBodyView("table")
  }

  // request data
  const { data, loading, error } = useFilterAssignment(dataFilter, sortBy, pageIndex, pageSize);
  const totalPage = data.totalPage;

  // render
  // render status fuction
  const renderDataColStatus = (status) => {
    let style = {};
    switch (status) {
      case "Pending":
        style = { "backgroundColor": "#A0A0A0",
          "width": "0.5rem",
          "height": "0.5rem"
        };
        break;
      case "Assigned":
        style = { "backgroundColor": "#007BFF",
          "width": "0.5rem",
          "height": "0.5rem"
        };
        break;
      case "In Progress":
        style = { "backgroundColor": "#FFA500",
          "width": "0.5rem",
          "height": "0.5rem"
        };
        break;
      case "On Hold":
        style = { "backgroundColor": "#FFD700",
          "width": "0.5rem",
          "height": "0.5rem"
        };
        break;
      case "Completed":
        style = { "backgroundColor": "#28A745",
          "width": "0.5rem",
          "height": "0.5rem"
        };
        break;
      case "Verified":
        style = { "backgroundColor": "#20C997",
          "width": "0.5rem",
          "height": "0.5rem"
        };
        break;
      case "Rejected":
        style = { "backgroundColor": "#DC3545",
          "width": "0.5rem",
          "height": "0.5rem"
        };
        break;
      case "Cancelled":
        style = { "backgroundColor": "#F8D7DA",
          "width": "0.5rem",
          "height": "0.5rem"
        };
        break;
      case "Failed":
        style = { "backgroundColor": "#B22222",
          "width": "0.5rem",
          "height": "0.5rem"
        };
        break;
      default:
        style = {};
    }
    return (
      <>
        <i style={style} />
        {status}
      </>
    );
  }

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
            {bodyView === "create"
              ? (<AssignmentAdd onCancel={handleCancelCreate} />)
              : (
                <Card className="shadow">
                  <CardHeader className="border-0">
                    <Container>
                      <Row>
                        <Col className="text-left">
                          <Button
                            className="btn-icon btn-3"
                            size="sm" color="primary"
                            type="button"
                            onClick={handleClickCreate}
                          >
                            <i className="fa-solid fa-plus mr-1"></i>
                            <span className="btn-inner--text m-0">Create</span>
                          </Button>
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
                            itemSelectOptions={itemSelectOptions}
                          />
                        </Col>
                      </Row>
                    </Container>
                  </CardHeader>
                  <Table className="align-items-center table-flush" responsive>
                    <thead className="thead-light">
                      <tr className="text-center">
                        <th scope="col">Id</th>
                        <th scope="col">Name</th>
                        <th scope="col">Priotity Level</th>
                        <th scope="col">Responsible Person</th>
                        <th scope="col">Project</th>
                        <th scope="col">Status</th>
                        <th scope="col" />
                      </tr>
                    </thead>
                    <tbody>
                      {data
                        ? data.results.map((item, index) => (
                          <tr key={index}>
                            <th scope="row">{item.id}</th>
                            <td>{item.name}</td>
                            <td className="text-center">{item.priotityLevel}</td>
                            <td>
                              {item.responsiblePesonId === null
                                ? "<not set>"
                                : `${item.responsiblePesonId} - ${item.responsiblePesonName}`
                              }
                            </td>
                            <td>{`${item.projectId} - ${item.projectName}`}</td>
                            <td><Badge
                              color=""
                              className="badge-dot mr-4">
                              {renderDataColStatus(item.status)}
                            </Badge>
                            </td>
                            <td className="text-right">
                              <UncontrolledDropdown>
                                <DropdownToggle
                                  className="btn-icon-only text-light"
                                  href="#pablo"
                                  role="button"
                                  size="sm"
                                  color=""
                                  onClick={(e) => e.preventDefault()}
                                >
                                  <i className="fas fa-ellipsis-v" />
                                </DropdownToggle>
                                <DropdownMenu className="dropdown-menu-arrow" right>
                                  <DropdownItem
                                    href={`assignment/view?id=${item.id}`}
                                  >
                                    View Details
                                  </DropdownItem>
                                  <DropdownItem
                                    href={`assignment/view?id=${item.id}&mode=edit`}
                                  >
                                    Edit
                                  </DropdownItem>
                                </DropdownMenu>
                              </UncontrolledDropdown>
                            </td>
                          </tr>
                        ))
                        : ""}
                    </tbody>
                  </Table>
                  <CardFooter className="py-4">
                    <Container>
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
              )
            }
          </div>
        </Row>
      </Container>
    </>
  );
};

export default Tables;
